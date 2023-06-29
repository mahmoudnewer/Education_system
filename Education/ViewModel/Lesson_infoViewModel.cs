using Education.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Education.ViewModel
{
    public class Lesson_infoViewModel
    {
       
        public int Number { get; set; }
        public string Name { get; set; }
        public Topic_TypeValues Type { get; set; } 
        public DateTime Date { get; set; }
        public string InstructorName { set; get; }
    }
}
