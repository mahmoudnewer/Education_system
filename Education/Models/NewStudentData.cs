using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Models
{
    public class NewStudentData
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
 
        [Range(5, 50)]
        public int Age { get; set; }
        public byte[]? image { get; set; }
        public string? Address { get; set; }
        [RegularExpression("^[0-9]{7,11}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(7)]
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("StudentRequests")]
        public int StudentReqId { get; set; }

        public virtual StudentRequests? StudentRequests { get; set; }
    }
}


