<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmBedStatus.aspx.cs" Inherits="HospitalBilling.UI.frmBedStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       
 
  
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

 

   
            <div class="container">

                <div class="panel-group">

                    <div class="row">
                       
                         <div class="col-md-5">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Ward Bed Staus</div>
                                <div class="panel-body">
                                    <asp:PlaceHolder ID="WardPlaceHolder" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-5">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Cabin Bed Staus</div>
                                <div class="panel-body">
                                    <asp:PlaceHolder ID="CabinPlaceHolder" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>

                       

                    </div>
                  

                </div>
            </div>

     
   
</asp:Content>
