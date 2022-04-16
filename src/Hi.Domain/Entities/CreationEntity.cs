namespace Hi.Domain.Entities;

/// <summary>
/// This class can be used to simplify implementing <see cref="T:Volo.Abp.Auditing.ICreationAuditedObject" /> for an entity.
/// </summary>
[Serializable]
public abstract class CreationEntity : Entity
{
    /// <inheritdoc />
    public virtual DateTime CreationTime { get; protected set; }

    /// <inheritdoc />
    public virtual long? CreatorId { get; protected set; }
}

/// <summary>
/// This class can be used to simplify implementing <see cref="ICreationAuditedObject"/> for an entity.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class CreationEntity<TKey> : Entity<TKey>
{
    /// <inheritdoc />
    public virtual DateTime CreationTime { get; protected set; }

    /// <inheritdoc />
    public virtual long? CreatorId { get; protected set; }

    protected CreationEntity()
    {

    }

    protected CreationEntity(TKey id)
        : base(id)
    {

    }
}
