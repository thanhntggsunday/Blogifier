﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Domain
{
    public class Profile : BaseEntity
    {
        public Profile() { }

        [Required]
        [StringLength(100)]
        public string IdentityName { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug format not valid.")]
        [StringLength(160)]
        public string Slug { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(160)]
        public string AuthorEmail { get; set; }

        [StringLength(4000)]
        public string Bio { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        [StringLength(160)]
        public string BlogTheme { get; set; }
                
        [StringLength(160)]
        public string Logo { get; set; }
        [StringLength(160)]
        public string Avatar { get; set; }
        [StringLength(160)]
        public string Image { get; set; }

        public List<BlogPost> BlogPosts { get; set; }
        public List<Asset> Assets { get; set; }
    }
}