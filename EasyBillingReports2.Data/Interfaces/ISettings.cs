namespace EasyBillingReports2.Data.Interfaces
{
    public interface ISettings
    {
        string Url { get; }
        string Repo { get; }
        List<string> Tags { get; }
        int AmountPerHour { get; }
    }
}
