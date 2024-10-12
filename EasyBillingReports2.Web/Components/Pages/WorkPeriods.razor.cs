using EasyBillingReports2.Data;
using Radzen;
using Radzen.Blazor;

namespace EasyBillingReports2.Web.Components.Pages
{
    public partial class WorkPeriods
    {
        RadzenScheduler<WorkPeriod> _scheduler;
        Dictionary<DateTime, string> events = new ();
        IList<WorkPeriod> _workPeriods;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _workPeriods = WrkPrds.WorkPeriods;
        }

        void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            // Highlight today in month view
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: var(--rz-scheduler-highlight-background-color, rgba(255,220,40,.2));";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Week" || args.View.Text == "Day") && args.Start.Hour > 8 && args.Start.Hour < 19)
            {
                args.Attributes["style"] = "background: var(--rz-scheduler-highlight-background-color, rgba(255,220,40,.2));";
            }
        }

        async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
        }

        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<WorkPeriod> args)
        {
        }

        void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<WorkPeriod> args)
        {
            // Never call StateHasChanged in AppointmentRender - would lead to infinite loop

            if (args.Data.Text == "Birthday")
            {
                args.Attributes["style"] = "background: red";
            }
        }

        async Task OnAppointmentMove(SchedulerAppointmentMoveEventArgs args)
        {
            var draggedAppointment = _workPeriods.FirstOrDefault(x => x == args.Appointment.Data);

            if (draggedAppointment != null)
            {
                draggedAppointment.Start = draggedAppointment.Start + args.TimeSpan;

                draggedAppointment.End = draggedAppointment.End + args.TimeSpan;

                await _scheduler.Reload();
            }
        }
    }
}