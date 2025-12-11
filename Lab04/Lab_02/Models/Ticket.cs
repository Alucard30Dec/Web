using System.ComponentModel.DataAnnotations;

namespace Lab_02.Models   
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Purchase Date")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
