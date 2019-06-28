using eThorWebApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eThorWebApp.Client
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            this.AuthenticationStateChanged += ServerAuthenticationStateProvider_AuthenticationStateChanged;
        }

        public Task<AuthenticationState> AuthState { get; set; }
        private void ServerAuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            AuthState = task;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userInfo = await _httpClient.GetJsonAsync<UserInfo>("user");

            var identity = userInfo.IsAuthenticated
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo.Name) }, "serverauth")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
