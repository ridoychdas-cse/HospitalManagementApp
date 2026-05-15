<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndoorPatientMoneyReceiveForm.aspx.cs" Inherits="HospitalBilling.UI.IndoorPatientMoneyReceiveForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:HiddenField ID="idHiddenField" runat="server" />
    
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Money Receive</li>
        </ol>
    </div>
    
    
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-5 col-lg-5 col-sm-5">
                                <div class="input-group" style="border: 1px solid black; margin-bottom: 20px; border-radius: 5px;">
                                    <asp:TextBox ID="searchTextBox" CssClass="form-control borderRignt" runat="server" placeholder="Search"></asp:TextBox>
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
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label class=" control-label">Patient Id: </label>
                                <asp:TextBox ID="patientIdTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Patient Name: </label>
                                <asp:TextBox ID="patientNameTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Phone No: </label>
                                <asp:TextBox ID="phoneNoTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Bed Bill: </label>
                                <asp:TextBox ID="bedBillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Diagnosis Bill: </label>
                                <asp:TextBox ID="diagnosisbillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Total Bill: </label>
                                <asp:TextBox ID="totalBillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Total Pay: </label>
                                <asp:TextBox ID="totalPayTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Total Due: </label>
                                <asp:TextBox ID="totalDueTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4" runat="server" id="payDiv">
                                <label class="control-label" runat="server" id="payLabel">Pay Amount: </label>
                                <asp:TextBox ID="payTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Advance Amount: </label>
                                <asp:TextBox ID="advanceAmountTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4" runat="server" id="discountDiv">
                                <label class="control-label" runat="server" id="specialDiscountLable">Special Discount: </label>
                                <asp:TextBox ID="specialDiscountTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Vat: </label>
                                <asp:TextBox ID="ipVatTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            

                            <div class="form-group col-md-4">
                                <br/>
                                <div class="col-md-8">
                                    <asp:Button ID="paymentButton" CssClass="btn btn-info" runat="server" Text="Pay" OnClick="paymentButton_Click" />
                                </div>
                            </div>
                            
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
            $("#billingDropdown").addClass('in');
        });
    </script>
</asp:Content>
