using Radzen.Blazor;

namespace EasyBillingReports2.Web.Components.Fragments
{
    public partial class ProjectSelection
    {
        private RadzenDropDown<string> _comboSelPrj;
        private string _selProject;
        private List<string> _projects;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _projects = [ ProjectLst.Arexdata.ToDesc(), ProjectLst.IQVisio.ToDesc() ];
            _selProject = Prj.Selected.ToDesc();
        }

        private void OnSelectedItemChanged()
        {            
            Prj.Selected = _comboSelPrj.SelectedItem.ToString().ToEnum();
            Navigator.Refresh(forceReload: true);
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