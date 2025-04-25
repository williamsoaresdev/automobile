using AutomobileRentalManagementAPI.Domain.Repositories.Pagination;

namespace AutomobileRentalManagementAPI.WebApi.Common
{
    public class PagedRequest
    {
        public string? SortOrder { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 50;

        public PaginationRequestDTO ToDTO() => new()
        {
            OrderBy = SortOrder,
            PageNumber = CurrentPage,
            PageSize = ItemsPerPage,
        };
    }
}