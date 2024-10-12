namespace EasyBillingReports2.Data
{
    public class WorkPeriod
    {
        private List<Activity> _activities = new();

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<Activity> Activities => _activities;
    }
}
