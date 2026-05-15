<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatabaseBackupForm.aspx.cs" Inherits="HospitalBilling.UI.DatabaseBackupForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-hospital-o" aria-hidden="true"></i></a></li>
            <li class="active">Database Backup</li>
        </ol>
    </div>
    
    <div class="row">
        <div class="col-md-offset-3 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">Database Backup</div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:Button ID="dbBackupButton" runat="server" CssClass="btn btn-primary" Text="Backup" OnClick="dbBackupButton_Click" />
                    </div>

                    <div class="form-group">
                        <asp:Label ID="messageLabel" runat="server" CssClass="control-label" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
