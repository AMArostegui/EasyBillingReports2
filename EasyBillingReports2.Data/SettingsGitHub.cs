using EasyBillingReports2.Data.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace EasyBillingReports2.Data
{
    public class SettingsGitHub : ISettings
    {
        private static bool _loaded = false;

        public SettingsGitHub()
        {
            if (_loaded)
            {
                return;
            }

            Load();
        }

        public string Url { get; private set; }
        public string Repo { get; private set; }
        public List<string> Tags { get; private set; }
        public int AmountPerHour { get; private set; }

        public static SettingsICalLocRepo Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("EasyBillingReports2.Data.Arexdata.settings.json");
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            _loaded = true;
            return JsonSerializer.Deserialize<SettingsICalLocRepo>(json);
        }
    }
}