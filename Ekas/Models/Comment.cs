namespace Ekas.Monitoring.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int IncidentId { get; set; }
        public Incident Incident { get; set; }
    }
}
