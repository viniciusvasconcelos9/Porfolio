namespace ClinicManager.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

    }
}
