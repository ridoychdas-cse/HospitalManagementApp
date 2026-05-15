<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="HospitalBilling.LoginForm" %>

<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Login</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
	<link href="css/datepicker3.css" rel="stylesheet"/>
	<link href="css/styles.css" rel="stylesheet"/>
	<link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
<!--[if lt IE 9]>
<script src="../js/html5shiv.js"></script>
<script src="../js/respond.min.js"></script>
<![endif]-->

</head>

<body class="backgrountImage" >
    <form runat="server">
        <div class="row">
            <div class="col-xs-8 col-xs-offset-2 col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">Log in</div>
                    <div class="panel-body">
                        <div role="form">
                            <fieldset>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-user-circle-o fa-fw"></i></span>
                                    <asp:TextBox ID="userNameTextBox" CssClass="form-control" runat="server" placeholder="User Name"></asp:TextBox>
                                </div>
                                <br />
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-key fa-fw"></i></span>
                                    <asp:TextBox ID="passwordTextBox" CssClass="form-control" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input name="remember" type="checkbox" value="Remember Me">Remember Me
                                    </label>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 col-sm-3">
                                        <asp:Button ID="loginButton" CssClass="btn btn-primary" runat="server" Text="Login" OnClick="loginButton_Click" />
                                    </div>

                                    <div class="col-md-10 col-sm-9">
                                        <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>

                                <%--<a href="index.html" class="btn btn-primary">Login</a>--%>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.col-->
        </div>
        <!-- /.row -->
    </form>
		

	<script src="../js/jquery-1.11.1.min.js"></script>
	<script src="../js/bootstrap.min.js"></script>
<%--	<script src="../js/chart.min.js"></script>
	<script src="../js/chart-data.js"></script>
	<script src="../js/easypiechart.js"></script>
	<script src="../js/easypiechart-data.js"></script>
	<script src="../js/bootstrap-datepicker.js"></script>--%>
	<script>
		!function ($) {
			$(document).on("click","ul.nav li.parent > a > span.icon", function(){		  
				$(this).find('em:first').toggleClass("glyphicon-minus");	  
			}); 
			$(".sidebar span.icon").find('em:first').addClass("glyphicon-plus");
		}(window.jQuery);

		$(window).on('resize', function() {
			if ($(window).width() > 768) $('#sidebar-collapse').collapse('show');
		});
		$(window).on('resize', function() {
			if ($(window).width() <= 767) $('#sidebar-collapse').collapse('hide');
		});
	</script>	
</body>

</html>
