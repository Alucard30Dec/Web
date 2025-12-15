namespace Lab05.Models
{
    // DTO dùng để giao tiếp với client
    // Không có Secret → tránh over-posting / lộ dữ liệu
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
