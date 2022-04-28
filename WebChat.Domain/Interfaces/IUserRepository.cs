using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Models;

namespace WebChat.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool SearchLoginUser(string login);
        string SearchIdUser(int Userid);
        bool SearchUserLoginAndPass(string login, string pass);
        User SearchUserModel(string name);
        List<User> GetAllUsers();
        void Create(User item);
    }
}
