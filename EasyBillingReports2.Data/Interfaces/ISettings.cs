namespace EasyBillingReports2.Data.Interfaces
{
    public interface ISettings
    {
        string Url { get; set; }
        string Repo { get; set; }
        List<string> Tags { get; set; }
        int AmountPerHour { get; set; }

        void Load();
    }
}
