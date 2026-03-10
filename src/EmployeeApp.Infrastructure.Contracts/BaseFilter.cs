namespace EmployeeApp.Infrastructure.Contracts;

public class BaseFilter
{
    public int PageSize { get; set; } = 20;

    public int PageNumber { get; set; } = 1;

    public bool WithPagination { get; set; }
}