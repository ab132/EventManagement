using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class EventModelContext : DbContext
    {
        public EventModelContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<Event> Events { get; set; }
        DbSet<PrivateGuest>PrivateGuests {get; set;}
        DbSet<LegalPerson> LegalPersons { get; set; }
    }
}
