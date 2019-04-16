using System;

using System.ComponentModel.DataAnnotations;

namespace blog.Model
{
    public class CategoryModel
    {         
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}