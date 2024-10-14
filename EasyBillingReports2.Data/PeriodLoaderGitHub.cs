using EasyBillingReports2.Data.Interfaces;
using LibGit2Sharp;
using Octokit;
using System.Web;

namespace EasyBillingReports2.Data
{
    public class PeriodLoaderGitHub : IPeriodLoader
    {
        private readonly ISettings _settings;
        private static List<Period> _workPeriods = null;

        public PeriodLoaderGitHub(ISettings settings)
        {
            _settings = settings;
            Load();
        }

        public List<Period> WorkPeriods => _workPeriods;

        public void Load()
        {
            if (_workPeriods != null)
            {
                return;
            }

            var client = new GitHubClient(new ProductHeaderValue("EasyBillingReports2"));

            try
            {
                var settingsGitHub = _settings as SettingsGitHub;

                var owner = settingsGitHub.Owner;
                var repo = _settings.Repo;

                // TODO: I know I should be awaiting, but time presses. To change in the future
                var commits = client.Repository.Commit.GetAll(owner, repo).Result;

                _workPeriods = new List<Period>();

                var commitsByShortDate = commits.GroupBy(x => x.Commit.Author.Date.DateTime.ToShortDateString());
                foreach (var kvp in commitsByShortDate)
                {
                    var dt = DateTime.Parse(kvp.Key);
                    var period = new Period()
                    {
                        Start = dt,
                        End = dt,
                        Text = $"{kvp.Key} {kvp.Count()} commits"
                    };

                    foreach (var commit in kvp)
                    {
                        var activity = new Activity()
                        {
                            Name = commit.Commit.Message,
                            Dt = commit.Commit.Author.Date.DateTime
                        };

                        period.Activities.Add(activity);
                    }

                    period.Activities.Sort((x, y) => x.Dt.CompareTo(y.Dt));
                    _workPeriods.Add(period);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching commits: {ex.Message}");
            }
        }
    }
}
