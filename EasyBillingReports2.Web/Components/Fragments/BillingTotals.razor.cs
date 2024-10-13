using Microsoft.AspNetCore.Components;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class BillingTotals
    {
        private int _eurHour = 30;
        private int _hours = 30;
        private int _total = 457;

        [Parameter]
        public int Month { get; set; }
        [Parameter]
        public int Year { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}