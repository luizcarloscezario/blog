using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class Media : BaseEntity
    {
        [Required]
        public string Path { get; set; }

        [Required]
        public string Name { get; set; }
                
    }
}