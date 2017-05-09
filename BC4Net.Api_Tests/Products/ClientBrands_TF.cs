using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Products
{
    public class ClientBrands_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_CountAsync()
        {
            var response = await Client.Brands.CountAsync();

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_FilteredAsync()
        {
            var filter = new Api.FilterOrders()
            {
                MaximumId = 100
            };

            var response = await Client.Brands.GetAsync(filter);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_Filtered_RecordPagingAsync()
        {
            var filter = new Api.FilterOrders()
            {
                MinimumId = 3000
            };
            var response_count = await Client.Brands.CountAsync(filter);
            var response_list = await Client.Brands.GetListAsync(filter);

            Assert.Equal(response_list.Count, response_count.Data.Count);
        }

        [Fact]
        public async Task Can_Get_Non_Filtered_RecordPagingAsync()
        {
            var response_count = await Client.Brands.CountAsync();
            var response_list = await Client.Brands.GetListAsync();

            Assert.Equal(response_list.Count, response_count.Data.Count);
        }

        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.Brands.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
