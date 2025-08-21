namespace EcommerceAPI.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

    }
}
