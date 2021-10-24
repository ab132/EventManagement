using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.ViewModels
{
    public class GuestViewModel
    {
        public int EventId { get; set; }
        public PrivateGuestViewModel PrivateGuestViewModel { get; set; }
        public LegalPersonViewModel LegalPersonViewModel { get; set; }
        public IEnumerable<string> PaymentOptions { get; set; }
        public string GuestType { get; set; }
    }
}
