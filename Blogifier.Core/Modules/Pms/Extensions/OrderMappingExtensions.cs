using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Extensions
{
    public static class OrderItemMappingExtensions
    {
        public static OrderItem ToEntity(this OrderItemDto dto)
        {
            if (dto == null) return null;

            var result = new OrderItem
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
                OrderId = dto.OrderId,
                Order = dto.Order.ToEntity(),
                Product = dto.Product.ToEntity()
            };

            return result;
        }

        public static OrderItemDto ToDto(this OrderItem entity)
        {
            if (entity == null) return null;

            var result = new OrderItemDto
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
                OrderId = entity.OrderId,
                Order = entity.Order.ToDto(),
                Product = entity.Product.ToDto()
            };

            return result;
        }
    }


    //============================================================================//
    public static class OrderMappingExtensions
    {
        public static Order ToEntity(this OrderDto dto)
        {
            if (dto == null) return null;

            var resut = new Order
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                UserName = dto.UserName,
                Items = new List<OrderItem>()
            };

            for (int i = 0; i < dto.Items.Count; i++)
            {
                var e = dto.Items[i];

                resut.Items.Add(e.ToEntity());
            }

            return resut;
        }

        public static OrderDto ToDto(this Order dto)
        {
            if (dto == null) return null;

            var result = new OrderDto
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                ModifiedDate = dto.ModifiedDate,
                ModifiedBy = dto.ModifiedBy,
                UserName = dto.UserName,
                Items = new List<OrderItemDto>()
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
