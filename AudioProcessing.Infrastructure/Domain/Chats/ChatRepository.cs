using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using AudioProcessing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.PgOutput;


namespace AudioProcessing.Infrastructure.Domain.Chats
{
    public class ChatRepository(
        ApplicationDbContext dbContext) : IChatRepository
    {
        public async Task<Chat> GetByIdAsync(ChatId chatId, CancellationToken cancellationToken = default)
        {
            return await dbContext.Chats
                .Include (chat => chat.ChatResponces)  
                .FirstOrDefaultAsync(chat => chat.Id == chatId, cancellationToken);
        }

        public async Task<List<Chat>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            return await dbContext.Chats
                .Where(chat => chat.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Chat>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Chats
                .Include(chat => chat.ChatResponces)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Chat chat, CancellationToken cancellationToken = default)
        {
            await dbContext.Chats.AddAsync(chat, cancellationToken);
        }

        public Task UpdateAsync(Chat chat, CancellationToken cancellationToken = default)
        {
            dbContext.Chats.Update(chat);

            return Task.CompletedTask;
        }

        public async Task DeleteAsync(ChatId chatId, CancellationToken cancellationToken = default)
        {
            var chat = await GetByIdAsync(chatId, cancellationToken);
            if (chat != null)
            {
                dbContext.Chats.Remove(chat);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
