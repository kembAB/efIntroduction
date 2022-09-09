using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public AjaxController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            bool status = _personRepository.Delete(id);

            if(status)
                return Ok("Person with ID: " + id + " was deleted");
            else
                return Content("Person with ID: " + id + " not found! Person was not deleted!");
        }

        [HttpGet]
        public IActionResult GetPeople()
        {
            IEnumerable<Person> PersonList = _personRepository.GetAllPersons();

            return PartialView("_partialPersonListView", PersonList);
        }

        [HttpPost]
        public IActionResult GetPerson(int id)
        {
            Person person = _personRepository.GetPerson(id);

            if (person != null)
            {
                List<Person> PersonList = new List<Person>();
                PersonList.Add(person);
                return PartialView("_partialPersonListView", PersonList);
            }

            return Content("Person with ID: " + id + " not found! No Details to show!");
        }
    }
}
