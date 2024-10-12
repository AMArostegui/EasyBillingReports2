namespace EasyBillingReports2.Data
{
    public interface IProjectSettings
    {
        string Url { get; set; }
        List<string> Tags { get; set; }
        int AmountPerHour { get; set; }

        void Load();
    }
}
