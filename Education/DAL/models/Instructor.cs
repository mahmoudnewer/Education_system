using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.DAL.models
{
    public class Instructor
    {
        public int Id { set; get; }
        [MinLength(3)]
        public string Name { set; get;}
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { set; get;}
        [RegularExpression("^[0-9]{7,11}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(7)]
        public string Phone { set; get;}
        public string? Address { set; get;}
        [DataType(DataType.Password)]
        public string Password { set; get; }
        public int? Age { set; get; }
        public byte[]? image { set; get; }
        public bool Isdeleted { set; get; }

        [ForeignKey("role")]
        public int RoleId { set; get; }
        public virtual Role? role { set; get; }
        public virtual List<Topic>? Topic { get; set; } = new List<Topic>();
        public virtual List<Student>? Students { set; get; } = new List<Student>();
        public virtual List<StudentRequests>? StudentRequests { set; get; } = new List<StudentRequests>();
    
    }
}
