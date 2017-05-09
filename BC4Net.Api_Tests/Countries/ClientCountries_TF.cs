using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Countries
{
    public class ClientCountries_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_CountAsync()
        {

            var response = await Client.Countries.CountAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }
        [Fact]
        public async Task Can_Get_All_Countries_Default_PagingAsync()
        {

            var response = await Client.Countries.GetAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.Equal(response.Data.Count, 50);
        }
        [Fact]
        public async Task Can_Get_All_Countries_With_Limit_Parameter_PagingAsync()
        {
            var filter = new Api.FilterCountries
            {
                Limit = 200
            };

            var response = await Client.Countries.GetAsync(filter);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.Equal(response.Data.Count, 200);
        }

        [Fact]
        public async Task Can_Get_One_Country_By_IdAsync()
        {

            var response = await Client.Countries.GetAsync(TEST_COUNTRY_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, TEST_COUNTRY_ID);
        }
        [Fact]
        public async Task Can_Get_All_Countries_With_PagingAsync()
        {

            var response = await Client.Countries.GetListAsync();
            Assert.True(response.Count > 50);
        }

        [Fact]
        public async Task Can_Get_Countries_GetHttpOptionsAsync()
        {

            var response = await Client.Countries.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
