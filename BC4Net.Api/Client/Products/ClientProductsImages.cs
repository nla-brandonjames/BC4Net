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
using BigCommerce4Net.Api.ExtensionMethods;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientProductsImages : 
        ClientBase, 
        IChildResourceGetUpdateDeleteCreate<ProductsImage>,
        IChildResourceCount
    {
        public ClientProductsImages(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/images/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(int productid, IFilter filter)
        {
            string resourceEndpoint = string.Format("products/{0}/images/count", productid);
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<ProductsImage>>> GetAsync()
        {
            string resourceEndpoint = string.Format("products/images");
            return await GetDataAsync<List<ProductsImage>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<ProductsImage>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("products/images");
            return await GetDataAsync<List<ProductsImage>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<ProductsImage>>> GetAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/images", productid);
            return await GetDataAsync<List<ProductsImage>>(resourceEndpoint);
        }
        public async Task<IClientResponse<ProductsImage>> GetAsync(int productid, int imageid)
        {
            string resourceEndpoint = string.Format("products/{0}/images/{1}", productid, imageid);
            return await GetDataAsync<ProductsImage>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<ProductsImage>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<ProductsImage>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<ProductsImage>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<ProductsImage>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<ProductsImage>> UpdateAsync(int productid, int imageid, string json)
        {
            string resourceEndpoint = string.Format("products/{0}/images/{1}", productid, imageid);
            return await PutDataAsync<ProductsImage>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<ProductsImage>> UpdateAsync(int productid, int imageid, object obj)
        {
            string resourceEndpoint = string.Format("products/{0}/images/{1}", productid, imageid);
            return await PutDataAsync<ProductsImage>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<ProductsImage>> CreateAsync(int id, string json)
        {
            string resourceEndpoint = string.Format("products/{0}/images", id);
            return await PostDataAsync<ProductsImage>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<ProductsImage>> CreateAsync(int id, object obj)
        {
            string resourceEndpoint = string.Format("products/{0}/images", id);
            return await PostDataAsync<ProductsImage>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int productid, int imageid)
        {
            string resourceEndpoint = string.Format("products/{0}/images/{1}", productid, imageid);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid)
        {
            string resourceEndpoint = string.Format("products/{0}/images", productid);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int productid, int imageid)
        {
            string resourceEndpoint = string.Format("products/{0}/images/{1}", productid, imageid);
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
                        item.Images.Add(xitem);
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
                    item.Images.Add(xitem);
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
