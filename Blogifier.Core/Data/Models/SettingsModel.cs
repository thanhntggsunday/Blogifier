﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Models
{
    public class SettingsProfile : AdminBaseModel
    {
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(160)]
        public string AuthorEmail { get; set; }
        [StringLength(160)]
        public string Avatar { get; set; }
        public bool EmailEnabled { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }

    public class SettingsGeneral : AdminBaseModel
    {
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        [StringLength(160)]
        public string Logo { get; set; }
        [StringLength(160)]
        public string Image { get; set; }
        [StringLength(160)]
        public string Avatar { get; set; }

        [StringLength(280)]
        public string EmailKey { get; set; }
        
        public string BlogHead { get; set; }
        public string BlogFooter { get; set; }

        [Required]
        [StringLength(160)]
        public string BlogTheme { get; set; }
        public IList<SelectListItem> BlogThemes { get; set; }
    }

    public class SettingsPosts : AdminBaseModel
    {
        [StringLength(160)]
        public string PostImage { get; set; }
        public string PostFooter { get; set; }
        public int ItemsPerPage { get; set; }
    }

    public class SettingsAdvanced : AdminBaseModel
    {
        
    }
}