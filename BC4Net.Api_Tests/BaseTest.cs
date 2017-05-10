using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests
{
    public class BaseTest
    {
        public Api.Client Client { get; set; }
        public Api.Configuration Api_Configuration { get; set; }
        public const int TEST_ORDER_ID = 116;
        public const int TEST_CUSTOMER_ID = 4;
        public const int TEST_PRODUCT_ID = 7982;
        public const int TEST_STATE_ID = 22;
        public const int TEST_COUNTRY_ID = 226;
        public const int TEST_COUPON_ID = 1;

        public BaseTest()
        {
            SetupContext();
            Client = new Api.Client(this.Api_Configuration);
        }

        public void SetupContext()
        {
            var testSettings = JsonConvert.DeserializeObject<TestSettings>(File.ReadAllText("TEST_SETTINGS.json"));

            Api_Configuration = new Api.Configuration()
            {
                StoreHash = testSettings.StoreHash,
                UserName = testSettings.UserName,
                UserApiKey = testSettings.UserApiKey
            };

        }
    }
    
    class TestSettings
    {
        public string StoreHash = "";
        public string UserName = "";
        public string UserApiKey = "";
    }
}
