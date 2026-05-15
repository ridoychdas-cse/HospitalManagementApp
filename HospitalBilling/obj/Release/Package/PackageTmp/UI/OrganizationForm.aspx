<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrganizationForm.aspx.cs" Inherits="HospitalBilling.UI.OrganizationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        function SetImage() {
            document.getElementById('<%=lbImgUpload.ClientID %>').click();
         }
     </script>
    

    <asp:HiddenField ID="idHiddenField" runat="server" />
    
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Organization Setup</li>
        </ol>
    </div>
    
    

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">Organization</div>
                <div class="panel-body">
                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>Name</label><strong style="color: red; font-size: 18px">*</strong>
                                    <asp:TextBox ID="orgNameTextBox" CssClass="form-control" runat="server" placeholder="Organization Name"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Short Name</label>
                                    <asp:TextBox ID="orgShortNameTextBox" CssClass="form-control" runat="server" placeholder="Organization Short Name"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Phone No</label><strong style="color: red; font-size: 18px">*</strong>
                                    <asp:TextBox ID="phoneNoTextBox" CssClass="form-control" runat="server" placeholder="Organization Phone No"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="emailTextBox" CssClass="form-control" runat="server" placeholder="Organization Email"></asp:TextBox>
                                </div>
                                
                                <div class="form-group">
                                <label>Patient Image</label>
                                <asp:FileUpload ID="imageFileUpload" onchange="javascript:SetImage();" CssClass="form-control input-sm" runat="server" />
                                <asp:Button ID="lbImgUpload" runat="server" Text="Upload" Font-Size="8pt" Style="display: none"
                                    Width="50px" Height="20px" OnClick="lbImgUpload_Click"></asp:Button>
                            </div>

                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>

                    <div class="col-md-5">
                        <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Address</label><strong style="color: red; font-size: 18px">*</strong>
                                <asp:TextBox ID="addressTextBox" CssClass="form-control" runat="server" placeholder="Organization Address"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Organization Speech</label>
                                <asp:TextBox ID="speechTextBox" CssClass="form-control" runat="server" placeholder="Organization Speech"></asp:TextBox>
                            </div>
                            
                            <div class="form-group">
                                <label>Description</label>
                                <asp:TextBox ID="descriptionTextBox" CssClass="form-control" runat="server" placeholder="Organization Description" Height="100px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    </div>
                    <br/>
                    <div class="col-md-offset-2 col-md-8">
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
        <div class=" col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">Organization Details</div>
                <div class="panel-body">
                    <asp:GridView ID="organizationGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333" OnSelectedIndexChanged="organizationGridView_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />

                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="idLabel" Text='<%#Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
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

                            <asp:TemplateField HeaderText="Phone No">
                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("PhoneNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Email">
                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Address">
                                <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Address") %>'></asp:Label>
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
            $("#medicalSetupDropdown").addClass('in');
        });
    </script>
</asp:Content>
