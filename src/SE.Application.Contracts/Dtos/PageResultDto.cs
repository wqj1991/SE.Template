namespace SE.Application.Contracts.Dtos;

[Serializable]
public class PageResultDto<T>: ListResultDto<T>
{
    public long Total { get; set; }

    public PageResultDto(long total, List<T> items) : base(items)
    {
        Total = total;
    }

    public PageResultDto()
    {
        
    }
}