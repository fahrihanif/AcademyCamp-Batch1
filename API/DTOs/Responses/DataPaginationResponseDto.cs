namespace API.DTOs.Responses;

public class DataPaginationResponseDto<TEntity> where TEntity : class
{
    public DataPaginationResponseDto(int code, string message, int pageIndex, int pageSize, int count,
                                     IEnumerable<TEntity> data)
    {
        Code = code;
        Message = message;
        PageIndex = pageIndex;
        PageSize = pageSize;
        PageCount = (count + pageSize - 1) / pageSize;
        Data = data;
    }

    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public IEnumerable<TEntity> Data { get; set; }
}
