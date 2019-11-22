using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Domain
{
    public class Configuration : BaseEntity
    {

        public string Head { get; set; }

        public string Body { get; set; }

        public string Footer { get; set; }
             
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        
    }
}