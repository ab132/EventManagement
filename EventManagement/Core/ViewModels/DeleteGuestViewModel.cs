using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.ViewModels
{
    public class DeleteGuestViewModel
    {
        public int Id { get; set; }
        public string GuestType { get; set; }
        public int EventId { get; set; }
    }
}
