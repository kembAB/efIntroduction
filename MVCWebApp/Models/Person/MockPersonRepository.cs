using MVCWebApp.Data;
using MVCWebApp.Models.Person.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person
{
    public class PersonRepository : IPersonRepository
    {
        //private List<Person> _people;
        private readonly ApplicationDbContext _context;
        //public PersonRepository()
        //{
        //    _people = new List<Person>()
        //    {
        //        new Person { ID = 1, Name = "Abel Magicho", City = "gothenburg", PhoneNumber = "0743675431" }),
        //    new Person { ID = 2, Name = "Josefin  Larsson", City = "Stockholm", PhoneNumber = "0743345434" }),
        //    new Person { ID = 3, Name = "yonas  walters", City = "dc", PhoneNumber = "0143345444" })
        //};
    
    
        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Person> GetAllPersons()
        {
            return _context.People.ToList();
        }

        public Person GetPerson(int id)
        {
            return _context.People.Find(id);
        }

        public List<Person> Search(string searchTerm, bool caseSensitive)
        {
            List<Person> searchList = new List<Person>();

            if (searchTerm != null)
            {
                if (caseSensitive)
                {
                    IEnumerable<Person> searchList2 = from Person in _context.People
                                                      where Person.Name.Contains(searchTerm) || Person.City.Contains(searchTerm)
                                                      select Person;

                    //cheat case sensitive
                    foreach (Person item in searchList2)
                    {
                        if (item.Name.Contains(searchTerm) || item.City.Contains(searchTerm))
                        {
                            searchList.Add(item);
                        }
                    }
                }
                else
                {
                    searchList = _context.People.Where(p => p.City.Contains(searchTerm) ||
                                                    p.Name.Contains(searchTerm)).ToList();
                }
            }

            return searchList;
        }

        public List<Person> Sort(SortOptionsViewModel sortOptions, string sortType)
        {
            //default by ID
            List<Person> sortedList = _context.People.ToList();

            if (sortType == "city")
            {
                sortedList = _context.People.OrderBy(p => p.City).ToList();
            }
            else if(sortType == "name")
            {
                sortedList = _context.People.OrderBy(p => p.Name).ToList();
            }

            if (sortOptions.ReverseAplhabeticalOrder == true)
            {
                sortedList.Reverse();
            }

            return sortedList;
        }

        public Person Add(CreatePersonViewModel createPersonViewModel)
        {
            Person person = new Person();
            person.Name = createPersonViewModel.Name;
            person.City = createPersonViewModel.City;
            person.PhoneNumber = createPersonViewModel.PhoneNumber;

            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }

        public bool Delete(int id)
        {
           // Person person =_people
        //optional 
            if (id > 0)
            {
                var personToDelete = _context.People.Find(id);

                if (personToDelete != null)
                {
                    _context.People.Remove(personToDelete);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Person Update(Person personChanges)
        {
            throw new NotImplementedException();
        }
        //implementation for update person 
        //public Person Update(Person personChanges)
        //{

        //    throw new NotImplementedException();
        //}
    }
}