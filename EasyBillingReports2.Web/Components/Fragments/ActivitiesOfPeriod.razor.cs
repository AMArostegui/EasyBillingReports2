using Microsoft.AspNetCore.Components;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class ActivitiesOfPeriod
    {
        private string _value = "";
        private List<string> _activities = new();

        [Parameter]
        public Guid PeriodId {get; set;}

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _activities.Clear();
            if (Wpl.WorkPeriods == null)
            {
                return;
            }

            var period = Wpl.WorkPeriods.FirstOrDefault(x => x.Id == PeriodId);
            if (period == null)
            {
                return;
            }

            foreach (var activity in period.Activities)
            {
                _activities.Add(activity.ToString());                
            }
        }
    }
}