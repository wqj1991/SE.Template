namespace Hi.Infrastructure.Dos;

public interface IDo
{
}

public interface IDo<TKey> : IDo
{
    TKey Id { get; }
}