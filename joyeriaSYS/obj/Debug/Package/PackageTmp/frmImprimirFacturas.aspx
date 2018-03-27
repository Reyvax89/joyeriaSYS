<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmImprimirFacturas.aspx.cs" Inherits="joyeriaSYS.frmImprimirFacturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpLeerFactura" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <!-- One -->
    <section id="one" class="wrapper spotlight style1">
            <div class="inner">
                    <asp:HiddenField ID="hdfIdFactura" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>
                            <div class="row uniform">
                                <div class="" id="reader">
                                    <label class="label">Facturas:</label>
                                    <asp:DropDownList ID="ddlFacturas" runat="server"></asp:DropDownList>
                                </div>
                                <div class="">
                                    <ul class="actions">
                                        <li>
                                            <asp:Button ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" Text="Imprimir" CssClass="special" />
                                        </li>
                                    </ul>
                                </div>
                                <div class="">
                                    <label for="demo-cliente">Producto</label>
                                    <div class="content">
										<asp:GridView runat="server" ID="gvwDetalleFactura" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="categoria" HeaderText="Id" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="idProducto" HeaderText="No Factura" AccessibleHeaderText="NoFactura" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="CantidadProducto" HeaderText="Código de la Tabla" AccessibleHeaderText="CodTabla" InsertVisible="False"></asp:BoundField>
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
                        </section>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlFacturas" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnImprimir" EventName="Click" />
        </Triggers>
                </asp:UpdatePanel>
</asp:Content>
