namespace SE.Abp.Domain.Entities;

[Serializable]
public abstract class BasicEntity : CreationEntity
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }
}


[Serializable]
public abstract class BasicEntity<TKey> : CreationEntity<TKey>
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public virtual long? LastModifierId { get; set; }

    protected BasicEntity()
    {

    }

    protected BasicEntity(TKey id)
        : base(id)
    {

    }
}
