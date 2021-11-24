using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core.Jwt.Example.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
    }

    public class LoginResponseModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Display(Name = "Token")]
        public string token { get; set; }

        [Display(Name = "Id")]
        public int? idUser { get; set; }

        [Display(Name = "Expiration")]
        public DateTime expiration { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        
    }
}
