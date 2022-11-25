<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_5.Deklaratyvusis_programavimas.WebForm1" %>

<link href="StyleSheet1.css" rel="stylesheet" type="text/css"/>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Nuskaityti duomenis" CssClass="newStyle2" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Atlikti skaičiavimus" CssClass="newStyle2" EnableTheming="True" OnClick="Button2_Click"/>
            <br />
            <asp:Label ID="Label1" runat="server" CssClass="newStyle1"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" CssClass="newStyle3" Font-Size="X-Small" Height="400px" TextMode="MultiLine" Width="900px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <asp:Table ID="Table1" runat="server" GridLines="Both">
            </asp:Table>
            <br />
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <asp:Table ID="Table2" runat="server" GridLines="Both">
            </asp:Table>
            <br />
            <asp:Label ID="Label4" runat="server"></asp:Label>
            <asp:Table ID="Table3" runat="server" GridLines="Both">
            </asp:Table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
