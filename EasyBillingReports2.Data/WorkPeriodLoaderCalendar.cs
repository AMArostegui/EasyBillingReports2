using EasyBillingReports2.Data.Interfaces;
using System.Globalization;
using System.Net;
using System.Web;
using Ical.Net;

namespace EasyBillingReports2.Data
{
    public class WorkPeriodLoaderCalendar : IWorkPeriodLoader
    {
        private readonly IProjectSettings _settings;
        private List<WorkPeriod> _workPeriods = new();

        public WorkPeriodLoaderCalendar(IProjectSettings settings)
        {            
            _settings = settings;
            Load();
        }

        public List<WorkPeriod> WorkPeriods => _workPeriods;

        public void Load()
        {
            var urlDecoded = HttpUtility.UrlDecode(_settings.Url);

            var client = new WebClient();            
            using var ms = new MemoryStream(client.DownloadData(urlDecoded));
            using var reader = new StreamReader(ms);
            var calendarTxt = reader.ReadToEnd();

            var calendar = Ical.Net.Calendar.Load(calendarTxt);
            _workPeriods = new List<WorkPeriod>();
            foreach (var evnt in calendar.Events)
            {
                var period = new WorkPeriod()
                {
                    Start = evnt.Start.Value,
                    End = evnt.End.Value,
                    Text = evnt.Summary
                };

                _workPeriods.Add(period);
            }

            //var random = new Random();
            //foreach (var wk in _workPeriods)
            //{
            //    for (var i = 0; i < random.Next(0, 10); i++)
            //    {
            //        var activity = new Activity()
            //        {
            //            Start = wk.Start,
            //            End = wk.End,
            //            Name = "Test"
            //        };

            //        wk.Activities.Add(activity);
            //    }                
            //}
        }
    }
}
