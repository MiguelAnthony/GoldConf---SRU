using GoldConf.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoldConf.Repository
{
    public interface IAuthRepository
    {
        User GetUsuario(string username, string password);
        void SaveUsuario(User user);
        List<User> GetUsuarios();
    }
    public class AuthRepository: IAuthRepository
    {
        private readonly IGoldConfContext context;
        private readonly IConfiguration configuration;

        public AuthRepository(IGoldConfContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public User GetUsuario(string username, string password)
        {
            return context.Users.Where(o => o.Username == username && o.Password == CreateHash(password)).FirstOrDefault();
        }

        public List<User> GetUsuarios()
        {
            return context.Users.ToList();
        }

        public void SaveUsuario(User user)
        {
            user.Password = CreateHash(user.Password);
            context.Users.Add(user);
            context.SaveChanges();
        }

        protected string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
