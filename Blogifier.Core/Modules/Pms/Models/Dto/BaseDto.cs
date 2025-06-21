using System;
using System.Security.Cryptography;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public void SetModificationInfo(string modifiedBy)
        {
            ModifiedDate = DateTime.Now;
            ModifiedBy = modifiedBy;
        }

        public void SetCreatedInfo(string createdBy)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
        }
    }
}
