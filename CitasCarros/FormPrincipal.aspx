<%@ Page Title="Gestión de Citas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPrincipal.aspx.cs" Inherits="CitasCarros.FormPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Horario y Áreas</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

    Fecha Entrada: 
    <asp:TextBox ID="txtFechaEntrada" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="txtFechaEntrada_TextChanged" />
    
    <br /><br />
    <asp:PlaceHolder ID="phTablaHorarios" runat="server"></asp:PlaceHolder>

    <hr />

    <h2>Registrar Cita</h2>
    <table>
        <tr>
            <td><asp:Label ID="lblNombre" runat="server" Text="Nombre Cliente:" /></td>
            <td><asp:TextBox ID="txtNombre" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCorreo" runat="server" Text="Correo Cliente:" /></td>
            <td><asp:TextBox ID="txtCorreo" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblModelo" runat="server" Text="Modelo Carro:" /></td>
            <td><asp:TextBox ID="txtModelo" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTipoServicio" runat="server" Text="Tipo de Servicio:" />
                <asp:DropDownList ID="ddTipoSer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddTipoSer_Cambio">
                    <asp:ListItem Text="Basico" Value="0" />
                <asp:ListItem Text="General" Value="1" />
                <asp:ListItem Text="Full Service" Value="2" />
            </asp:DropDownList> </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblHorarioEntrada" runat="server" Text="Horario Entrada:" />
                <asp:DropDownList ID="ddlHorarioEntrada" runat="server" OnSelectedIndexChanged="ddHorarioEnt_Cambio">
                <asp:ListItem Text="08:00" Value="08:00" />
                <asp:ListItem Text="09:00" Value="09:00" />
                <asp:ListItem Text="10:00" Value="10:00" />
                <asp:ListItem Text="11:00" Value="11:00" />
                <asp:ListItem Text="12:00" Value="12:00" />
                <asp:ListItem Text="13:00" Value="13:00" />
                <asp:ListItem Text="14:00" Value="14:00" />
                <asp:ListItem Text="15:00" Value="15:00" />
                <asp:ListItem Text="16:00" Value="16:00" />
                <asp:ListItem Text="17:00" Value="17:00" />
                <%--<asp:ListItem Text="18:00" Value="18:00" />--%>
                <%--<asp:ListItem Text="19:00" Value="19:00" />--%>
            </asp:DropDownList></td>
        </tr>
        
        <%--<tr>
            <td><asp:Label ID="lblHorarioSalida" runat="server" Text="Horario Salida:" /></td>
            <td><asp:TextBox ID="txtHorarioSalida" runat="server" TextMode="Time" /></td>
        </tr>--%>
        
        <tr>
            <td><asp:Label ID="lblArea" runat="server" Text="Área:" /></td>
            <td>
                <asp:DropDownList ID="ddlArea" runat="server">
                    <asp:ListItem Text="Área 1" Value="Área 1" />
                    <asp:ListItem Text="Área 2" Value="Área 2" />
                </asp:DropDownList>
            </td>
        </tr>
        <%--<td>
    
</td>--%>
        <tr>
            <td colspan="2" style="text-align:center; padding-top: 10px;">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cita" OnClick="btnGuardar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center; padding-top: 10px;">
                <asp:Button ID="ButtonReiniciar" runat="server" Text="Eliminar Cita" OnClick="btnReiniciar_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
