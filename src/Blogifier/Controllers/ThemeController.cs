using Blogifier.Core.Providers;
using Blogifier.Middleware;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ThemeController : ControllerBase
	{
		private readonly IThemeProvider _themeProvider;
		private readonly IStorageProvider _storageProvider;

		public ThemeController(IThemeProvider themeProvider, IStorageProvider storageProvider)
		{
			_themeProvider = themeProvider;
			_storageProvider = storageProvider;
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("{theme}")]
		public async Task<ThemeSettings> GetThemeSettings(string theme)
		{
			return await _storageProvider.GetThemeSettings(theme);
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpPost("{theme}")]
		public async Task<bool> SaveThemeSettings(string theme, ThemeSettings settings)
		{
			return await _storageProvider.SaveThemeSettings(theme, settings);
		}
	}
}
