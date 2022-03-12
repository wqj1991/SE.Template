namespace SE.Abp.Domain.Entities;

[Serializable]
public abstract class CreationAggregateRoot : AggregateRoot
{
    /// <inheritdoc />
    public virtual DateTime CreationTime { get; protected set; }

    /// <inheritdoc />
    public virtual long? CreatorId { get; protected set; }
}

[Serializable]
public abstract class CreationAggregateRoot<TKey> : AggregateRoot<TKey>
{
    /// <inheritdoc />
    public virtual DateTime CreationTime { get; set; }

    /// <inheritdoc />
    public virtual long? CreatorId { get; set; }

    protected CreationAggregateRoot()
    {

    }

    protected CreationAggregateRoot(TKey id)
        : base(id)
    {

    }
}
