using System;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coldairarrow.Business.Job;
using Coldairarrow.Util.ApiHelper.WeChat;
using Hangfire;

namespace Coldairarrow.Business.ServerFood
{
    public class F_FoodInfoBusiness : BaseBusiness<F_FoodInfo>, IF_FoodInfoBusiness, ITransientDependency
    {
        public IOperator oOperator;
        public IJobServer Job;
        public F_FoodInfoBusiness(IRepository repository, IOperator op, IJobServer _job)
            : base(repository)
        {
            oOperator = op;
            Job = _job;
        }

        #region 外部接口

        public async Task<PageResult<F_FoodInfoResultDto>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<F_FoodInfo, F_ShopInfo, F_FoodInfoResultDto>> select = (a, b) => new F_FoodInfoResultDto
            {
                ShopName = b.ShopName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_ShopInfo>() on a.ShopInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);


            var where = LinqHelper.True<F_FoodInfoResultDto>();
            var search = input.Search;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_FoodInfoResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<F_FoodInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_FoodInfo data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_FoodInfo data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task PublishFoodDataAsync(List<string> ids)
        {
           var tempQuery =Service.GetIQueryable<F_PublishFood>().Where(a => ids.Contains(a.FoodInfoId) && a.PublishDate > DateTime.Now.Date &&   a.PublishDate< DateTime.Now.Date.AddDays(1))
                .ToList();
            if (tempQuery.Count > 0)
            {
                throw new BusException("["+string.Join(",", tempQuery.Select(a => a.FoodName)) +"]菜品当日已经发布过,请重新选择!");
            }
            var query = GetIQueryable().Where(a => ids.Contains(a.Id)).ToList();
            if(query.Count==0) throw new BusException("数据异常!");
            if(query.GroupBy(a => a.ShopInfoId)?.Count() > 1) throw new BusException("不能同时发布两个门店菜品!");
            string shopInfoId = query?.FirstOrDefault()?.ShopInfoId;
            var shopInfo = Service.GetIQueryable<F_ShopInfoSet>().FirstOrDefault(a => a.ShopInfoId == shopInfoId);
            var toDayFoodsCount = Service.GetIQueryable<F_PublishFood>().Count(a =>
                a.PublishDate > DateTime.Now.Date && a.PublishDate < DateTime.Now.Date.AddDays(1) && a.ShopInfoId== shopInfoId);
            List<F_PublishFood> publishFoodList = new List<F_PublishFood>();
            query.ForEach(a =>
            {
                F_PublishFood publishFood = new F_PublishFood()
                {
                    Id =IdHelper.GetId(),
                    CreateTime = DateTime.Now,
                    CreatorId = oOperator.UserId,
                    CreatorName = oOperator.Property.RealName,
                    ShopInfoId = a.ShopInfoId,
                    SupplierName = a.SupplierName,
                    Price = a.Price,
                    FoodQty = 999,
                    FoodDesc =a.FoodDesc,
                    FoodName = a.FoodName,
                    ImgUrl = a.ImgUrl,
                    PublishDate = DateTime.Now,
                    FoodInfoId = a.Id,
                    Limit = a.Limit
            
                };
                publishFoodList.Add(publishFood);
            });
            await Service.InsertAsync(publishFoodList);
            //如果今天有发布的菜品则不再发送消息
            if (shopInfo!=null && !string.IsNullOrEmpty(shopInfo.OrderBeginRemind) && shopInfo.OrderBeginDate.HasValue && toDayFoodsCount<=0)
            {
                //发送开始点餐信息
                var timeSpan = shopInfo.OrderBeginDate.Value.TimeOfDay - DateTime.Now.TimeOfDay;
                //如果当前时间已经过了点餐时间不在发送小
                if (shopInfo.OrderBeginEnd.HasValue && DateTime.Now.TimeOfDay < shopInfo.OrderBeginEnd.Value.TimeOfDay)
                {
                    //添加发送消息
                    BackgroundJob.Schedule(() => PublishFoodSendToWeChat(shopInfoId, shopInfo.OrderBeginRemind),
                        timeSpan);
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 发布消息到微信
        /// </summary>
        public void PublishFoodSendToWeChat(string shopId,string msg)
        {
            //查询门店需要发送时间
            var userList = Service.GetIQueryable<F_UserInfo>().Where(a => a.ShopInfoId == shopId).Select(a=>a.WeCharUserId).ToList();
            var joinUser = string.Join("|", userList);
            LogHelper.WriteLog_LocalTxt(joinUser);
            //查询是否已经发送消息
            var token = WeChatOperation.GetToken(EnumWeChatAppType.Food);
            WeChatSendMsgContext weChatSendMsgContext=new WeChatSendMsgContext();
            weChatSendMsgContext.title = "点餐提醒";
            weChatSendMsgContext.btntxt = "点餐";
            weChatSendMsgContext.description = "<div class=\"gray\">"+DateTime.Now.ToString("yyyy-MM-dd")+ "</div> <div class=\"normal\">" + msg + "</div>";
            WeChatOperation.SendMsg(token, joinUser, EnumWeChatAppType.Food, weChatSendMsgContext);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}