using Education.DAL.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.DAL.models
{
    public class Grade
    {
        public int Id { get; set; }
        public decimal grade { get; set; }
        [EnumDataType(typeof(TypeAllowedValues),ErrorMessage ="Invalid Input")]
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("student")]
        public int studentId { set; get; }

        public virtual Student? student { set; get; }

    }
}
