namespace EmailService.Domain.Models
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
