using System;

namespace blog.Domain
{
    public class MediaArticle
    {
        public int Id { get; set; }

        public int IdArticle { get; set; }

        public Article Article { get; set; }

        public int IdMedia { get; set; }

        public Media Media { get; set; }
    }
}