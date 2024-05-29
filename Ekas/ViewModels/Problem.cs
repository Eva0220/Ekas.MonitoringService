namespace Ekas.Monitoring.ViewModels
{
    public class Problem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int OpenCount { get; set; }
        public int InworkCount { get; set; }
        public int ClosedCount { get; set; }
    }
}
