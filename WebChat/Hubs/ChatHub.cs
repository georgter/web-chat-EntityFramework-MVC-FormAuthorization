using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebChat.BLL.DTOModels;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Services;
using WebChat.Models;

namespace WebChat.Hubs
{
    public class ChatHub:Hub
    {
        private IChatLogService _chatLogService;

        public ChatHub()
        {
            _chatLogService = new LogChatServices();
        }
        //Для временого отображения пользователя на странице
        static List<User> Users = new List<User>();

        /// <summary>
        /// Отправка сообщений 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="idUser"></param>
        public void Send(string name , string message ,int idUser)
        {
            var date = DateTime.Now;
            var newMessage = new LogChatDTO() { LogDate = date, LogMessage = message, UserId = idUser};
            //Запись в базу
            _chatLogService.AddPost(newMessage);
            // Печать на экране
            Clients.All.addMessage(name, message + "~" + date.ToString("dd.MM.yyyy HH:mm:ss"));
        }
        /// <summary>
        /// Подключения нового пользователя
        /// </summary>
        /// <param name="userName"></param>
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId ==id))
            {
                Users.Add(new User{ConnectionId = id, Login = userName});

                //Посылаем сообщения текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                //Посылаем сообщения всем кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
        /// <summary>
        /// Дисконект пользователя
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if(item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Login);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}