using System.Collections.Generic;

namespace blog.Model
{
    public class CommentListModel
    {
        public IEnumerable<CommentModel> Comments { get; set; }
    }
}