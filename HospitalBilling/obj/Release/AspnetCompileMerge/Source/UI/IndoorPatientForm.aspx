<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndoorPatientForm.aspx.cs" Inherits="HospitalBilling.UI.IndoorPatientForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script type="text/javascript">
        //$(function () {
        //    $('#entryDateTextBox').datepicker({ dateFormat: 'yyy-dd-mm HH:MM:ss' }).val();
        //});
    </script>--%>

     <script type="text/javascript">
         function SetImage() {
             document.getElementById('<%=lbImgUpload.ClientID %>').click();
        }
     </script>

    <asp:HiddenField ID="idHiddenField" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Indoor Patient</li>
        </ol>
    </div>

    <div class="row">
        <div class="col-md-8 col-lg-8">
            <div class="panel panel-default">
                <div class="panel-heading">Patient Info</div>
                <div class="panel-body">
                    <div class="col-md-6">
                        <div role="form">

                            <div class="form-group">
                                <label>Patient Id <strong style="color: red; font-size: 15px">*</strong></label>
                                <div class="input-group" style="border: 1px solid black; border-radius: 5px;">
                                    <asp:TextBox ID="patientIdTextBox" CssClass="form-control input-sm" runat="server" placeholder="Patient ID"></asp:TextBox>

                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="searchLinkButton" CssClass="btn btn-info btn-sm" runat="server" Height="25px" OnClick="searchLinkButton_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

                                    </div>
                                </div>
                            </div>


                            <div class="form-group">
                                <label>Patient Name <strong style="color: red; font-size: 15px">*</strong></label>
                                <asp:TextBox ID="nameTextBox" CssClass="form-control input-sm" runat="server" placeholder="Patient Name" Height="30px"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Gender</label>
                                <asp:RadioButton ID="maleRadioButton" CssClass="radio-inline" runat="server" Text="Male" GroupName="Gender" Checked="True" />
                                <asp:RadioButton ID="femaleRadioButton" CssClass="radio-inline" runat="server" Text="Female" GroupName="Gender" />
                                <asp:RadioButton ID="otherRadioButton" CssClass="radio-inline" runat="server" Text="Other" GroupName="Gender" />
                            </div>

                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Age</label>
                                        <asp:TextBox ID="ageTextBox" CssClass="form-control input-sm" runat="server" placeholder="Age" Height="30px"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-7">
                                    <div class="form-group">
                                        <label>Blood Group <strong style="color: red; font-size: 15px">*</strong></label>
                                        <asp:DropDownList ID="bloodGroupDropDownList" CssClass="form-control input-sm" runat="server" Height="30px"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label>Phone No</label>
                                <asp:TextBox ID="phoneNoTextBox" CssClass="form-control input-sm" runat="server" placeholder="Patient Phone No" Height="30px"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Address</label>
                                <asp:TextBox ID="addressTextBox" CssClass="form-control" runat="server" placeholder="Patient Address" Height="60px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>Division <strong style="color: red; font-size: 15px">*</strong></label>
                                <asp:DropDownList runat="server" ID="divisionDropdown" CssClass="form-control input-sm"  Height="30px" AutoPostBack="True" OnSelectedIndexChanged="divisionDropdown_SelectedIndexChanged" ></asp:DropDownList>                              
                            </div>
                             <div class="form-group">
                                <label>District</label>
                                <asp:DropDownList runat="server" ID="DistrictsDropdown" CssClass="form-control input-sm"  Height="30px" AutoPostBack="True" OnSelectedIndexChanged="DistrictsDropdown_SelectedIndexChanged" ></asp:DropDownList> 
                            </div>
                        </div>
                    </div>


                    <div class="col-md-6">
                        <div role="form">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Image ID="patientImage" CssClass="form-control " runat="server" Height="175px" Width="170" ImageUrl="../image/images.png" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Patient Image</label>
                                <asp:FileUpload ID="imageFileUpload" onchange="javascript:SetImage();" CssClass="form-control input-sm" runat="server" />
                                <asp:Button ID="lbImgUpload" runat="server" Text="Upload" Font-Size="8pt" Style="display: none"
                                    Width="50px" Height="20px" OnClick="lbImgUpload_Click"></asp:Button>
                            </div>
                            <div class="form-group">
                                <label>Admit Date <strong style="color: red; font-size: 15px">*</strong></label>
                                <div class="input-group">
                                    <asp:TextBox ID="entryDateTextBox" CssClass="form-control input-sm" runat="server" Height="30px"></asp:TextBox>
                                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="entryDateTextBox" Format="dd-MM-yyyy" runat="server" />--%>
                                    <%--  <asp:TextBox ID="timeTextBox" runat="server"></asp:TextBox>--%>
                                </div>

                            </div>
                            <div class="form-group">
                                <label>Remark</label>
                                <asp:TextBox ID="remarkTextBox" CssClass="form-control input-sm" runat="server" placeholder="Remark" Height="30px"></asp:TextBox>
                            </div>
                            
                             <div class="form-group">
                                <label>Union/Thana</label>
                                <asp:DropDownList runat="server" ID="ThanaDropdown" CssClass="form-control input-sm"  Height="30px"></asp:DropDownList> 
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="col-md-4 col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">Guardian Info</div>
                <div class="panel-body">
                    <div role="form">
                        <div class="form-group">
                            <label>Guardian Name <strong style="color: red; font-size: 15px">*</strong></label>
                            <asp:TextBox ID="gNameTextBox" CssClass="form-control input-sm" runat="server" placeholder="Guardine Name" Height="30px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Phone No</label>
                            <asp:TextBox ID="gPhoneNoTextBox" CssClass="form-control input-sm" runat="server" placeholder="Guardine Phone No" Height="30px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Gender</label>
                            <asp:RadioButton ID="gMaleRadioButton" CssClass="radio-inline" runat="server" Text="Male" GroupName="GGender" Checked="True" />
                            <asp:RadioButton ID="gFemaleRadioButton" CssClass="radio-inline" runat="server" Text="Female" GroupName="GGender" />
                            <asp:RadioButton ID="gOtherRadioButton" CssClass="radio-inline" runat="server" Text="Other" GroupName="GGender" />
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Age</label>
                                    <asp:TextBox ID="gAgeTextBox" CssClass="form-control input-sm" runat="server" placeholder="Age" Height="30px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>Relation <strong style="color: red; font-size: 15px">*</strong></label>
                                    <%--<asp:TextBox ID="relationTextBox" CssClass="form-control input-sm" runat="server" placeholder="Relation" Height="30px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="relationDropDownList" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label>Address</label>
                            <asp:TextBox ID="gAddressTextBox" CssClass="form-control" runat="server" placeholder="Guardine Address" Height="60px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-md-4 col-lg-4">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div role="form">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Consultant</label>
                                <asp:DropDownList ID="consultantDropDownList" CssClass="form-control input-sm" runat="server" Height="30px"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Reference By</label>
                                <asp:DropDownList ID="referanceByDropDownList" CssClass="form-control input-sm" runat="server" Height="30px"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <br />
                            <asp:Button ID="newReferenceButton" CssClass="btn btn-sm" runat="server" Text="New Ref" />
                        </div>

                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Status <strong style="color: red; font-size: 15px">*</strong></label>
                                <asp:DropDownList ID="statisDropDownList" CssClass="form-control input-sm" runat="server" Height="30px"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-8 col-lg-8">
            <div class="panel panel-default">

                <div class="panel-body">
                    <asp:UpdatePanel ID="roomSelectUpdatePanel" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div role="form">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Bed Type <strong style="color: red; font-size: 15px">*</strong></label>
                                        <asp:DropDownList ID="roomDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="roomDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div id="WardUpdatePanel" runat="server">
                                        <div class="form-group">
                                            <label>Ward Name <strong style="color: red; font-size: 15px">*</strong></label>
                                            <asp:DropDownList ID="wardDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="wardDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Bed Name <strong style="color: red; font-size: 15px">*</strong></label>
                                            <asp:DropDownList ID="bedDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="bedDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div id="cabinUpdatePanel" runat="server">
                                        <div class="form-group">
                                            <label>Cabin Type <strong style="color: red; font-size: 15px">*</strong></label>
                                            <asp:DropDownList ID="cabinTypeDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cabinTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Cabin Name <strong style="color: red; font-size: 15px">*</strong></label>
                                            <asp:DropDownList ID="cabinDropDownList" CssClass="form-control input-sm" runat="server" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cabinDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Price (Daily)</label>
                                        <asp:UpdatePanel runat="server" ID="priceUpdatePanel" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="priceTextBox" CssClass="form-control input-sm" runat="server" Height="30px" ReadOnly="True">0</asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>


                                    <div class="form-group">
                                        <label>Advance Payment</label>
                                        <asp:TextBox ID="advancePaymentTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>


                                    <div class="form-group">
                                        <label>Payment Methode:</label>
                                        <asp:DropDownList ID="paymentTypeDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="paymentTypeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div id="paymentTypeDiv" runat="server">
                                        <div class="form-group">
                                            <label>Bank Name:</label>
                                            <asp:TextBox ID="bankNameTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Cheque No:</label>
                                            <asp:TextBox ID="chequeTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Cheque Date:</label>
                                            <asp:TextBox ID="chequeDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <div>
        <div class="form-group">
            <div class="col-md-offset-1 col-md-3">
                <asp:LinkButton ID="saveLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="saveLinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Save</asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="resetLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="resetLinkButton_Click"><i class="fa fa-eraser" aria-hidden="true"></i> Reset</asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="refressLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="clearLinkButton_Click"><i class="fa fa-refresh" aria-hidden="true"></i> Clear</asp:LinkButton>
            </div>

            <div class="col-md-2">
                <asp:LinkButton ID="reportLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="reportLinkButton_Click"><i class="fa fa-file" aria-hidden="true"></i> Report</asp:LinkButton>
            </div>
        </div>
        <br />
        <div>
            <asp:Label ID="messageLabel" runat="server" Width="100%"></asp:Label>
        </div>
    </div>
    
    
    <%-- **************************************** Ajax Modal Popup************************************* --%>

    <ajaxToolkit:ModalPopupExtender runat="server" ID="diagnosisPopup" TargetControlID="newReferenceButton" BackgroundCssClass="modalBackground"
        PopupControlID="diagnosisTypePanel" CancelControlID="closeLinkButton" />
    
    <%-- ************************************** Start Panel Style="display: none"************************************************ --%>
    <asp:Panel runat="server" ID="diagnosisTypePanel" Width="40%">
        <asp:UpdatePanel ID="dignososTypeUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Ref By Setup</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div role="form">
                                    <asp:HiddenField ID="refByHiddenField" runat="server" />

                                    <div class="form-group">
                                        <label>Ref by Name <strong style="color: red; font-size: 18px">*</strong></label>
                                            <asp:TextBox ID="refNameTextBox" runat="server" class="form-control" placeholder="Ref By Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div>

                                    <table class="nav-justified">
                                        <tr>
                                            <td>&nbsp;<asp:LinkButton ID="refSaveLinkButton" runat="server" CssClass="btn btn-primary" Width="70%" OnClick="refSaveLinkButton_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Save</asp:LinkButton></td>
                                            <td>&nbsp;<asp:LinkButton ID="closeLinkButton" runat="server" OnClientClick="HideModalDiv();" CssClass="btn btn-primary" Width="70%" OnClick="closeLinkButton_Click" ><i class="fa fa-window-close-o" aria-hidden="true"></i> Close</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div>
                                        <asp:Label ID="refMessageLabel" runat="server" Width="100%"></asp:Label>
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

    <script type="text/javascript">
        $(document).ready(function () {
            $("#indoorPatientDropdown").addClass('in');
        });
    </script>


</asp:Content>


