
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.BLL.DTOModels;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Utilities;
using WebChat.Domain.Interfaces;
using WebChat.Domain.Models;
using WebChat.Domain.Repositories;

namespace WebChat.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        private Hash hash = new Hash(); 

        
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public Result<bool> RegistredUser(UserDTO userDTO)
        {
            var errors = new StringBuilder();

            if (userDTO == null || userDTO.Login == null || userDTO.Login.Trim() == "")
            {
                errors.Append("Все поля должны быть заполнины \n");
            }
            if (_userRepository.SearchLoginUser(userDTO.Login))
            {
                errors.Append("Такой пользователь уже есть \n");
            }
            var errorMessage = errors.ToString();
            if (!string.IsNullOrEmpty(errorMessage))
                return new Result<bool> { IsSuccess = false, Message = errorMessage };

            try
            {

                var user = new User { Login = userDTO.Login, Pass = hash.GetHashPasvord(userDTO.Pass) };
                _userRepository.Create(user);
                return new Result<bool>
                {
                    IsSuccess = true,
                    Message = "",
                    ResultData = true
                };
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        
        public Result<bool> AvtorizeitUser(UserDTO userDTO)
        {

            var errors = new StringBuilder();
            
            if (userDTO == null || userDTO.Login == null || userDTO.Pass == null)
            {
                errors.Append("Все поля должны быть заполнины \n");
                var errorMessage = errors.ToString();
                return new Result<bool> { IsSuccess = false, Message = errorMessage };

            }

            if (!_userRepository.SearchUserLoginAndPass(userDTO.Login, hash.GetHashPasvord(userDTO.Pass)))
            {
                errors.Append("Такого пользователя нет \n");
                var errorMessage = errors.ToString();
                return new Result<bool> { IsSuccess = false, Message = errorMessage };
            }
           
            try
            {
                return new Result<bool>
                {
                    IsSuccess = true,
                    Message = "",
                    ResultData = true
                };
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Проверка есть ли такой пользователь
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public User SersUser(UserDTO userDTO) => _userRepository.SearchUserModel(userDTO.Login);
        
        /// <summary>
        /// Возращает всь список пользователей
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAllDTOUser()
        {
            var userDTOList = new List<UserDTO>();
            var userRepository = new UserRepository();
            var users = new List<User>();
            users = userRepository.GetAllUsers();
            foreach (var user in users)
            {
                UserDTO userDTO = new UserDTO() { Id = user.Id, Login = user.Login };
                userDTOList.Add(userDTO);
            }

            return userDTOList;
        }
    }
}
