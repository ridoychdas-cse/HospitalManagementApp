<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BedTransferOrReleseForm.aspx.cs" Inherits="HospitalBilling.UI.BedTransferOrReleseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="idHiddenField" runat="server" />
    <asp:HiddenField ID="IdHiddenField2" runat="server" />
    <asp:HiddenField ID="roomTypeHiddenField" runat="server" />
    <asp:HiddenField ID="bedNumberHiddenField" runat="server" />

    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Transfer Or Relese</li>
        </ol>
    </div>

    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-5 col-lg-5 col-sm-5 col-md-offset-1 col-sm-offset-1 col-lg-offset-1 ">
                                <asp:RadioButton ID="transferRadioButton" CssClass="radio-inline" runat="server" Text="Bed Transfer" GroupName="PatientType" Checked="True" OnCheckedChanged="transferRadioButton_CheckedChanged" AutoPostBack="True" />
                                <asp:RadioButton ID="releseRadioButton" CssClass="radio-inline" runat="server" Text="Bed Relese" GroupName="PatientType" OnCheckedChanged="releseRadioButton_CheckedChanged" AutoPostBack="True" />
                            </div>
                            <div class="col-md-5 col-lg-5 col-sm-5">
                                <div class="input-group" style="border: 1px solid black; margin-bottom: 20px; border-radius: 5px;">
                                    <asp:TextBox ID="searchTextBox" CssClass="form-control borderRignt" runat="server" placeholder="Search by Patient Id"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

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
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <%--<div class="col-md-6">--%>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Patient Id: </label>
                                    <asp:TextBox ID="patientIdTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class=" control-label">Patient Name: </label>
                                    <asp:TextBox ID="patientNameTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Phone No: </label>
                                    <asp:TextBox ID="phoneNoTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class=" control-label">Bed Type: </label>
                                    <asp:TextBox ID="bedTypeTextBox" CssClass="form-control input-sm"  runat="server" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>

                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class=" control-label">Bed/Cabin No: </label>
                                    <asp:TextBox ID="bedNoTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Hospital Admit Date: </label>
                                    <asp:TextBox ID="admitDateTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <asp:UpdatePanel ID="transferUpdatePanel" runat="server" Visible="False">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Bed Type <strong style="color: red; font-size: 15px">*</strong></label>
                                            <asp:DropDownList ID="roomDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="roomDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-8">
                                        <asp:UpdatePanel ID="WardUpdatePanel" runat="server" Visible="False">
                                            <ContentTemplate>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Ward Name <strong style="color: red; font-size: 15px">*</strong></label>
                                                        <asp:DropDownList ID="wardDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="wardDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Bed Name <strong style="color: red; font-size: 15px">*</strong></label>
                                                        <asp:DropDownList ID="bedDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="bedDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:UpdatePanel ID="cabinUpdatePanel" runat="server" Visible="False">
                                            <ContentTemplate>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Cabin Type <strong style="color: red; font-size: 15px">*</strong></label>
                                                        <asp:DropDownList ID="cabinTypeDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cabinTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Cabin Name <strong style="color: red; font-size: 15px">*</strong></label>
                                                        <asp:DropDownList ID="cabinDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cabinDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Price (Daily)</label>
                                            <asp:UpdatePanel runat="server" ID="priceUpdatePanel" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="priceTextBox" CssClass="form-control input-sm" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Transfer/Relese Date: <strong style="color: red; font-size: 15px">*</strong></label>
                                    <asp:TextBox ID="updateAdmitDateTextBox" CssClass="form-control input-sm" runat="server" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                            </div>

                            <div class="col-md-4">
                                <label></label>
                                <div class="form-group">
                                    <asp:Button ID="transferButton" CssClass="btn btn-info btn-sm" runat="server" Text="Transfer/ Relese" OnClick="transferButton_Click" />
                                </div>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div>
                                <asp:Label ID="messageLabel" runat="server" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#indoorPatientDropdown").addClass('in');
        });
    </script>

</asp:Content>
