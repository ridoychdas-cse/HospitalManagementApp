<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IpMoneyReceiveForm.aspx.cs" Inherits="HospitalBilling.UI.IpMoneyReceiveForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Money Receive Form</li>
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
                                        <asp:TextBox ID="patientIdTextBox" CssClass="form-control input-sm" runat="server"  placeholder="Search By Patient Id/ Name/ Phone No" AutoPostBack="True" OnTextChanged="patientIdTextBox_TextChanged"></asp:TextBox>
                                          <ajaxToolkit:AutoCompleteExtender ID="patientIdTextBox_AutoCompleteExtender"
                                                    CompletionInterval="20" CompletionSetCount="30"
                                                    DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetSponserSearch" ServicePath="~/AutoComplete.asmx"
                                                    TargetControlID="patientIdTextBox" runat="server">
                                                </ajaxToolkit:AutoCompleteExtender>
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
                            <%-- Left Side ---------------------------------------------------------------------------------- --%>
                            <div class="col-md-7 col-sm-7">
                                <%--<div class="row">--%>
                                    <div class="panel-heading">Bill Information</div>
                                    <asp:GridView ID="billinfoGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("Serial") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Type">
                                                <ItemStyle HorizontalAlign="Center" Width="45%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("Particular") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemStyle HorizontalAlign="Right" Width="45%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
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
                                <%--</div>--%>


                                <%-- Footer Side ---------------------------------------------------------------------------------- --%>
                                <div class="row">
                                    <div class="col-md-6">
                                        <%-- Payment Type --%>
                                        <br/>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label class="col-md-5 col-sm-5  control-label">Payment Methode:<strong style="color: red; font-size: 18px">*</strong></label>
                                                    <div class="col-md-7 col-sm-7">
                                                        <asp:DropDownList ID="paymentTypeDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="paymentTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div id="paymentTypeDiv" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-5 col-sm-5  control-label">Bank Name:</label>
                                                        <div class="col-md-7 col-sm-7">
                                                            <asp:TextBox ID="bankNameTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-5 col-sm-5  control-label">Cheque No:</label>
                                                        <div class="col-md-7 col-sm-7">
                                                            <asp:TextBox ID="chequeTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-5 col-sm-5  control-label">Cheque Date:</label>
                                                        <div class="col-md-7 col-sm-7">
                                                            <asp:TextBox ID="chequeDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-6 col-sm-6  control-label">Gross Amount:</label>
                                            <div class="col-md-6 col-sm-6 float-right">
                                                <asp:TextBox ID="totalBillTextBox" CssClass="form-control input-sm text-right" runat="server" Text="0" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 col-sm-6  control-label">Previous Discount:</label>
                                            <div class="col-md-6 col-sm-6 float-right">
                                                <asp:TextBox ID="totalDiscountTextBox" CssClass="form-control input-sm text-right" runat="server" Text="0" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 col-sm-6  control-label">Total Paid:</label>
                                            <div class="col-md-6 col-sm-6 float-right">
                                                <asp:TextBox ID="totalPaidTextBox" CssClass="form-control input-sm text-right" runat="server" Text="0" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 col-sm-6   control-label">Due Amount:</label>
                                            <div class="col-md-6 col-sm-6 float-right">
                                                <asp:TextBox ID="totalDueTextBox" CssClass="form-control input-sm text-right" runat="server" Text="0" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 col-sm-6  control-label">Discount:</label>
                                            <div class="col-md-6 col-sm-6 float-right">
                                                <asp:TextBox ID="specialDiscountTextBox" CssClass="form-control input-sm text-right" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 col-sm-6  control-label">Net Receivable:</label>
                                            <div class="col-md-6 col-sm-6 float-right">
                                                <asp:TextBox ID="payAmountTextBox" CssClass="form-control input-sm text-right" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-6 col-md-6 col-sm-offset-6 col-sm-6">
                                                <asp:Button ID="reloadButton" CssClass="btn btn-default" runat="server" Text="Reset" OnClick="reloadButton_Click"/>
                                                <asp:Button ID="payButton" CssClass="btn btn-primary" runat="server" Text="Pay" OnClick="payButton_Click" />
                                                <asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Text="Repport" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-5 col-sm-5">
                                <div class="panel-heading col-md-offset-1 col-sm-offset-1">Payment History</div>
                                <div class="row col-md-offset-1 col-sm-offset-1">
                                    <%-- Right Side ------------------------------------------------------------------------- --%>
                                    <asp:GridView ID="paymentHistoryGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Payment Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("PayType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pay Methode">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("PayMethode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pay Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("PayDate") %>'></asp:Label>
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
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#indoorPatientDropdown").addClass('in');
        });
    </script>
</asp:Content>
