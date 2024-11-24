using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;

namespace AudioProcessing.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Set<Chat>().Any())
            {
                var userId = new UserId(Guid.NewGuid());

                var chats = new List<Chat>();

                for (int i = 0; i <= 100; i++)
                {
                    var chat = Chat.Create(userId, $"Chat about AI {Guid.NewGuid()}");

                    for (int j = 0; j <= 10; j++)
                    {
                        chat.AddChatResponceOnText($" Response from chatId: {chat.Id.Value}, with value: content {Guid.NewGuid()}");
                    }

                    chats.Add(chat);
                }

                await context.Chats.AddRangeAsync(chats);
                await context.SaveChangesAsync();
            }
        }
    }
}