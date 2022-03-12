namespace SE.Abp.Core;

[Serializable]
public class PageResult<T>: ListResult<T>
{
    public long TotalCount { get; set; }

    public PageResult(long totalCount, List<T> items) : base(items)
    {
        totalCount = TotalCount;
    }

    public PageResult()
    {
        
    }
}