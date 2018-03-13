<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearFactura.aspx.cs" Inherits="joyeriaSYS.frmCrearFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFactura" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- One -->
            <section id="one" class="wrapper spotlight style1">
                <div class="inner">
                    <a href="#" class="image">
                        <img src="../images/pic01.jpg" alt="" />
                    </a>
                    <asp:HiddenField ID="hdfIdFactura" Visible="true" Value="-1" runat="server" />
                    <asp:HiddenField ID="hdfIdDetalleFactura" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>
                            <h3 class="major">Creación de factura por tabla</h3>
                    <div class="row uniform">
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Código Tabla:</label>
                            <asp:TextBox ID="txtCodTabla" AutoPostBack="false" runat="server" name="demo-name"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Número de Factura:</label>
                            <asp:TextBox ID="txtCodFactura" AutoPostBack="false" runat="server" name="demo-name"></asp:TextBox>
                        </div>
                        <div class="12u$">
                            <label for="demo-cliente">Cliente</label>
                            <div class="select-wrapper">
                                <asp:DropDownList ID="ddlCliente" name="demo-cliente" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="12u$">
                            <label for="demo-cliente">Producto</label>
                            <div class="select-wrapper">
                                <asp:DropDownList ID="ddlProducto" name="demo-cliente" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Cantidad:</label>
                            <asp:TextBox ID="txtCantidad" AutoPostBack="false" name="demo-name" runat="server"></asp:TextBox>
                        </div>
                        <div class="12u$">
                            <ul class="actions">
                                <li>
                                    <asp:Button ID="btnInsertarActualizar" runat="server" OnClick="btnInsertarActualizar_Click" Text="Añadir a factura" CssClass="special" />
                                </li>
                                <li>
                                    <asp:Button ID="btnFinalizar" runat="server" OnClick="btnFinalizar_Click" Text="Finalizar Factura" CssClass="special" />
                                </li>
                            </ul>
                        </div>
                        </section>
                    </div>
                </div>
            </section>
            <!-- Two -->
            <section id="two" class="wrapper alt spotlight style2">
                <div class="inner">
                    <a href="#" class="image">
                        <img src="../images/pic02.jpg" alt="" /></a>
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
								<div class="inner">
									<a href="#" class="image"><img src="../images/pic03.jpg" alt="" /></a>
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
            <asp:AsyncPostBackTrigger ControlID="btnInsertarActualizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnFinalizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwFacturas" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvwDetalleFactura" EventName="SelectedIndexChanged" />
        </Triggers>
                </asp:UpdatePanel>
</asp:Content>
