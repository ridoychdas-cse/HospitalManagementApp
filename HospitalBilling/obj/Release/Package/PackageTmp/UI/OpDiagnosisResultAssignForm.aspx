<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpDiagnosisResultAssignForm.aspx.cs" Inherits="HospitalBilling.UI.OpDiagnosisResultAssignForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Outdoor Patient Diagnosis Assign Form</li>
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
                                    <label class="col-md-2 col-sm-3 control-label">Bill No:</label>
                                    <div class="col-md-4 col-sm-4 ">
                                        <asp:TextBox ID="BillNoTextBox" CssClass="form-control input-sm" runat="server" placeholder="Search By Bill No"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-primary btn-sm" runat="server" OnClick="searchLinkButton_Click" ><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:Button ID="reportButton" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="reportButton_Click"/>
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
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Diagnosis List</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="diagnosisBillListUpdatePanel" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="diagnosisBillGridView" runat="server" AutoGenerateColumns="False" CssClass="table" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>

                                            <asp:TemplateField Visible="False" HeaderText="DtlId">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="DtlId" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="False" HeaderText="DiagnosisTypeId">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="diagnosisTypeIdLabel" Text='<%#Eval("DiagnosisTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Diagnosis Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("DiagnosisTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="60%" />
                                                <ItemStyle Width="60%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="False" HeaderText="DiagnosisId">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="diagnosisIdLabel" Text='<%#Eval("DiagnosisId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Diagnosis">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("DiagnosisName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="Result">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label" ></asp:Label>
                                                    <asp:TextBox ID="ResultValueTextBox" Width="100%" runat="server" Text='<%#Eval("ResultValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                                <ItemStyle Width="20%" />
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
                 <div class="col-md-12 col-lg-12"">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="saveAssignButton" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="saveAssignButton_Click" />
                        </div>
                    </div>
            </div>
        
        </div>

        <%-- ******************************** Right Side****************************** --%>
        <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading"></div>
                <div class="panel-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <br />
                          
                            <%--  <asp:Button ID="saveAssignButton" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="saveAssignButton_Click" />--%>
                        </div>
                    </div>
                    
                    
                </div>
            </div>
        </div>
    </div>
    
 
    

    <script type="text/javascript">
        $(document).ready(function () {
            $("#outdoorPatientDropdown").addClass('in');
        });
    </script>



</asp:Content>
