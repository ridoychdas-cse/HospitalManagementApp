<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyMoneyCalclutionForm.aspx.cs" Inherits="HospitalBilling.UI.DailyMoneyCalclutionForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
     <script type="text/javascript">
        function LoadModalDiv() {

            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";

        }
        function HideModalDiv() {

            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";

        }

    </script>

    <%--<asp:HiddenField ID="idHiddenField" runat="server" />--%>
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Daily Money Calclution</li>
        </ol>
    </div>
    
    
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-inline">
                        <label class="control-label">Start Date </label>
                                <asp:TextBox ID="startDateTextBox" CssClass="form-control" runat="server" placeholder="Select Start Date"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="startDateCalendarExtender" TargetControlID="startDateTextBox" Format="dd/MM/yyyy" runat="server" />


                        <label class="control-label">  End Date </label>
                                <asp:TextBox ID="endDateTextBox" CssClass="form-control" runat="server" placeholder="Select End Date"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="endtDateCalendarExtender" TargetControlID="endDateTextBox" Format="dd/MM/yyyy" runat="server" />
                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">&nbsp;Money Receive List</div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                        <label>Total Collected:</label>
                                        <asp:TextBox ID="totalTextBox" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                    </div>
                            </div>

                            <div class="col-md-12">
                                <asp:GridView ID="moneyReceiveGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay Amount">
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("PayAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay Methode">
                                            <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("PayMethode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Money ReceiveBy">
                                            <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("MoneyReceiveBy") %>'></asp:Label>
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
        
        

    </div>
    
    
    
    

</asp:Content>
