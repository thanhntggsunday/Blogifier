using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Extensions
{
    public static class ProductMappingExtensions
    {
        public static ProductDto ToDto(this Product entity)
        {
            if (entity == null) return null;

            return new ProductDto
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                ModifiedDate = entity.ModifiedDate,
                ModifiedBy = entity.ModifiedBy,
                Name = entity.Name,
                Slug = entity.Slug,
                Summary = entity.Summary,
                Description = entity.Description,
                ImageFile = entity.ImageFile,
                UnitPrice = entity.UnitPrice,
                UnitsInStock = entity.UnitsInStock,
                Star = entity.Star,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category?.Name
            };
        }

        public static Product ToEntity(this ProductDto dto)
        {
            if (dto == null) return null;

            return new Product
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                Name = dto.Name,
                Slug = dto.Slug,
                Summary = dto.Summary,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock,
                Star = dto.Star,
                CategoryId = dto.CategoryId
                
            };
        }
    }

}
