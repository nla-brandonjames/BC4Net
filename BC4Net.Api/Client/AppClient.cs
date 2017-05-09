using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string AccessToken { get; set; }

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
        
        public async void Initialize()
        {
            var authenticationClient = new RestClient("https://login.bigcommerce.com");
            var request = new RestRequest("oauth2/token", Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");

            request.AddParameter("client_id", ClientId, ParameterType.RequestBody);
            request.AddParameter("client_secret", ClientSecret, ParameterType.RequestBody);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("context", Context);
            request.AddParameter("redirect_uri", RedirectUri);
            request.AddParameter("scope", Scope);

            var response = await authenticationClient.Execute<ValidationResponse>(request);
            AccessToken = response.Data.AccessToken;
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
