using Microsoft.AspNetCore.Components;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class BillingTotals
    {
        private int _eurHour;
        private string _hours;
        private int _total;

        [Parameter]
        public int Month { get; set; }
        [Parameter]
        public int Year { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var dt = new DateTime(Year, Month, 1);
            Bll.Calculate(dt, out _eurHour, out var minutes, out var hours, out int _total);
            _hours = $"{hours}h{minutes}'";
        }
    }
}