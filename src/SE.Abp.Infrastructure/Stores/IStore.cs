namespace SE.Abp.Infrastructure.Stores;

public interface IStore
{
}

public interface IStore<TKey> : IStore
{
    TKey Id { get; }
}