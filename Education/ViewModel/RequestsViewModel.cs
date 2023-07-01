using System.ComponentModel.DataAnnotations;

namespace Education.ViewModel
{
    public class RequestsViewModel
    {
        public int Id { get; set; }

        [MinLength(3,ErrorMessage ="Name must be 3 charachters or more")]
        public string StudentName { get; set; }

        [Range(5, 60)]
        public int Age { get; set; }

        [RegularExpression("^[0-9]{7,11}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(7)]
        public string Phone { get; set; }
        public string? Address { get; set; }
        public byte[]? Image { get; set; }
        public string? InstructorName { get; set; }
        public string? Status { get; set; }
        public string? OperationType { get; set; }

    }
}
