using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BLL.DTOModels;

namespace WebChat.BLL.Interfaces
{
    public interface IChatLogService
    {
        int AddPost(LogChatDTO logChatDTO);
        List<LogChatDTO> PrintAllPost();
        List<LogChatDTO> PrintPostSearchDate(DateTime? dateTimeStart, DateTime? dateTimeEnd);
        List<LogChatDTO> PrintPostSearchDateUser(DateTime? dateTimeStart, DateTime? dateTimeEnd, string login);
        List<LogChatDTO> PrintPostSearchUser(string login);
        string SearchUserId(int Userid);
    }
}
