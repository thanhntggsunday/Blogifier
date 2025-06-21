using Blogifier.Core.AdoNet;
using Blogifier.Core.Modules.Pms.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class ProductRepository
    {
        public static List<ProductDto> GetProducts(this Blogifier.Core.AdoNet.SQLite.DataAccess dataAccess, string cultureName = "")
        {
            var mapper = Blogifier.Core.AdoNet.SQLite.Mapper.CreateMapper<ProductDto>();
            return dataAccess.GetAllItems<ProductDto>("Select * From Product", CommandType.Text, mapper);
        }
    }
}
