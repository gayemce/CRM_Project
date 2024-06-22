using Microsoft.AspNetCore.Http;

namespace Domain;
public record PagingMetadata
{
    public PagingMetadata(int pageSize, int pageNumber, long itemCount)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        PageCount = (int)Math.Ceiling((decimal)itemCount / pageSize);
        RangeStart = ((pageNumber - 1) * pageSize) + 1;
        RangeEnd = Math.Min(pageNumber * pageSize, itemCount);
        ItemCount = itemCount;
    }

    public int PageSize { get; }
    public int PageNumber { get; }
    public int PageCount { get; }
    public long RangeStart { get; }
    public long RangeEnd { get; }
    public long ItemCount { get; }

    public override string ToString()
    {
        return $"Page = {PageNumber} of {PageCount} PageSize = {PageSize} " +
               $"Items = {RangeStart} - {RangeEnd} of {ItemCount}";
    }
}

public static class ResponseExtensions
{
    public static void AddPagingHeaders(this HttpResponse response, PagingMetadata metadata)
    {
        response.Headers.Add("X-Paging-PageSize", metadata.PageSize.ToString());
        response.Headers.Add("X-Paging-PageNumber", metadata.PageNumber.ToString());
        response.Headers.Add("X-Paging-PageCount", metadata.PageCount.ToString());
        response.Headers.Add("X-Paging-RangeStart", metadata.RangeStart.ToString());
        response.Headers.Add("X-Paging-RangeEnd", metadata.RangeEnd.ToString());
        response.Headers.Add("X-Paging-ItemCount", metadata.ItemCount.ToString());
    }
}
