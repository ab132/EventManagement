using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.ViewModels
{
    public class PrivateGuestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a firstname")]
        [DisplayName("Firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a lastname")]
        [DisplayName("Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an identification code")]
        [DisplayName("Personal identification code")]
        public string PersonalIdentificationCode { get; set; }

        [Required(ErrorMessage = "Please choose a payment option")]
        [DisplayName("Payment option")]
        public PaymentOption PaymentOption { get; set; }

        [StringLength(1500, ErrorMessage = "Additional Info can be max 1500 characters.")]
        [DisplayName("Additional info")]
        public string AdditionalInfo { get; set; }
        public int? EventId { get; set; }
    }
}
