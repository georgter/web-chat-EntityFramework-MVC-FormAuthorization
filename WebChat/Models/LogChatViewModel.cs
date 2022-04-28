using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Models
{
    public class LogChatViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDate { get; set; }
    }
}