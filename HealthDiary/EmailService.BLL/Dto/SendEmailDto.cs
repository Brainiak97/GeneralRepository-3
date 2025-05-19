namespace EmailService.BLL.Dto
{
    public class SendEmailDto
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
