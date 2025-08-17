namespace ClinicManager.Infrastructure.Persistence.Repositories.Helpers
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }
        public int TotalCount { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
