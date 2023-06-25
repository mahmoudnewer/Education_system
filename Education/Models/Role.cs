using Education.Enums;
using System.ComponentModel.DataAnnotations;

namespace Education.Models
{
    public class Role
    {
        public int Id { get; set; }
        [EnumDataType(typeof(RoleInstructorValuesEnum), ErrorMessage = "Invalid Input")]
        public string Name { get; set; }

        public virtual List<Instructor>? instructor { get; set; } = new List<Instructor>();
    }
}
