using System.ComponentModel.DataAnnotations;

namespace GrandNodeOrderIntegration.Models.GrandNode
{
    public class CustomerModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Username boş değer kabul edilmez.")]
        public string Username { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Email boş değer kabul edilmez.")]
        public string Email { get; set; }

        public string AdminComment { get; set; }

        public bool IsTaxExempt { get; set; }

        public bool FreeShipping { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "FirstName boş değer kabul edilmez.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "LastName boş değer kabul edilmez.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "StoreId boş değer kabul edilmez.")]
        public string StoreId { get; set; }

        public List<string> Groups { get; set; }
    }
}
