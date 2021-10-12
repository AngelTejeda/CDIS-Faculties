<%@ Page Title="Consultar Facultades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="faculty_s.aspx.cs" Inherits="School.Faculties.Faculty_s" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%-- Styles --%>
    <link href="../css/faculty_s.css" rel="stylesheet" />
    <link href="../css/tables.css" rel="stylesheet" />

    <%-- Content --%>
    <asp:GridView CssClass="table" ID="grdFaculties" AutoGenerateColumns="false" runat="server" OnRowCommand="grdFaculties_RowCommand">
        <Columns>

            <%-- Edit Button --%>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="buttonContainer">
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Editar"
                            CommandName="Edit" CommandArgument='<%# Eval("faculty_id") %>'/>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- Delete Button --%>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="buttonContainer">
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/trash.png" ToolTip="Eliminar"
                            CommandName="Delete" CommandArgument='<%# Eval("faculty_id") %>'/>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <%-- Table Content --%>
            <asp:BoundField HeaderText="ID" DataField="faculty_id"/>
            <asp:BoundField HeaderText="Código" DataField="code"/>
            <asp:BoundField HeaderText="Nombre" DataField="faculty_name"/>
            <asp:BoundField HeaderText="Fecha de Creación" DataField="foundation_date" DataFormatString="{0:yyyy'/'MMMM'/'dd}"/>
            <asp:BoundField HeaderText="Universidad" DataField="university_name"/>
            <asp:BoundField HeaderText="Pais" DataField="country_name"/>
            <asp:BoundField HeaderText="Estado" DataField="state_name"/>
            <asp:BoundField HeaderText="Ciudad" DataField="city_name"/>

        </Columns>
    </asp:GridView>

</asp:Content>