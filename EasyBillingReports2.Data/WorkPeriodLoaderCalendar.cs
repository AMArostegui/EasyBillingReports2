
namespace EasyBillingReports2.Data
{
    public class WorkPeriodLoaderCalendar : IWorkPeriodLoader
    {
        private List<WorkPeriod> _workPeriods = new();

        public WorkPeriodLoaderCalendar()
        {
            Load();
        }

        public List<WorkPeriod> WorkPeriods => _workPeriods;

        public void Load()
        {
            _workPeriods = new List<WorkPeriod>()
            {
                new WorkPeriod { Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-2), Text = "Birthday" },
                new WorkPeriod { Start = DateTime.Today.AddDays(-11), End = DateTime.Today.AddDays(-10), Text = "Day off" },
                new WorkPeriod { Start = DateTime.Today.AddDays(-10), End = DateTime.Today.AddDays(-8), Text = "Work from home" },
                new WorkPeriod { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(12), Text = "Online meeting" },
                new WorkPeriod { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(13), Text = "Skype call" },
                new WorkPeriod { Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(14).AddMinutes(30), Text = "Dentist appointment" },
                new WorkPeriod { Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(12), Text = "Vacation" },
            };
        }
    }
}
