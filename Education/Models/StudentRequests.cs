using Education.DAL.Custom_Validtion;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Models
{
    public class StudentRequests
    {
        public int Id { set; get; }
   
        [AllowedValues(new string[] { "pending", "aceepted", "rejected" })]
        public string Status { get; set; }

        [AllowedValues(new string[] { "add", "edit", "delete" })]
        public string OperationType { get; set; }
        public bool IsDeleted { get; set; }


        [ForeignKey("Instructor")]
        public int InstructorId { set; get; }
        public virtual Instructor? Instructor { set; get; }


        [ForeignKey("Student")]
        public int StudentId { set; get; }
        public virtual Student? Student { set; get; }

        public virtual NewStudentData? NewStudentData { set; get; }
    }
}
