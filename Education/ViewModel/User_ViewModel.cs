using System.ComponentModel.DataAnnotations;

namespace Education.ViewModel
{
    public class User_ViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
