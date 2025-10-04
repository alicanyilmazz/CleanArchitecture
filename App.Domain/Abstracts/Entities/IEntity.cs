namespace App.Domain.Abstracts.Entities;

public interface IEntity<T> : IAuditable
{
    public T Id { get; set; }
}
