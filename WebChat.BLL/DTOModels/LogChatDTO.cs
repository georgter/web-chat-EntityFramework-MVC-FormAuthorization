using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.BLL.DTOModels
{
    public class LogChatDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDate { get; set; }
    }
}
