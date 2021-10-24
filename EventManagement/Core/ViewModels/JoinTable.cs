using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.ViewModels
{
    public class JoinTable
    {
        public IEnumerable<PrivateGuest> PrivateGuests { get; set; } = new List<PrivateGuest>();
        public IEnumerable<LegalPerson> LegalPersons { get; set; } = new List<LegalPerson>();

        public int EventId { get; set; }

    }
}
