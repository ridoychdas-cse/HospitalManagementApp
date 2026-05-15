<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IpOtherBillForm.aspx.cs" Inherits="HospitalBilling.UI.IpOtherBillForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Other Bill Form</li>
        </ol>
    </div>


    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label class="col-md-2 col-sm-3 control-label">Patient Id:</label>
                                    <div class="col-md-4 col-sm-4 ">
                                        <asp:TextBox ID="patientIdTextBox" CssClass="form-control input-sm" runat="server"  placeholder="Search By Patient Id/ Name/ Phone No"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-primary" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-6">
                                <div class="form-group">
                                    <label class="col-md-4  col-sm-4 control-label">Patient Name:</label>
                                    <div class="col-md-8 col-sm-8">
                                        <asp:TextBox ID="patientNameTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-6">
                                <div class="form-group">
                                    <label class="col-md-4  col-sm-4  control-label">Phone No:</label>
                                    <div class="col-md-8 col-sm-8">
                                        <asp:TextBox ID="phoneNoTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
    
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-md-6 col-sm-6">
                                <div class="panel-heading">Other Bill Assign</div>
                                <br/>
                                <div class="form-group">
                                    <label class="col-md-4  col-sm-4 control-label">Bill Type:</label>
                                    <div class="col-md-8 col-sm-8">
                                        <asp:DropDownList ID="otherBillTypeDropDownList"  CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4  col-sm-4 control-label">Price:</label>
                                    <div class="col-md-8 col-sm-8">
                                        <asp:TextBox ID="priceTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <div class="col-md-offset-4 col-md-5 col-sm-offset-4 col-sm-5">
                                        <asp:Button ID="reloadButton" CssClass="btn btn-default" runat="server" Text="Reset" OnClick="reloadButton_Click" />
                                        <asp:Button ID="assignButton" CssClass="btn btn-primary" runat="server" Text="Assign" OnClick="assignButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#indoorPatientDropdown").addClass('in');
        });
    </script>
</asp:Content>
