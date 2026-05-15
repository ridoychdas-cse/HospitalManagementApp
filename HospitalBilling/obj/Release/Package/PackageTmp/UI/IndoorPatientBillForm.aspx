<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndoorPatientBillForm.aspx.cs" Inherits="HospitalBilling.UI.IndoorPatientBillForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Bill</li>
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
                                <asp:TextBox ID="nameTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Phone No: </label>
                                <asp:TextBox ID="phoneNoTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Diagnosis Bill: </label>
                                <asp:TextBox ID="TotalDiagnosisBillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Bed Bill: </label>
                                <asp:TextBox ID="TotalBedBillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <label class="control-label">Total Bill: </label>
                                <asp:TextBox ID="TotalBillTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Paid Amount: </label>
                                <asp:TextBox ID="TotalPaidAmountTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Due Amount: </label>
                                <asp:TextBox ID="TotalDueAmountTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                            <div class="form-group col-md-4">
                                <label class="control-label">Pay Amount: </label>
                                <asp:TextBox ID="payAmountTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <br/>
                                <div class="col-md-8">
                                    <asp:Button ID="payButton" CssClass="btn btn-info" runat="server" Text="Pay" OnClick="payButton_Click" />
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
    
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <asp:GridView ID="billGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Room Type">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("RoomType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bed No / Ward No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("BedNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Daily Charge">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("DailyCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Admit Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("AdmitDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Relese Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("ReleseDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#billingDropdown").addClass('in');
        });
    </script>

</asp:Content>
