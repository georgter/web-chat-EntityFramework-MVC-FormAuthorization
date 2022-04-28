using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebChat.BLL.DTOModels;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Services;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatLogService _logChatService;
        private readonly IUserService _userService;


        public HomeController()
        {
            _logChatService = new LogChatServices();
            _userService = new UserService();
        }



        /// <summary>
        /// Первоначальны запуск страници с проверкой каректности авторизированого пользователя 
        /// </summary>
        /// <param name="UserLogin"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            string UserLogin = User.Identity.Name;
            

            var avtoriUser = new UserDTO() { Login = UserLogin, };
            var modelUser = _userService.SersUser(avtoriUser);

            if (modelUser == null)
                return RedirectToAction("Login", "User");

            ViewBag.UserLogin = UserLogin;
            ViewBag.UserId = modelUser.Id;

            return View();
        }

        /// <summary>
        /// Запрос на отсортированый список сообщений по датам 
        /// </summary>
        /// <param name="dateOt"></param>
        /// <param name="dateDo"></param>
        /// <returns></returns>
        public JsonResult HistoriDate(DateTime? dateOt, DateTime? dateDo)
        {
            var logChatsRes = new List<LogChat>();
            var logChats = new List<LogChatDTO>();
            
            logChats = _logChatService.PrintPostSearchDate(dateOt, dateDo);

            foreach (var item in logChats)
            {
                logChatsRes.Add(new LogChat() { Id = item.Id, LogDate = item.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    LogMessage = item.LogMessage, UserId = item.UserId ,
                    Login = (_logChatService.SearchUserId(item.UserId)) });
            }
            return Json(new {usr = logChatsRes}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Запрос на отсортированый список сообщений по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public JsonResult HistoriLogin(string login)
        {
            var logChatsRes = new List<LogChat>();
            var logChats = new List<LogChatDTO>();
            
            logChats = _logChatService.PrintPostSearchUser(login);
            foreach (var item in logChats)
            {
                logChatsRes.Add(new LogChat()
                {
                    Id = item.Id,
                    LogDate = item.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    LogMessage = item.LogMessage,
                    UserId = item.UserId,
                    Login = (_logChatService.SearchUserId(item.UserId))
                });
            }
            return Json(new { usr = logChatsRes }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Вытаскивает всех пользователей для установки в фильтер
        /// </summary>
        /// <returns></returns>
        public JsonResult AllUser()
        {
            
            var users = _userService.GetAllDTOUser();
            return Json(new {usr =users}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Запрос на отсортированый список сообщений по логину и датам
        /// </summary>
        /// <param name="dateOt"></param>
        /// <param name="dateDo"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public JsonResult HistoriAll(DateTime? dateOt, DateTime? dateDo, string login)
        {
            var logChatsRes = new List<LogChat>();
            var logChats = new List<LogChatDTO>();
            
            logChats = _logChatService.PrintPostSearchDateUser(dateOt, dateDo,login);

            foreach (var item in logChats)
            {
                logChatsRes.Add(new LogChat() { Id = item.Id, LogDate = item.LogDate.ToString("dd.MM.yyyy HH:mm:ss"),
                    LogMessage = item.LogMessage, UserId = item.UserId,
                    Login = (_logChatService.SearchUserId(item.UserId))});
            }
            return Json(new { usr = logChatsRes }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Выход из приложения с очиской имени пользователя 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogUot()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

    }
}