using System.Collections.Generic;

namespace blog.Model
{
    public class ArticleListModel
    {
        public IEnumerable<ArticleModel> Articles { get; set; }
    }
}