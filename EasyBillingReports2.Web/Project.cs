namespace EasyBillingReports2
{
    public class Project
    {
        public enum Projects
        {
            Arexdata,
            IQVisio
        }

        public Projects Selected { get; set; } = Projects.IQVisio;
    }
}