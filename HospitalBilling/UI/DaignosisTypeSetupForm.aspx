<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DaignosisTypeSetupForm.aspx.cs" Inherits="HospitalBilling.UI.DaignosisTypeSetupForm" %>

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
            <li class="active">Daignosis Type Setup</li>
        </ol>
    </div>

    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                   <div class="form-inline">
                        <asp:LinkButton ID="createNewLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-default btnDefaultOverride"><i class="fa fa-plus-square-o" aria-hidden="true"></i> Create New</asp:LinkButton>
                        <asp:TextBox ID="searchTextBox" CssClass="form-control" runat="server" placeholder="Search by Name or ShortName"></asp:TextBox>
                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-default" runat="server" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">Daignosis Type List</div>
                <div class="panel-body">
                    <asp:GridView ID="diagnosisTypeGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="idLabel" Text='<%#Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type Name">
                                <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Short Name">
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("ShortName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Daignosis Details">
                                <ItemStyle HorizontalAlign="Center" Width="545%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Details") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-primary" OnClick="EditLinkButton_Click"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete" Visible="False">
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="deleteLinkButton" runat="server" OnClientClick="LoadModalDiv();" CssClass="btn btn-danger" OnClick="deleteLinkButton_Click"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
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

    <%-- **************************************** Ajax Modal Popup************************************* --%>

    <ajaxToolkit:ModalPopupExtender runat="server" ID="diagnosisPopup" TargetControlID="createNewLinkButton" BackgroundCssClass="modalBackground"
        PopupControlID="diagnosisTypePanel" CancelControlID="closeLinkButton" />

    <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="idHiddenField" ID="editModalPopupExtender" BackgroundCssClass="modalBackground"
        PopupControlID="editPanel" CancelControlID="close2LinkButton" />

    <ajaxToolkit:ModalPopupExtender runat="server" TargetControlID="idHiddenField" ID="deleteModalPopupExtender" BackgroundCssClass="modalBackground"
        PopupControlID="deletePanel" CancelControlID="close3LinkButton" />

    <%-- ************************************** Start Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="diagnosisTypePanel" Style="display: none" Width="40%">
        <asp:UpdatePanel ID="dignososTypeUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Diagnosis Type Setup</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div role="form">
                                    <div class="form-group">
                                        <label>Type Name <strong style="color: red; font-size: 18px">*</strong></label>
                                            <asp:TextBox ID="nameTextBox" runat="server" class="form-control" placeholder="Diagnosis Type Name"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Short Name</label>
                                        <asp:TextBox ID="shortNameTextBox" runat="server" class="form-control" placeholder="Short Name"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Deatils</label>
                                        <asp:TextBox ID="detailsTextBox" runat="server" class="form-control" placeholder="Department Details" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div>

                                    <table class="nav-justified">
                                        <tr>
                                            <td>&nbsp;<asp:LinkButton ID="saveLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="saveLinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Save</asp:LinkButton></td>
                                            <td>&nbsp;<asp:LinkButton ID="clearLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="clearLinkButton_Click"><i class="fa fa-refresh" aria-hidden="true"></i> Clear</asp:LinkButton></td>
                                            <td>&nbsp;<asp:LinkButton ID="closeLinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" Width="70%" OnClick="closeLinkButton_Click"><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton></td>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="closeLinkButton" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- ********************************************************** End Panel ************************************************************* --%>


    <%-- ************************************** Start Edit Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="editPanel" Style="display: none" Width="40%">
        <asp:UpdatePanel ID="editUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Edit Diagnosis Type</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div role="form">
                                    <div class="form-group">
                                        <label>Type Name <strong style="color: red; font-size: 18px">*</strong></label>
                                            <asp:TextBox ID="editNameTextBox" runat="server" class="form-control" placeholder="Department Name"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Short Name</label>
                                        <asp:TextBox ID="editShortNameTextBox" runat="server" class="form-control" placeholder="Short Name"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Deatils</label>
                                        <asp:TextBox ID="editDetailsTextBox" runat="server" class="form-control" placeholder="Department Details" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div>

                                    <table class="nav-justified">
                                        <tr>
                                            <td>&nbsp;<asp:LinkButton ID="updateLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="updateLinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Update</asp:LinkButton></td>
                                            <td>&nbsp;<asp:LinkButton ID="clear2LinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="clearLinkButton_Click"><i class="fa fa-refresh" aria-hidden="true"></i> Clear</asp:LinkButton></td>
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
    <asp:Panel runat="server" ID="deletePanel" Style="display: none" Width="40%">
        <asp:UpdatePanel ID="deleteUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Delete Diagnosis Type</div>
                        <div class="panel-body">
                            <div class="col-md-12">

                                <div>
                                    <label>Are you sure to Delete This Diagnosis</label><br />
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


    <div id="divBackground" style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; left: 0; background-color: #7E7E7E; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; -webkit-opacity: 0.8; display: none">
    </div>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#daignosisSetupDropdown").addClass('in');
        });
    </script>
</asp:Content>

