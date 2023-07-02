using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Education.Models
{
    public class Instructor
    {
        public int Id { set; get; }
        [MinLength(3)]
        public string Name { set; get; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { set; get; }
        [RegularExpression("^[0-9]{7,11}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(7)]
        public string Phone { set; get; }
        public string? Address { set; get; }

        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Compare("Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [Range(15, 100)]
        public int? Age { set; get; }
        [Column(TypeName = "image")]
        
        public byte[]? image { set; get; }

        //public IFormFile ImageFile;

        public bool Isdeleted { set; get; }

        [ForeignKey("role")]
        public int RoleId { set; get; }
        public virtual Role? role { set; get; }
        public virtual List<Topic>? Topic { get; set; } = new List<Topic>();
        public virtual List<Instructor_Student>? instructorStudents { set; get; } = new List<Instructor_Student>();
        public virtual List<StudentRequests>? StudentRequests { set; get; } = new List<StudentRequests>();

    }
}
