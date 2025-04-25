namespace Customers.Management.Application.Commons;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
