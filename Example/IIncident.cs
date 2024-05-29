namespace Ekas.ExampleService;

public interface IIncident
{
    string Name { get; set; }
    string ErrorDescription { get; set; }
    DateTime CreatedDate { get; set; }
    string Category { get; set; }
    string ServiceName { get; set; }
}