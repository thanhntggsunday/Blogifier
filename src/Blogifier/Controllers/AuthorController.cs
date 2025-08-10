using Blogifier.Core.Providers;
using Blogifier.Middleware;
using Blogifier.Shared;
using Blogifier.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly IIdentityProvider _identityProvider;

		public AuthorController(IIdentityProvider authorProvider)
		{
			_identityProvider = authorProvider;
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("all")]
		public async Task<List<User>> All()
		{
			return await _identityProvider.GetUsers();
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("email/{email}")]
		public async Task<ActionResult<User>> FindByEmail(string email)
		{
			return await _identityProvider.FindByEmail(email);
		}

		[HttpGet("getcurrent")]
		public async Task<ActionResult<UserModel>> GetCurrentAuthor()
		{
            if (!User.Identity.IsAuthenticated)
            {
                return new UserModel();
            }

            var u = await _identityProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
            var uvm = new UserModel();
            uvm.DisplayName = u.DisplayName;
            uvm.Email = u.Email;
            uvm.Roles = _identityProvider.GetRolesOfUser(u);

            return uvm;
        }

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpDelete("{id:int}")]
		public async Task<ActionResult<bool>> RemoveAuthor(int id)
		{
			return await _identityProvider.Remove(id);
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpPost("add")]
		public async Task<ActionResult<bool>> Add(User author)
		{
			var success = await _identityProvider.Add(author);
			return success ? Ok() : BadRequest();
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpPut("update")]
		public async Task<ActionResult<bool>> Update(User author)
		{
			var success = await _identityProvider.Update(author);
			return success ? Ok() : BadRequest();
		}

		[HttpPost("register")]
		public async Task<ActionResult<bool>> Register(RegisterModel model)
		{
			var success = await _identityProvider.Register(model);
			return success ? Ok() : BadRequest();
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginModel model)
		{
			if (await _identityProvider.Verify(model) == false)
				return BadRequest();

            //var claim = new Claim(ClaimTypes.Name, model.Email);
            //var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var user = _identityProvider.FindByEmail(model.Email).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = _identityProvider.GetRolesOfUser(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
			return Ok();
		}

		[HttpGet("logout")]
		public async Task<ActionResult<bool>> LogOutUser()
		{
			await HttpContext.SignOutAsync();
			return await Task.FromResult(true);
		}

		[Authorize]
		[HttpPut("changepassword")]
		public async Task<ActionResult<bool>> ChangePassword(RegisterModel model)
		{
			var success = await _identityProvider.ChangePassword(model);
			return success ? Ok() : BadRequest();
		}
	}
}
