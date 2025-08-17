using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - AddClientAsync() - {client.Id}");
                var conste = _context.Clients.Add(client);

                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] Client added successfully - {client.Id}");
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error adding client - {client.Id}");
                throw;
            }
        }

        public async Task DeleteClientAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - DeleteClientAsync() - ClientId: {id}");
                var client = await _context.Clients.FindAsync(id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[{DateTime.Now}] Client deleted successfully - ClientId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Client not found - ClientId: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error deleting client - ClientId: {id}");
                throw;
            }
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllClientsAsync()");
                var clients = await _context.Clients.ToListAsync();
                _logger.LogInformation($"[{DateTime.Now}] Retrieved {clients.Count} clients successfully");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving clients");
                throw;
            }
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetClientByIdAsync() - ClientId: {id}");
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
                if (client != null)
                {
                    _logger.LogInformation($"[{DateTime.Now}] Client retrieved successfully - ClientId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Client not found - ClientId: {id}");
                }
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving client - ClientId: {id}");
                throw;
            }
        }

        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - UpdateClientAsync() - ClientId: {client.Id}");
                var existingClient = await _context.Clients.FindAsync(client.Id);
                if (existingClient == null)
                {
                    _logger.LogWarning($"[{DateTime.Now}] Client not found - ClientId: {client.Id}");
                    throw new ArgumentException($"Não foi possível encontrar o cliente com o ID {client.Id}");
                }

                _context.Entry(existingClient).CurrentValues.SetValues(client);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] Client updated successfully - ClientId: {client.Id}");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"[{DateTime.Now}] {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error updating client - ClientId: {client.Id}");
                throw new Exception("Não foi possível atualizar o cliente!", ex);
            }
        }

        public async Task<PaginatedList<Client>> GetAllClientsPaginatedAsync(
    int pageIndex,
    int pageSize,
    string searchTerm = null,
    bool? Active = null)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllClientsPaginatedAsync() - PageIndex: {pageIndex}, PageSize: {pageSize}, SearchTerm: {searchTerm}, Active: {Active}");

                if (pageIndex < 1) pageIndex = 1;
                searchTerm = searchTerm?.Trim();

                var query = _context.Clients.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
                }

                if (Active.HasValue)
                {
                    query = query.Where(c => c.Active == Active.Value);
                }

                query = query.OrderBy(c => c.Name);

                var totalCount = await query.CountAsync();
                var clients = await query
                                      .Skip((pageIndex - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

                _logger.LogInformation($"[{DateTime.Now}] Retrieved {clients.Count} clients successfully - PageIndex: {pageIndex}, PageSize: {pageSize}, SearchTerm: {searchTerm}, Active: {Active}");

                return new PaginatedList<Client>(clients, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving clients - PageIndex: {pageIndex}, PageSize: {pageSize}, SearchTerm: {searchTerm}, Active: {Active}");
                throw;
            }
        }


    }
}