namespace Hi.Infrastructure.Dos;

[Serializable]
public abstract class FullDo : BasicDo
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }
}

[Serializable]
public abstract class FullDo<TKey> : BasicDo<TKey>
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }

    protected FullDo()
    {

    }

    protected FullDo(TKey id)
        : base(id)
    {

    }
}
