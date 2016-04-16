<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="nc.UserItems.Test" %>

<%@ Register Src="~/Controls/userPosts.ascx" TagPrefix="uc1" TagName="userPosts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:userPosts runat="server" id="userPosts" />
    </form>
</body>
</html>