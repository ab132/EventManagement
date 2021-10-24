using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models.SeedData
{
    internal class SeedEvents : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasData(
                new Event { Id = 1, EventName = "Event1", Date = DateTime.Now.AddDays(2), Venue = "Venue1", NumberOfGuests = 0, AdditionalInfo = "Additional info 1" },
                new Event { Id = 2, EventName = "Event2", Date = DateTime.Now.AddDays(5), Venue = "Venue2", NumberOfGuests = 0, AdditionalInfo = "Additional info 2" },
                new Event { Id = 3, EventName = "Event3", Date = DateTime.Now.AddDays(7), Venue = "Venue3", NumberOfGuests = 0, AdditionalInfo = "Additional info 3" },
                new Event { Id = 4, EventName = "Event4", Date = DateTime.Now.AddDays(10), Venue = "Venue4", NumberOfGuests = 0, AdditionalInfo = "Additional info 4" }
            );
        }
    }
}
