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
using BigCommerce4Net.Domain;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientOrdersProducts : 
        ClientBase,
        IChildResourceGet<OrdersProduct>,
        IChildResourceCount
    {
        public ClientOrdersProducts(Configuration configuration)
            :base(configuration)
        {}

        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/products/count", orderId);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId, IFilter filter)
        {
            string resourceEndpoint = string.Format("orders/{0}/products/count", orderId);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OrdersProduct>>> GetAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/products", orderId);
            return await GetDataAsync<List<OrdersProduct>>(resourceEndpoint);
        }
        public async Task<IClientResponse<OrdersProduct>> GetAsync(int orderId, int productId)
        {
            string resourceEndpoint = string.Format("orders/{0}/products/{1}", orderId, productId);
            return await GetDataAsync<OrdersProduct>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OrdersProduct>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OrdersProduct>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OrdersProduct>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OrdersProduct>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/products", orderId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId, int productId)
        {
            string resourceEndpoint = string.Format("orders/{0}/products/{1}", orderId, productId);
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
                        item.Products.Add(xitem);
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
                    order.Products.Add(xitem);
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
