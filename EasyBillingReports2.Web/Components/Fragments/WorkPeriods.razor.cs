using EasyBillingReports2.Data;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class WorkPeriods
    {
        RadzenScheduler<WorkPeriod> _scheduler;
        private List<WorkPeriod> _workPeriods;

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

        private void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<WorkPeriod> args)
        {
            var guid = args.Data.Id;
            Navigation.NavigateTo($"{Year}/{Month}/{guid}");
        }
    }
}