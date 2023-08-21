using System.ComponentModel.DataAnnotations;
using Vidly.Models.CustomValidations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [CustomerBirthdayValidation]
        [Display(Name="Date of Birth")]
        public DateTime? BirthDate { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType? MembershipType { get; set; } 
        
        [Display(Name="Membershipe Type")]
        public byte MembershipTypeId { get; set; }
    }
}
