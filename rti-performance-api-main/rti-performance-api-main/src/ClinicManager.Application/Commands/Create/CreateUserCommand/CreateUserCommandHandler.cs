using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Application.Commands.Create.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseBase<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Guid>();

            try
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] Handler - Creating user with login: {request.Login}");

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Login = request.Login,
                    PasswordHash = HashPassword(request.Password),
                    AccessRule = request.AccessRule,
                    CreatedAt = DateTime.UtcNow,
                };

                await _userRepository.AddUserAsync(user);
                await _unitOfWork.CommitAsync();

                response.Success = true;
                response.Data = user.Id;
                response.Message = "Usuario criado com sucesso.";

                _logger.LogInformation($"[{DateTime.UtcNow}] Handler - User created successfully with ID: {user.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.UtcNow}] Handler - Error creating user with login: {request.Login}");
                response.Success = false;
                response.Message = "Erro ao criar cliente.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
