using EasyBillingReports2.Data;
using EasyBillingReports2.Data.Interfaces;

namespace EasyBillingReports2.BusinessLogic
{
    public class Billing
    {
        private readonly ISettings _settings;
        private readonly IPeriodLoader _loader;

        public Billing(ISettings settings, IPeriodLoader loader)
        {
            _settings = settings;
            _loader = loader;
        }

        public void Calculate(DateTime month, out int amountPerHour, out int minutes, out int hours, out int total)
        {
            amountPerHour = _settings.AmountPerHour;
            minutes = 0;
            hours = 0;
            total = 0;

            if (_loader.WorkPeriods == null)
            {
                return;
            }

            var ts = new TimeSpan();
            var periods = _loader.WorkPeriods.Where(x => x.Start.Month == month.Month && x.Start.Year == month.Year);

            foreach (var period in periods)
            {
                var tsPeriod = period.End - period.Start;
                ts = ts.Add(tsPeriod);                
            }
            
            hours = (int)ts.TotalHours;
            ts = ts.Add(-TimeSpan.FromHours(hours));
            minutes = (int)ts.TotalMinutes;

            var amountHours = amountPerHour * hours;
            var amountPerQuarter = amountPerHour / 4;
            var quarters = (int)ts.TotalMinutes / 15;
            var amountQuarter = quarters * amountPerQuarter;

            total = amountHours + amountQuarter;
        }
    }
}
