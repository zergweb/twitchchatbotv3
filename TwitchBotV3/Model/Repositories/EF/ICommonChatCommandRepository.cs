using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Model.Repositories
{
    public interface ICommonChatCommandRepository<T>: IEntityRepository<T>
        where T : class
    {
        void DeleteByName(string name);
    }
}
