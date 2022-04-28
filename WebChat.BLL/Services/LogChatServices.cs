

using System;
using System.Collections.Generic;
using WebChat.BLL.DTOModels;
using WebChat.BLL.Interfaces;
using WebChat.Domain.Interfaces;
using WebChat.Domain.Models;
using WebChat.Domain.Repositories;

namespace WebChat.BLL.Services
{
    public class LogChatServices : IChatLogService
    {
        private readonly ILogChatRepository _logChatRepository;

        public LogChatServices()
        {
            _logChatRepository = new LogChatRepository();
        }
        
        /// <summary>
        /// Передача данных в Bll на запись в базу
        /// </summary>
        /// <param name="logChatDTO"></param>
        /// <returns></returns>
        public int AddPost(LogChatDTO logChatDTO)
        {
            if (logChatDTO == null)
            {
                return 2; //Все поля должны быть заполнины
            }
            else
            {
                var logChat = new LogChat() {UserId = logChatDTO.UserId, LogMessage = logChatDTO.LogMessage , LogDate = logChatDTO.LogDate};
                _logChatRepository.Create(logChat);
                return 1; // Все отлично 
            }
        }
        /// <summary>
        /// Вывод всех постов 
        /// </summary>
        /// <returns></returns>
        public List<LogChatDTO> PrintAllPost()
        {
            var logChatDTO = new List<LogChatDTO>();

            var logChatAll = _logChatRepository.PrintAllPost();

            foreach (var items in logChatAll)
            {
                var itemsDTO = new LogChatDTO { UserId = items.UserId, LogDate = items.LogDate, LogMessage = items.LogMessage };

                logChatDTO.Add(itemsDTO);
            }

            return logChatDTO;
        }

        /// <summary>
        /// Вывод всех постов  отсортированых по дате 
        /// </summary>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <returns></returns>
        public List<LogChatDTO> PrintPostSearchDate(DateTime? dateTimeStart, DateTime? dateTimeEnd )
        {
            var logChatDTO = new List<LogChatDTO>();
            
            if (dateTimeStart == null && dateTimeEnd == null)
            {
                dateTimeStart = DateTime.MinValue;
                dateTimeEnd = DateTime.Now;
            }
            else if(dateTimeEnd == null)
            {
                dateTimeEnd = DateTime.Now;

            }else if (dateTimeStart == null)
            {
                dateTimeStart = DateTime.MinValue;
            }else if (dateTimeStart> dateTimeEnd)
            {
                return logChatDTO;
            }


            var logChatAll = _logChatRepository.SearchPostDate(dateTimeStart,  dateTimeEnd);

            foreach (var items in logChatAll)
            {
                var itemsDTO = new LogChatDTO { UserId = items.UserId, LogDate = items.LogDate, LogMessage = items.LogMessage };

                logChatDTO.Add(itemsDTO);
            }

            return logChatDTO;
        }

        /// <summary>
        /// фильтрация комбо по имени и дате 
        /// </summary>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public List<LogChatDTO> PrintPostSearchDateUser(DateTime? dateTimeStart, DateTime? dateTimeEnd, string login)
        {
            List<LogChatDTO> logChatDTO = new List<LogChatDTO>();
            
            if (dateTimeStart == null && dateTimeEnd == null)
            {
                dateTimeStart = DateTime.MinValue;
                dateTimeEnd = DateTime.Now;
            }
            else if (dateTimeEnd == null)
            {
                dateTimeEnd = DateTime.Now;

            }
            else if (dateTimeStart == null)
            {
                dateTimeStart = DateTime.MinValue;
            }
            else if (dateTimeStart > dateTimeEnd)
            {
                return logChatDTO;
            }


            var logChatAll = _logChatRepository.SearchPostDateAndName(login,dateTimeStart, dateTimeEnd);

            foreach (var items in logChatAll)
            {
                var itemsDTO = new LogChatDTO { UserId = items.UserId, LogDate = items.LogDate, LogMessage = items.LogMessage };

                logChatDTO.Add(itemsDTO);
            }

            return logChatDTO;
        }


        /// <summary>
        /// Вывод всех постов  отсортированых по имени
        /// </summary>
        /// <param name="name"></param>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public List<LogChatDTO> PrintPostSearchUser(string login)
        {
            List<LogChatDTO> logChatDTO = new List<LogChatDTO>();
            if (login == null && _logChatRepository.SearchUser(login))
            {
                return logChatDTO;
            }

            var logChatAll = _logChatRepository.SearchPostName(login);

            foreach (var items in logChatAll)
            {
                var itemsDTO = new LogChatDTO { UserId = items.UserId, LogDate = items.LogDate, LogMessage = items.LogMessage };

                logChatDTO.Add(itemsDTO);
            }

            return logChatDTO;
        }


        /// <summary>
        /// Возрощения имини Usera по id
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public string SearchUserId(int Userid)
        {
            UserRepository userRe = new UserRepository();

            return userRe.SearchIdUser(Userid);
        }
    }
}
