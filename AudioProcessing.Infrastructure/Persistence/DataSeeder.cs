using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using Microsoft.EntityFrameworkCore;

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

                //var chat = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

                //chat.AddChatResponceOnText("sdfsdf");

                //await context.Chats.AddAsync(chat);
                //await context.SaveChangesAsync();


                //var chat1 = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

                //chat1.AddChatResponceOnText("sdfsdf");

                //await context.Chats.AddAsync(chat1);
                //await context.SaveChangesAsync();



                for (int i = 0; i <= 100; i++)
                {
                    var chat = Chat.Create(userId, $"Chat about AI {Guid.NewGuid()}");

                    for (int j = 0; j <= 10; j++)
                    {
                        chat.AddChatResponceOnText($" Response from chatId: {chat.Id.Value}, with value: content {Guid.NewGuid()}");
                    }

                    chats.Add(chat);

                    //await context.Chats.AddAsync(chat);
                    //await context.SaveChangesAsync();
                }


                await context.Chats.AddRangeAsync(chats);
                await context.SaveChangesAsync();

                //foreach (var chat in chats)
                //{
                //    var invalidResponses = chat.ChatResponces
                //        .Where(response => response.PromtType == null)
                //        .ToList();

                //    foreach (var invalidResponse in invalidResponses)
                //    {
                //        chat.ChatResponces.Remove(invalidResponse);
                //    }
                //}

                //await context.Chats.AddRangeAsync(chats);
                //await context.SaveChangesAsync();
            }
        }
    }
}