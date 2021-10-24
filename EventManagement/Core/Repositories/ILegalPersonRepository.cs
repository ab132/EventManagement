using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Core.Repositories
{
    public interface ILegalPersonRepository
    {
        LegalPerson GetLegalPerson(int legalPersonId);
        IEnumerable<LegalPerson> GetLegalPersons();
        IEnumerable<LegalPerson> GetLegalPersons(int id);
        void Add(LegalPerson legalPerson);
        LegalPerson Find(int id);
        void Update(LegalPerson legalPerson);
        LegalPerson GetLegalPerson(int id, string registrycode);
    }
}
