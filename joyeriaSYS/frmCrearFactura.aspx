﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearFactura.aspx.cs" Inherits="joyeriaSYS.frmCrearFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFactura" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- One -->
            <section id="one" class="wrapper spotlight style1">
                <div class="inner">
                    <asp:HiddenField ID="hdfIdFactura" Visible="true" Value="-1" runat="server" />
                    <asp:HiddenField ID="hdfIdDetalleFactura" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>                        
                    <div class="container uniform">
						<h1 class="major">Creación de factura por tabla</h1>
                        <%--<div class="6u 12u$(xsmall)">
                            <label for="demo-name">Código Tabla:</label>
                            <asp:TextBox ID="txtCodTabla" AutoPostBack="false" runat="server" name="demo-name"></asp:TextBox>
                        </div>--%>
                        <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Número de Factura:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtCodFactura" AutoPostBack="false" runat="server" name="demo-name"></asp:TextBox>
							</div>
                        </div>
                        <div class="row margin-b 12u$">
							<div class="col-sm-5 col-md-3">
								<label for="demo-cliente">Cliente</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<div class="select-wrapper">
									<asp:DropDownList ID="ddlCliente" name="demo-cliente" runat="server">
									</asp:DropDownList>
								</div>
							</div>
                        </div>
                        <div class="row margin-b 12u$">
							<div class="col-sm-5 col-md-3">
								<label for="demo-cliente">Producto</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<div class="select-wrapper">
									<asp:DropDownList ID="ddlProducto" name="demo-cliente" runat="server">
									</asp:DropDownList>
								</div>
							</div>
                        </div>
                        <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Cantidad:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtCantidad" AutoPostBack="false" name="demo-name" runat="server"></asp:TextBox>
							</div>
                        </div>
                        <div class="row margin-b 12u$">
							<div class="col-sm-5 col-md-3">
								
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:Button ID="btnInsertarActualizar" runat="server" OnClick="btnInsertarActualizar_Click" Text="Añadir a factura" CssClass="btn btn-primary special" />
								<asp:Button ID="btnFinalizar" runat="server" OnClick="btnFinalizar_Click" Text="Finalizar Factura" CssClass="btn btn-info" />
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
                    <div class="container">
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
							<section id="three" class="container spotlight style3">
								<div class="inner">
									<div class="content">
										<asp:GridView runat="server" ID="gvwFacturas" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwFacturas_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="idFactura" HeaderText="Id" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="NoFactura" HeaderText="No Factura" AccessibleHeaderText="NoFactura" InsertVisible="False"></asp:BoundField>
                                <%--<asp:BoundField DataField="CodTabla" HeaderText="Código de la Tabla" AccessibleHeaderText="CodTabla" InsertVisible="False"></asp:BoundField>--%>
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
