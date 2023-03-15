﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversalUserAPI.Models.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [StringLength(320)]
        [Unicode(false)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Password { get; set; } = null!;       
    }
}
