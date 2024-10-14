using EasyBillingReports2.Data.Interfaces;
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

        public async void Load()
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

                var commits = await client.Repository.Commit.GetAll(owner, repo);

                _workPeriods = new List<Period>();
                foreach (var commit in commits)
                {
                    var dt = commit.Commit.Author.Date.DateTime;
                    var period = new Period()
                    {
                        Start = dt,
                        End = dt,
                        Text = $"{dt.ToShortTimeString()} {commit.Commit.Message}"
                    };

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
