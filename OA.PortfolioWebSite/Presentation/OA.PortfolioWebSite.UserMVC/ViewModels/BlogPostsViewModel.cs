﻿namespace OA.PortfolioWebSite.UserMVC.ViewModels
{
    public class BlogPostsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}
