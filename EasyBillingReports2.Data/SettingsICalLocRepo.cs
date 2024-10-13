using EasyBillingReports2.Data.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace EasyBillingReports2.Data
{
    public class SettingsICalLocRepo : ISettings
    {
        private static bool _isLoaded = false;

        public SettingsICalLocRepo()
        {
            if (!_isLoaded)
            {
                Load();
            }            
        }

        public string Url { get; set; }
        public string Repo { get; set; }
        public List<string> Tags { get; set; }
        public int AmountPerHour { get; set; }

        public void Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("EasyBillingReports2.Data.IQVisio.settings.json");
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            _isLoaded = true;
            var settings = JsonSerializer.Deserialize<SettingsICalLocRepo>(json);
            Copy(settings);
        }

        public void Copy(SettingsICalLocRepo settings)
        {
            Url = settings.Url;
            Repo = settings.Repo;
            Tags = settings.Tags.ToList();
            AmountPerHour = settings.AmountPerHour;
        }
    }
}
