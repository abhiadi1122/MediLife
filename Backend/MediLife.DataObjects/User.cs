using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediLife.DataObjects
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        [StringLength(maximumLength: 10, MinimumLength = 10)]
        public string MobileNumber { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
