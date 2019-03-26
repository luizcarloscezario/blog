using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
        
    }
}