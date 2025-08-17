using Clinic_Manager.Core.Entities;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;

namespace Clinic_Manager.Core.Interface
{
    public interface IUserRepository
    {
        public Task<User> AddUserAsync(User user);
        public Task DeleteUserAsync(Guid id);
        public Task<PaginatedList<User>> GetAllUsersPaginatedAsync(int pageIndex, int pageSize);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task UpdateUserAsync(User user);
        public Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}
