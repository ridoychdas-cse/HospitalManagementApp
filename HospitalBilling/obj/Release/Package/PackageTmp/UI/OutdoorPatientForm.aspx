<%@Page Title="Outdoor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OutdoorPatientForm.aspx.cs" Inherits="HospitalBilling.UI.OutdoorPatientForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Outdoor Patient</li>
        </ol>
    </div>

    <div class="row">
        <div class="col-md-4 col-lg-4 col-md-offset-2 col-lg-offset-2">
            <div class="panel panel-default">
                <div class="panel-heading">Patient Info</div>
                <div class="panel-body">
                    <div role="form">
                        <div class="form-group">
                            <label>Patient Id <strong style="color: red; font-size: 18px">*</strong></label>
                              <asp:TextBox ID="patientIdTextBox" runat="server" class="form-control" placeholder="Patient Id" ReadOnly="True"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Name <strong style="color: red; font-size: 18px">*</strong></label>
                              <asp:TextBox ID="nameTextBox" runat="server" class="form-control" placeholder="Patient Name"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Phone No</label>
                            <asp:TextBox ID="phoneNoTextBox" CssClass="form-control" runat="server" placeholder="Patient Phone No" Height="30px"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Gender</label>   
                                         
                        </div>
                        <div class="form-group">
                            <asp:RadioButton ID="maleRadioButton" CssClass="radio-inline" runat="server" Text="Male" GroupName="Gender" Checked="True" />
                            <asp:RadioButton ID="femaleRadioButton" CssClass="radio-inline" runat="server" Text="Female" GroupName="Gender" />
                            <asp:RadioButton ID="otherRadioButton" CssClass="radio-inline" runat="server" Text="Other" GroupName="Gender" /> 
                        </div>

                        <div class="form-group">
                            <label>Age</label>
                            <asp:TextBox ID="ageTextBox" CssClass="form-control" runat="server" placeholder="Age" Height="30px"></asp:TextBox>
                        </div>
                          <div class="form-group">
                            <label>Division</label>
                              
                            <asp:DropDownList runat="server" ID="divisionDropdown" CssClass="form-control"  Height="30px" AutoPostBack="True" OnSelectedIndexChanged="divisionDropdown_SelectedIndexChanged"></asp:DropDownList>
                            
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
        
        <div class="col-md-4 col-sm-4">
            <div class="panel panel-default">
                <div class="panel-heading"></div>
                <div class="panel-body">
                    <div role="form">
                        
                        <div class="form-group">
                            <label>Entry Date <strong style="color: red; font-size: 18px">*</strong></label>
                            <asp:TextBox ID="entryDateTextBox" CssClass="form-control" runat="server" placeholder="EntryDate" Height="30px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="entryDateCalendarExtender" TargetControlID="entryDateTextBox" Format="dd-MM-yyyy" runat="server" />
                        </div>
               


                        <div class="form-group">
                            <label>Department</label>
                            <br />
                            <asp:DropDownList ID="departmentDropDownList" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                        </div>
                      
                        <div class="form-group">
                            <label>Consultant</label>
                            <asp:DropDownList ID="consultantDropDownList" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Reference By</label>
                            <asp:DropDownList ID="referanceByDropDownList" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                        </div>
                           
                            <div class="form-group">
                            <label>Districts</label>
                            <asp:DropDownList ID="DistrictsDropdown" CssClass="form-control" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="DistrictsDropdown_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                            <div class="form-group">
                            <label>Union/Thana</label>
                            <asp:DropDownList ID="ThanaDropdown" CssClass="form-control" runat="server" Height="30px"></asp:DropDownList>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                </div>
                             <div class="col-md-2">
                                 </div>
                            <div class="col-md-4">
                                <asp:Button ID="saveButton" runat="server" cssclass="btn btn-primary" Text="Save" OnClick="saveButton_Click" Width="80px" />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="resetButton" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="resetButton_Click" Width="80px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
    <div class="row">
        <div class="col-md-8 col-lg-8 col-md-offset-2 col-lg-offset-2">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div role="form">
                        <div class="form-group">
                            <asp:Label ID="messageLabel" runat="server" CssClass="control-label" Text="" Width="100%"></asp:Label>
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
