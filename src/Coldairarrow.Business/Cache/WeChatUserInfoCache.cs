using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Coldairarrow.Business.Cache
{
     class WechatUserInfoCache : BaseCache<F_UserInfo>, IWeChatUserInfoCache, ITransientDependency
    {
        readonly IServiceProvider _serviceProvider;
        public WechatUserInfoCache(IServiceProvider serviceProvider, IDistributedCache cache)
            : base(cache)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<F_UserInfo> GetDbDataAsync(string id)
        {
           
            var list = await _serviceProvider.GetService<IF_UserInfoBusiness>().GetTheDataByWeChatIdAsync(id);

            return list;
        }
    }
}
