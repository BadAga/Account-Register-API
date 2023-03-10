using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversalUserAPI.Models.DTOs
{
    public class RegisterDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Surname { get; set; } = null!;
        [Required]
        [EmailAddress]
        [StringLength(320)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(11)]
        [Unicode(false)]
        public string Pesel { get; set; } = null!;
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [Required]
        // [Phone]
        [StringLength(15)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        public int? Age { get; set; }
    }
}
