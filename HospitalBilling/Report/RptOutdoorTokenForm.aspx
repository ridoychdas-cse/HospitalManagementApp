<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RptOutdoorTokenForm.aspx.cs" Inherits="HospitalBilling.Report.RptOutdoorTokenForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#reportDropdown").addClass('in');
        });
    </script>
    <asp:Button ID="reportButton" runat="server" Text="Report" OnClick="reportButton_Click" />
</asp:Content>
