using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace k8s_demo.Controllers
{
    [Route("persons")]
    public class PersonsController : Controller
    {
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return new Person
            {
                Name = "wawa",
                Id = id
            };
        }

        [HttpGet("")]
        public List<Person> Get()
        {
            return new List<Person>()
            {
                new Person
                {
                   Name = "wawa",Id = 1
                },
                 new Person
                {
                   Name = "sally",Id = 2
                }
            };
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}