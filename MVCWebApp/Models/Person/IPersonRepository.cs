using MVCWebApp.Models.Person.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person
{
    //interface for CRUD operations and more...
    public interface IPersonRepository
    {
        public List<Person> GetAllPersons();
        public Person GetPerson(int id);
        public Person Add(CreatePersonViewModel createPersonViewModel);
        public bool Delete(int id);
        public List<Person> Search(string searchTerm, bool caseSensitive);
        public List<Person> Sort(SortOptionsViewModel sortOptions, string sortType);
        //update ...
        //public Person Update(Person personChanges);
    }
}
