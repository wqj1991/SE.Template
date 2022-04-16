namespace Hi.Infrastructure.Dos;

[Serializable]
public abstract class BasicDo : CreationDo
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }
}


[Serializable]
public abstract class BasicDo<TKey> : CreationDo<TKey>
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }

    protected BasicDo()
    {

    }

    protected BasicDo(TKey id)
        : base(id)
    {

    }
}
