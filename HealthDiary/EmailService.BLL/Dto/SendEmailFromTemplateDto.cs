namespace EmailService.BLL.Dto
{
    public class SendEmailFromTemplateDto
    {
        public string? TemplateName { get; set; }
        public Dictionary<string, string>? Placeholders { get; set; }
        public string? To { get; set; }
    }
}
