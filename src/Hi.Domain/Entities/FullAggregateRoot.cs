namespace Hi.Domain.Entities;

[Serializable]
public abstract class FullAggregateRoot : BasicAggregateRoot
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }
}

[Serializable]
public abstract class FullAggregateRoot<TKey> : BasicAggregateRoot<TKey>
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }
    

    protected FullAggregateRoot()
    {

    }

    protected FullAggregateRoot(TKey id)
    : base(id)
    {

    }
}
