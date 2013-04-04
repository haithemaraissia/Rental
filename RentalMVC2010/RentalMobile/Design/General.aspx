<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="General.aspx.cs" Inherits="RentalMobile.Design.General" %>

<!DOCTYPE html>
<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!-->
<html lang="en">
<!--<![endif]-->
<head>
    <!-- Basic Page Needs
  	================================================== -->
    <meta charset="utf-8">
    <title>@ViewBag.Title</title>
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Mobile Specific Metas
  	================================================== -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <!-- CSS
  	================================================== -->
    <link rel="stylesheet" href="../css/dotcss/base.css">
    <link rel="stylesheet" href="../css/dotcss/skeleton.css">
    <link rel="stylesheet" href="../css/dotcss/layout.css">
    <!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <!-- JS
	================================================== -->
    <script type="text/javascript" src="../Scripts/js/jquery.min.js"></script>
    <!-- Favicons
	================================================== -->
    <link rel="shortcut icon" href="../images/dotimages/favicon.ico">
    <link rel="apple-touch-icon" href="../images/dotimages/apple-touch-icon.png">
    <link rel="apple-touch-icon" sizes="72x72" href="../images/dotimages/apple-touch-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="114x114" href="../images/dotimages/apple-touch-icon-114x114.png">
    <script src='../WIP/google_analytics_auto.html'></script>
</head>
<body>
    <div class="container page add-bottom">
        <!-- top-bar -->
        <div class="top-bar clearfix">
            <p class="eight columns">
                Sales: 555 912 5412 | Email: <a href="#">info@yourcompany.com</a></p>
            <!-- Language nav -->
            <ul class="nav lang">
                <li><a href="#" class="selected">
                    <img src="../images/dotimages/icons/es.gif" alt="spain" /></a></li>
                <li><a href="#">
                    <img src="../images/dotimages/icons/it.gif" alt="italy" /></a></li>
                <li><a href="#">
                    <img src="../images/dotimages/icons/en.gif" alt="uk" /></a></li>
                <li><a href="#">
                    <img src="../images/dotimages/icons/fr.gif" alt="frence" /></a></li>
                <li><a href="#">
                    <img src="../images/dotimages/icons/us.gif" alt="usa" /></a></li>
            </ul>
            <ul class="menu user">
                <li><a href="/Account/Register" class="register-btn">Register</a></li>
                <li><a href="#" data-dropdown="#login">Login</a></li>
                <li><a href="../Views/Shared/Help">Help</a></li>
            </ul>
        </div>
        <!-- /.top-bar -->
        <header class="row">
            <!-- brand -->
            <h1 class="logo one-half column">
                <a href="">
                    <img src="../images/dotimages/logo.png" alt="Dot. Real State"></a>
            </h1>
            <!-- follow us -->
            <div class="follow">
                <h6 class="half-bottom">
                    <span>Follow us</span></h6>
                <ul class="menu social">
                    <li><a href="#" title="Twitter"><i class="icon-twitter"></i></a></li>
                    <li><a href="#" title="Flickr"><i class="icon-flickr"></i></a></li>
                    <li><a href="#" title="Facebook"><i class="icon-facebook"></i></a></li>
                    <li><a href="#" title="Vimeo"><i class="icon-vimeo"></i></a></li>
                    <li><a href="#" title="Skype"><i class="icon-skype"></i></a></li>
                </ul>
            </div>
            <!-- /.follow -->
            <!-- Main nav-bar -->
            <nav class="menu-bar sixteen columns row">
                <!-- Main menu -->
                <ul class="menu clearfix">
                    <li><a href="">Rent</a></li>
                    <li><a href="">Post</a></li>
                    <li><a href="">Maintain</a></li>
                    <li><a href="">Legal</a></li>
                    <li><a href="">About</a></li>
                    <li><a href="">Contact</a></li>
                </ul>
                <!-- Home btn -->
                <a href="#" class="home-btn" title="Home"><i class="icon-home"></i></a>
            </nav>
            <!-- /.nav-bar -->
        </header>
        <!-- /header-->
        <div class="main" role="main">
            @RenderBody()
        </div>
        <!-- /.main -->
        <ol class="breadcrumbs fourteen columns remove-bottom">
            <li>You are here:</li>
            <li><a href="../../WIP/index.html">Home</a> / Registration</li>
        </ol>
        <a href="#" class="scroll-top fr">Top <i class="icon-up-open"></i></a>
    </div>
    <!-- /.container -->
    <footer class="container">
        <p class="remove-bottom">
            Copyright &copy; DOT Real State. All Rights Reserved. Designed by Coralix Themes.</p>
        <p>
            Vivamus cursus eget Phasellus quis wisi.</p>
    </footer>
    <!-- /footer -->
    <div id="login" class="dropdown dropdown-tip dropdown-anchor-right">
        <div class="dropdown-panel">
            <img src="../images/dotimages/logo-small.png" alt="//" />
            <form action="#" class="remove-bottom login-form">
            <label>
                <span class="entypo"><i class="icon-user"></i></span>
                <input type="text" class="inline remove-bottom" placeholder="USER">
            </label>
            <label class="half-bottom">
                <span class="entypo"><i class="icon-lock"></i></span>
                <input type="password" class="inline remove-bottom" placeholder="PASSWORD">
            </label>
            <input type="submit" value="Log in" class="remove-bottom full-width">
            </form>
            <!-- Addition for the Profile-->
            <a href="../Views/Tenant">Tenant</a>
        </div>
    </div>
    <!-- Scripts & Plugins
  ================================================== -->
    <!-- jQuery dropdown -->
    <script src="../Scripts/js/jquery.dropdown.js" type="text/javascript"></script>
    <!-- Tipsy -->
    <script src="../Scripts/js/jquery.tipsy.js" type="text/javascript"></script>
    <!-- Custom Scripts -->
    <script src="../Scripts/js/scripts.js" type="text/javascript"></script>
    ================================================== -->
    <!-- BX Slider -->
    <script src="../Scripts/js/jquery.bxslider.min.js" type="text/javascript"></script>
    <!-- Tabs -->
    <script src="../Scripts/js/jquery.easytabs.min.js" type="text/javascript"></script>
    <!-- Gallery switcher -->
    <script src="../Scripts/js/switcher.js" type="text/javascript"></script>
    <!-- jQuery dropdown -->
    <script src="../Scripts/js/jquery.dropdown.js" type="text/javascript"></script>
    <!-- Tipsy -->
    <script src="../Scripts/js/jquery.tipsy.js" type="text/javascript"></script>
    <!-- jQuery Custom selects-->
    <script src="../Scripts/js/jquery.customSelect.min.js" type="text/javascript"></script>
    <!-- Custom Scripts -->
    <script src="../Scripts/js/scripts.js" type="text/javascript"></script>
    <script type="text/javascript">
		
		$(document).ready(function($) {
			
			// Init bx slider 
			$('.bxslider').bxSlider({
	  			mode: 'fade',
	  			captions: true,
	  			pagerCustom: '#bx-pager',
	  			auto: true,
	  		});

	  		//Easy tabs
	  		$('.tab-container').easytabs();

	  		//Custom selects
			$('.custom-select').customSelect();
			
		

	  	});
	  	
  	</script>
</body>
</html>

<asp:login runat="server">
    <LayoutTemplate>
        <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
            <tr>
                <td>
                    <table cellpadding="0">
                        <tr>
                            <td align="center" colspan="2">
                                Log In</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="ctl02">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." ValidationGroup="ctl02">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                                    ValidationGroup="ctl02" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:login>
