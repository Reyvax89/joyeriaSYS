<%@ Page Title="Liquidar factura" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeerQR.aspx.cs" Inherits="joyeriaSYS.LeerQR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<div class="container">
		<div class="row">
			<div class="col-sm12 col-md-12">
				<h1>Leer códigos QRs.</h1>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpLeerFactura" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <!-- One -->
    <section id="one" class="wrapper spotlight style1">
            <div class="inner">
                    <asp:HiddenField ID="hdfIdFactura" Visible="true" Value="-1" runat="server" />
                    <asp:HiddenField ID="hdfIdDetalleFactura" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>
							<div class="container">
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
										<label for="demo-cliente">Leer los productos de la factura:</label>
									</div>
									<div class="col-sm-7 col-md-9">
										<asp:TextBox ID="txtCodigo"  AutoPostBack="true" OnTextChanged="txtCodigo_TextChanged" placeholder="Aqui se muestra el idproducto" runat="server"></asp:TextBox>
									</div>
								</div>
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
										<label for="demo-facturas">Seleccione el numero de factura:</label>
									</div>
									<div class="col-sm-7 col-md-9">
										<div class="select-wrapper">
											<asp:DropDownList ID="ddlFactura" AutoPostBack="true" OnTextChanged="ddlFactura_DropDownChanged" Enabled="true" name="ddlFactura" runat="server">
											</asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
										<label for="demo-cliente">Producto</label>
									</div>
									<div class="col-sm-7 col-md-9">
										<div class="select-wrapper">
											<asp:DropDownList ID="ddlProducto" AutoPostBack="false" Enabled="false" name="demo-producto" runat="server">
											</asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
										<label for="demo-name">Código Tabla:</label>
									</div>
									<div class="col-sm-7 col-md-9">
										<asp:TextBox ID="txtCodTabla" Enabled="false" AutoPostBack="false" runat="server" ></asp:TextBox>
									</div>
								</div>
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
										<label for="demo-cliente">Cliente</label>
									</div>
									<div class="col-sm-7 col-md-9">
										<div class="select-wrapper">
											<asp:DropDownList ID="ddlCliente" AutoPostBack="false" Enabled="false" name="demo-cliente" runat="server">
											</asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
												
									</div>
									<div class="col-sm-7 col-md-9">
										<asp:Button ID="btnCalcular" runat="server" OnClick="btnCalcular_Click" Text="Finalizar Factura" CssClass="btn btn-primary special" />
										<asp:Button ID="btnValidar" style="display:none;" runat="server" Text="Finalizar Factura" CssClass="special" />
									</div>
								</div>
							</div>
                        </section>
                    </div>
                </div>
    <!-- Two -->
            <section id="two" class="wrapper alt spotlight style2">
				<h3>Detalles de items de la factura.</h3>
                <div class="inner">
                    <div class="content">
                        <asp:GridView runat="server" ID="gvwDetalleFactura" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwDetalleFactura_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="idFactura" HeaderText="Id Factura" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="idProducto" HeaderText="Id Producto" AccessibleHeaderText="idProducto" InsertVisible="False"></asp:BoundField>
								<asp:BoundField DataField="codProducto" HeaderText="Código Producto" AccessibleHeaderText="codProducto" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="CantidadProducto" HeaderText="Cantidad" AccessibleHeaderText="CantidadProducto" InsertVisible="False"></asp:BoundField>
								<asp:templatefield HeaderText="Regresado">
									<itemtemplate>
										<asp:checkbox ID="cbSelect"
										CssClass="gridCB" runat="server" Checked='<%# Convert.ToBoolean(Eval("Regresado"))%>' Enabled="false"></asp:checkbox>
									</itemtemplate>
								</asp:templatefield>
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
            <!-- Three -->
			<section id="three" class="wrapper spotlight style3">
				<h3>Resumen factura.</h3>
				<div class="content">
					<asp:GridView runat="server" ID="gvwFacturas" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwFacturas_SelectedIndexChanged">
						<Columns>
							<asp:BoundField DataField="idFactura" HeaderText="Id" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
							<asp:BoundField DataField="NoFactura" HeaderText="No Factura" AccessibleHeaderText="NoFactura" InsertVisible="False"></asp:BoundField>
							<asp:BoundField DataField="montoFactura" HeaderText="Monto Factura" AccessibleHeaderText="montoFactura" InsertVisible="False"></asp:BoundField>
							<asp:BoundField DataField="estado" HeaderText="Estado" AccessibleHeaderText="estado" InsertVisible="False"></asp:BoundField>
							<asp:BoundField DataField="totalPiezas" HeaderText="Total Piezas" AccessibleHeaderText="totalPiezas" InsertVisible="False"></asp:BoundField>
							<asp:BoundField DataField="idCliente" HeaderText="Id Cliente" AccessibleHeaderText="idCliente" InsertVisible="False"></asp:BoundField>
							<%--<asp:BoundField DataField="fechaCreacion" HeaderText="fechaCreacion" AccessibleHeaderText="fechaCreacion" InsertVisible="False"></asp:BoundField>
							<asp:BoundField DataField="fechaLiquidacion" HeaderText="fechaLiquidacion" AccessibleHeaderText="fechaLiquidacion" InsertVisible="False"></asp:BoundField>--%>
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
			</section>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnInsertarActualizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnFinalizar" EventName="Click" />--%>
            <asp:AsyncPostBackTrigger ControlID="gvwFacturas" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvwDetalleFactura" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnValidar" EventName="Click" />
        </Triggers>
                </asp:UpdatePanel>
    <%--<script src="Scripts/jquery-1.9.1.min.js"></script>--%>
    <%--<script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>--%>
    <%--<script src="Scripts/instascan.min.js"></script>--%>
    <%--<script src="Scripts/html5-qrcode.min.js"></script>--%>
    <%--<script src="Scripts/leerQR.js"></script>--%>

</asp:Content>
