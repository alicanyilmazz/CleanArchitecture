using App.Domain.Abstracts.Entities;

namespace App.Domain.Concretes.Entities;

public class Entity<T> : IEntity<T> where T : struct
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
