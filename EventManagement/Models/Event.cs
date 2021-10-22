using EventManagement.AttributeExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the event name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Please enter the event date")]
        [FutureDate(ErrorMessage = "Date should be in the future.")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Please enter the venue name")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "Please enter the number of guests")]
        public int? NumberOfGuests { get; set; }

        [StringLength(1000, ErrorMessage = "Additional Info can be max 1000 characters.")]
        public string AdditionalInfo { get; set; }
        public IEnumerable<PrivateGuest> PrivateGuests { get; set; }
        public IEnumerable<LegalPerson> LegalPersons { get; set; }
    }
}
