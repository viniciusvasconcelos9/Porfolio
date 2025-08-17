using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicManager.Application.Commands.Create.CreateLoginCommand
{
    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, ResponseBase<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateLoginCommandHandler> _logger;
        private readonly IConfiguration _configuration;

        public CreateLoginCommandHandler(
            IUserRepository userRepository,  
            ILogger<CreateLoginCommandHandler> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ResponseBase<string>> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<string>();

            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Handler - Login service initiated for user: {request.Login}");

                var user = await _userRepository.GetUserByLoginAndPasswordAsync(request.Login, request.Password);
                if (user == null)
                {
                    _logger.LogWarning($"[{DateTime.Now}] Invalid login attempt - User: {request.Login}");
                    response.Success = false;
                    response.Message = "Invalid login or password.";
                    return response;
                }

                var token = GenerateJwtToken(user);
                response.Success = true;
                response.Data = token;
                response.Message = "Login successful.";

                _logger.LogInformation($"[{DateTime.Now}] User authenticated successfully - User: {request.Login}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error during login process for user: {request.Login}");
                response.Success = false;
                response.Message = "An error occurred during the login process.";
            }

            return response;
        }

        private string GenerateJwtToken(User user)
        {
            string chaveSecreta = "6baf3137-314c-4af5-90cf-24b86066eb65";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("AccessRule", user.AccessRule)
            }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
