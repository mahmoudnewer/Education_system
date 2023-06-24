using Education.DAL.Custom_Validtion;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.DAL.models
{
    public class StudentRequests
    {
        public int Id { set; get; }
        public int St_id { get; set; }
        [AllowedValues(new string[] { "Pending", "Aceepted","Rejected" })]
        public string Status { get; set; }
        [AllowedValues(new string[] { "Add", "Edit", "Delete" })]
        public string OnType { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { set; get; }
        public virtual Instructor? Instructor { set; get; }
        public virtual NewStudentData? NewStudentData { set; get; }
    }
}
