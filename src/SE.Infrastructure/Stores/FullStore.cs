namespace SE.Infrastructure.Stores;

[Serializable]
public abstract class FullStore : BasicStore
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }
}

[Serializable]
public abstract class FullStore<TKey> : BasicStore<TKey>
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }

    protected FullStore()
    {

    }

    protected FullStore(TKey id)
        : base(id)
    {

    }
}
