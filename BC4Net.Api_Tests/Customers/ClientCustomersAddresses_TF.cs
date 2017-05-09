using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BC4Net.Api_Tests.Customers
{
    public class ClientCustomersAddresses_TF : BaseTest
    {
        [Fact]
        public async System.Threading.Tasks.Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.CustomersAddresses.GetHttpOptionsAsync(TEST_CUSTOMER_ID);
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
