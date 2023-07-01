using Education.DAL.Custom_Validtion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Models
{
    public class Student
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        [Range(5,60)]
        public int Age { get; set; }
        [RegularExpression("^[0-9]{11,11}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(7)]
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string  confirm { get; set; } 
        public byte[]? image { get; set; } 

        public bool IsDeleted { set; get; }
        public virtual List<Instructor_Student>? instructorStudents { get; set; } = new List<Instructor_Student>();
        public virtual List<StudentRequests>? StudentRequests { set; get; } = new List<StudentRequests>();

        public virtual List<Grade>? Grades { get; set; } = new List<Grade>();

    }
}
