using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BC4Net.Api_Tests.Products
{
    public class ClientProducts_TF : BaseTest
    {
        [Fact]
        public async Task Can_GetListAsync()
        {
            var response = await Client.Products.GetListAsync();

            Assert.True(response.Count > 0);
        }

        [Fact]
        public async Task Can_Get_HttpOptionsAsync()
        {
            var response = await Client.Products.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
