using MVCWebApp.Models.Person;
using MVCWebApp.Models.Person.ViewModels;
using MVCWebApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace MVCWebApp.Models
{
    public class SQLpersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext context;
        //ctor injects DBContext
        public SQLpersonRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Person.Person Add(CreatePersonViewModel createPersonViewModel)
        {
            Person.Person person = new Person.Person();
            person.Name = createPersonViewModel.Name;
            person.City = createPersonViewModel.City;
            person.PhoneNumber = createPersonViewModel.PhoneNumber;

            context.People.Add(person);
            context.SaveChanges();

            return person;
            //throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            if (id > 0)
            {
                var personToDelete = context.People.Find(id);

                if (personToDelete != null)
                {
                    context.People.Remove(personToDelete);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
            //throw new System.NotImplementedException();
        }

        public List<Person.Person> GetAllPersons()
        {
            return context.People.ToList();
        }

        public Person.Person GetPerson(int id)
        {
            return context.People.Find(id);
        }

        public List<Person.Person> Search(string searchTerm, bool caseSensitive)
        {
            List<Person.Person> searchList = new List<Person.Person>();

            if (searchTerm != null)
            {
                if (caseSensitive)
                {
                    IEnumerable<Person.Person> searchList2 = from Person in context.People
                                                      where Person.Name.Contains(searchTerm) || Person.City.Contains(searchTerm)
                                                      select Person;

                    //cheat case sensitive
                    foreach (Person.Person item in searchList2)
                    {
                        if (item.Name.Contains(searchTerm) || item.City.Contains(searchTerm))
                        {
                            searchList.Add(item);
                        }
                    }
                }
                else
                {
                    searchList = context.People.Where(p => p.City.Contains(searchTerm) ||
                                                    p.Name.Contains(searchTerm)).ToList();
                }
            }

            return searchList;
            throw new System.NotImplementedException();
        }

        public List<Person.Person> Sort(SortOptionsViewModel sortOptions, string sortType)
        {
            //default by ID
            List<Person.Person> sortedList = context.People.ToList();

            if (sortType == "city")
            {
                sortedList = context.People.OrderBy(p => p.City).ToList();
            }
            else if (sortType == "name")
            {
                sortedList = context.People.OrderBy(p => p.Name).ToList();
            }

            if (sortOptions.ReverseAplhabeticalOrder == true)
            {
                sortedList.Reverse();
            }

            return sortedList;
           // throw new System.NotImplementedException();
        }

        //public Person.Person Update(Person.Person personChanges)
        //{
        //    Person.Person person = context.Find(p => p.id == personChanges.ID);
        //        if(person != null)
        //    {
        //        person.Name = person.Name;
        //        person.City = person.City;
        //        person.PhoneNumber = person.PhoneNumber;

        //    }
        //    return person;
        //    //throw new System.NotImplementedException();
        //}
    }
}
