namespace SE.Abp.Infrastructure.Stores;

[Serializable]
public abstract class BasicStore : CreationStore
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }
}


[Serializable]
public abstract class BasicStore<TKey> : CreationStore<TKey>
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }

    protected BasicStore()
    {

    }

    protected BasicStore(TKey id)
        : base(id)
    {

    }
}
