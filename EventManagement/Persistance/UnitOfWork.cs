using EventManagement.Core;
using EventManagement.Core.Repositories;
using EventManagement.Models;
using EventManagement.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private EventModelContext _context;

        public IEventRepository Events { get; private set; }
        public IPrivateGuestRepository PrivateGuests { get; private set; }
        public ILegalPersonRepository LegalPersons { get; private set; }

        public UnitOfWork(EventModelContext context)
        {
            _context = context;
            Events = new EventRepository(context);
            PrivateGuests = new PrivateGuestRepository(context);
            LegalPersons = new LegalPersonRepository(context);
            _context.SaveChanges();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
