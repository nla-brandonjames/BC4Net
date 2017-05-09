using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Products
{
    public class ClientProductsDiscountRules_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_ProductsDiscountRules_For_A_ProductAsync()
        {
            var response = await Client.ProductsDiscountRules.GetAsync(TEST_PRODUCT_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Can_Get_Count_With_FilterAsync()
        {
            var filter = new Api.Filter()
            {
                MinDateModified = DateTime.Now.AddYears(-1)
            };

            var response = await Client.ProductsDiscountRules.CountAsync(TEST_PRODUCT_ID, filter);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Can_Get_With_FilterAsync()
        {
            var filter = new Api.Filter()
            {
                MinDateModified = DateTime.Now.AddYears(-1)
            };

            var response = await Client.ProductsDiscountRules.GetAsync(TEST_PRODUCT_ID, filter);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {

            var response = await Client.ProductsDiscountRules.GetHttpOptionsAsync(TEST_PRODUCT_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
