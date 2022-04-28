using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebChat.BLL.DTOModels;
using WebChat.BLL.Services;
using WebChat.BLL.Interfaces;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        /// <summary>
        /// Стартовый запуск страници 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Обрабатка нажатия 
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        
        public JsonResult Login(UsersViewModel userViewModel)
        {
            
            var userDTO = new UserDTO() { Login = userViewModel.Login, Pass = userViewModel.Pass };
            var isValid = _userService.AvtorizeitUser(userDTO);

            //Записываем логин в куки 
            if (isValid.ResultData)
            {
                string cookeUerPas = userDTO.Login;
                FormsAuthentication.SetAuthCookie(cookeUerPas, true);
            }
            return Json(new { result = isValid }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Стартовый запуск страници 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Registred()
        {
            return View();
        }
        /// <summary>
        /// Обрабатка нажатия 
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Registred(UsersViewModel userViewModel)
        {
            

            var userDTO = new UserDTO() { Login = userViewModel.Login, Pass = userViewModel.Pass};

            var isValid = _userService.RegistredUser(userDTO);
            if (isValid.ResultData)
                FormsAuthentication.SetAuthCookie(userDTO.Login, true);
            return Json(new { result = isValid }, JsonRequestBehavior.AllowGet);
        }

        
        
    }
}