namespace Microservice.Register.Function.Helpers.Exceptions;
public class DatabaseConnectionStringNotFound : Exception
{
    public DatabaseConnectionStringNotFound()
    {
    }

    public DatabaseConnectionStringNotFound(string message)
        : base(message)
    {
    }

    public DatabaseConnectionStringNotFound(string message, Exception inner)
        : base(message, inner)
    {
    }
}
