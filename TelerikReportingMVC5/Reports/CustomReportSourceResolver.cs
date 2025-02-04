using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Telerik.Reporting.Services;
using Report = Telerik.Reporting.Report;
using Table = Telerik.Reporting.Table;

namespace TelerikReportingMVC5.Reports
{
    public class CustomReportSourceResolver : IReportSourceResolver
    {
        private readonly string reportsDirectory;

        public CustomReportSourceResolver(string reportsDirectory)
        {
            this.reportsDirectory = reportsDirectory;
        }

        public ReportSource Resolve(string reportId, OperationOrigin operationOrigin, IDictionary<string, object> currentParameterValues)
        {
            string pathToReport = Path.Combine(reportsDirectory, reportId);
            Report report = default;
            ReportPackager reportPackager = new ReportPackager();

            using (FileStream sourceStream = File.OpenRead(pathToReport))
            {
                report = (Report)reportPackager.UnpackageDocument(sourceStream);
            }

            if (operationOrigin == OperationOrigin.ResolveReportParameters)
            {

            }

            if (operationOrigin == OperationOrigin.GenerateReportDocument)
            {
                if (reportId == "Report1.trdp")
                {
                    report.ReportParameters.Clear();
                    report.ReportParameters.Add(new ReportParameter(
                        "JoinedSince",
                        ReportParameterType.DateTime,
                        currentParameterValues["JoinedSince"]));

                    string sqlQuery = @"
                        SELECT FirstName, MiddleName, LastName, EmailAddress, ModifiedDate 
                        FROM Person.Contact 
                        WHERE ModifiedDate >= @JoinedSince
                        ORDER BY ModifiedDate DESC";

                    SqlDataSource sqlDataSource = new SqlDataSource(
                        providerName: "System.Data.SqlClient",
                        connectionString: "Data Source=(local);Initial Catalog=AdventureWorks;Integrated Security=True;Trusted_Connection=True;",
                        selectCommand: sqlQuery);

                    sqlDataSource.Parameters.Add("@JoinedSince", DbType.DateTime, "=Parameters.JoinedSince");

                    report.DataSource = sqlDataSource;
                }
            };

            return new InstanceReportSource
            {
                ReportDocument = report
            };
        }
    }
}