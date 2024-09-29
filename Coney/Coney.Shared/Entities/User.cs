﻿using System.ComponentModel.DataAnnotations;

namespace Coney.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}