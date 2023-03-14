namespace MyAspDotNet6App.Domain
{
    public class User
    {
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string? MailAddress { get; set; }
        public DateOnly? JoinedDate { get; set; }
    }
}
