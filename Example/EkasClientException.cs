namespace Ekas.ExampleService;

public class EkasClientException : Exception
{
    public EkasClientException(string message) : base(message)
    {
    }
}