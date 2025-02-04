namespace TelerikReportingMVC5
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Telerik.Reporting.Services.WebApi.ReportsControllerConfiguration.RegisterRoutes(config);
        }
    }
}
