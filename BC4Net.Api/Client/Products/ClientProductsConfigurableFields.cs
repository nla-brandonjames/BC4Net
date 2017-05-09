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
    public class ClientProductsConfigurableFields : 
        ClientBase, 
        IChildResourceGet<ProductsConfigurableField>,
        IChildResourceCount
    {
        public ClientProductsConfigurableFields(Configuration configuration)
            : base(configuration) { }

        public async Task<IClientResponse<ItemCount>> CountAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/configurablefields/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int productid, IFilter filter)
        {
            string resourceEndpoint = string.Format("products/{0}/configurablefields/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<ProductsConfigurableField>>> GetAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/configurablefields", productid);
            return await GetDataAsync<List<ProductsConfigurableField>>(resourceEndpoint);
        }
        public async Task<IClientResponse<ProductsConfigurableField>> GetAsync(int productid, int fieldId)
        {
            string resourceEndpoint = string.Format("products/{0}/configurablefields/{1}", productid, fieldId);
            return await GetDataAsync<ProductsConfigurableField>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<ProductsConfigurableField>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<ProductsConfigurableField>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<ProductsConfigurableField>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<ProductsConfigurableField>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/configurablefields", productid);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid, int fieldId)
        {
            string resourceEndpoint = string.Format("products/{0}/configurablefields/{1}", productid, fieldId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }


        public async Task GetAsync(IList<Product> items)
        {
            foreach (var item in items)
            {
                var response = await GetAsync(item.Id);

                if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.Data != null && response.Data != null)
                {
                    foreach (var xitem in response.Data)
                    {
                        item.ConfigurableFields.Add(xitem);
                    }
                    ShowIdAndApiLimit(item.Id, response.RestResponse);
                }
                else
                {
                    StatusCodeLogging(response.RestResponse, GetType());
                }
            }
        }
        public async Task GetAsync(Product item)
        {
            var response = await GetAsync(item.Id);

            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                response.Data != null && response.Data != null)
            {
                foreach (var xitem in response.Data)
                {
                    item.ConfigurableFields.Add(xitem);
                }
                ShowIdAndApiLimit(item.Id, response.RestResponse);
            }
            else
            {
                StatusCodeLogging(response.RestResponse, GetType());
            }
        }

    }
}
