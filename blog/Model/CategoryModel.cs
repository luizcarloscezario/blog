using System;

using System.ComponentModel.DataAnnotations;

namespace blog.Model
{
    public class CategoryModel
    {         
        public int Id { get; set; }

       [Required(AllowEmptyStrings=false)]  
       [DisplayFormat(ConvertEmptyStringToNull=false)] 
        public string Name { get; set; }

       [Required(AllowEmptyStrings=false)]
       [DisplayFormat(ConvertEmptyStringToNull=false)]
        public string Description { get; set; }
    }
}