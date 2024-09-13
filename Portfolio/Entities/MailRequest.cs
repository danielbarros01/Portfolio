using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class MailRequest
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Message { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
    }
}
