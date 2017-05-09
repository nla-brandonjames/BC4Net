#region License
//   Copyright 2013 Ken Worst - R.C. Worst & Company Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigCommerce4Net.Domain;
using BigCommerce4Net.Api.ExtensionMethods;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientCoupons : 
        ClientBase, 
        IParentResourceGetUpdateDeleteCreate<Coupon> 
    {
        public ClientCoupons(Configuration configuration)
            : base(configuration) { }

        public async Task<IClientResponse<List<Coupon>>> GetAsync()
        {
            var filter = new FilterCoupons();
            return await GetAsync(filter);
        }
        public async Task<IClientResponse<List<Coupon>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "coupons";
            return await GetDataAsync<List<Coupon>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Coupon>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("coupons/{0}", id);
            return await GetDataAsync<Coupon>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Coupon>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("coupons/{0}", id);
            return await GetDataAsync<Coupon>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<Coupon>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Coupon>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Coupon>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Coupon>>(resourceEndPoint, filter);
        }
        public async Task<IClientResponse<Coupon>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = string.Format("coupons/{0}", id);
            return await PutDataAsync<Coupon>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Coupon>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = string.Format("coupons/{0}", id);
            return await PutDataAsync<Coupon>(resourceEndpoint, obj.SerializeObject());
        }
        public async Task<IClientResponse<Coupon>> CreateAsync(string json)
        {
            string resourceEndpoint = "coupons";
            return await PostDataAsync<Coupon>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Coupon>> CreateAsync(object obj)
        {
            string resourceEndpoint = "coupons";
            return await PostDataAsync<Coupon>(resourceEndpoint, obj.SerializeObject());
        }
        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = string.Format("coupons/{0}", id);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("coupons/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("coupons");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

    }
}
