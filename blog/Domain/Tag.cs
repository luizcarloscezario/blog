
using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class Tag : BaseEntity
    {
        [Required]
        [StringLength(40)]
        public string  Name { get; set; }

    }
}