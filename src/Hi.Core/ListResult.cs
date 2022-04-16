namespace Hi;

[Serializable]
public class ListResult<T>
{
    public IList<T> Items { get; set; }

    public ListResult(IList<T> items)
    {
        Items = items;
    }

    public ListResult()
    {
        
    }
}