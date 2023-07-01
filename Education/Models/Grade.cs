using Education.DAL.Custom_Validtion;
using Education.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal grade { get; set; }


        [EnumDataType(typeof(TypeAllowedValues), ErrorMessage = "Please Select Grade Type")]
        [Required(ErrorMessage = "Please Select Grade Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please Select Grade Date")]
        [Date]
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("student")]
        public int studentId { set; get; }

        public virtual Student? student { set; get; }

    }
}
