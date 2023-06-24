using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.DAL.models
{
    public class Student
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        [Range(5, 50)]
        public int Age { get; set;}
        [RegularExpression("^[0-9]{7,11}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(7)]
        public string Phone { get; set; }
        public string?  Address { get; set; }
        public bool confirm { get; set; }
        public byte[]? image { get; set; }
        public bool IsDeleted { set; get; }

        [ForeignKey("instructor")]
        public int InstructorId { set; get; }

        public virtual Instructor? instructor { set; get; }

        public virtual List<Grade>? Grades { get; set; }  = new List<Grade>();
   
    }
}
