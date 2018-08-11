<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="joyeriaSYS.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="form-horizontal container">
		<h2><%: Title %>.</h2>
        <h4>Crear nueva cuenta de usuario</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Inactivar:</asp:Label>
            <div class="col-md-5">
                <asp:CheckBox ID="chbActivoInactivo" runat="server" />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Nombre:</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Apellido:</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Nombre de usuario:</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Contraseña:</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Confirmar contraseña:</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" CssClass="col-md-4 control-label">Rol:</asp:Label>
            <div class="col-md-5">
                <div class="select-wrapper">
					<asp:DropDownList ID="ddlRol" runat="server">
					</asp:DropDownList>
				</div>
            </div>
        </div>
        <div class="form-group row">
			<div class="col-md-4"></div>
            <div class="col-md-5">
                <asp:Button ID="btnInsertarActualizar" runat="server" OnClick="CreateUser_Click" Text="Guardar" CssClass="btn btn-default" />
				<p class="text-danger">
					<asp:Literal runat="server" ID="ErrorMessage" />
				</p>
            </div>
        </div>
    </div>
    <div class="container">
		<h4>Lista de usuarios</h4>
        <div class="row">
			<div class="col-sm-12 col-md-12">
				<asp:HiddenField ID="hdfId" Visible="true" Value="-1" runat="server" />
				<asp:GridView runat="server" ID="gvwUsuario" BackColor="White" OnSelectedIndexChanged="gvwUsuario_SelectedIndexChanged" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" >
					<Columns>
						<asp:BoundField DataField="idUsuario" HeaderText="ID" AccessibleHeaderText="idUsuario" InsertVisible="False"></asp:BoundField>
						<asp:BoundField DataField="mombre" HeaderText="Nombre" AccessibleHeaderText="nombre" InsertVisible="False"></asp:BoundField>
						<asp:BoundField DataField="mombreUsuario" HeaderText="Nombre de usuario" AccessibleHeaderText="nombreUsuario" InsertVisible="False"></asp:BoundField>
						<asp:BoundField DataField="apellido" HeaderText="Apellido" AccessibleHeaderText="apellido" InsertVisible="False"></asp:BoundField>
                        <asp:BoundField DataField="rol" HeaderText="Rol" AccessibleHeaderText="rol" InsertVisible="False"></asp:BoundField>
                        <asp:BoundField DataField="LockoutEnabled" HeaderText="Inactivo" AccessibleHeaderText="LockoutEnabled" InsertVisible="False"></asp:BoundField>
						<asp:CommandField ShowSelectButton="True" HeaderText="Seleccionar"></asp:CommandField>
					</Columns>
					<FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
					<HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White"></HeaderStyle>
					<PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
					<SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
					<SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
					<SortedAscendingHeaderStyle BackColor="#4B4B4B"></SortedAscendingHeaderStyle>
					<SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
					<SortedDescendingHeaderStyle BackColor="#242121"></SortedDescendingHeaderStyle>
				</asp:GridView>
			</div>
        </div>
    </div>
</asp:Content>
