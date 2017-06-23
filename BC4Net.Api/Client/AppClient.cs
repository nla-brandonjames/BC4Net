using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

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
        public string AuthenticationResponse { get; set; }

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

        public async System.Threading.Tasks.Task Authenticate()
        {
            var authDetails = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri },
                { "grant_type", "authorization_code" },
                { "code", Code },
                { "scope", Scope },
                { "context", Context },
            };

            var payload = new FormUrlEncodedContent(authDetails);
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("https://login.bigcommerce.com");

            HttpResponseMessage response = await client.PostAsync("/oauth2/token", payload);
            HttpContent content = response.Content;
            string authenticationContent = await content.ReadAsStringAsync();
            AuthenticationResponse = authenticationContent;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("There was a problem with your request!");
            }

            JObject data = JObject.Parse(authenticationContent);
            string[] storeDetails = data["context"].ToString().Split('/');
            string accessToken = data["access_token"].ToString();
            _Authentication.AccessToken = accessToken;
        }

        public async System.Threading.Tasks.Task Authenticate(
                        string clientId,
                        string clientSecret,
                        string redirectUri,
                        string code,
                        string scope,
                        string context)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            Code = code;
            Scope = scope;
            Context = context;

            _Configuration.UserName = clientId;

            if (context.Contains("/"))
            {
                _Configuration.StoreHash = context.Split('/')[1];
            }

            await Authenticate();
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
