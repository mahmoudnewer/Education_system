using Education.DAL.CustomValidation;
using Education.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Education.DAL.models
{
    public class Role
    {
        public int Id { get; set; }
        [EnumDataType(typeof(RoleInstructorValuesEnum), ErrorMessage = "Invalid Input")]
        public string Name { get; set; }

        public virtual List<Instructor>? instructor { get; set; } = new List<Instructor>();
    }
}
