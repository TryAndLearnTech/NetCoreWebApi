namespace NetCoreWebApi.Application.Dtos
{
    public class QueryParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SortBy { get; set; }  // e.g., "Title" or "Author.Name"
        public bool SortDescending { get; set; } = false;

        public string? Filter { get; set; } // simple filter string, can be applied in repository
    }
}
