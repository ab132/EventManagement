using EventManagement.AttributeExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Core.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name of event")]
        public string EventName { get; set; }

        [DisplayName("Date of event")]
        public DateTime? Date { get; set; }

        [DisplayName("Venue")]
        public string Venue { get; set; }

        [DisplayName("Number of guests")]
        public int? NumberOfGuests { get; set; }

        [DisplayName("Additional info")]
        public string AdditionalInfo { get; set; }
    }
}
