namespace SE.Abp.Application.Contracts.Dtos;

[Serializable]
public class PageResultDto<T>: ListResultDto<T>
{
    public long Total { get; set; }

    public PageResultDto(long totalCount, List<T> items) : base(items)
    {
        totalCount = Total;
    }

    public PageResultDto()
    {
        
    }
}