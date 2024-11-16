using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using Microsoft.EntityFrameworkCore;
using AudioProcessing.Infrastructure.Persistence;

namespace AudioProcessing.Infrastructure.Domain.Users
{
    public class UserRepository(
        ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<User> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await dbContext.Users.AddAsync(user, cancellationToken);
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            dbContext.Users.Update(user);

            return Task.CompletedTask;
        }

        public async Task DeleteAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(userId, cancellationToken);

            if (user != null)
                dbContext.Users.Remove(user);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
