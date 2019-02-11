using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Model.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        int Add(TEntity b);
        void Update(TEntity b);
        void Delete(int id);
    }
}
