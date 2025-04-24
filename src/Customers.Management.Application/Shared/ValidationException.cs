namespace Customers.Management.Application.Shared;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
