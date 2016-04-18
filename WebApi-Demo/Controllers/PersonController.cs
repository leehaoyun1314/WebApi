using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi_Demo.Storages;

namespace WebApi_Demo.Controllers
{
    public class PersonController : ApiController
    {
        public static IList<Person> _list = new List<Person>() {
            new Person { ID=1, Name="刘备", Age=40 },
            new Person { ID=2, Name="张飞", Age=35 },
            new Person { ID=3, Name="关羽", Age=30 },
            new Person { ID=4, Name="赵云", Age=20 }
        };

        public IList<Person> Get()
        {
            return _list;
        }

        public Person Get(int id)
        {
            return _list.Where(p => p.ID == id).FirstOrDefault();
        }

        public void Post(Person person)
        {
            _list.Add(person);
        }

        public void Put(Person person)
        {
            _list.RemoveAt(person.ID - 1);
            _list.Insert(person.ID - 1, person);
        }

        public void Delete (int id)
        {
            _list.RemoveAt(id - 1);
        }
    }
}