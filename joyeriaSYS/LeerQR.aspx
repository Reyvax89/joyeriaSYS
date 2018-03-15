<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeerQR.aspx.cs" Inherits="joyeriaSYS.LeerQR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
                            <div class="row uniform">
                                <video id="preview"></video>
                                <div class="12u$" id="reader" style="width: 300px; height: 250px">
                                    <label for="demo-cliente">Leer los productos de la factura:</label>
                                    <asp:TextBox ID="txtCodigo"  AutoPostBack="true" OnTextChanged="txtCodigo_TextChanged" placeholder="Aqui se muestra el idproducto" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtNumeroFactura" AutoPostBack="true" OnTextChanged="txtNumeroFactura_TextChanged" placeholder="Aquí la factura" runat="server"></asp:TextBox>
                                </div>
                                <div class="12u$">
                                    <label for="demo-cliente">Producto</label>
                                    <div class="select-wrapper">
                                        <asp:DropDownList ID="ddlProducto" AutoPostBack="false" Enabled="false" name="demo-producto" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="6u 12u$(xsmall)">
                                    <label for="demo-name">Código Tabla:</label>
                                    <asp:TextBox ID="txtCodTabla" Enabled="false" AutoPostBack="false" runat="server" ></asp:TextBox>
                                </div>
                                <div class="12u$">
                                    <label for="demo-cliente">Cliente</label>
                                    <div class="select-wrapper">
                                        <asp:DropDownList ID="ddlCliente" AutoPostBack="false" Enabled="false" name="demo-cliente" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="12u$">
                                    <ul class="actions">
                                        <li>
                                            <asp:Button ID="btnCalcular" runat="server" OnClick="btnCalcular_Click" Text="Finalizar Factura" CssClass="special" />
                                            <asp:Button ID="btnValidar" style="display:none;" runat="server" Text="Finalizar Factura" CssClass="special" />
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
    <!-- Two -->
            <section id="two" class="wrapper alt spotlight style2">
                <div class="inner">
                    <div class="content">
                        <asp:GridView runat="server" ID="gvwDetalleFactura" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwDetalleFactura_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="idDetalleFactura" HeaderText="idDetalleFactura" AccessibleHeaderText="idDetalleFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="idFactura" HeaderText="idFactura" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="idProducto" HeaderText="idProducto" AccessibleHeaderText="idProducto" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="CantidadProducto" HeaderText="CantidadProducto" AccessibleHeaderText="CantidadProducto" InsertVisible="False"></asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" HeaderText="Eliminar"></asp:CommandField>
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
									<div class="content">
										<asp:GridView runat="server" ID="gvwFacturas" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwFacturas_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="idFactura" HeaderText="Id" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="NoFactura" HeaderText="No Factura" AccessibleHeaderText="NoFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="CodTabla" HeaderText="Código de la Tabla" AccessibleHeaderText="CodTabla" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="montoFactura" HeaderText="montoFactura" AccessibleHeaderText="montoFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="estado" AccessibleHeaderText="estado" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="totalPiezas" HeaderText="totalPiezas" AccessibleHeaderText="totalPiezas" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="idCliente" HeaderText="idCliente" AccessibleHeaderText="idCliente" InsertVisible="False"></asp:BoundField>
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