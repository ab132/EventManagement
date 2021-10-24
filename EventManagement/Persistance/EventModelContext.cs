using EventManagement.Models.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.ViewModels;
using EventManagement.Persistance;

namespace EventManagement.Models
{
    public class EventModelContext : DbContext, IEventModelContext
    {
        public EventModelContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<PrivateGuest>PrivateGuests {get; set;}
        public DbSet<LegalPerson> LegalPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed initial data
            modelBuilder.ApplyConfiguration(new SeedEvents());
            modelBuilder.Entity<PrivateGuest>()
                .HasOne(g => g.Event)
                .WithMany(e => e.PrivateGuests)
                .OnDelete(DeleteBehavior.Cascade);  //delete dependent rows.
            modelBuilder.Entity<LegalPerson>()
                .HasOne(g => g.Event)
                .WithMany(l => l.LegalPersons)
                .OnDelete(DeleteBehavior.Cascade);  //delete dependent rows.
        }
    }
}
