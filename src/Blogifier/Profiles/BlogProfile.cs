using AutoMapper;
using Blogifier.Core.Blogs;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class BlogProfile : Profile
{
  public BlogProfile()
  {
    CreateMap<BlogData, MainDto>();
    CreateMap<BlogData, BlogEitorDto>().ReverseMap();
  }
}
