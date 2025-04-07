namespace BoilerPlate.Domain.Entities.Bl.Paginated
{
    public class BasePaginatedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Filter { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
        public string GroupBy { get; set; } = string.Empty;
        public string GroupView { get; set; } = string.Empty;
        public BasePaginatedRequest()
        {
            PageNumber = 1;
            PageSize = 50;
        }
        public BasePaginatedRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
        }
    }
}
