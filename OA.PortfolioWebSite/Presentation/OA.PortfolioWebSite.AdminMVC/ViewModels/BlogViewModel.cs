namespace OA.PortfolioWebSite.AdminMVC.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
       public int? AuthorId { get; set; }
    }
}
