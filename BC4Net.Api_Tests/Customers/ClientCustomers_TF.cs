using BigCommerce4Net.Api;
using BigCommerce4Net.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Customers
{
    public class ClientCustomers_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_Count_Of_CustomersAsync()
        {
            var response = await Client.Customers.CountAsync();

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_List_Of_Customers_Using_FilterAsync()
        {
            var filter = new Api.FilterCustomers()
            {
                MinimumId = 3,
                MaximumId = 3
            };

            var response = await Client.Customers.GetAsync(filter);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.Equal(1, response.Data.Count);
            Assert.Equal(3, response.Data[0].Id);
        }

        [Fact]
        public async Task Can_Customer_StoreCredit_Be_UpdatedAsync()
        {
            decimal storecredit = 8000.00M;

            var updatedata = new { store_credit = storecredit };

            string json = JsonConvert.SerializeObject(updatedata, Formatting.None);

            var response = await Client.Customers.UpdateAsync(TEST_CUSTOMER_ID, json);

            Assert.NotEqual(null, response.Data);
            Assert.Equal(TEST_CUSTOMER_ID, response.Data.Id);
            Assert.Equal(storecredit, response.Data.StoreCredit);
        }

        [Fact]
        public async Task Can_Customer_StoreCredit_Be_Updated_Then_Get_Will_IfModifiedSinceAsync()
        {
            var response = await Client.Utilities.GetTimeAsync();
            var date = response.Data.CurrentDateTime;
            var serverdatetime = string.Format("{0:r}", date);

            decimal storecredit = 8000.00M;

            var response1 = await Client.Customers.UpdateAsync(TEST_CUSTOMER_ID, new { store_credit = storecredit });

            Assert.NotEqual(null, response.Data);
            Assert.Equal(TEST_CUSTOMER_ID, response1.Data.Id);
            Assert.Equal(storecredit, response1.Data.StoreCredit);

            var filter = new FilterCustomers();
            filter.MinDateModified = ((DateTime)date).AddMinutes(-10);

            var response2 = await Client.Customers.CountAsync(filter);
            Assert.True(response2.Data.Count > 0);
        }

        [Fact]
        public async Task Check_Customer_Modified_Since()
        {
            IEntity order = new BigCommerce4Net.Domain.Order()
            {
                Id = 1,
                PaymentStatus = PaymentStatus.PartiallyRefunded
            };
            
            string json = JsonConvert.SerializeObject(order, Formatting.Indented);
            
            var client = new Client(this.Api_Configuration);
            var response1 = await client.Utilities.GetTimeAsync();
            var date = response1.Data.CurrentDateTime;

            var filter = new FilterCustomers();
            filter.MinDateModified = ((DateTime)date).AddMinutes(-10);
            var response2 = client.Customers.GetAsync(filter);
            var response3 = client.Customers.CountAsync(filter);
        }

        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.Customers.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
