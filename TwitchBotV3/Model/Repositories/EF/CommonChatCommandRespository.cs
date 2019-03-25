using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Model.ChatCommands;

namespace TwitchBotV3.Model.Repositories
{
    public class CommonChatCommandRespository: ICommonChatCommandRepository<CommonChatCommand>
    {
        public AppDbContext db;
        public CommonChatCommandRespository(AppDbContext _db)
        {
            db = _db;
        }
        public int Add(CommonChatCommand b)
        {
            db.CommonChatCommands.Add(b);
            int id = db.SaveChanges();
            return id;
        }
        public void Delete(int id)
        {
            var p = db.CommonChatCommands.FirstOrDefault(b => b.Id == id);
            if (p != null)
            {
                db.CommonChatCommands.Remove(p);
                db.SaveChanges();
            }
        }
        public void DeleteByName(string name)
        {
            var p = db.CommonChatCommands.FirstOrDefault(b => b.CommandName==name);
            if (p != null)
            {
                db.CommonChatCommands.Remove(p);
                db.SaveChanges();
            }
        }
        public CommonChatCommand Get(int id)
        {
            return db.CommonChatCommands.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<CommonChatCommand> GetAll()
        {
            return db.CommonChatCommands.ToList();
        }
        public void Update(CommonChatCommand b)
        {
            db.CommonChatCommands.Update(b);
        }
    }
}
