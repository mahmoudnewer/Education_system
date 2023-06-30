using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Education.ViewModel
{
    public class InstructorPassword
    {
        [DataType(DataType.Password)]
        public string Password { set; get; }

        public string OldPassword { set; get; }
        [Compare("Password")]
       
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}
