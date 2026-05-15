<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientReleseForm.aspx.cs" Inherits="HospitalBilling.UI.PatientReleseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:HiddenField ID="idHiddenField" runat="server" />
    <asp:HiddenField ID="IdHiddenField2" runat="server" />
    
    <asp:HiddenField ID="bedIdHiddenField" runat="server" />
    <asp:HiddenField ID="bedTypeHiddenField" runat="server" />
    


    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Relese</li>
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
                                <asp:TextBox ID="patientIdTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Patient Name: </label>
                                <asp:TextBox ID="patientNameTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Phone No: </label>
                                <asp:TextBox ID="phoneNoTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <%--<div class="form-group col-md-4">
                                <label class="control-label">Bed Bill: </label>
                                <asp:TextBox ID="bedBillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Diagnosis Bill: </label>
                                <asp:TextBox ID="diagnosisbillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>--%>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Total Bill: </label>
                                <asp:TextBox ID="totalBillTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Total Pay: </label>
                                <asp:TextBox ID="totalPayTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Total Due: </label>
                                <asp:TextBox ID="totalDueTextBox" CssClass="form-control input-sm" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Relese Date: <strong style="color: red; font-size: 15px">*</strong></label>
                                <asp:TextBox ID="releseDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Special Discount:</label>
                                    <asp:TextBox ID="specialDiscountTextBox" CssClass="form-control input-sm " runat="server"></asp:TextBox>
                                </div>
                            </div>
                                
                            <div class="form-group col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Pay Amount:</label>
                                    <asp:TextBox ID="payAmountTextBox" CssClass="form-control input-sm " runat="server"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Payment Methode:<strong style="color: red; font-size: 18px">*</strong></label>
                                    <asp:DropDownList ID="paymentTypeDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="paymentTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>


                            <div id="paymentTypeDiv" runat="server">
                                <div class="form-group col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Bank Name:</label>
                                        <asp:TextBox ID="bankNameTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group col-md-4">
                                    <div class="form-group">
                                        <label class=" control-label">Cheque No:</label>
                                        <asp:TextBox ID="chequeTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Cheque Date:</label>
                                        <asp:TextBox ID="chequeDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <br/>
                                <div class="col-md-8">
                                    <asp:Button ID="releseButton" CssClass="btn btn-info btn-sm" runat="server" Text="Relese" OnClick="releseButton_Click" />
                                </div>
                            </div>
                            <div class="form-group col-md-4">
                                <br/>
                                <div class="col-md-8">
                                    <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="btn btn-info btn-sm" OnClick="btnClear_Click" />
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
            $("#indoorPatientDropdown").addClass('in');
        });
    </script>

</asp:Content>
