namespace SE.Infrastructure.Stores;

public interface IStore
{
}

public interface IStore<TKey> : IStore
{
    TKey Id { get; }
}