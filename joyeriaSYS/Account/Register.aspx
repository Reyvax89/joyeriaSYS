<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="joyeriaSYS.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="form-horizontal container">
		<h2><%: Title %>.</h2>
        <h4>Crear nueva cuenta de usuario</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
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
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Registrar" CssClass="btn btn-default" />
				<p class="text-danger">
					<asp:Literal runat="server" ID="ErrorMessage" />
				</p>
            </div>
        </div>
    </div>
</asp:Content>
