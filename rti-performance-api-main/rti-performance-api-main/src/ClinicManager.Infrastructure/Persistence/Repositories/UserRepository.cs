using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - AddUserAsync() - {user.Login}");
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] User added successfully - {user.Login}");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error adding user - {user.Login}");
                throw;
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - DeleteUserAsync() - UserId: {id}");
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[{DateTime.Now}] User deleted successfully - UserId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] User not found - UserId: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error deleting user - UserId: {id}");
                throw;
            }
        }

        public async Task<PaginatedList<User>> GetAllUsersPaginatedAsync(int pageIndex, int pageSize)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetUsersPaginatedAsync() - PageIndex: {pageIndex}, PageSize: {pageSize}");

                var totalCount = await _context.Users.CountAsync();
                var users = await _context.Users
                                          .Skip((pageIndex - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync();

                _logger.LogInformation($"[{DateTime.Now}] Retrieved {users.Count} users successfully - PageIndex: {pageIndex}, PageSize: {pageSize}");

                return new PaginatedList<User>(users, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving users - PageIndex: {pageIndex}, PageSize: {pageSize}");
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetUserByIdAsync() - UserId: {id}");
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    _logger.LogInformation($"[{DateTime.Now}] User retrieved successfully - UserId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] User not found - UserId: {id}");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving user - UserId: {id}");
                throw;
            }
        }

        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetUserByLoginAndPasswordAsync() - Login: {login}");
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

                if (user == null)
                {
                    _logger.LogWarning($"[{DateTime.Now}] User not found - Login: {login}");
                    return null;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    _logger.LogWarning($"[{DateTime.Now}] Invalid password - Login: {login}");
                    return null;
                }

                _logger.LogInformation($"[{DateTime.Now}] User authenticated successfully - Login: {login}");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error authenticating user - Login: {login}");
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - UpdateUserAsync() - UserId: {user.Id}");
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    _logger.LogWarning($"[{DateTime.Now}] User not found - UserId: {user.Id}");
                    throw new ArgumentException($"Não foi possível encontrar o usuário com o ID {user.Id}");
                }

                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] User updated successfully - UserId: {user.Id}");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"[{DateTime.Now}] {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error updating user - UserId: {user.Id}");
                throw new Exception("Não foi possível atualizar o usuário!", ex);
            }
        }
    }
}
