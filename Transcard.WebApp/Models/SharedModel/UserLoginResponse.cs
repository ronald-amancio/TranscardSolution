using System.ComponentModel.DataAnnotations;

namespace Transcard.WebApp.Models.SharedModel
{
    public class UserLoginResponse
    {
        public int Id { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
        //public string? UserName { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        //public string? Password { get; set; }

        public string? Role { get; set; }

        public int LoginId { get; set; }
        public string IPAddress { get; set; } //newly added
    }
}