using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Model
{
    
        public class Person
        {
            public int Id { get; set; }
            public String Login { get; set; }
            public String Password { get; set; }
            public int RoleId { get; set; }
            public Role Role { get; set; }
            public String Name { get; set; }
            public DateTime? Date { get; set; }
        }
    
}
