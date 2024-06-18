namespace API.DTOs.Requests;

public class EmployeeDetailRequestDto
{
    private const int MAX_PAGE_SIZE = 100;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
    }

    public string? SortColumn { get; set; }
    public bool IsDescending { get; set; } = false;
    public string? Search { get; set; }
    public Guid? Manager { get; set; }
}
