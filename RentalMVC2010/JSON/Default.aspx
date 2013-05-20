<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:label ID="Label1" runat="server" text="Address"></asp:label>
    <asp:TextBox ID="Address" runat="server" Width="279px"></asp:TextBox>
    <br />
    <asp:label ID="Label2" runat="server" text="City"></asp:label>
    <asp:TextBox ID="City" runat="server" Width="280px"></asp:TextBox>
    <br />
    <asp:label ID="Label3" runat="server" text="State"></asp:label>
    <asp:TextBox ID="State" runat="server" Width="222px"></asp:TextBox>
    <br />
        <asp:label ID="Label4" runat="server" text="Country"></asp:label>
    <asp:TextBox ID="Country" runat="server" Width="184px"></asp:TextBox>
    <br />
    <asp:label runat="server" text="Zipcode"></asp:label>
    <asp:TextBox ID="Zipcode" runat="server" Width="223px"></asp:TextBox>
    <br />
    
        <asp:Button ID="Check" runat="server" Text="Button" onclick="Check_Click" />

    </div>
    </form>
</body>
</html>
