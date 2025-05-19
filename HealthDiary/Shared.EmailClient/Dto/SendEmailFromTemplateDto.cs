namespace Shared.EmailClient.Dto
{
    public class SendEmailFromTemplateDto
    {
        public required string TemplateName { get; set; }
        public required Dictionary<string, string> Placeholders { get; set; }
        public required string To { get; set; }
    }
}
