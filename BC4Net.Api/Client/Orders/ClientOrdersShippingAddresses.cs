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
using System.Reflection;
using System.Threading.Tasks;
//using bigcommerce4net.Utilities;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientOrdersShippingAddresses : 
        ClientBase,
        IChildResourceGet<OrdersShippingAddress>,
        IChildResourceCount
    {
        public ClientOrdersShippingAddresses(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId)
        {
            string resourceEndpoint = $"orders/{orderId}/shipping_addresses/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId, IFilter filter)
        {
            string resourceEndpoint = $"orders/{orderId}/shipping_addresses/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OrdersShippingAddress>>> GetAsync(int orderId)
        {
            string resourceEndpoint = $"orders/{orderId}/shipping_addresses/";
            return await GetDataAsync<List<OrdersShippingAddress>>(resourceEndpoint);
        }
        public async Task<IClientResponse<OrdersShippingAddress>> GetAsync(int orderId, int shippingaddressId)
        {
            string resourceEndpoint = $"orders/{orderId}/shipping_addresses/{shippingaddressId}";
            return await GetDataAsync<OrdersShippingAddress>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OrdersShippingAddress>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OrdersShippingAddress>>(resourceEndPoint);
        }

        public async Task<IClientResponse<List<OrdersShippingAddress>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OrdersShippingAddress>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId)
        {
            string resourceEndpoint = $"orders/{orderId}/shipping_addresses";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId, int shippingaddressId)
        {
            string resourceEndpoint = $"orders/{orderId}/shipping_addresses/{shippingaddressId}";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task GetAsync(IList<Order> orders)
        {

            foreach (var item in orders)
            {
                var response = await GetAsync(item.ResourceShippingAddresses.ResourceEndPoint);

                if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.Data != null && response.Data != null)
                {

                    foreach (var xitem in response.Data)
                    {
                        item.ShippingAddresses.Add(xitem);
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
            var orderAddress = order.ResourceShippingAddresses.ResourceEndPoint.Replace("shippingaddresses", "shipping_addresses");

            var response = await GetAsync(orderAddress);

            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                response.Data != null && response.Data != null)
            {

                foreach (var xitem in response.Data)
                {
                    order.ShippingAddresses.Add(xitem);
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
