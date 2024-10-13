using EasyBillingReports2.Data.Interfaces;
using LibGit2Sharp;
using System.Net;
using System.Web;

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

            var repo = new Repository(_settings.Repo);
            var repoCommits = repo.Commits.ToList();

            foreach (var evnt in calendar.Events)
            {
                var period = new WorkPeriod()
                {
                    Start = evnt.Start.Value,
                    End = evnt.End.Value,
                    Text = $"{evnt.Start.Value.ToShortTimeString()} to {evnt.End.Value.ToShortTimeString()} {evnt.Summary}"
                };

                var commits = repoCommits
                    .Where(x => x.Committer.When >= period.Start && x.Committer.When <= period.End);

                foreach (var commit in commits)
                {
                    var activity = new Activity()
                    {
                        Name = commit.MessageShort,
                        Dt = commit.Committer.When.DateTime
                    };

                    period.Activities.Add(activity);                
                }

                _workPeriods.Add(period);
            }
        }
    }
}
