namespace EasyBillingReports2
{
    public enum ProjectLst
    {
        Arexdata,
        IQVisio
    }

    public class Project
    {
        public ProjectLst Selected { get; set; } = ProjectLst.IQVisio;
    }

    public static class ProjectEm
    {
        public static string ToDesc(this ProjectLst project)
        {
            return project switch
            {
                ProjectLst.Arexdata => "Arexdata",
                ProjectLst.IQVisio => "IQVisio",
                _ => throw new NotImplementedException(),
            };
        }

        public static ProjectLst ToEnum(this string project)
        {
            return project switch
            {
                "Arexdata" => ProjectLst.Arexdata,
                "IQVisio" => ProjectLst.IQVisio,
                _ => throw new NotImplementedException(),
            };
        }
    }
}