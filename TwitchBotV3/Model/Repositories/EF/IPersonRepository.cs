using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Model.Repositories
{
    public interface IPersonRepository<T>:IEntityRepository<T> 
        where T:class
    {
        T GetPerson(string loging, string pass);
    }
}
