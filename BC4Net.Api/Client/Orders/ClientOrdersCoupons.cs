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

using System.Collections.Generic;
using BigCommerce4Net.Domain;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientOrdersCoupons : 
        ClientBase, 
        IChildResourceGet<OrdersCoupon>,
        IChildResourceCount
    {
        public ClientOrdersCoupons(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/coupons/count", orderId);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId, IFilter filter)
        {
            string resourceEndpoint = string.Format("orders/{0}/coupons/count", orderId);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OrdersCoupon>>> GetAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/coupons", orderId);
            return await GetDataAsync<List<OrdersCoupon>>(resourceEndpoint);
        }
        public async Task<IClientResponse<OrdersCoupon>> GetAsync(int orderId, int couponId)
        {
            string resourceEndpoint = string.Format("orders/{0}/coupons/{1}", orderId, couponId);
            return await GetDataAsync<OrdersCoupon>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OrdersCoupon>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OrdersCoupon>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OrdersCoupon>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OrdersCoupon>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/coupons", orderId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId, int couponId)
        {
            string resourceEndpoint = string.Format("orders/{0}/coupons/{1}", orderId, couponId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }


        public async Task GetAsync(IList<Order> orders)
        {
            foreach (var item in orders)
            {
                var response = await GetAsync(item.Id);

                if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.Data != null && response.Data != null)
                {
                    foreach (var xitem in response.Data)
                    {
                        item.Coupons.Add(xitem);
                    }
                    ShowIdAndApiLimit(item.Id, response.RestResponse);
                }
                else
                {
                    StatusCodeLogging(response.RestResponse, GetType());
                }
            }
        }
        public async Task GetAsync(Order order)
        {
            var response = await GetAsync(order.Id);

            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                response.Data != null && response.Data != null)
            {
                foreach (var xitem in response.Data)
                {
                    order.Coupons.Add(xitem);
                }
                ShowIdAndApiLimit(order.Id, response.RestResponse);
            }
            else
            {
                StatusCodeLogging(response.RestResponse, GetType());
            }
        }

    }
}