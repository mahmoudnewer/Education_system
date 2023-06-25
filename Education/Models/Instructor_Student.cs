using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Models
{
    public class Instructor_Student
    {
        [ForeignKey("student")]
        public int Student_Id { get; set; }

        public Student student { get; set; }

        [ForeignKey("instructor")]
        public int Instructor_Id { get; set; }

        public Instructor instructor { get; set; }
    }
}
