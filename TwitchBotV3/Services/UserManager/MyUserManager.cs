using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TwitchBotApiV_2.Model;


namespace TwitchBotApiV_2.Services.UserManager
{
    public class MyUserManager
    {
        public MyUserManager()
        {

        }
        public Person RegisterNewPerson(String name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Person pers = db.Persons.Include(p => p.Role).FirstOrDefault(x => x.Login == name);
                if (pers == null)
                {
                    Person newUser = new Person
                    {
                        Name = name,
                        Date = DateTime.Now,
                        Login = name,
                        Password = CreatePassword(20),
                        RoleId = 2
                    };
                    db.Persons.Add(newUser);
                    db.SaveChanges();
                    return newUser;
                }
                else
                {
                    return pers;
                }
            }          
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            using(ApplicationDbContext db= new ApplicationDbContext())
            {
                Person person = db.Persons.Include(p => p.Role).FirstOrDefault(x => x.Login == username && x.Password == password);
                if (person != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.RoleName),

                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
                return null;
            }           
        }
        private string CreatePassword(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                int numb = Convert.ToInt32(Math.Floor(52 * random.NextDouble()));
                numb += (numb < 27) ? 65 : 70;
                ch = Convert.ToChar(numb);
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
