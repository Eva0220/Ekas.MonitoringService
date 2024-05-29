using System.Collections.Concurrent;
using Ekas.Monitoring.Models;

namespace Ekas.Monitoring.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationContext)
        {
            this.applicationDbContext = applicationContext;
        }
        public void AddChatId(long chatId)
        {
            User user = new();
            user.ChatId = chatId;
            applicationDbContext.Users.Add(user);
            applicationDbContext.SaveChanges();
        } 

        public List<long> GetChatIds() => applicationDbContext.Users.Select(c => c.ChatId).ToList();
    }
}
