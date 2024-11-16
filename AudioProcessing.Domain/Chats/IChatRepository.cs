using AudioProcessing.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats
{
    public interface IChatRepository 
    {
        Task<Chat> GetByIdAsync(ChatId chatId, CancellationToken cancellationToken = default);
        Task<List<Chat>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Chat>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
        Task AddAsync(Chat chat, CancellationToken cancellationToken = default);
        Task UpdateAsync(Chat chat, CancellationToken cancellationToken = default);
        Task DeleteAsync(ChatId chatId, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
