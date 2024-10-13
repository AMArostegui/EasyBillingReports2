using EasyBillingReports2.Data.Interfaces;
using LibGit2Sharp;
using System.Net;
using System.Web;

namespace EasyBillingReports2.Data
{
    public class PeriodLoaderICalLocRepo : IPeriodLoader
    {
        private readonly ISettings _settings;
        private List<Period> _workPeriods = new();

        public PeriodLoaderICalLocRepo(ISettings settings)
        {            
            _settings = settings;
            Load();
        }

        public List<Period> WorkPeriods => _workPeriods;

        public void Load()
        {
            var urlDecoded = HttpUtility.UrlDecode(_settings.Url);

            var client = new WebClient();            
            using var ms = new MemoryStream(client.DownloadData(urlDecoded));
            using var reader = new StreamReader(ms);
            var calendarTxt = reader.ReadToEnd();

            var calendar = Ical.Net.Calendar.Load(calendarTxt);
            _workPeriods = new List<Period>();

            var repo = new Repository(_settings.Repo);
            var repoCommits = repo.Commits.ToList();

            foreach (var evnt in calendar.Events)
            {
                var period = new Period()
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

                period.Activities.Sort((x, y) => x.Dt.CompareTo(y.Dt));
                _workPeriods.Add(period);
            }
        }
    }
}
