using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class TagRepository
    {
        public static TagDto GetTagById(this DataAccess dataAccess, TagDto dto)
        {
            var mapper = Mapper.CreateMapper<TagDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("Tags");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<TagDto> FindTag(this DataAccess dataAccess, string productName = "")
        {
            var mapper = Mapper.CreateMapper<TagDto>();
            var cmd = new SqlCommand(@"select * from Tags where [Name] like @Name");

            var value = $"'%{productName}%'";
            cmd.Parameters.AddWithValue("@Name", value);
            return dataAccess.Find(cmd, mapper);
        }

        public static void AddTag(this DataAccess dataAccess, TagDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("Tags");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateTag(this DataAccess dataAccess, TagDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("Tags", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveTag(this DataAccess dataAccess, TagDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("Tags");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}
