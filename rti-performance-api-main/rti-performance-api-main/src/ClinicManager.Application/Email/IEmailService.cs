
namespace ClinicManager.Application.Email
{
    public interface IEmailService
    {
        Task<bool> ServiceEmail(string email, string subject, string message);
    }
}
