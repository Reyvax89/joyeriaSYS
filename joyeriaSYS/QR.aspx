<%@ Page Title="Administrar inventario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QR.aspx.cs" Inherits="joyeriaSYS.QR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpGenerarQR" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <!-- One -->
    <section id="one" class="wrapper spotlight style1">
        <div class="inner">
            <a href="#" class="image">
                    <asp:Image runat="server" ID="QRImage" />
            </a>
            <div class="content container">
                <section>
                    <br />
                    <h3 class="major">Formulario ingreso de Producto</h3>
                    <asp:HiddenField ID="hdfId" Visible="true" Value="-1" runat="server" />
                    <div class="uniform">
                        <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Nombre:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtNombreProducto" runat="server"></asp:TextBox>
							</div>
                        </div>
                        <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Código Númerico:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtCodNumerico" placeholder="000" AutoPostBack="true" OnTextChanged="txtCodNumerico_TextChanged" style="color: black;"  runat="server"></asp:TextBox>
							</div>
                        </div>
                        <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Precio:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtPrecio" placeholder="¢000" Enabled="false" style="color: black;" runat="server"></asp:TextBox>
							</div>
                        </div>
                        <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Cantidad:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtCantidad" placeholder="000" style="color: black;"  runat="server"></asp:TextBox>
							</div>
                        </div>
                        <div class="row margin-b 12u$">
							<div class="col-sm-5 col-md-3">
								<label for="demo-category">Categoría</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<div class="select-wrapper">
									<asp:DropDownList ID="ddlCategoria" AutoPostBack="false" runat="server">
									</asp:DropDownList>
								</div>
							</div>
                        </div>
                        <div class="row margin-b 12u$">
							<div class="col-sm-5 col-md-3">
								
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:Button ID="btnInsertarActualizar" runat="server" OnClick="btnInsertarActualizar_Click" Text="Guardar" CssClass="btn btn-primary special" />
								<asp:Button ID="btnLimpiar" runat="server" Text="Nuevo" OnClick="btnLimpiar_Click" CssClass="btn btn-default special" />
							</div>
                        </div>
                    </div>
                </section>
            </div>

        </div>
            </section>
            <!-- Two -->
        <section id="two" class="wrapper alt spotlight style2">
            <div class="inner">
                <div class="content">
                    <asp:GridView runat="server" AllowPaging="true" ID="gvwProductos" OnPageIndexChanging="gvwProductos_PageIndexChanging" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwProductos_SelectedIndexChanged" PagerSettings-Mode="NextPrevious" PageSize="15">
        <Columns>
            <asp:BoundField DataField="IdProducto" HeaderText="Id" AccessibleHeaderText="IdProducto" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" AccessibleHeaderText="NombreProducto" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="Metal" HeaderText="Categoría" AccessibleHeaderText="Metal" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="CodigoNumerico" HeaderText="Código" AccessibleHeaderText="CodigoNumerico" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="Precio" HeaderText="Precio" AccessibleHeaderText="Precio" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="Inventario" HeaderText="Cantidad" AccessibleHeaderText="Inventario" InsertVisible="False"></asp:BoundField>
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
        </section>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInsertarActualizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwProductos" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="gvwProductos" EventName="PageIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtCodNumerico" EventName="TextChanged" />
        </Triggers>
        </asp:UpdatePanel>
    <%--<script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>--%>
    <%--<script src="Scripts/html5-qrcode.min.js"></script>--%>
    <%--<script src="Scripts/leerQR.js"></script>--%>
    
</asp:Content>

