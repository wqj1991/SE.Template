namespace SE.Application.Contracts.Dtos;

[Serializable]
public class ListResultDto<T>
{
    public IList<T> Items { get; set; }

    public ListResultDto(IList<T> items)
    {
        Items = items;
    }

    public ListResultDto()
    {
        
    }
}