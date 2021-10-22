using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class PrivateGuest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an identification code")]
        public Guid PersonalIdentificationCode { get; set; }

        [Required(ErrorMessage = "Please choose a payment option")]
        public PaymentOption PaymentOption { get; set; }

        [StringLength(1500, ErrorMessage = "Additional Info can be max 1500 characters.")]
        public string AdditionalInfo { get; set; }

        [Required(ErrorMessage = "Please enter an event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
