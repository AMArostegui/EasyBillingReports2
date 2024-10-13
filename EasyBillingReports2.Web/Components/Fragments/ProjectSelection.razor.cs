using Radzen;
using System;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class ProjectSelection
    {
        string value = "Around the Horn";
        IEnumerable<string> companyNames;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            companyNames = [ "Arexdata", "IQVisio" ];
        }

        public void OnPrevButtonClicked()
        {
            IncMonth(false);
        }

        public void OnNextButtonClicked()
        {
            IncMonth(true);
        }

        private void IncMonth(bool inc)
        {
            var dt = Utils.ParseUrl(Navigator.Uri, out var periodId);
            dt = inc ? dt.AddMonths(1) : dt.AddMonths(-1);

            var periodIdStr = periodId == Guid.Empty ? "" : $"/{periodId}";
            Navigator.NavigateTo($"{dt.Year}/{dt.Month}{periodIdStr}");
        }
    }
}