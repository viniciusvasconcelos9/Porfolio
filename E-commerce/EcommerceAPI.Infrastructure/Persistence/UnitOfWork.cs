namespace EcommerceAPI.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceDbContext _ecommerceDbContext;

        public UnitOfWork(EcommerceDbContext ecommerceDbContext)
        {
            _ecommerceDbContext = ecommerceDbContext;
        }

        public async Task<int> CommitAsync()
        {
            return await _ecommerceDbContext.SaveChangesAsync();
        }

    }
}
