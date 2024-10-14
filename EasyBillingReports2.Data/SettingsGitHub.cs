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

        public string Url { get; set; }
        public string Repo { get; set; }
        public List<string> Tags { get; set; }
        public int AmountPerHour { get; set; }

        public void Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("EasyBillingReports2.Data.Arexdata.settings.json");
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            _loaded = true;
            var tmp = JsonSerializer.Deserialize<SettingsGitHub>(json);

            Url = tmp.Url;
            Repo = tmp.Repo;
            Tags = tmp.Tags.ToList();
            AmountPerHour = tmp.AmountPerHour;
        }
    }
}