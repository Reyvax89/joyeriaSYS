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
                <div class="container uniform">
                    <h1 class="major">Imprimir Facturas</h1>
                    <div class="row margin-b 12u$">
                        <div class="col-sm-5 col-md-3">
                            <label for="demo-cliente">Facturas</label>
                        </div>
                        <div class="col-sm-7 col-md-9">
                            <div class="select-wrapper">
                                <asp:DropDownList ID="ddlFacturas" runat="server" OnSelectedIndexChanged="ddlFacturas_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row margin-b 6u 12u$(xsmall)">
							<div class="col-sm-5 col-md-3">
								<label for="demo-name">Vendedor:</label>
							</div>
							<div class="col-sm-7 col-md-9">
								<asp:TextBox ID="txtNombreVendedor" AutoPostBack="false" runat="server" name="demo-name"></asp:TextBox>
							</div>
                        </div>
                    <div class="col-sm-7 col-md-9">
                        <asp:Button ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" Text="Imprimir" CssClass="btn btn-primary special" />
                    </div>
                    <div class="inner">
                        <div class="container">
                            <asp:GridView runat="server" ID="gvwDetalleFactura" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="categoria" HeaderText="Producto" AccessibleHeaderText="idFactura" InsertVisible="False"></asp:BoundField>
                                    <asp:BoundField DataField="idProducto" HeaderText="Código" AccessibleHeaderText="NoFactura" InsertVisible="False"></asp:BoundField>
                                    <asp:BoundField DataField="CantidadProducto" HeaderText="Cantidad" AccessibleHeaderText="CodTabla" InsertVisible="False"></asp:BoundField>
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
