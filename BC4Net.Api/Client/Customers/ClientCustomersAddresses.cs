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
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientCustomersAddresses : 
        ClientBase, 
        IChildResourceGet<CustomersAddress>,
        IChildResourceCount
    {
        public ClientCustomersAddresses(Configuration configuration)
            :base(configuration)
        {}
        public async Task<IClientResponse<ItemCount>> CountAsync(int customerId)
        {
            string resourceEndpoint = string.Format("customers/{0}/addresses/count", customerId);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int customerId, IFilter filter)
        {
            string resourceEndpoint = string.Format("customers/{0}/addresses/count", customerId);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<CustomersAddress>>> GetAsync(int customerId)
        {
            string resourceEndpoint = string.Format("customers/{0}/addresses", customerId);
            return await GetDataAsync<List<CustomersAddress>>(resourceEndpoint);
        }
        public async Task<IClientResponse<CustomersAddress>> GetAsync(int customerId, int recordId)
        {
            string resourceEndpoint = string.Format("customers/{0}/addresses/{1}", customerId, recordId);
            return await GetDataAsync<CustomersAddress>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<CustomersAddress>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<CustomersAddress>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<CustomersAddress>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<CustomersAddress>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int customerId)
        {
            string resourceEndpoint = string.Format("customers/{0}/addresses", customerId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int customerId, int recordId)
        {
            string resourceEndpoint = string.Format("customers/{0}/addresses/{1}", customerId, recordId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }



        public async Task GetAsync(IList<Customer> customers)
        {
            foreach (var item in customers)
            {
                var response = await GetAsync(item.Id);

                if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.Data != null && response.Data != null)
                {

                    foreach (var xitem in response.Data)
                    {
                        item.Addresses.Add(xitem);
                    }
                    ShowIdAndApiLimit(item.Id, response.RestResponse);
                }
                else
                {
                    StatusCodeLogging(response.RestResponse, GetType());
                }
            }
        }
        public async Task GetAsync(Customer customer)
        {
            var response = await GetAsync(customer.Id);

            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                response.Data != null && response.Data != null)
            {

                foreach (var xitem in response.Data)
                {
                    customer.Addresses.Add(xitem);
                }
                ShowIdAndApiLimit(customer.Id, response.RestResponse);
            }
            else
            {
                StatusCodeLogging(response.RestResponse, GetType());
            }
        }

    }
}
