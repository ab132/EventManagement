using EventManagement.Core.Repositories;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Persistance.Repositories
{
    public class PrivateGuestRepository : IPrivateGuestRepository
    {
        private readonly IEventModelContext _context;

        public PrivateGuestRepository(IEventModelContext context)
        {
            _context = context;
        }

        public PrivateGuest GetPrivateGuest(int privateGuestId)
        {
            return _context.PrivateGuests
                    .SingleOrDefault(p => p.Id == privateGuestId);
        }

        public IEnumerable<PrivateGuest> GetPrivateGuests()
        {
            return _context.PrivateGuests.ToList();
        }

        public IEnumerable<PrivateGuest> GetPrivateGuests(int id)
        {
            return _context.PrivateGuests.Where(p => p.EventId == id).ToList();
        }

        public PrivateGuest Find(int id)
        {
            return _context.PrivateGuests.Find(id);
        }

        public void Add(PrivateGuest privateGuest)
        {
            _context.PrivateGuests.Add(privateGuest);
        }

        public void Update(PrivateGuest privateGuest)
        {
            _context.PrivateGuests.Update(privateGuest);
        }
        public PrivateGuest GetPrivateGuest(int id, string identification)
        {
            return _context.PrivateGuests.Where(p => p.PersonalIdentificationCode == identification && p.EventId == id).FirstOrDefault();
        }
    }
}
