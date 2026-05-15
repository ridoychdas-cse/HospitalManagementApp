<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmIndoorPatientReport.aspx.cs" Inherits="HospitalBilling.frmIndoorPatientReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active"> Indoor Patient Report</li>
        </ol>
    </div>
    
    
   <%-- <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-5 col-lg-5 col-sm-5">
                                <div class="input-group" style="border: 1px solid black; margin-bottom: 20px; border-radius: 5px;">
                                    <asp:TextBox ID="searchTextBox" CssClass="form-control borderRignt" runat="server" placeholder="Search"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" ><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    
    
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label class=" control-label">Report Type</label>
                                <br />
                                <asp:DropDownList ID="ReportTypeDropDownList" AutoPostBack="true" runat="server" Height="33px" Width="100%" OnSelectedIndexChanged="ReportTypeDropDownList_SelectedIndexChanged">
                                  <%--  <asp:ListItem Value="BWDR">Bill Wise Diagnosis Report</asp:ListItem>
                                    <asp:ListItem Value="PWDR">Patient Wise Diagnosis Report</asp:ListItem>
                                   --%>
                                     <asp:ListItem Value="DDR">Diagnosis Details Report</asp:ListItem>
                                     <asp:ListItem Value="SDR">Surgery Details Report</asp:ListItem>
                                    <asp:ListItem Value="BWPR">BIll Wish Ptient Report</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Start Date </label>
                                <asp:TextBox ID="startDateTextBox" CssClass="form-control" runat="server" placeholder="Select Start Date"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="startDateCalendarExtender" TargetControlID="startDateTextBox" Format="dd/MM/yyyy" runat="server" />
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">End Date </label>
                                <asp:TextBox ID="endDateTextBox" CssClass="form-control" runat="server" placeholder="Select End Date"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="endtDateCalendarExtender" TargetControlID="endDateTextBox" Format="dd/MM/yyyy" runat="server" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                <asp:Label ID="daignosisTypeLabel" Visible="false" runat="server" Text="Daignosis Type"></asp:Label>
                                <br />
                                <asp:DropDownList ID="daignosisTypeDropDownList" AutoPostBack="true" Visible="false" runat="server" Height="33px" Width="100%" OnSelectedIndexChanged="daignosisTypeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-4">
                                <asp:Label ID="daignosisLabel" runat="server" Visible="false" Text="Daignosis"></asp:Label>
                                <br />
                                <asp:DropDownList ID="daignosisDropDownList" Visible="false" runat="server" Height="33px" Width="100%">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-4">
                            </div>

                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label class="control-label" id="billNoLabel">Bill No</label>
                                <asp:TextBox ID="billNoTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label" id="patientIdNoLabel">Patient Id </label>
                                <asp:TextBox ID="patientIdNoTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-2">
                                <br />
                                <asp:Button ID="printButton" CssClass="btn btn-info" runat="server" Text="View" OnClick="printButton_Click" />
                                <br />
                            </div>
                             <div class="form-group col-md-2">
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
                            
                            <rsweb:ReportViewer ID="IndoorPatientReportViewer"  Width="100%" runat="server"></rsweb:ReportViewer>
                        </div>
                        </div>
                    </div>
                 </div>


        </div>
    </div>
    
    
    

</asp:Content>

    

