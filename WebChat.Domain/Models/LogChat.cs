using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Interfaces;
namespace WebChat.Domain.Models
{
    public class LogChat: IEntity<int>
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDate { get; set; }
        public User User { get; set; }

    }
}
