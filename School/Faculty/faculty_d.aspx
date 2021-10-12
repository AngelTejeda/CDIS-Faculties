<%@ Page Title="Eliminar Facultad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="faculty_d.aspx.cs" Inherits="School.Faculties.Faculty_d" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%-- Styles --%> 
    <link href="../css/faculty_d.css" rel="stylesheet" />
    <style>
        .formInput {
            width: 500px;
        }
    </style>


    <%-- Content --%>
    <div class="fieldContainer">

        <div class="field">
            <label for="id"><b>ID de Facultad:</b></label>
            <asp:Label name="id" ID="lblId" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="code"><b>Código de Facultad:</b></label>
            <asp:Label name="code" ID="lblCode" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="name"><b>Nombre de Facultad:</b></label>
            <asp:Label nmae="name" ID="lblName" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="date"><b>Fecha de Fundación:</b></label>
            <asp:Label name="date" ID="lblFoundationDate" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="university"><b>Universidad:</b></label>
            <asp:Label name="university" ID="lblUniversity" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="country"><b>Pais:</b></label>
            <asp:Label name="country" ID="lblCountry" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="state"><b>Estado:</b></label>
            <asp:Label name="state" ID="lblState" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="city"><b>Ciudad:</b></label>
            <asp:Label name="city" ID="lblCity" runat="server" Text=""></asp:Label>
        </div>

        <div class="field">
            <label for="subjects"><b>Materias</b></label>
            <asp:ListBox class="formInput chosen" disabled="disabled" name="subjects" SelectionMode="Multiple" ID="lbSubjects" runat="server"></asp:ListBox>
        </div>

        <asp:Button class="button" ID="btnDelete" runat="server" Text="Eliminar" OnClick="btnDelete_Click" />

    </div>


    <%-- Scripts --%>
    <script type="text/javascript">
        function applyStyles() {
            <%-- Chosen --%>
            $(".chosen").chosen();
        }

        $(document).ready(function () {
            applyStyles();

            var manager = Sys.WebForms.PageRequestManager.getInstance();

            manager.add_endRequest(applyStyles);
        });
    </script>

</asp:Content>