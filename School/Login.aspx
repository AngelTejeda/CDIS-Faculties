<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="School.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="css/forms.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet" />
</head>
<body>
    
    <form id="formLogin" runat="server">

        <div class="form center">
            <%-- Image --%>
            <div class="imgcontainer">
                <div id="imgLogin"></div>
            </div>

            <%-- Username --%>
            <div class="inputContainer">
                <label for="username"><b>Username</b></label>
                <asp:TextBox class="formInput" ID="txtUser" name="username" runat="server"></asp:TextBox>

                <asp:RequiredFieldValidator class="formValidation" ID="rfvUser" runat="server"
                    ControlToValidate="txtUser" ValidationGroup="vgLogin"
                    Display="Dynamic" ErrorMessage="El usuario es requerido."></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator class="formValidation" ID="revUser" runat="server"
                    ControlToValidate="txtUser" ValidationGroup="vgLogin" ValidationExpression="[a-zA-Z0-9_-]{1,}" 
                    Display="Dynamic" ErrorMessage="Únicamente letras, números y los caracteres '-' y '_' están permitidos."></asp:RegularExpressionValidator>
            </div>

            <%-- Password --%>
            <div class="inputContainer">
                <label for="password"><b>Password</b></label>
                <asp:TextBox class="formInput" ID="txtPassword" name="password" TextMode="Password" runat="server"></asp:TextBox>

                <asp:RequiredFieldValidator class="formValidation" ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" ValidationGroup="vgLogin"
                    Display="Dynamic" ErrorMessage="La contraseña es requerida."></asp:RequiredFieldValidator>
            </div>

            <%-- Button --%>
            <div class="inputContainer">
                <asp:Button ValidationGroup="vgLogin" class="formInput" ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click"/>
            </div>
        </div>

    </form>

</body>
</html>