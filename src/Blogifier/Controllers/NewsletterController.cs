using Blogifier.Core.Providers;
using Blogifier.Middleware;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsletterController : ControllerBase
	{
		protected readonly INewsletterProvider _newsletterProvider;

		public NewsletterController(INewsletterProvider newsletterProvider)
		{
			_newsletterProvider = newsletterProvider;
		}

		[HttpPost("subscribe")]
		public async Task<ActionResult<bool>> Subscribe([FromBody] Subscriber subscriber)
		{
			return await _newsletterProvider.AddSubscriber(subscriber);
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("subscribers")]
		public async Task<List<Subscriber>> GetSubscribers()
		{
			return await _newsletterProvider.GetSubscribers();
		}

		[HttpDelete("unsubscribe/{id:int}")]
		public async Task<ActionResult<bool>> RemoveSubscriber(int id)
		{
			return await _newsletterProvider.RemoveSubscriber(id);
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("newsletters")]
		public async Task<List<Newsletter>> GetNewsletters()
		{
			return await _newsletterProvider.GetNewsletters();
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("send/{postId:int}")]
		public async Task<bool> SendNewsletter(int postId)
		{
			return await _newsletterProvider.SendNewsletter(postId);
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpDelete("remove/{id:int}")]
		public async Task<ActionResult<bool>> RemoveNewsletter(int id)
		{
			return await _newsletterProvider.RemoveNewsletter(id);
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpGet("mailsettings")]
		public async Task<MailSetting> GetMailSettings()
		{
			return await _newsletterProvider.GetMailSettings();
		}

		[Authorize]
        [AuthorizeRole("Admin")]
        [HttpPut("mailsettings")]
		public async Task<ActionResult<bool>> SaveMailSettings([FromBody] MailSetting mailSettings)
		{
			return await _newsletterProvider.SaveMailSettings(mailSettings);
		}
	}
}
