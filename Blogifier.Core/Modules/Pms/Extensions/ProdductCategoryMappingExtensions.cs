using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Extensions
{
    public static class ProdductCategoryMappingExtensions
    {
        public static ProductCategory ToEntity(this ProductCategoryDto dto)
        {
            if (dto == null) return null;

            return new ProductCategory
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                Name = dto.Name,
                Description = dto.Description,
                ImageName = dto.ImageName
            };
        }

        public static ProductCategoryDto ToDto(this ProductCategory entity)
        {
            if (entity == null) return null;

            return new ProductCategoryDto
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                ModifiedDate = entity.ModifiedDate,
                ModifiedBy = entity.ModifiedBy,
                Name = entity.Name,
                Description = entity.Description,
                ImageName = entity.ImageName
            };
        }
    }
}
