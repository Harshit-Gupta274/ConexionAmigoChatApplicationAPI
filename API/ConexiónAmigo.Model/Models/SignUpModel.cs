using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Model.Models
{
    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string AvatarImageName { get; set; }
        public string Avatar { get; set; }  

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateTime CreatedDate { get; set; }
        public string VerificationToken { get; set; }

        public DateTime VerificationExpire { get; set; }  
    }

    public class SignUpResponseModel
    {
        public bool Success { get; set; }
    }
}
