using EasyBillingReports2.Data.Interfaces;
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

            var urlDecoded = HttpUtility.UrlDecode(_settings.Url);

            _workPeriods = new List<Period>();
        }
    }
}
