using Blogifier.Shared;
using Blogifier.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogifier.Admin
{
	public class BlogAuthenticationStateProvider : AuthenticationStateProvider
	{
        public const string AuthenticationScheme = "Cookies";
        private readonly HttpClient _httpClient;

		public BlogAuthenticationStateProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var user = await _httpClient.GetFromJsonAsync<UserModel>("api/author/getcurrent");

			if (user != null && user.Email != null)
			{
                //var claim = new Claim(ClaimTypes.Name, author.Email);
                //var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                foreach (var role in user.Roles.Select(ur => ur.Name))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
			}
			else
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}
	}
}
