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
using BigCommerce4Net.Api.ExtensionMethods;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientOrdersShipments : 
        ClientBase, 
        IChildResourceGetUpdateDeleteCreate<OrdersShipment>,
        IChildResourceCount
    {
        public ClientOrdersShipments(Configuration configuration)
            :base(configuration)
        {}

        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/count", orderId);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int orderId, IFilter filter)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/count", orderId);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OrdersShipment>>> GetAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments", orderId);
            return await GetDataAsync<List<OrdersShipment>>(resourceEndpoint);
        }
        public async Task<IClientResponse<OrdersShipment>> GetAsync(int orderId, int shipmentId)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/{1}", orderId, shipmentId);
            return await GetDataAsync<OrdersShipment>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OrdersShipment>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OrdersShipment>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OrdersShipment>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OrdersShipment>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<OrdersShipment>> UpdateAsync(int orderId, int shipmentId, string json)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/{1}", orderId, shipmentId);
            return await PutDataAsync<OrdersShipment>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<OrdersShipment>> UpdateAsync(int orderId, int shipmentId, object obj)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/{1}", orderId, shipmentId);
            return await PutDataAsync<OrdersShipment>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<OrdersShipment>> CreateAsync(int orderId, string json)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments", orderId);
            return await PostDataAsync<OrdersShipment>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<OrdersShipment>> CreateAsync(int orderId, object obj)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments", orderId);
            return await PostDataAsync<OrdersShipment>(resourceEndpoint, obj.SerializeObject());
        }


        public async Task<IClientResponse<bool>> DeleteAsync(int orderId, int shipmentId)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/{1}", orderId, shipmentId);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments", orderId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int orderId, int shipmentId)
        {
            string resourceEndpoint = string.Format("orders/{0}/shipments/{1}", orderId, shipmentId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task GetAsync(IList<Order> orders)
        {
            List<OrdersShipment> items = new List<OrdersShipment>();

            foreach (var item in orders)
            {
                var response = await GetAsync(item.Id);

                if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.Data != null && response.Data != null)
                {

                    foreach (var xitem in response.Data)
                    {
                        item.Shipments.Add(xitem);
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
                    order.Shipments.Add(xitem);
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
