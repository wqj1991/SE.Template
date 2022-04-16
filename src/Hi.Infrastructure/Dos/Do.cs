namespace Hi.Infrastructure.Dos;

/// <inheritdoc/>
[Serializable]
public abstract class Do : IDo
{
}

/// <inheritdoc cref="IDo{TKey}" />
[Serializable]
public abstract class Do<TKey> : Do, IDo<TKey>
{
    /// <inheritdoc/>
    public virtual TKey Id { get; protected set; }

    protected Do()
    {

    }

    protected Do(TKey id)
    {
        Id = id;
    }
}