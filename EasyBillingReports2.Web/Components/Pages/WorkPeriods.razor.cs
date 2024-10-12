using EasyBillingReports2.Data;
using Radzen;
using Radzen.Blazor;

namespace EasyBillingReports2.Web.Components.Pages
{
    public partial class WorkPeriods
    {
        RadzenScheduler<WorkPeriod> _scheduler;
        IList<WorkPeriod> _workPeriods;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _workPeriods = Wpl.WorkPeriods;
        }

        private void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<WorkPeriod> args)
        {
            var guid = args.Data.Id;
            Navigation.NavigateTo($"activities/{guid}");
        }
    }
}