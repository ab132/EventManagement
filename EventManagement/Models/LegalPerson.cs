using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class LegalPerson
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the organization name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the registry code")]
        public int? RegistryCode { get; set; }

        [Required(ErrorMessage = "Please enter the number of guests")]
        public int? NumberOfGuests { get; set; }

        [Required(ErrorMessage = "Please choose a payment option")]
        public PaymentOption PaymentOption { get; set; }

        [StringLength(5000, ErrorMessage = "Additional Info can be max 5000 characters.")]
        public string AdditionalInfo { get; set; }

        [Required(ErrorMessage = "Please enter an event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
