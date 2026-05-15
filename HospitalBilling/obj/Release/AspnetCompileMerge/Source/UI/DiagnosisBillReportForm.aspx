<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiagnosisBillReportForm.aspx.cs" Inherits="HospitalBilling.UI.DiagnosisBillReportForm" %>
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
            <li class="active">Daignosis Bill Summery Report</li>
        </ol>
    </div>
    
    
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-inline">
                         <asp:Label ID="Label2" runat="server" Text="Start"></asp:Label>
                        <asp:TextBox ID="startDateTextBox" CssClass="form-control" runat="server" placeholder="Select Start Date"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="entryDateCalendarExtender" TargetControlID="startDateTextBox" Format="dd/MM/yyyy" runat="server" />
                        <asp:Label ID="Label1" runat="server" Text=" To"></asp:Label>
                         <asp:TextBox ID="endTextBox" CssClass="form-control" runat="server" placeholder="Select Start Date"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="endTextBox" Format="dd/MM/yyyy" runat="server" />
                        
                        <asp:Label ID="Label3" runat="server" Text="Type"></asp:Label>
                        <asp:DropDownList ID="typeDropDownList" runat="server" Height="31px">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="OP">Outdoor</asp:ListItem>
                            <asp:ListItem Value="IP">Indoor</asp:ListItem>
                         </asp:DropDownList>
                        
                        
                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" OnClick="searchLinkButton_Click" ><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Daignosis Bill Summery List</div>
                        <div class="panel-body">
                            <div class="col-md-4">
                             
                            </div>

                        
                                <asp:GridView ID="DiagnosisBillSummeryGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                       <asp:BoundField DataField="DiagnosisType" HeaderText="Diagnosis Type">
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiagnosisName" HeaderText="Diagnosis Name">
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalDiagnosis" HeaderText="Total">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Price" HeaderText="Price">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Discount" HeaderText="Discount">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalPayableAmount" HeaderText="Payable Amount">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        </asp:BoundField>

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
