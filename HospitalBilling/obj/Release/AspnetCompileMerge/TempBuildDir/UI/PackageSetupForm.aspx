<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackageSetupForm.aspx.cs" Inherits="HospitalBilling.UI.PackageSetupForm" %>
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
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Package Setup</li>
        </ol>
    </div>

    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-inline">
                        <asp:LinkButton ID="createNewLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-default btnDefaultOverride"><i class="fa fa-plus-square-o" aria-hidden="true"></i> Create New</asp:LinkButton>
                        <asp:TextBox ID="searchTextBox" CssClass="form-control" runat="server" placeholder="Search"></asp:TextBox>
                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-lg-12">
            <asp:GridView ID="packageGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="idLabel" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Short Name">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("ShortName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("PackageTypeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Package Type">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("PackageTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Regular Fee">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("RegularFee") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Discount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Fee">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TotalFee") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:LinkButton ID="detailsLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-primary" OnClick="detailsLinkButton_Click"><i class="fa fa-info-circle" aria-hidden="true"></i> </asp:LinkButton> <%--OnClick="EditLinkButton_Click"--%>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-primary" OnClick="EditLinkButton_Click" ><i class="fa fa-pencil-square-o" aria-hidden="true"></i> </asp:LinkButton> <%--OnClick="EditLinkButton_Click"--%>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="deleteLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-danger" OnClick="deleteLinkButton_Click"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>  <%--OnClick="deleteLinkButton_Click"--%>
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
    
     <%-- **************************************** Ajax Modal Popup************************************* --%>

    <ajaxToolkit:ModalPopupExtender runat="server" ID="surgeryPopup" TargetControlID="createNewLinkButton" BackgroundCssClass="modalBackground"
        PopupControlID="packagePanel" CancelControlID="closeLinkButton" />
    
    <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="idHiddenField" ID="editModalPopupExtender" BackgroundCssClass="modalBackground"
        PopupControlID="editPanel" CancelControlID="close2LinkButton" />
    
    <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="idHiddenField" ID="deleteModalPopupExtender" BackgroundCssClass="modalBackground"
        PopupControlID="deletePanel" CancelControlID="close3LinkButton" />
    
    <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="idHiddenField" ID="detailsModalPopupExtender" BackgroundCssClass="modalBackground"
        PopupControlID="detailsPanel" CancelControlID="close4LinkButton" />
    
    <%-- ************************************** Start Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="packagePanel" Style="display: none" Width="80%">
        <asp:UpdatePanel ID="packageUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Package Setup</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div role="form">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Package Type</label>
                                            <asp:DropDownList ID="packageTypeDropDownList" class="form-control" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Name</label>
                                            *
                                            <asp:TextBox ID="nameTextBox" runat="server" class="form-control" placeholder="Package Name"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Short Name</label>
                                            <asp:TextBox ID="shortNameTextBox" runat="server" class="form-control" placeholder="Package Short Name"></asp:TextBox>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Regular Fee</label>
                                                    *
                                                    <asp:TextBox ID="regularFeeTextBox" runat="server" class="form-control" placeholder="Regular Fee" AutoPostBack="True" OnTextChanged="regularFeeTextBox_TextChanged">0</asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Discount</label>
                                                    *
                                                    <asp:TextBox ID="discountTextBox" runat="server" class="form-control" placeholder="Discount" AutoPostBack="True" OnTextChanged="discountTextBox_TextChanged">0</asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Total Fee</label>
                                                    *
                                                <asp:TextBox ID="totalFeeTextBox" runat="server" class="form-control" ReadOnly="True">0</asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Description</label>
                                            <asp:TextBox ID="descriptionTextBox" runat="server" class="form-control" placeholder="Package Details" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                        </div>


                                        <div class="form-group">
                                            <label>Service Type</label>
                                            <asp:DropDownList ID="serviceTypeDropDownList" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="serviceTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Service Name</label>
                                            <asp:DropDownList ID="serviceDropDownList" class="form-control" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="addButton" CssClass="btn btn-success" runat="server" Text="Add" OnClick="addButton_Click" />
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-offset-2 col-md-8">
                                            <asp:GridView ID="packageDtlGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Service Type">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("ServiceType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("ServiceId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Service">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
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


                                    <div class="row">
                                        <table class="nav-justified">
                                            <tr>
                                                <td>&nbsp;<asp:LinkButton ID="saveLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="saveLinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Save</asp:LinkButton></td>
                                                <td>&nbsp;<asp:LinkButton ID="clearLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="clearLinkButton_Click"><i class="fa fa-refresh" aria-hidden="true"></i> Clear</asp:LinkButton></td>
                                                <%--OnClick="clearLinkButton_Click"--%>
                                                <td>&nbsp;<asp:LinkButton ID="closeLinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" Width="70%"><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton></td>
                                                <%--OnClick="closeLinkButton_Click"--%>
                                            </tr>
                                        </table>
                                        <br />
                                        <div>
                                            <asp:Label ID="messageLabel" runat="server" Width="100%"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="closeLinkButton" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- ********************************************************** End Panel ************************************************************* --%>
    
    <%-- ************************************** Start Edit Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="editPanel" Style="display: none"  Width="40%">
        <asp:UpdatePanel ID="editUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Edit Package</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div role="form">
                                    <div class="form-group">
                                        <label>Name</label>
                                        *
                                            <asp:TextBox ID="editNameTextBox" runat="server" class="form-control" placeholder="Package Name"></asp:TextBox>
                                    </div>


                                    <div class="form-group">
                                        <label>Short Name</label>
                                        *
                                            <asp:TextBox ID="editShortNameTextBox" runat="server" class="form-control" placeholder="Package Short Name"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Surgery Type</label>
                                        <asp:DropDownList ID="editPackageTypeDropDownList" class="form-control" runat="server"></asp:DropDownList>
                                        <%--<asp:TextBox ID="shortNameTextBox" runat="server" class="form-control" placeholder="Short Name"></asp:TextBox>--%>
                                    </div>

                                    <div class="form-group">
                                        <label>Description</label>
                                        <asp:TextBox ID="editDescriptionTextBox" runat="server" class="form-control" placeholder="Surgery Details" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Regular Fee</label>
                                        *
                                            <asp:TextBox ID="editRegularFeeTextBox" runat="server" class="form-control" placeholder="Regular Fee" AutoPostBack="True" OnTextChanged="editRegularFeeTextBox_TextChanged">0</asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Discount</label>
                                        *
                                            <asp:TextBox ID="editDiscountTextBox" runat="server" class="form-control" placeholder="Discount" AutoPostBack="True" OnTextChanged="editDiscountTextBox_TextChanged">0</asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Total Fee</label>
                                        *
                                            <asp:TextBox ID="editTotalFeeTextBox" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div>

                                    <table class="nav-justified">
                                        <tr>
                                            <td>&nbsp;<asp:LinkButton ID="updateLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="updateLinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Update</asp:LinkButton></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;<asp:LinkButton ID="close2LinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" Width="70%" OnClick="close2LinkButton_Click"><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div>
                                        <asp:Label ID="message2Label" runat="server" Width="100%"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="closeLinkButton" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- ********************************************************** End Panel ************************************************************* --%>
    
    <%-- ************************************** Start Ddelete Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="deletePanel" Style="display: none"  Width="40%">
        <asp:UpdatePanel ID="deleteUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Delete Package</div>
                        <div class="panel-body">
                            <div class="col-md-12">

                                <div>
                                    <label>Are you sure to Delete This Package</label><br />
                                    <table class="nav-justified">
                                        <tr>
                                            <td>&nbsp;<asp:LinkButton ID="delete1LinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="delete1LinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Ok</asp:LinkButton></td>
                                            <td>&nbsp;<asp:LinkButton ID="close3LinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" Width="70%" OnClick="close3LinkButton_Click"><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div>
                                        <asp:Label ID="Label1" runat="server" Width="100%"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="closeLinkButton" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- ********************************************************** End Panel ************************************************************* --%>
    
    
    <%-- ************************************** Start Details Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="detailsPanel" Style="display: none" Width="40%">
        <asp:UpdatePanel ID="detailsUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Package Details</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                
                                <div class="row">
                                    <asp:GridView ID="packageDetailsGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="packageIdLabel" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="packageMstIdLabel" Text='<%#Eval("PackageMstId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("ServiceType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="EditLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-primary" OnClick="EditLinkButton_Click"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="deleteLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-danger" OnClick="deleteLinkButton_Click"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                
                                <div class="row">
                                    <div class="col-md-offset-9 col-md-3">
                                    <asp:LinkButton ID="close4LinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" Width="70%" OnClick="close4LinkButton_Click"><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="close4LinkButton" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- ********************************************************** End Panel ************************************************************* --%>


    <div id="divBackground" style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; left: 0; background-color: #7E7E7E; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; -webkit-opacity: 0.8; display: none">
    </div>
    
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#packageSetupDropdown").addClass('in');
        });
    </script>

</asp:Content>
