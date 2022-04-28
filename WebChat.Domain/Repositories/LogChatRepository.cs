using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebChat.Domain.Context;
using WebChat.Domain.Interfaces;
using WebChat.Domain.Models;

namespace WebChat.Domain.Repositories
{
    public class LogChatRepository : IRepository<LogChat>, ILogChatRepository
    {
        private ChatDbContext dbContext;

        public LogChatRepository()
        {
            dbContext = new ChatDbContext();
        }

        /// <summary>
        /// Запись в базу нового сообщения 
        /// </summary>
        /// <param name="item"></param>
        public void Create(LogChat item)
        {
            dbContext.LogChats.Add(item);
            dbContext.SaveChanges();
        }
        /// <summary>
        /// Фильрация по пользователям 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<LogChat> SearchPostName(string name)
        {
            var LogChatName = new List<LogChat>();
            
            var UserId = dbContext.Users.FirstOrDefault(x => x.Login == name);
            if (name != null)
            {
                LogChatName = dbContext.LogChats.Where(x => x.UserId == UserId.Id).ToList();
            }
            return LogChatName;
        }
        /// <summary>
        /// Фильтрация по датам 
        /// </summary>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <returns></returns>
        public List<LogChat> SearchPostDate(DateTime? dateTimeStart, DateTime? dateTimeEnd) => dbContext.LogChats
            .Where(x => x.LogDate >= dateTimeStart && x.LogDate <= dateTimeEnd)
            .ToList();
        
        /// <summary>
        /// Фильраци и по дате и по пользовутилю
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <returns></returns>
        public List<LogChat> SearchPostDateAndName(string name,DateTime? dateTimeStart, DateTime? dateTimeEnd)
        {
            var LogChatName = new List<LogChat>();
            if (name != null)
            {
                var UserId = dbContext.Users.FirstOrDefault(x => x.Login == name);
                LogChatName = dbContext.LogChats.Where(x => (x.LogDate >= dateTimeStart && x.LogDate <= dateTimeEnd) && x.UserId == UserId.Id).ToList();
            }
            return LogChatName;
        }
        /// <summary>
        /// Вывод всех сообщений из базы 
        /// </summary>
        /// <returns></returns>
        public List<LogChat> PrintAllPost() => dbContext.LogChats.ToList();

        /// <summary>
        /// Поиск есть ли такой пользователь 
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public bool SearchUser(string Login)
        {
            var userRepository = new UserRepository();
            return userRepository.SearchLoginUser(Login);
        }
    }
}
