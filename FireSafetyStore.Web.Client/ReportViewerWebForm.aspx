<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerWebForm.aspx.cs" Inherits="FireSafetyStore.Web.Client.ReportViewerWebForm" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!doctype html>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scriptManagerReport" runat="server">
            </asp:ScriptManager>

            <rsweb:ReportViewer runat="server" ShowPrintButton="false" Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" ID="rvSiteMapping" SizeToReportContent="false">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>