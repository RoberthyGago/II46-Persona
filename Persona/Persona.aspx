  <%--'esta es la vista, el código de la lógica de negocio se encuentra en Persona.aspx.vb--%>
<%@ Page Title="Persona" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Persona.aspx.vb" Inherits="Persona.Persona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<asp:HiddenField ID="hfIDPersona" runat="server" />
    <div class="form-group">
        <asp:Label ID="lblTipoDoc" runat="server" Text="Tipo de Documento" CssClass="control-label"></asp:Label>
        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control">
            <asp:ListItem Text="Seleccione un tipo de documento" Value="" />
            <asp:ListItem Text="Cédula Fisica" Value="1"></asp:ListItem>
            <asp:ListItem Text="Cédula Jurídica" Value="2" />
            <asp:ListItem Text="Pasaporte" Value="3" />
        </asp:DropDownList>
    </div>

    <%--Validacion de Tipo documento--%>
    <%--Tiene prioridad sobre el botón--%>
    <asp:RequiredFieldValidator ID="rfvTipoDocumento" runat="server"
        ErrorMessage="Es necesario seleccionar un tipo de documento"
        ControlToValidate="ddlTipoDocumento" Display="Dynamic"></asp:RequiredFieldValidator>

    <div class="form-group">
        <asp:Label ID="lblNumDoc" runat="server" Text="Documento" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtDocumento" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
    </div>
    <asp:RequiredFieldValidator ID="rfvDocumento" runat="server"
        ErrorMessage="Es necesario indicar el número de documento"
        ControlToValidate="txtDocumento" Display="Dynamic"></asp:RequiredFieldValidator>

    <div class="form-group">
        <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server" placeholder="Roberth"  CssClass="form-control"></asp:TextBox>
    </div>
    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
        ErrorMessage="Es necesario indicar el nombre"
        ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>

    <div class="form-group">
        <asp:Label ID="lblApellidos" runat="server" Text="Apellidos" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtApellidos" runat="server" placeholder="Cordero Alvarez" CssClass="form-control"></asp:TextBox>
    </div>

    <asp:RequiredFieldValidator ID="rfvApellidos" runat="server"
        ErrorMessage="Es necesario indicar los apellidos"
        ControlToValidate="txtApellidos" Display="Dynamic"></asp:RequiredFieldValidator>

    <div class="form-group">
        <asp:Label ID="lblFechaNac" runat="server" Text="Fecha Nacimiento" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtFechaNac" runat="server" placeholder="29/12/96" CssClass="form-control" TextMode="Date"></asp:TextBox>
    </div>
    
    <%--Validacion de Fecha--%>
    <asp:RequiredFieldValidator ID="rfvFechaNac" runat="server"
        ErrorMessage="Es necesario indicar la fecha de nacimiento"
        ControlToValidate="txtFechaNac" Display="Dynamic"></asp:RequiredFieldValidator>

    <div class="form-group">
        <asp:Label ID="lblCorreo" runat="server" Text="Correo" CssClass="control-label"></asp:Label>
        <asp:TextBox ID="txtCorreo" runat="server" placeholder="rcordero@edu.ac.cr" CssClass="form-control"></asp:TextBox>
    </div>
    
    <asp:RequiredFieldValidator ID="rfvCorreo" runat="server"
        ErrorMessage="Es necesario indicar el correo"
        ControlToValidate="txtCorreo" Display="Dynamic"></asp:RequiredFieldValidator>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary my-2" OnClick="btnGuardar_Click" />
    <asp:Label ID="lblResultado" runat="server" Text="" CssClass="control-label"></asp:Label>
      <%--creo eventos nuevos--%>
    <asp:GridView ID="gvPersonas" CssClass="table  table-striped table-hover"
        runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="IDPersona"
       

        OnRowEditing="gvPersonas_RowEditing"
        OnSelectedIndexChanged="gvPersonas_SelectedIndexChanged"
        OnRowDeleting="gvPersonas_RowDeleting">
        <Columns>            
<asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-primary" SelectText="<i class='bi bi-pencil'>" />

<asp:BoundField DataField="IDPersona" HeaderText="IDPersona" InsertVisible="False" ReadOnly="True" SortExpression="IDPersona" />
<asp:BoundField ControlStyle-CssClass="d-none" DataField="TipoDocumento" HeaderText="TipoDocumento" SortExpression="TipoDocumento" />
<asp:BoundField DataField="Documento" HeaderText="Documento" SortExpression="Documento" />
<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
<asp:BoundField DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos" />
<asp:BoundField DataField="FechaNac" HeaderText="FechaNac" SortExpression="FechaNac" />
<asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
<asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" DeleteText="<i class='bi bi-trash'>" />



</Columns>
    </asp:GridView>
    <%--<asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary" EditText="<i class='bi bi-pencil'>" />--%>
    
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:II-46ConnectionString %>" ProviderName="<%$ ConnectionStrings:II-46ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Personas]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

</asp:Content>
