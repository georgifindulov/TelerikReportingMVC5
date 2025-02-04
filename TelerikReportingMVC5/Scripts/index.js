$(document).ready(function () {
    $('#JoinedSince').datepicker({
        defaultDate: '01/01/2005',
        dateFormat: 'mm/dd/yy'
    });

    $('#btnRefresh').on('click', onRefreshReport);
});

function onRefreshReport() {
    const joinedSince = $('#JoinedSince').val();
    const viewer = $("#reportViewer").data("telerik_ReportViewer");

    viewer.reportSource({
        report: viewer.reportSource().report,
        parameters: {
            JoinedSince: joinedSince
        }
    });

    //setting the HTML5 Viewer's reportSource causes a refresh automatically
    //if you need to force a refresh for other cases, use:
    //viewer.refreshReport();
}