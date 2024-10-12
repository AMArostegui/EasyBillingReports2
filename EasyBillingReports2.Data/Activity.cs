namespace EasyBillingReports2.Data
{
    public class Activity
    {
        public string Name { get; set; }
        public DateTime Dt { get; set; }

        public override string ToString()
        {
            return $"{Dt.ToLongTimeString()}: {Name}";
        }
    }
}
