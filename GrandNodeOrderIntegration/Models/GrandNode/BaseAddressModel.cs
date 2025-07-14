using System.ComponentModel.DataAnnotations;

namespace GrandNodeOrderIntegration.Models.GrandNode
{
    public class BaseAddressModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "FirstName boş değer kabul edilmez.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "LastName boş değer kabul edilmez.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Email boş değer kabul edilmez.")]
        public string Email { get; set; }

        public string Company { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "CountryId boş değer kabul edilmez.")]
        public string CountryId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "StateProvinceId boş değer kabul edilmez.")]
        public string StateProvinceId { get; set; }

        public string City { get; set; }
        
        public string Address1 { get; set; }
        
        public string Address2 { get; set; }
    
        [Required]
        [MinLength(1, ErrorMessage = "ZipPostalCode boş değer kabul edilmez.")]
        public string ZipPostalCode { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "PhoneNumber boş değer kabul edilmez.")]
        public string PhoneNumber { get; set; }
        
        public string Note { get; set; }
        
        public int AddressType { get; set; }
        
        public DateTime CreatedOnUtc { get; set; }
    }
}
