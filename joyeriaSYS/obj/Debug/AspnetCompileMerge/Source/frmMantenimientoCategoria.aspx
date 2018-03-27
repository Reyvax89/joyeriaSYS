<%@ Page Title="Mantenimiento Categoria" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMantenimientoCategoria.aspx.cs" Inherits="joyeriaSYS.frmMantenimientoCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpCategoria" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- One -->
            <section id="one" class="wrapper spotlight style1">
                <div class="inner">
                    <asp:HiddenField ID="hdfId" Visible="true" Value="-1" runat="server" />
                    <div class="content">
                        <section>                        
							<div class="container">
								<h1 class="major">Mantenimiento Categoría</h1>
								<div class="row margin-b">
									<div class="col-sm-5 col-md-3">
										<label for="demo-name">Nombre:</label>
									</div>
									<div class="col-sm-7 col-md-9">
										<asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
									</div>
								</div>
								<div class="row margin-b">
									<div class="col-sm-7 col-md-9">
										<asp:Button ID="btnInsertarActualizar" runat="server" OnClick="btnInsertarActualizar_Click" Text="Guardar" CssClass="btn btn-primary special" />
										<input type="reset" value="Reset" class="btn btn-default">
									</div>
								</div>
							</div>
                        </section>
                    </div>
                </div>
            </section>
            <!-- Two -->
            <section id="two" class="wrapper alt spotlight style2">
                <div class="container">
                    <div class="row">
						<div class="col-sm-12 col-md-12">
							<asp:GridView runat="server" ID="gvwCategoria" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gvwCategoria_SelectedIndexChanged">
								<Columns>
									<asp:BoundField DataField="idCategoria" HeaderText="Id" AccessibleHeaderText="idCategoria" InsertVisible="False"></asp:BoundField>
									<asp:BoundField DataField="Nombre" HeaderText="Nombre" AccessibleHeaderText="Nombre" InsertVisible="False"></asp:BoundField>
									<%--<asp:BoundField DataField="Codigo" HeaderText="Codigo" AccessibleHeaderText="Codigo" InsertVisible="False"></asp:BoundField>--%>
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
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInsertarActualizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwCategoria" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
