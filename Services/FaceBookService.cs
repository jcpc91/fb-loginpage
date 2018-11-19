using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using fb_loginpage.Models;

namespace fb_loginpage.Services
{
    ///MAneja la conexión con el api de facebook
    public class FaceBookService : IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;

        public FaceBookService(IConfiguration _config)
        {
            this.configuration = _config;
            this.httpClient = new HttpClient(){
                BaseAddress = new Uri(_config["facebookgraph:baseurl"])
            };
        }

        public async Task<User> GetMeAsync(string token){
            var result = await GetAsync<User>(token, "me");
            
            return result;
        }

        private async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var response = await this.httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}