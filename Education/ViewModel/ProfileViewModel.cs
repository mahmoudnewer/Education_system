using System.ComponentModel.DataAnnotations;

namespace Education.ViewModel
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string? Address { set; get; }
        public int? Age { set; get; }
        public byte[]? image { set; get; }
        public string Role { set;get; }
    }
}
