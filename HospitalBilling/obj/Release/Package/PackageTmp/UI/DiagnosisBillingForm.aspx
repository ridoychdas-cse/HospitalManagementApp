<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiagnosisBillingForm.aspx.cs" Inherits="HospitalBilling.UI.DiagnosisBillingForm" %>
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

    <asp:HiddenField ID="idHiddenField" runat="server" />
    <asp:HiddenField ID="diagnosisBillMstIdHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Daigonosis Billing</li>
        </ol>
    </div>

    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-inline">

                        <div class="col-md-4 col-lg-4 col-sm-4">
                            <label>Patient Type :</label>
                            <asp:RadioButton ID="outdoorRadioButton" CssClass="radio-inline" runat="server" Text="Outdoor" GroupName="PatientType" Checked="True" AutoPostBack="True" OnCheckedChanged="outdoorRadioButton_CheckedChanged" />
                            <asp:RadioButton ID="indooorRadioButton" CssClass="radio-inline" runat="server" Text="Indoor" GroupName="PatientType" AutoPostBack="True" OnCheckedChanged="indooorRadioButton_CheckedChanged" />
                        </div>

                        <div class="col-md-4 col-lg-4 col-sm-4">
                            <asp:TextBox ID="searchTextBox" CssClass="form-control" runat="server" placeholder="Search by Patient Id"></asp:TextBox>
                            <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            <asp:LinkButton ID="createLinkButton" CssClass="btn btn-default" OnClientClick="LoadModalDiv();" runat="server" OnClick="createLinkButton_Click1" ><i class="fa fa-plus-square-o" aria-hidden="true"></i></asp:LinkButton>
                        </div>

                        <div class="col-md-4 col-lg-4 col-sm-4">
                            <label>Entry Date <strong style="color: red; font-size: 18px">*</strong></label>
                            <asp:TextBox ID="entryDateTextBox" CssClass="form-control" runat="server" Height="30px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="entryDateCalendarExtender" TargetControlID="entryDateTextBox" Format="dd-MM-yyyy" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-8 col-lg-8">
            <div class="panel panel-default">
                <div class="panel-heading">Diagnosis Info</div>
                <div class="panel-body">
                    <div role="form">
                        <div class="row">
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>Patient Id</label>
                                    <asp:TextBox ID="patientIdTextBox" runat="server" class="form-control" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>Name</label>
                                    <asp:TextBox ID="nameTextBox" runat="server" class="form-control" ReadOnly="True" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>Phone No</label>
                                    <asp:TextBox ID="phoneNoTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>Admit Date</label>
                                    <asp:TextBox ID="admitDateTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <asp:UpdatePanel ID="DiagnosisSelectDiv" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Diagnosis Type <strong style="color: red; font-size: 18px">*</strong></label>
                                            <asp:DropDownList ID="diagnosisTypeDropDownList" CssClass="form-control" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="diagnosisTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Diagnosis <strong style="color: red; font-size: 18px">*</strong></label>
                                            <asp:DropDownList ID="diagnosisDropDownList" CssClass="form-control" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="diagnosisDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Delivery Date <strong style="color: red; font-size: 18px">*</strong></label>
                                            <asp:TextBox ID="deliveryDateTextBox" CssClass="form-control" runat="server" Height="30px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="deliveryDateCalendarExtender" TargetControlID="deliveryDateTextBox" Format="dd-MM-yyyy" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Price</label>
                                            <asp:TextBox ID="priceTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Discount</label>
                                            <asp:TextBox ID="discountByTestTextBox" CssClass="form-control" runat="server" Height="30px" AutoPostBack="True" OnTextChanged="discountByTestTextBox_TextChanged">0</asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <label>Total Price</label>
                                            <asp:TextBox ID="totalPriceTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-offset-9 col-lg-offset-9 col-md-3 col-lg-3">
                                <br />
                                <div class="form-group">
                                    <asp:Button ID="assignButton" runat="server" CssClass="btn btn-info" Text="Assign" OnClick="assignButton_Click" />&nbsp;
                                    <asp:Button ID="saveAssignButton" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="saveAssignButton_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">Billing Info</div>
                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-12 col-sm-12">
                            <div class="form-group">
                                <div class="form-inline">
                                    <asp:TextBox ID="BillNoTextBox" CssClass="form-control" runat="server" placeholder="Bill No Search" Height="30px" ></asp:TextBox>
                                    <asp:LinkButton ID="billSearchLinkButton" CssClass="btn btn-default" runat="server" OnClick="billSearchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>Total Price</label>
                                <asp:TextBox ID="totalAmountTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>Discount</label>
                                <asp:TextBox ID="discountByTotalTextBox" CssClass="form-control" runat="server" Height="30px" AutoPostBack="True" OnTextChanged="discountByTotalTextBox_TextChanged">0</asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>Vat (%)</label>
                                <asp:TextBox ID="vatTextBox" CssClass="form-control" runat="server" Height="30px" OnTextChanged="vatTextBox_TextChanged" AutoPostBack="True">0</asp:TextBox>
                            </div>
                        </div>


                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>Payable Amount</label>
                                <asp:TextBox ID="payAbleAmountTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <label runat="server" id="paidAmountLabel">Paid Amount</label>
                                <asp:TextBox ID="paidAmountTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <label runat="server" id="dueAmountLabel">Due Amount</label>
                                <asp:TextBox ID="dueAmountTextBox" CssClass="form-control" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-8 col-lg-8">
                            <div class="form-group">
                                <label runat="server" id="payAmountLabel">Pay Amount</label>
                                <asp:TextBox ID="payAmountTextBox" CssClass="form-control" runat="server" Height="30px">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4 col-lg-4">
                            <br/>
                            <div class="form-group">
                                <asp:Button ID="payButton" runat="server" CssClass="btn btn-primary" Text="Pay" OnClick="payButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    

    <div class="row">
        <div class="col-md-8 col-lg-8">
            <asp:GridView ID="diagnosisBillGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333" Caption="Assign Diagnosis">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <%--<asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="idLabel" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Bill No">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("BillNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

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
        </div>
        
        <div class="col-md-4">
            <asp:GridView ID="billGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333" OnSelectedIndexChanged="billGridView_SelectedIndexChanged" Caption="Bill Due History" CaptionAlign="Left">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:CommandField ShowSelectButton="True" />

                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="idLabel" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill No">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("BillNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Payable Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TotalPayableAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pay Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TotalPayAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Due Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("DueAmount") %>'></asp:Label>
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
    
    
    
    <asp:HiddenField runat="server" ID="noneHiddenField"/>
    
    <%--<ajaxToolkit:ModalPopupExtender runat="server" ID="ModalPopup" TargetControlID="noneHiddenField" BackgroundCssClass="modalBackground"
        PopupControlID="nonePopup" CancelControlID="noneButton" />--%>
    
    <ajaxToolkit:ModalPopupExtender runat="server" ID="outdoorPatientPopup" TargetControlID="createLinkButton" BackgroundCssClass="modalBackground"
        PopupControlID="outdoorPatientPanel" CancelControlID="closeLinkButton" />
    
    
    <%--<asp:Panel id="nonePopup" runat="server" Style="display: none"><asp:Button id="noneButton" style="display: none" runat="server"></asp:Button></asp:Panel>--%>
     <%-- ************************************** Start Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="outdoorPatientPanel" Style="display: none" Width="60%">
        <asp:UpdatePanel ID="outdoorpatientUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Outdoor Pateint</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div role="form">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Pateint Id</label>
                                            *
                                            <asp:TextBox ID="newPatientIdTextBox" runat="server" class="form-control" placeholder="Patient Id"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Name</label>
                                            *
                                            <asp:TextBox ID="newPatientNameTextBox" runat="server" class="form-control" placeholder="Patient Name"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Phone No</label>
                                            *
                                            <asp:TextBox ID="newPhoneNoTextBox" runat="server" class="form-control" placeholder="Phone No"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Gender</label>*
                                        <asp:RadioButton ID="maleRadioButton" CssClass="radio-inline" runat="server" Text="Male" GroupName="Gender" Checked="True" />
                                            <asp:RadioButton ID="femaleRadioButton" CssClass="radio-inline" runat="server" Text="Female" GroupName="Gender" />
                                            <asp:RadioButton ID="otherRadioButton" CssClass="radio-inline" runat="server" Text="Other" GroupName="Gender" />
                                        </div>

                                        <div class="form-group">
                                            <label>Age</label>

                                            <asp:TextBox ID="newAgeTextBox" runat="server" class="form-control" placeholder="Diagnosis Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Entry Date</label>
                                            <asp:TextBox ID="newEntryTextBox" CssClass="form-control" runat="server" placeholder="EntryDate" Height="30px"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList ID="newDepartmentDropDownList" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Consultant</label>
                                            <asp:DropDownList ID="newConsultantDropDownList" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Reference By</label>
                                            <asp:DropDownList ID="newReferanceByDropDownList" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="newPatientSaveButton" runat="server" CssClass="btn btn-primary" OnClick="createLinkButton_Click" Text="Save" />
                                            <asp:LinkButton ID="closeLinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" ><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton>
                                        </div>
                                    </div>

                                   
                                </div>
                                <div>

                                    <div>
                                        <asp:Label ID="messageLabel" runat="server" Width="100%"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="closeLinkButton" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </asp:Panel>
    
     <div id="divBackground" style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; left: 0; background-color: #7E7E7E; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; -webkit-opacity: 0.8; display: none">
    </div>
    
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#billingDropdown").addClass('in');
        });
    </script>

</asp:Content>
