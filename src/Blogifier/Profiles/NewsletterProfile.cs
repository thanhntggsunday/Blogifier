using AutoMapper;
using Blogifier.Core.Newsletters;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class NewsletterProfile : Profile
{
  public NewsletterProfile() => CreateMap<Newsletter, NewsletterDto>();
}
