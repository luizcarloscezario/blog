using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string  Description { get; set; }

        public int? MediaId { get; set; }
        public virtual Media Media { get; set; }
        
    }
}