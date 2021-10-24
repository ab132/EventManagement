using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.ViewModels
{
    public class LegalPersonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the organization name")]
        [DisplayName("Name of company")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the registry code")]
        [DisplayName("Registry code")]
        public int? RegistryCode { get; set; }

        [Required(ErrorMessage = "Please enter the number of guests")]
        [DisplayName("Number of guests")]
        public int? NumberOfGuests { get; set; }

        [Required(ErrorMessage = "Please choose a payment option")]
        [DisplayName("Payment option")]
        public PaymentOption PaymentOption { get; set; }

        [StringLength(5000, ErrorMessage = "Additional Info can be max 5000 characters.")]
        [DisplayName("Additional info")]
        public string AdditionalInfo { get; set; }
        public int? EventId { get; set; }
    }
}
