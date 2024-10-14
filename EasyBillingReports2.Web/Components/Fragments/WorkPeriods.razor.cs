using EasyBillingReports2.Data;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class WorkPeriods
    {
        RadzenScheduler<Period> _scheduler;
        private List<Period> _workPeriods = new();

        [Parameter]
        public int Month { get; set; }
        [Parameter]
        public int Year { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _workPeriods = Wpl.WorkPeriods;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            _scheduler.CurrentDate = new DateTime(Year, Month, 1);
        }

        private void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Period> args)
        {
            var guid = args.Data.Id;
            Navigation.NavigateTo($"{Year}/{Month}/{guid}");
        }
    }
}