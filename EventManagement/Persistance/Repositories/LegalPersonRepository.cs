using EventManagement.Core.Repositories;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Persistance.Repositories
{
    public class LegalPersonRepository : ILegalPersonRepository
    {
        private readonly IEventModelContext _context;

        public LegalPersonRepository(IEventModelContext context)
        {
            _context = context;
        }

        public LegalPerson GetLegalPerson(int legalPersonId)
        {
            return _context.LegalPersons
                    .SingleOrDefault(l => l.Id == legalPersonId);
        }
        public IEnumerable<LegalPerson> GetLegalPersons()
        {
            return _context.LegalPersons.ToList();
        }
        public IEnumerable<LegalPerson> GetLegalPersons(int id)
        {
            return _context.LegalPersons.Where(p => p.EventId == id).ToList();
        }
        public void Add(LegalPerson legalPerson)
        {
            _context.LegalPersons.Add(legalPerson);
        }
        public LegalPerson Find(int id)
        {
            return _context.LegalPersons.Find(id);
        }
        public void Update(LegalPerson legalPerson)
        {
            _context.LegalPersons.Update(legalPerson);
        }
        public LegalPerson GetLegalPerson(int id, string registrycode)
        {
            return _context.LegalPersons.Where(p => p.RegistryCode == registrycode && p.EventId == id).FirstOrDefault();
        }
    }
}
