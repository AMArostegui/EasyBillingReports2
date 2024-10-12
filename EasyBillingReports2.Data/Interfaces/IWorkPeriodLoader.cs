namespace EasyBillingReports2.Data
{
    public interface IWorkPeriodLoader
    {
        void Load();

        List<WorkPeriod> WorkPeriods { get; }
    }
}
