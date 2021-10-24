using EventManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Core
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        ILegalPersonRepository LegalPersons { get; }
        IPrivateGuestRepository PrivateGuests { get; }
        void Complete();
    }
}
