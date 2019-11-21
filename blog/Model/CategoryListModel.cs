using System.Collections.Generic;
using blog.Domain;

namespace blog.Model
{
    public class CategoryListModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}