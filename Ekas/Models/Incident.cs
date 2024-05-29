
using System.ComponentModel.DataAnnotations;

namespace Ekas.Monitoring.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public string ServiceName { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        [Display(Name = "Открыт")]
        Open,

        [Display(Name = "В работе")]
        Inwork,

        [Display(Name = "Закрыт")]
        Close
    }
}
