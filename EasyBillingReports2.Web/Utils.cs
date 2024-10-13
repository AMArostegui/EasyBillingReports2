using Radzen;

namespace EasyBillingReports2.Web
{
    public static class Utils
    {
        public static DateTime ParseUrl(string url, out Guid periodId)
        {
            var ret = DateTime.Now;

            periodId = Guid.Empty;

            // TODO: For the life of me, I'm unable to get the framework to
            // populate parameter Id. I'll debug more in the future with more time
            // For now, extract parameters directly from the Url
            var builder = new UriBuilder(url);
            var path = builder.Path;
            var parameters = path
                .Split("/")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            if (parameters.Count <= 1)
            {
                return ret;
            }

            var year = int.Parse(parameters[0]);
            var month = int.Parse(parameters[1]);
            ret = new DateTime(year, month, 1);

            if (parameters.Count >= 3)
            {
                periodId = Guid.Parse(parameters[2]);
            }

            return ret;
        }
    }
}
