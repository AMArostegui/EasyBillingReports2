namespace EasyBillingReports2.Data
{
    public class Activity
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString()
        {
            return $"{Start.ToShortTimeString()} to {End.ToShortTimeString()}: {Name}";
        }
    }
}
