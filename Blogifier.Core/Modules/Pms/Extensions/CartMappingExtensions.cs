using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Extensions
{
    public static class CartItemMappingExtensions
    {
        public static CartItem ToEntity(this CartItemDto dto)
        {
            if (dto == null) return null;

            var result = new CartItem
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                Quantity = dto.Quantity,
                Color = dto.Color,
                UnitPrice = dto.UnitPrice,
                TotalPrice = dto.TotalPrice,
                ProductId = dto.ProductId,
                CartId = dto.CartId,
                Cart = dto.Cart.ToEntity(),
                Product = dto.Product.ToEntity()
            };

            return result;
        }

        public static CartItemDto ToDto(this CartItem entity)
        {
            if (entity == null) return null;

            var result = new CartItemDto
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                ModifiedDate = entity.ModifiedDate,
                ModifiedBy = entity.ModifiedBy,
                Quantity = entity.Quantity,
                Color = entity.Color,
                UnitPrice = entity.UnitPrice,
                TotalPrice = entity.TotalPrice,
                ProductId = entity.ProductId,
                CartId = entity.CartId,
                Cart = entity.Cart.ToDto(),
                Product = entity.Product.ToDto()
            };

            return result;
        }
    }


    //============================================================================//
    public static class CartMappingExtensions
    {
        public static Cart ToEntity(this CartDto dto)
        {
            if (dto == null) return null;

            var resut = new Cart
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                UserName = dto.UserName,
                Items = new List<CartItem>()
            };

            for (int i = 0; i < dto.Items.Count; i++)
            {
                var e = dto.Items[i];

                resut.Items.Add(e.ToEntity());
            }

            return resut;
        }

        public static CartDto ToDto(this Cart dto)
        {
            if (dto == null) return null;

            var result = new CartDto
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                UserName = dto.UserName,
                Items =  new List<CartItemDto>()
            };

            for (int i = 0; i < dto.Items.Count; i++)
            {
                var e = dto.Items[i];

                result.Items.Add(e.ToDto());
            }

            return result;
        }
    }
}
