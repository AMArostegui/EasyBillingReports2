using EasyBillingReports2.Data;
using EasyBillingReports2.Data.Interfaces;

namespace EasyBillingReports2.BusinessLogic
{
    public class Billing
    {
        private readonly IProjectSettings _settings;
        private readonly IWorkPeriodLoader _loader;

        public Billing(IProjectSettings settings, IWorkPeriodLoader loader)
        {
            _settings = settings;
            _loader = loader;
        }

        public void Calculate(DateTime month, out int amountPerHour, out int minutes, out int hours, out int total)
        {
            amountPerHour = _settings.AmountPerHour;

            var ts = new TimeSpan();
            var periods = _loader.WorkPeriods.Where(x => x.Start.Month == month.Month && x.Start.Year == month.Year);

            foreach (var period in periods)
            {
                var tsPeriod = period.End - period.Start;
                ts.Add(tsPeriod);                
            }

            var seconds = ts.TotalSeconds;
            hours = ts.Hours;
            minutes = (int)ts.TotalMinutes;

            var amountHours = amountPerHour * hours;
            var amountPerQuarter = amountPerHour / 4;
            var quarters = ts.Minutes / 15;
            var amountQuarter = quarters * amountPerQuarter;

            total = amountHours + amountQuarter;
        }
    }
}
