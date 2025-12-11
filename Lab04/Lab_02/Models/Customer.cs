using System.ComponentModel.DataAnnotations;

namespace Lab_02.Models   
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }
    }
}
