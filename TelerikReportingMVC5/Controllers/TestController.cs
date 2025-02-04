using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Http;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Report = Telerik.Reporting.Report;

namespace TelerikReportingMVC5.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [ActionName("Report1")]
        public IHttpActionResult Report1()
        {
            string appPath = HttpContext.Current.Server.MapPath("~/");
            string reportsPath = Path.Combine(appPath, "Reports");
            string pathToReport = Path.Combine(reportsPath, "Report1.trdp");

            ReportPackager reportPackager = new ReportPackager();

            using (FileStream sourceStream = File.OpenRead(pathToReport))
            {
                Report report = (Report)reportPackager.UnpackageDocument(sourceStream);

                string sqlQuery = "SELECT ROW_NUMBER() OVER (ORDER BY ModifiedDate DESC) AS Number, Title, FirstName, MiddleName, LastName, EmailAddress FROM Person.Contact ORDER BY ModifiedDate DESC";

                SqlDataSource sqlDataSource = new SqlDataSource(
                    providerName: "System.Data.SqlClient",
                    connectionString: "Data Source=(local);Initial Catalog=AdventureWorks;Integrated Security=True;Trusted_Connection=True;",
                    selectCommand: sqlQuery);
                sqlDataSource.Name = "sqlDataSource1";

                report.DataSource = sqlDataSource;
            }

            return Ok();
        }
    }
}
