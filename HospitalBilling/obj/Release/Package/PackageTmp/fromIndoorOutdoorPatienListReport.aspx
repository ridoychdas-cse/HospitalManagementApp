<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="fromIndoorOutdoorPatienListReport.aspx.cs" Inherits="HospitalBilling.fromIndoorOutdoorPatienListReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label class=" control-label">Report Type</label>
                                <br />
                                <asp:DropDownList ID="ddlReportType" runat="server" Height="33px" Width="100%" >
                                  <%--  <asp:ListItem Value="BWDR">Bill Wise Diagnosis Report</asp:ListItem>
                                    <asp:ListItem Value="PWDR">Patient Wise Diagnosis Report</asp:ListItem>
                                   --%>
                                     <asp:ListItem Value="Blank">---Select Option---</asp:ListItem>
                                     <asp:ListItem Value="IN">Indoor Patien List</asp:ListItem>
                                     <asp:ListItem Value="OUT">Out Door Patien List</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                
                            <div class="form-group col-md-4">
                                <br />
                                <asp:Button ID="printButton" CssClass="btn btn-info" runat="server" Text="View" OnClick="printButton_Click" />
                                <br />
                            </div>
                            <div class="form-group col-md-4">
                                <br />
                                <asp:Button ID="btnClear" CssClass="btn btn-info" runat="server" Text="Clear" OnClick="btnClear_Click"  />
                                <br />
                            </div>

                            <div>
                                <asp:Label ID="messageLabel" runat="server" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </div>
            </div>
        </div>

             <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            
                            <rsweb:ReportViewer ID="IndoorOutdoorPatienReport" runat="server" Height="500px" Width="100%">
                            </rsweb:ReportViewer>
                        </div>
                        </div>
                    </div>
                 </div>

            <script type="text/javascript">
                $(document).ready(function () {
                    $("#billingDropdown").addClass('in');
                  });
    </script>
        </div>
    </div>
</asp:Content>
