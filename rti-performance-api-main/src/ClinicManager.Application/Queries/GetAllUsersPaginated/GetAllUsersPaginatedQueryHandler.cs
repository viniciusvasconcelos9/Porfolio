using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Application.Queries.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQueryHandler : IRequestHandler<GetAllUsersPaginatedQuery, ResponseBase<PaginatedList<User>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAllUsersPaginatedQueryHandler> _logger;

        public GetAllUsersPaginatedQueryHandler(IUserRepository userRepository, ILogger<GetAllUsersPaginatedQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<PaginatedList<User>>> Handle(GetAllUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<PaginatedList<User>>();

            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Handler - GetUsersPaginatedQueryHandler initiated.");
                var usersPaginated = await _userRepository.GetAllUsersPaginatedAsync(request.PageIndex, request.PageSize);

                response.Success = true;
                response.Data = usersPaginated;
                response.Message = "Users retrieved successfully.";
                _logger.LogInformation($"[{DateTime.Now}] Users retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving paginated users.");
                response.Success = false;
                response.Message = "An error occurred while retrieving users.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
