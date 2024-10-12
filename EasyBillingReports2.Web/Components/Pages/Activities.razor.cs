using EasyBillingReports2.Data;
using Microsoft.AspNetCore.Components;


namespace EasyBillingReports2.Web.Components.Pages
{
    public partial class Activities
    {
        private string _value = "";
        private List<string> _activities = new();

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _activities.Clear();

            // For the life of me, I'm unable to get the framework to
            // populate parameter Id. I'll debug more in the future with more time
            // So, for now, extract from navigator object
            var parameters = Navigator.Uri.Split("/");
            var lastParam = parameters.Last();
            if (int.TryParse(lastParam, out int periodId))
            {
                for (int i = 0; i < periodId; i++)
                {
                    _activities.Add($"Jaremore {i}");
                }
            }
        }
    }
}