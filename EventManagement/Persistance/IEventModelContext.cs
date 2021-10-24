using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Persistance
{
    public interface IEventModelContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<PrivateGuest> PrivateGuests { get; set; }
        DbSet<LegalPerson> LegalPersons { get; set; }
    }
}
