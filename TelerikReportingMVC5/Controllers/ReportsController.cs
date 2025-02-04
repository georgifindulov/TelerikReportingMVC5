namespace CSharp.NetFramework.AspNetMvcIntegrationDemo.Controllers
{
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Web;
    using Telerik.Reporting.Services;
    using Telerik.Reporting.Services.WebApi;
    using TelerikReportingMVC5.Reports;

    public class ReportsController : ReportsControllerBase
    {
        static ReportServiceConfiguration preservedConfiguration;

        static IReportServiceConfiguration PreservedConfiguration
        {
            get
            {
                if (preservedConfiguration == null)
                {
                    string appPath = HttpContext.Current.Server.MapPath("~/");
                    string reportsPath = Path.Combine(appPath, "Reports");

                    // Configure dependencies for ReportsController.
                    // For detailed reports service configuration take a look at the ReportingRestServiceCorsDemo example.
                    preservedConfiguration = AddTelerikReporting("MvcDemoApp", reportsPath);

                    preservedConfiguration.ReportSourceResolver = new CustomReportSourceResolver(reportsPath);
                    preservedConfiguration.ExceptionsVerbosity = "detailed";
                }

                return preservedConfiguration;
            }
        }

        public ReportsController()
        {
            this.ReportServiceConfiguration = PreservedConfiguration;
        }

        protected override HttpStatusCode SendMailMessage(MailMessage mailMessage)
        {
            throw new System.NotImplementedException("This method should be implemented in order to send mail messages.");

            // using (var smtpClient = new SmtpClient("smtp01.mycompany.com", 25))
            // {
            //     smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //     smtpClient.EnableSsl = false;
            //     smtpClient.Send(mailMessage);
            // }
            // return HttpStatusCode.OK;
        }
    }
}
