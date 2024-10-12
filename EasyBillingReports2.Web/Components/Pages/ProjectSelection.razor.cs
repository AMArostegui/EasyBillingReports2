namespace EasyBillingReports2.Web.Components.Pages
{
    public partial class ProjectSelection
    {
        private Project _selectedProject;
        List<Project> _projects;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _projects = [new Project() { Name = "Arexdata" }, new Project() { Name = "IQVisio" }];
        }

        public void OnPrevButtonClicked()
        {
        }

        public void OnNextButtonClicked()
        {
        }
    }
}