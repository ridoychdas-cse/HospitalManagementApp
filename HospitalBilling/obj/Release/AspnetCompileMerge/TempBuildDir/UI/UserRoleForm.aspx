<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRoleForm.aspx.cs" Inherits="HospitalBilling.UI.UserRoleForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:HiddenField ID="idHiddenField" runat="server" />
    
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">User Role Setup</li>
        </ol>
    </div>
    
    

    <div class="row">
        <div class="col-md-offset-3 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">User Role</div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="form-group">
                            <label>Name</label><strong style="color: red; font-size: 18px">*</strong>
                            <asp:TextBox ID="nameTextBox" CssClass="form-control" runat="server" placeholder="Role Name"></asp:TextBox>
                        </div>
                        
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="descriptionTextBox" CssClass="form-control" runat="server" placeholder="Role Description"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <div class="col-md-3">
                                <asp:Button ID="saveButton" Width="90%" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="saveButton_Click" />
                            </div>

                            <div class="col-md-3">
                                <asp:Button ID="updateButton" Width="90%" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="updateButton_Click" />
                            </div>

                            <div class="col-md-3">
                                <asp:Button ID="deleteButton" Width="90%" CssClass="btn btn-primary" runat="server" Text="Delete" OnClick="deleteButton_Click" />
                            </div>
                            
                            <div class="col-md-3">
                                <asp:Button ID="reloadButton" Width="90%" CssClass="btn btn-primary" runat="server" Text="Reload" OnClick="reloadButton_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
    
    <div class="row">
        <div class="col-md-offset-3 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">User Role List</div>
                <div class="panel-body">
                    <asp:GridView ID="roleGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333" OnSelectedIndexChanged="organizationGridView_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />

                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="idLabel" Text='<%#Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemStyle HorizontalAlign="Left" Width="80%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Name") %>'></asp:Label>
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
    
    

    <script type="text/javascript">
        $(document).ready(function () {
            $("#administrationDropdown").addClass('in');
        });
    </script>
</asp:Content>
