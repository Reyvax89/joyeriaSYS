<%@ Page Title="Ingresar Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QR.aspx.cs" Inherits="joyeriaSYS.QR" %>

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
            <div class="content">
                <section>
                    <h3 class="major">Formulario ingreso de Producto</h3>
                    <asp:HiddenField ID="hdfId" Visible="true" Value="-1" runat="server" />
                    <div class="row uniform">
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Nombre:</label>
                            <asp:TextBox ID="txtNombreProducto" runat="server"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Código Númerico:</label>
                            <asp:TextBox ID="txtCodNumerico" placeholder="000" style="color: black;"  runat="server"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Precio:</label>
                            <asp:TextBox ID="txtPrecio" placeholder="¢000" style="color: black;" runat="server"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Cantidad:</label>
                            <asp:TextBox ID="txtCantidad" placeholder="000" style="color: black;"  runat="server"></asp:TextBox>
                        </div>
                        <div class="12u$">
                            <label for="demo-category">Categoría</label>
                            <div class="select-wrapper">
                                <asp:DropDownList ID="ddlCategoria" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="12u$">
                            <ul class="actions">
                                <li>
                                    <asp:Button ID="btnInsertarActualizar" runat="server" OnClick="btnInsertarActualizar_Click" Text="Guardar" CssClass="special" />
                                </li>
                                <li>
                                    <asp:Button ID="btnLimpiar" runat="server" Text="Nuevo" OnClick="btnLimpiar_Click" CssClass="special" />
                                </li>
                            </ul>
                        </div>
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
                    <asp:GridView runat="server" ID="gvwProductos" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwProductos_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="IdProducto" HeaderText="Id" AccessibleHeaderText="IdProducto" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" AccessibleHeaderText="NombreProducto" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="IdCategoria" HeaderText="Categoría" AccessibleHeaderText="IdCategoria" InsertVisible="False"></asp:BoundField>
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
            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
</asp:Content>
