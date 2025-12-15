using System.ComponentModel.DataAnnotations;

namespace Lab05.Models
{
    // Model chính lưu trong database
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool IsComplete { get; set; }

        // Thuộc tính nhạy cảm, không cho client nhìn thấy
        public string? Secret { get; set; }
    }
}
