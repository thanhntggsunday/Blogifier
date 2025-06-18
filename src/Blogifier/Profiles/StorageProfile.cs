using AutoMapper;
using Blogifier.Core.Storages;
using Blogifier.Shared;

namespace Blogifier.Profiles;

public class StorageProfile : Profile
{
  public StorageProfile() => CreateMap<Storage, StorageDto>().ReverseMap();
  //CreateMap<StorageReference, StorageDto>()//  .IncludeMembers(m => m.Storage)//  .ReverseMap();
}
