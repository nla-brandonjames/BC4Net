using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Coupons
{
    public class ClientCoupons_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_All_Coupons_Default_PagingAsync()
        {
            var response = await Client.Coupons.GetAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_Coupons_With_Limit_Parameter_PagingAsync()
        {
            var filter = new Api.FilterStates
            {
                Limit = 5
            };

            var response = await Client.Coupons.GetAsync(filter);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Count, 5);
        }

        [Fact]
        public async Task Can_Get_One_Coupon_By_IdAsync()
        {
            var response = await Client.Coupons.GetAsync(TEST_COUPON_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, TEST_COUPON_ID);
        }

        [Fact]
        public async Task Can_Get_Coupons_OptionsAsync()
        {
            var response = await Client.Coupons.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
        }
    }
}
