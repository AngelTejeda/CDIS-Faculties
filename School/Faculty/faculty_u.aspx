<%@ Page Title="Editar Facultad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="faculty_u.aspx.cs" Inherits="School.Faculties.Faculty_u" %>
<asp:Content autocomplete="off" ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%-- Styles --%>
    <link href="../css/forms.css" rel="stylesheet" />
    <style>
        .form {
            width: 50%;
        }
    </style>


    <%-- Content --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="form" runat="server">

                <%-- Faculty ID --%>
                <div class="inputContainer">
                    <label for="id"><b>ID de Facultad</b></label>
                    <asp:TextBox class="formInput" ID="txtId" disabled="disabled" name="code" runat="server" MaxLength="6"></asp:TextBox>
                </div>

                <%-- Faculty Code --%>
                <div class="inputContainer">
                    <label for="code"><b>Código</b></label>
                    <asp:TextBox class="formInput" ID="txtCode" name="code" placeholder="AAAA00" runat="server" MaxLength="6"></asp:TextBox>
                    
                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvCode" runat="server"
                        ControlToValidate="txtCode" ValidationGroup="vgEdit"
                        Display="Dynamic" ErrorMessage="El código es requerido."></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator class="formValidation" ID="revCode" runat="server"
                        ControlToValidate="txtCode" ValidationGroup="vgEdit" ValidationExpression="[A-Z]{4}[0-9]{2}" 
                        Display="Dynamic" ErrorMessage="Formato inválido (AAAA00)."></asp:RegularExpressionValidator>
                </div>

                <%-- Faculty Name --%>
                <div class="inputContainer">
                    <label for="name"><b>Nombre de Facultad</b></label>
                    <asp:TextBox class="formInput" name="name" ID="txtName" runat="server"></asp:TextBox>

                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvName" runat="server"
                        ControlToValidate="txtName" ValidationGroup="vgEdit"
                        Display="Dynamic" ErrorMessage="El nombre es requerido."></asp:RequiredFieldValidator>
                </div>
            
                <%-- Foundation Date --%>
                <div class="inputContainer">
                    <label for="date"><b>Fecha de Fundación</b></label>
                    <asp:TextBox class="formInput" name="date" placeholder="yyyy/mm/dd" ID="txtFoundationDate" runat="server"></asp:TextBox>

                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvFoundationDate" runat="server"
                        ControlToValidate="txtFoundationDate" ValidationGroup="vgEdit"
                        Display="Dynamic" ErrorMessage="La fecha es requerida." ></asp:RequiredFieldValidator>

                    <asp:CompareValidator class="formValidation" ID="cvFoundationDate" runat="server"
                        ControlToValidate="txtFoundationDate" ValidationGroup="vgEdit" Type="Date" Operator="DataTypeCheck" 
                        Display="Dynamic" ErrorMessage="El formato de fecha es incorrecto o la fecha es inválida."></asp:CompareValidator>
                </div>
           
                <%-- University --%>
                <div class="inputContainer">
                    <label for="university"><b>Universidad</b></label>
                    <asp:DropDownList class="formInput chosen" name="university" ID="ddlUniversity" runat="server"></asp:DropDownList>

                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvUniversity" runat="server"
                        ControlToValidate="ddlUniversity" ValidationGroup="vgEdit" InitialValue="0"
                        Display="Dynamic" ErrorMessage="La universidad es requerida."></asp:RequiredFieldValidator>
                </div>

                <%-- Country --%>
                <div class="inputContainer">
                    <label for="country"><b>Pais</b></label>
                    <asp:DropDownList class="formInput chosen" name="country" ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>

                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvCountry" runat="server"
                        ControlToValidate="ddlCountry" ValidationGroup="vgInsert" InitialValue="0"
                        Display="Dynamic" ErrorMessage="El Pais es requerido."></asp:RequiredFieldValidator>
                </div>
            
                <%-- State --%>
                <div class="inputContainer">
                    <label for="state"><b>Estado</b></label>
                    <asp:DropDownList class="formInput chosen" name="state" ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>

                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvState" runat="server"
                        ControlToValidate="ddlState" ValidationGroup="vgEdit" InitialValue="0"
                        Display="Dynamic" ErrorMessage="El Estado es requerido."></asp:RequiredFieldValidator>
                </div>

                <%-- City --%>
                <div class="inputContainer">
                    <label for="city"><b>Ciudad</b></label>
                    <asp:DropDownList class="formInput chosen" name="city" ID="ddlCity" runat="server"></asp:DropDownList>

                    <%-- Validations --%>
                    <asp:RequiredFieldValidator class="formValidation" ID="rfvCity" runat="server"
                        ControlToValidate="ddlCity" ValidationGroup="vgEdit" InitialValue="0"
                        Display="Dynamic" ErrorMessage="La ciudad es requerida."></asp:RequiredFieldValidator>
                </div>

                <%-- Subjects --%>
                <div class="inputContainer">
                    <label for="subjects"><b>Materias</b></label>
                    <asp:ListBox class="formInput chosen" name="subjects" SelectionMode="Multiple" ID="lbSubjects" runat="server"></asp:ListBox>
                </div>

                <%-- Button --%>
                <div class="inputContainer">
                    <asp:Button class="formInput" ID="btnEdit" runat="server" Text="Editar" OnClick="btnEdit_Click" ValidationGroup="vgEdit"/>
                </div>
        
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <%-- Scripts --%>
    <script type="text/javascript">
        function applyStyles() {
            <%-- Calendar --%>
            $("#MainContent_txtFoundationDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2010",
                dateFormat: "yy'/'mm'/'dd",
                defaultDate: new Date(2010, 0, 1)
            });

            <%-- No Auto Complete --%>
            $("input[type=text]").attr("autocomplete", "off");

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