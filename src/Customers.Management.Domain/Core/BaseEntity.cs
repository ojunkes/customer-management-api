namespace Customers.Management.Domain.Core;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? ModifiedAt { get; protected set; }
}
