using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Extensions
{
    public static class TagMappingExtensions
    {
        public static Tag ToEntity(this TagDto dto)
        {
            if (dto == null) return null;

            return new Tag()
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                Name = dto.Name
            };
        }

        public static TagDto ToDto(this Tag dto)
        {
            if (dto == null) return null;

            return new TagDto()
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                Name = dto.Name
            };
        }
    }
}
