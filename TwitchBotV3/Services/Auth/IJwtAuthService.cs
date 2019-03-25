using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Services.Auth
{
    public interface IJwtAuthService
    {
        String GetToken(string username, string password);
    }
}
