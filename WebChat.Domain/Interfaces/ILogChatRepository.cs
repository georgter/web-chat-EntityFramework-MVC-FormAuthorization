using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Models;

namespace WebChat.Domain.Interfaces
{
    public interface ILogChatRepository
    {
        List<LogChat> SearchPostName(string name);
        List<LogChat> SearchPostDate(DateTime? dateTimeStart, DateTime? dateTimeEnd);
        List<LogChat> SearchPostDateAndName(string name, DateTime? dateTimeStart, DateTime? dateTimeEnd);
        List<LogChat> PrintAllPost();
        bool SearchUser(string Login);
        void Create(LogChat item);
    }
}
