using System.ComponentModel.DataAnnotations;

namespace LapWeb01.Models
{
    public class Movie
    {
        // Khóa chính (Primary Key) của database sau này
        public int Id { get; set; }

        // Tên phim
        public string? Title { get; set; }

        // Ngày phát hành (Chỉ lấy ngày, không lấy giờ)
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Thể loại
        public string? Genre { get; set; }

        // Giá vé
        public decimal Price { get; set; }
    }
}