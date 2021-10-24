using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Core.Repositories
{
    public interface IPrivateGuestRepository
    {
        PrivateGuest GetPrivateGuest(int privateGuestId);
        IEnumerable<PrivateGuest> GetPrivateGuests();
        IEnumerable<PrivateGuest> GetPrivateGuests(int id);
        void Add(PrivateGuest privateGuest);
        PrivateGuest Find(int id);
        void Update(PrivateGuest privateGuest);
        PrivateGuest GetPrivateGuest(int id, string identification);
    }
}
