using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UniversalUserAPI.Models;

public partial class User
{
    [Required(AllowEmptyStrings=false)]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Surname { get; set; } = null!;

    [Required]
    //[EmailAddress]
    [StringLength(320)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Required]
    [Column("PESEL")]
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

    [Key]
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string? Role { get; set; }
}
