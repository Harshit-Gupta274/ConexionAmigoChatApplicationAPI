using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Model.Models
{
    public class SignInModel
    {
        public SignInModel() { }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string SessionToken { get; set; }

    }

    public class SignInDataGetModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string SessionToken { get; set; }
    }

    public class SignInResponseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string SessionToken { get; set; }
    }

}
