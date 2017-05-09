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
    public class ClientProductsDiscountRules : 
        ClientBase, 
        IChildResourceGet<ProductsDiscountRule>,
        IChildResourceCount
    {
        public ClientProductsDiscountRules(Configuration configuration)
            : base(configuration) { }

        public async Task<IClientResponse<ItemCount>> CountAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int productid, IFilter filter)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<ProductsDiscountRule>>> GetAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules", productid);
            return await GetDataAsync<List<ProductsDiscountRule>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<ProductsDiscountRule>>> GetAsync(int productid, IFilter filter)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules", productid, filter);
            return await GetDataAsync<List<ProductsDiscountRule>>(resourceEndpoint);
        }
        public async Task<IClientResponse<ProductsDiscountRule>> GetAsync(int productid, int rulesId)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules/{1}", productid, rulesId);
            return await GetDataAsync<ProductsDiscountRule>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<ProductsDiscountRule>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<ProductsDiscountRule>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<ProductsDiscountRule>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<ProductsDiscountRule>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules", productid);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid, int rulesId)
        {
            string resourceEndpoint = string.Format("products/{0}/discountrules/{1}", productid, rulesId);
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
                        item.DiscountRules.Add(xitem);
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
                    item.DiscountRules.Add(xitem);
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
