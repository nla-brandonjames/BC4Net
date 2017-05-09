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
    public class ClientProductsSkus :
        ClientBase,
        IChildResourceGetUpdateDeleteCreate<ProductsSku>,
        IChildResourceCount
    {
        public ClientProductsSkus(Configuration configuration)
            : base(configuration) { }

        public async Task<IClientResponse<ItemCount>> CountAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int productid, IFilter filter)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<ProductsSku>>> GetAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/skus", productid);
            return await GetDataAsync<List<ProductsSku>>(resourceEndpoint);
        }
        public async Task<IClientResponse<ProductsSku>> GetAsync(int productId, int skuId)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/{1}", productId, skuId);
            return await GetDataAsync<ProductsSku>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<ProductsSku>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<ProductsSku>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<ProductsSku>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<ProductsSku>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<ProductsSku>> UpdateAsync(int productId, int skuId, string json)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/{1}", productId, skuId);
            return await PutDataAsync<ProductsSku>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<ProductsSku>> UpdateAsync(int productId, int skuId, object obj)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/{1}", productId, skuId);
            return await PutDataAsync<ProductsSku>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<ProductsSku>> CreateAsync(int productId, string json)
        {
            string resourceEndpoint = string.Format("products/{0}/skus", productId);
            return await PostDataAsync<ProductsSku>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<ProductsSku>> CreateAsync(int productId, object obj)
        {
            string resourceEndpoint = string.Format("products/{0}/skus", productId);
            return await PostDataAsync<ProductsSku>(resourceEndpoint, obj.SerializeObject());
        }

        public async System.Threading.Tasks.Task<IClientResponse<bool>> DeleteAsync(int productId, int skuId)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/{1}", productId, skuId);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/skus", productid);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productId, int skuId)
        {
            string resourceEndpoint = string.Format("products/{0}/skus/{1}", productId, skuId);
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
                        item.Skus.Add(xitem);
                    }
                    ShowIdAndApiLimit(item.Id, response.RestResponse);
                }
                else
                {
                    StatusCodeLogging(response.RestResponse, GetType());
                }
            }
        }
        public async System.Threading.Tasks.Task GetAsync(Product item)
        {
            var response = await GetAsync(item.Id);

            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                response.Data != null && response.Data != null)
            {
                foreach (var xitem in response.Data)
                {
                    item.Skus.Add(xitem);
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
