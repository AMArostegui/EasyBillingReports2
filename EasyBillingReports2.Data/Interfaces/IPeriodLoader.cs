namespace EasyBillingReports2.Data
{
    public interface IPeriodLoader
    {
        void Load();

        List<Period> WorkPeriods { get; }
    }
}
