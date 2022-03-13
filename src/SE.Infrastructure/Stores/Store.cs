namespace SE.Infrastructure.Stores;

/// <inheritdoc/>
[Serializable]
public abstract class Store : IStore
{
}

/// <inheritdoc cref="IStore{TKey}" />
[Serializable]
public abstract class Store<TKey> : Store, IStore<TKey>
{
    /// <inheritdoc/>
    public virtual TKey Id { get; protected set; }

    protected Store()
    {

    }

    protected Store(TKey id)
    {
        Id = id;
    }
}