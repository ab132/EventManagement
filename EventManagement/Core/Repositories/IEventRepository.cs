using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Core.Repositories
{
    public interface IEventRepository
    {
        Event GetEvent(int? eventId);
        IEnumerable<Event> GetEvents();
        void Add(Event eventModel);
        void Update(Event eventModel);
        Event Find(int id);
        void Delete(Event eventModel);

    }
}
