using System.ComponentModel.DataAnnotations;

namespace UniversalUserAPI.DTOs
{
    public class EmailDTO
    {
        [EmailAddress]
        [Required]
        public string To { get; set; }=string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
    }
}
