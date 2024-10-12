using Microsoft.AspNetCore.Components;

namespace EasyBillingReports2.Web.Components.Pages
{
    public partial class Activities
    {
        private string _value = "";
        private List<string> _activities = new();

        [Parameter]
        public Guid Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _activities.Clear();

            // For the life of me, I'm unable to get the framework to
            // populate parameter Id. I'll debug more in the future with more time
            // So, for now, extract from navigator object
            var parameters = Navigator.Uri.Split("/");
            if (parameters.Length <= 0)
            {
                return;
            }

            var lastParam = parameters.Last();
            if (!Guid.TryParse(lastParam, out var guid))
            {
                return;
            }

            Id = guid;
            var period = Wpl.WorkPeriods.FirstOrDefault(x => x.Id == Id);
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