using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BC4Net.Api_Tests.Orders
{
    public class ClientOrdersShipments_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.OrdersShipments.GetHttpOptionsAsync(TEST_ORDER_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }

    }
}
