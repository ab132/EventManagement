using EventManagement.Core.Repositories;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Persistance.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventModelContext _context;

        public EventRepository(IEventModelContext context)
        {
            _context = context;
        }

        public Event GetEvent(int? eventId)
        {
            return _context.Events
                    .SingleOrDefault(e => e.Id == eventId);
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.ToList();
        }

        public void Add(Event eventModel)
        {
            _context.Events.Add(eventModel);
        }

        public void Update(Event eventModel)
        {
            _context.Events.Update(eventModel);
        }

        public Event Find(int id)
        {
            return _context.Events.Find(id);
        }
        public void Delete(Event eventModel)
        {
            _context.Events.Remove(eventModel);
        }
    }
}
