namespace WebApp_c04;

public class DomainException : Exception
{
    public DomainException(string? message) : base($"Domain Exception: {message}")
    {
    }
}