using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Model.Repositories
{
    public class PersonRepository : IPersonRepository<Person>
    {
        public AppDbContext db;
        public PersonRepository(AppDbContext _db)
        {
            db = _db;
        }
        public int Add(Person b)
        {
            db.Persons.Add(b);
            int id = db.SaveChanges();
            return id;
        }
        public void Delete(int id)
        {
            var person = db.Persons.FirstOrDefault(b => b.Id == id);
            if (person != null)
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
        }
        public Person Get(int id)
        {
            return db.Persons.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public Person GetPerson(string login, string pass)
        {
            return db.Persons.Include(p => p.Role).AsNoTracking().FirstOrDefault(x => x.Login == login && x.Password == pass);
        }
        public IEnumerable<Person> GetAll()
        {
            return db.Persons.ToList();
        }
        public void Update(Person b)
        {
            db.Persons.Update(b);
        }

    }
}
