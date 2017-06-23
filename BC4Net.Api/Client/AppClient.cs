using Newtonsoft.Json;
using System.Collections.Generic;

namespace BigCommerce4Net.Api
{
    public class AppClient : Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Code { get; set; }
        public string Scope { get; set; }
        public string Context { get; set; }

        public AppClient(Configuration configuration) : base(configuration) { }

        public AppClient(Configuration configuration,
                        string clientId,
                        string clientSecret,
                        string redirectUri,
                        string code,
                        string scope,
                        string context) : base(configuration)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            Code = code;
            Scope = scope;
            Context = context;

            configuration.UserName = clientId;

            if (context.Contains("/"))
            {
                configuration.StoreHash = context.Split('/')[1];
            }
        }
    }

    class ValidationResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("user")]
        public Dictionary<string, string> User { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }
    }
}
