<%@ Page Title="Mantenimiento Categoria" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMantenimientoCategoria.aspx.cs" Inherits="joyeriaSYS.frmMantenimientoCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpCategoria" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- One -->
            <section id="one" class="wrapper spotlight style1">
                <div class="inner">
                    <a href="#" class="image">
                        <img src="../images/pic01.jpg" alt="" />
                    </a>
                    <asp:HiddenField ID="hdfId" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>
                            <h3 class="major">Mantenimiento Categoría</h3>
                            <div class="row uniform">
                                <div class="6u 12u$(xsmall)">
                                    <label for="demo-name">Nombre:</label>
                                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                                </div>
                                <div class="6u 12u$(xsmall)">
                                    <label for="demo-name">Código:</label>
                                    <asp:TextBox ID="txtCodigo"  runat="server"></asp:TextBox>
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
                    <a href="#" class="image">
                        <img src="../images/pic02.jpg" alt="" /></a>
                    <div class="content">
                        <asp:GridView runat="server" ID="gvwCategoria" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwCategoria_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="idCategoria" HeaderText="Id" AccessibleHeaderText="idCategoria" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" AccessibleHeaderText="Nombre" InsertVisible="False"></asp:BoundField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" AccessibleHeaderText="Codigo" InsertVisible="False"></asp:BoundField>
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
            <asp:AsyncPostBackTrigger ControlID="gvwCategoria" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
