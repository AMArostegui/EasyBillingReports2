﻿using EasyBillingReports2.Data.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace EasyBillingReports2.Data
{
    public class SettingsGitHub : ISettings
    {
        private class SettingsGitHubFields
        {
            public string Url { get; set; }
            public string Repo { get; set; }
            public string Owner { get; set; }
            public List<string> Tags { get; set; }
            public int AmountPerHour { get; set; }
        }

        private static SettingsGitHubFields _fields = null;

        public SettingsGitHub()
        {
            Load();
        }

        public string Url => _fields.Url;
        public string Repo => _fields.Repo;
        public string Owner => _fields.Owner;
        public List<string> Tags => _fields.Tags;
        public int AmountPerHour => _fields.AmountPerHour;

        public void Load()
        {
            if (_fields != null)
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("EasyBillingReports2.Data.Arexdata.settings.json");
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            _fields = JsonSerializer.Deserialize<SettingsGitHubFields>(json);
        }
    }
}
