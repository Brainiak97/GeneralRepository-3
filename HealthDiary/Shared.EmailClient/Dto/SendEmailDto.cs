namespace Shared.EmailClient.Dto
{
    public class SendEmailDto
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
