<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMantenimientoClientes.aspx.cs" Inherits="joyeriaSYS.frmMantenimientoClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpCliente" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- One -->
            <section id="one" class="wrapper spotlight style1">
                <div class="inner">
                    </a>
                    <asp:HiddenField ID="hdfId" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>
                            <h3 class="major">Mantenimiento Clientes</h3>
                    <div class="row uniform">
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Nombre Encargado:</label>
                            <asp:TextBox ID="txtNombreEncargado" runat="server" ></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Nombre Joyería:</label>
                            <asp:TextBox ID="txtNombreJoyeria"  runat="server"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Célular:</label>
                            <asp:TextBox ID="txtCelular" TextMode="Phone" placeholder="88888888" runat="server" ></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Teléfono:</label>
                            <asp:TextBox ID="txtTelefono" TextMode="Phone" placeholder="22222222"  runat="server"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Dirección:</label>
                            <asp:TextBox ID="txtDirección"  runat="server"></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Provincia:</label>
                            <asp:TextBox ID="txtProvincia" runat="server" ></asp:TextBox>
                        </div>
                        <div class="6u 12u$(xsmall)">
                            <label for="demo-name">Cantón:</label>
                            <asp:TextBox ID="txtCanton"   runat="server"></asp:TextBox>
                        </div>
                        <div class="12u$">
                            <ul class="actions">
                                <li>
                                    <asp:Button ID="btnInsertarActualizar" runat="server" OnClick="btnInsertarActualizar_Click" Text="Guardar" CssClass="special" /></li>
                                <li>
                                    <input type="reset" value="Reset"></li>
                            </ul>
                        </div>
                        </section>
                    </div>
                </div>
            </section>
            <!-- Two -->
            <section id="two" class="wrapper alt spotlight style2">
                <div class="inner">
                    <div class="content">
                        <asp:GridView runat="server" ID="gvwClientes" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwCategoria_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="idCliente" HeaderText="Id" AccessibleHeaderText="idCliente" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="NombreEncargado" HeaderText="Nombre Encargado" AccessibleHeaderText="NombreEncargado" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="NombreJoyeria" HeaderText="Joyería" AccessibleHeaderText="NombreJoyeria" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Celular" HeaderText="Célular" AccessibleHeaderText="Celular" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" AccessibleHeaderText="Telefono" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" AccessibleHeaderText="Direccion" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Provincia" HeaderText="Provincia" AccessibleHeaderText="Provincia" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Canton" HeaderText="Cantón" AccessibleHeaderText="Canton" InsertVisible="False"></asp:BoundField>
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
            <asp:AsyncPostBackTrigger ControlID="gvwClientes" EventName="SelectedIndexChanged" />
        </Triggers>
                </asp:UpdatePanel>
</asp:Content>
