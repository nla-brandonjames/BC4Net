using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BC4Net.Api_Tests.Utilities
{
    public class ClientUtilities_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_ServerTimeAsync()
        {
            var response = await Client.Utilities.GetTimeAsync();

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.UnixTimeStamp > 0.00D);
        }

        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.Utilities.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
