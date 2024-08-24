namespace OA.PortfolioWebSite.UserMVC.ViewModels
{
    public class ContactMessageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; } = DateTime.UtcNow;  // Varsayılan olarak şu anki zaman
        public bool IsRead { get; set; } = false;                  // Varsayılan olarak okunmadı
        public string? Reply { get; set; } = string.Empty;         // Varsayılan olarak boş
        public DateTime? ReplyDate { get; set; }
    }

}
