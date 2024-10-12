namespace EasyBillingReports2.Web.Components.Pages
{
    public partial class ProjectSelection
    {
        string value = "Around the Horn";
        IEnumerable<string> companyNames;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            companyNames = [ "Arexdata", "IQVisio" ];
        }

        public void OnPrevButtonClicked()
        {
        }

        public void OnNextButtonClicked()
        {
        }
    }
}