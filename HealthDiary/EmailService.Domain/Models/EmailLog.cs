namespace EmailService.Domain.Models
{
    public class EmailLog
    {
        public int Id { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public bool IsSent { get; set; }
        public DateTime SentAt { get; set; }
    }
}
