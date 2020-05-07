using Coldairarrow.Business.Cache;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Coldairarrow.Entity.ServerFood;

namespace Coldairarrow.Business
{
    /// <summary>
    /// 操作者
    /// </summary>
    public class Operator : IOperator, IScopeDependency
    {
        readonly IBase_UserCache _userCache;
        readonly IServiceProvider _serviceProvider;
        readonly IWeChatUserInfoCache _weChatCache;
        public Operator(IBase_UserCache userCache,IWeChatUserInfoCache weChatCache, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userCache = userCache;
            _weChatCache = weChatCache;
            UserId = httpContextAccessor?.HttpContext?.Request.GetJWTPayload()?.UserId;
        }

        private Base_UserDTO _property;
        private F_UserInfo _weChatProperty;
        private object _lockObj = new object();

        /// <summary>
        /// 当前操作者UserId
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// 属性
        /// </summary>
        public Base_UserDTO Property
        {
            get
            {
                if (UserId.IsNullOrEmpty())
                    return default;

                if (_property == null)
                {
                    lock (_lockObj)
                    {
                        if (_property == null)
                        {
                            _property = AsyncHelper.RunSync(() => _userCache.GetCacheAsync(UserId));
                        }
                    }
                }

                return _property;
            }
        }

        public F_UserInfo WeChatProperty
        {
            get
            {
                if (UserId.IsNullOrEmpty())
                    return default;

                if (_weChatProperty == null)
                {
                    lock (_lockObj)
                    {
                        if (_weChatProperty == null)
                        {
                            _weChatProperty = AsyncHelper.RunSync(() => _weChatCache.GetCacheAsync(UserId));
                        }
                    }
                }

                return _weChatProperty;
            }
        }

        /// <summary>
        /// 判断是否为超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            var role = Property.RoleType;
            if (UserId == GlobalData.ADMINID || role.HasFlag(RoleTypes.超级管理员))
                return true;
            else
                return false;
        }

        public void WriteUserLog(UserLogType userLogType, string msg)
        {
            var log = new Base_UserLog
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                CreatorId = UserId,
                CreatorRealName = Property.RealName,
                LogContent = msg,
                LogType = userLogType.ToString()
            };

            Task.Factory.StartNew(async () =>
            {
                using (var scop = _serviceProvider.CreateScope())
                {
                    var db = scop.ServiceProvider.GetService<IRepository>();
                    await db.InsertAsync(log);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
