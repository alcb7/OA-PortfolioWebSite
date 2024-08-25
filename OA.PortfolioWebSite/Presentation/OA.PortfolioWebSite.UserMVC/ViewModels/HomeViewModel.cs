namespace OA.PortfolioWebSite.UserMVC.ViewModels
{
    public class HomeViewModel
    {
        public PersonelInfoViewModel PersonelInfo { get; set; }
        public AboutMeViewModel AboutMe { get; set; }
        public ContactMessageViewModel Contactmessage { get; set; }
        public List<EducationsViewModel> Educations { get; set; }
        public List<BlogPostsViewModel> BlogPosts { get; set; }

    }
}
