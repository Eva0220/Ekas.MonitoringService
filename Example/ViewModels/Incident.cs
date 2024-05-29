namespace Ekas.ExampleService.ViewModels;

public class Incident : IIncident
{
    public string Name { get; set; }
    public string ErrorDescription { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Category { get; set; }
    public string ServiceName { get; set; }
}