using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwitchBotV3.Model;
using TwitchBotV3.Model.Repositories;

namespace TwitchBotV3.Services.Auth
{
    public class JwtAuthService: IJwtAuthService
    {
        private AppDbContext db;
        private IConfiguration config;
        public JwtAuthService(AppDbContext _db, IConfiguration configuration)
        {
            db = _db;
            config = configuration;
        }
        public async Task<String> GetToken(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return null;
            }
            var jwt = new JwtSecurityToken(
                    issuer: config["JwtToken:Issuer"],
                    audience: config["JwtToken:Audience"],
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(config["JwtToken:LifeTime"]))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtToken:Key"])), SecurityAlgorithms.HmacSha256)
                    );
            var response = new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                UserName = identity.Name,
                Roles = identity.Claims
                 .Where(c => c.Type == ClaimTypes.Role)
                 .Select(c => c.Value)
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
                Person person = await db.Persons.Include(p => p.Role).AsNoTracking().FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
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
}
