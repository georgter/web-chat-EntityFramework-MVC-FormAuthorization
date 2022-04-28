using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Context;
using WebChat.Domain.Interfaces;
using WebChat.Domain.Models;

namespace WebChat.Domain.Repositories
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        private ChatDbContext dbContext;

        public UserRepository()
        {
            dbContext = new ChatDbContext();
        }

        /// <summary>
        /// Запись пользователя в базу
        /// </summary>
        /// <param name="item"></param>
        public void Create(User item)
        {
            
            dbContext.Users.Add(item);
            dbContext.SaveChanges();
            
        }
        /// <summary>
        /// Поиск пользователя по имени 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool SearchLoginUser(string login) => dbContext.Users.Any(w => w.Login == login);
        
        /// <summary>
        /// Поиск пользователя по id
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public string SearchIdUser(int Userid)
        {
            var Search = dbContext.Users.FirstOrDefault(x => x.Id == Userid);
            return Search.Login;
        }

        /// <summary>
        /// Поиск пользователя по логину и поролю
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool SearchUserLoginAndPass(string login, string pass) => dbContext.Users.Any(x => x.Login == login && x.Pass == pass);
        
        /// <summary>
        /// Поиск пользователя по логину и поролю и возрощения модели
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public User SearchUserModel(string name)
        {
            var  Search = dbContext.Users.FirstOrDefault(x => x.Login == name);
            return Search;
        }
        /// <summary>
        /// Получения всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            users = dbContext.Users.ToList();
            return users;
        }
    }
}
