namespace AutomobileRentalManagementAPI.Domain.Repositories.Pagination
{
    public class PaginationRequestDTO
    {
        public string? OrderBy { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}