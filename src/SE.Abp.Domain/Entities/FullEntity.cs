namespace SE.Abp.Domain.Entities;

[Serializable]
public abstract class FullEntity : BasicEntity
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }
}

[Serializable]
public abstract class FullEntity<TKey> : BasicEntity<TKey>
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }

    protected FullEntity()
    {

    }

    protected FullEntity(TKey id)
        : base(id)
    {

    }
}
