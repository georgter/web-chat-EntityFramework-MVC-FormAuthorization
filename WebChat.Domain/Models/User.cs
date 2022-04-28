using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Interfaces;

namespace WebChat.Domain.Models
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }

    }
}
