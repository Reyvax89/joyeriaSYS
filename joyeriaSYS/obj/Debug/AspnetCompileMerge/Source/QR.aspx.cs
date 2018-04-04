using System;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Data;
using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace joyeriaSYS
{
    public partial class QR : Page
    {
        private Producto objProd = new Producto();
        private Categoria objCat = new Categoria();
        private IdQR objQR = new IdQR();
        QrEncoder encoder = new QrEncoder();
        QrCode en = new QrCode();
        public MemoryStream ms = new MemoryStream();
        public BitMatrix Matrix;
        protected void Page_Load(object sender, EventArgs e)
        {
            //QRImage.ImageUrl = "../images/pic01.jpg";
            if (!IsPostBack)
            {
                cargarCategoria();
                CargarTablaProductos();
            }
        }
        public void MostrarMensaje(string Mensaje) {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Mensaje + "');", true);
        }
        public void ErrorMensajer(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Mensaje + "');", true);
        }
        public Image Render(MemoryStream ms)
        {
            //Image returnImage = Image.FromStream(ms, true);//Exception occurs here
            //try
            //{
                var render = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two));
                render.WriteToStream(Matrix, ImageFormat.Png, ms);
                //ContentType = "image/png";
                var returnImage = Image.FromStream(ms, true);//Exception occurs here
            //}
            //catch(Exception ex)
            //{
                //var err = ex.Message;
            //}
            return returnImage;
        }

        public void cargarCategoria()
        {
            try
            {
                var dt = new DataTable();
                var rows = objCat.Consultar();

                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "idCategoria";
                dt.Columns.Add("idCategoria", typeof(System.String));
                dt.Columns.Add("Nombre", typeof(System.String));

                foreach (CAT_CATEGORIA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idCategoria"] = r.idCategoria;
                    fila["Nombre"] = r.Nombre;
                    dt.Rows.Add(fila);
                }
                ddlCategoria.DataSource = dt;
                ddlCategoria.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        public string concatenarInformacion()
        {
            return txtCodNumerico.Text + ";" + ddlCategoria.SelectedValue;
        }

        public string generarQR(string idQR)
        {
            var codigoParaQR = concatenarInformacion();
            //var ruta = "C:\\users\\cerva\\documents\\visual studio 2015\\Projects\\joyeriaSYS\\joyeriaSYS\\ImagenesQR\\" + idQR + ".png";
            var ruta = "C:\\Users\\Administrator\\Documents\\CodigosQR\\" + idQR + ".png";
            Matrix = encoder.Encode(codigoParaQR).Matrix;
            ms = new MemoryStream();
            Image q = Render(ms);
            q.Save(ruta);

            //QRImage.ImageUrl = "../ImagenesQR/" + txtNombreProducto.Text + txtCodNumerico.Text + ".png";
            //QRImage.ImageUrl = "../ImagenesQR/" + idQR + ".png";
            QRImage.ImageUrl = "/CodigosQR/" + idQR + ".png";
            return ruta;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = "";
            txtCodNumerico.Text = "";
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
            hdfId.Value = "-1";
            //QRImage.ImageUrl = "../images/pic01.jpg";
        }
        protected void btnInsertarActualizar_Click(object sender, EventArgs e)
        {
            guardarActualizar(Convert.ToInt32(hdfId.Value));
            btnInsertarActualizar.Text = "Guardar";
            txtPrecio.Text = "";
            txtNombreProducto.Text = "";
            txtCodNumerico.Text = "";
            txtCantidad.Text = "";
            CargarTablaProductos();
        }
        public int guardarActualizar(int id)
        {
            PRO_PRODUCTO temp = new PRO_PRODUCTO();
            int existe = 0;
            temp.IdProducto = id;
            temp.CodigoNumerico = Convert.ToInt32(txtCodNumerico.Text);
            temp.IdCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            temp.Inventario = Convert.ToInt32(txtCantidad.Text);
            temp.NombreProducto = txtNombreProducto.Text;
            temp.Precio = Convert.ToInt32(txtPrecio.Text);

            existe = objProd.ConsultarPorNombreCodigoCategoria(temp).Count();

            CQR_CODIGO_QR tempQR = new CQR_CODIGO_QR();
            
            if (existe > 0)
            {
                //tempQR.idProducto = temp.IdProducto;
                //tempQR.idQR = Convert.ToInt32(temp.IdProducto+""+ temp.CodigoNumerico);
                //tempQR.urlImagen = generarQR(Convert.ToString(tempQR.idQR));
                objProd.Actualizar(temp);
                //objQR.Actualizar(tempQR);
            }
            else
            {
                if (objProd.Existe(temp))
                {
                    ErrorMensajer("Producto ya existe con esta categoria!");
                }else
                {
                    temp = objProd.Insertar(temp);
                }
                

                //tempQR.idProducto = temp.IdProducto;
                //tempQR.idQR = Convert.ToInt32(temp.IdProducto + "" + temp.CodigoNumerico);
                //tempQR.urlImagen = generarQR(Convert.ToString(tempQR.idQR));

                //objQR.Insertar(tempQR);
            }
            return 1;
        }

        public void CargarTablaProductos()
        {
            try
            {
                var dt = new DataTable();
                var rows = objProd.Consultar();

                dt.Columns.Add("IdProducto", typeof(System.String));
                dt.Columns.Add("NombreProducto", typeof(System.String));
                dt.Columns.Add("Metal", typeof(System.String));
                dt.Columns.Add("CodigoNumerico", typeof(System.String));
                dt.Columns.Add("Precio", typeof(System.String));
                dt.Columns.Add("Inventario", typeof(System.String));

                foreach (PRO_PRODUCTO r in rows)
                {
                    var tempCategoria = new CAT_CATEGORIA();
                    tempCategoria.idCategoria = r.IdCategoria;
                    DataRow fila = dt.NewRow();

                    fila["IdProducto"] = r.IdProducto;
                    fila["NombreProducto"] = r.NombreProducto;
                    fila["Metal"] = objCat.ConsultarPorId(tempCategoria).FirstOrDefault().Nombre;
                    fila["CodigoNumerico"] = r.CodigoNumerico;
                    fila["Precio"] = r.Precio;
                    fila["Inventario"] = r.Inventario;
                    dt.Rows.Add(fila);
                }
                gvwProductos.DataSource = dt;
                gvwProductos.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        protected void gvwProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tempCategoria = new CAT_CATEGORIA();
            tempCategoria.Nombre = gvwProductos.SelectedRow.Cells[2].Text;
            tempCategoria = objCat.ConsultarPorNombre(tempCategoria).FirstOrDefault();

            hdfId.Value = gvwProductos.SelectedRow.Cells[0].Text;
            txtNombreProducto.Text = gvwProductos.SelectedRow.Cells[1].Text;
            ddlCategoria.SelectedValue = tempCategoria.idCategoria.ToString();
            txtCodNumerico.Text = gvwProductos.SelectedRow.Cells[3].Text;
            txtPrecio.Text = gvwProductos.SelectedRow.Cells[4].Text;
            txtCantidad.Text = gvwProductos.SelectedRow.Cells[5].Text;
            btnInsertarActualizar.Text = "Actualizar";

            //var temp = new CQR_CODIGO_QR();
            //temp.idQR = Convert.ToInt32(hdfId.Value + "" + txtCodNumerico.Text);
            //temp = objQR.ConsultarPorId(temp).FirstOrDefault();

            //QRImage.ImageUrl = "../" + temp.urlImagen.Substring(70);
            //QRImage.ImageUrl = "../" + temp.urlImagen.Substring(32);
        }

        protected void txtCodNumerico_TextChanged(object sender, EventArgs e)
        {
            txtPrecio.Text = txtCodNumerico.Text + "00";
        }
        
        //OnPageIndexChanged="gvwProductos_PageIndexChanged"
        protected void gvwProductos_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvwProductos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvwProductos.PageIndex = e.NewPageIndex;
            CargarTablaProductos();
        }
    }
}