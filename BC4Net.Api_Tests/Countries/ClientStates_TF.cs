using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Countries
{
    public class ClientStates_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_CountAsync()
        {
            var response = await Client.States.CountAsync();

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_All_States_Default_PagingAsync()
        {
            var response = await Client.States.GetAsync();

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.Equal(response.Data.Count, 50);
        }

        [Fact]
        public async Task Can_Get_All_States_With_PagingAsync()
        {
            var response = await Client.States.GetListAsync();

            Assert.True(response.Count > 50);
        }

        [Fact]
        public async Task Can_Get_All_States_With_Limit_Parameter_PagingAsync()
        {
            var filter = new Api.FilterStates
            {
                Limit = 200
            };

            var response = await Client.States.GetAsync(filter);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Count, 200);
        }

        [Fact]
        public async Task Can_Get_One_State_By_IdAsync()
        {
            var response = await Client.States.GetAsync(TEST_STATE_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, TEST_STATE_ID);
        }

        [Fact]
        public async Task Can_Get_States_By_Countries_IdAsync()
        {
            var response = await Client.States.GetStatesInCountryAsync(TEST_COUNTRY_ID);

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.States.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Data);
        }
    }
}
