using System.Diagnostics;
using System.Text.Json;
using EasyBillingReports2.Data.Interfaces;

namespace EasyBillingReports2.Data
{
    public class ProjectSettingsCalendar : IProjectSettings
    {
        public ProjectSettingsCalendar()
        {
            Load();
        }

        public string Url { get; set; }
        public List<string> Tags { get; set; }
        public int AmountPerHour { get; set; }

        public void Load()
        {
            var sep = Path.DirectorySeparatorChar;
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = $"{basePath}{sep}EasyBillingReports2{sep}";            
            var fullFileName = $"{path}IQVisio.settings.json";
            if (!File.Exists(fullFileName))
            {
                Debug.Assert(false);
                Directory.CreateDirectory(fullFileName);                
                var json = JsonSerializer.Serialize<ProjectSettingsCalendar>(this);
                File.WriteAllText(fullFileName, json);
                return;
            }

            var settings = JsonSerializer.Deserialize<ProjectSettingsCalendar>(fullFileName);
            Copy(settings);
        }

        public void Copy(ProjectSettingsCalendar settings)
        {
            Url = settings.Url;
            Tags = settings.Tags.ToList();
            AmountPerHour = settings.AmountPerHour;
        }
    }
}
