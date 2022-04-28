using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebChat.Domain.Models;
using System.Data.Entity.Infrastructure;

namespace WebChat.Domain.Context
{
    public class ChatDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LogChat> LogChats { get; set; }


        public ChatDbContext() : base("DBCONNECT")
        {
            Database.SetInitializer<ChatDbContext>(null);
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = 12000;
        }
    }
}
