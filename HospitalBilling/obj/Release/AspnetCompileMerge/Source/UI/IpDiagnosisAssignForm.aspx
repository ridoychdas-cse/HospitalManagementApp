<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IpDiagnosisAssignForm.aspx.cs" Inherits="HospitalBilling.UI.IpDiagnosisAssignForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient Diagnosis Assign Form</li>
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
                                       
                                         <asp:TextBox ID="patientIdTextBox" CssClass="form-control input-sm" runat="server" placeholder="Search By Patient Id/ Name/ Phone No" AutoPostBack="true" OnTextChanged="patientIdTextBox_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="patientIdTextBox_AutoCompleteExtender"
                                                    CompletionInterval="20" CompletionSetCount="30"
                                                    DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2"
                                                    ServiceMethod="GetSponserSearch" ServicePath="~/AutoComplete.asmx"
                                                    TargetControlID="patientIdTextBox" runat="server">
                                                </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-primary" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:Button ID="reportButton" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="resetButton_Click" />

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
        <%-- **************************Left Side***************** --%>
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-heading">Diagnosis Info</div>
                <div class="panel-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label>Diagnosis Type <strong style="color: red; font-size: 15px">*</strong></label>
                                    <asp:DropDownList ID="diagnosisTypeDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="diagnosisTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label>Diagnosis <strong style="color: red; font-size: 15px">*</strong></label>
                                    <asp:DropDownList ID="diagnosisDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="diagnosisDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label>Delivery Date <strong style="color: red; font-size: 15px">*</strong></label>
                                    <asp:TextBox ID="deliveryDateTextBox" CssClass="form-control input-sm" runat="server" Height="30px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="deliveryDateCalendarExtender" TargetControlID="deliveryDateTextBox" Format="dd-MM-yyyy" runat="server" />
                                </div>
                            </div>

                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label>Price</label>
                                    <asp:TextBox ID="priceTextBox" CssClass="form-control input-sm" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label>Discount</label>
                                    <asp:TextBox ID="discountByTestTextBox" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnTextChanged="discountByTestTextBox_TextChanged">0</asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-lg-4">
                                <div class="form-group">
                                    <label>Total Price</label>
                                    <asp:TextBox ID="totalPriceTextBox" CssClass="form-control input-sm" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <div class="col-md-4 col-lg-4">
                        <div class="form-group">
                            <label>Reference </label>
                            <asp:DropDownList ID="referenceDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="col-md-4 col-lg-4"> <%--col-md-offset-8 col-lg-offset-8--%>
                        <div class="form-group">
                            <asp:Button ID="assignButton" runat="server" CssClass="btn btn-primary btn-sm" Text="Assign" OnClick="assignButton_Click" />&nbsp;
                        </div>
                    </div>
                </div>
            </div>
            
            
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Assign Diagnosis List</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="diagnosisBillListUpdatePanel" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="diagnosisBillGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="diagnosisTypeIdLabel" Text='<%#Eval("DiagnosisTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Diagnosis Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("DiagnosisTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="diagnosisIdLabel" Text='<%#Eval("DiagnosisId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Diagnosis">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("DiagnosisName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pirce">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("Discount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Price">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("PayableAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("DeliveryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="EditLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-primary" OnClick="EditLinkButton_Click"><i class="fa fa-times" aria-hidden="true"></i> </asp:LinkButton>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- ******************************** Right Side****************************** --%>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">Bill Info</div>
                <div class="panel-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>Total Price</label>
                                    <asp:TextBox ID="totalAmountTextBox" CssClass="form-control input-sm" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>Discount(%)</label>
                                    <asp:TextBox ID="discountByParcentTotalTextBox" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnTextChanged="discountByParcentTotalTextBox_TextChanged">0</asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>Discount</label>
                                    <asp:TextBox ID="discountByTotalTextBox" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnTextChanged="discountByTotalTextBox_TextChanged">0</asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>Vat (%)</label>
                                    <asp:TextBox ID="vatTextBox" CssClass="form-control input-sm" runat="server" Height="30px" OnTextChanged="vatTextBox_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>Payable Amount</label>
                                    <asp:TextBox ID="payAbleAmountTextBox" CssClass="form-control input-sm" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>Pay Amount</label>
                                    <asp:TextBox ID="payTextBox" CssClass="form-control input-sm" runat="server" Height="30px">0</asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>Payment Methode:</label>
                                    <asp:DropDownList ID="paymentTypeDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="paymentTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div id="paymentTypeDiv" runat="server">
                                <div class="col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Bank Name:</label>
                                        <asp:TextBox ID="bankNameTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Cheque No:</label>
                                        <asp:TextBox ID="chequeTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Cheque Date:</label>
                                        <asp:TextBox ID="chequeDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="saveAssignButton" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="saveAssignButton_Click" />
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
