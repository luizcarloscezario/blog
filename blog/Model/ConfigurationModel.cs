using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Model
{
    public class ConfigurationModel
    {
        public string Head { get; set; }

        public string Body { get; set; }

        public string Footer { get; set; }

        [Required]     
        public int ArticleId { get; set; }
        
    }
}