using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BC4Net.Api_Tests.Utilities
{
    class ClientShippingMethods_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_All_ShippingMethodsAsync()
        {
            var zones = await Client.ShippingZones.GetAsync();
            var response = await Client.ShippingMethods.GetAsync(zones.Data[0].Id);

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var zones = await Client.ShippingZones.GetAsync();
            var response = await Client.ShippingMethods.GetHttpOptionsAsync(zones.Data[0].Id);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
