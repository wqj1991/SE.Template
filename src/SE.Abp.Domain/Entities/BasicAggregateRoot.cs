namespace SE.Abp.Domain.Entities;

[Serializable]
public abstract class BasicAggregateRoot : CreationAggregateRoot
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }
}

[Serializable]
public abstract class BasicAggregateRoot<TKey> : CreationAggregateRoot<TKey>
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }

    protected BasicAggregateRoot()
    {

    }

    protected BasicAggregateRoot(TKey id)
        : base(id)
    {

    }
}
