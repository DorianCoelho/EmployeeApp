using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeApp.Services.Contracts;

public abstract class RequestParameters(int maxPageSize = 50)
{
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    [BindNever]
    public bool WithPagination { get; set; }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}