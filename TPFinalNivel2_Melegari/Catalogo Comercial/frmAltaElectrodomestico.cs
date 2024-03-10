using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Servicio;
using Service;

namespace Catalogo_Comercial
{
    public partial class frmAltaElectrodomestico : Form
    {
        private Electrodomestico electrodomestico = null;
        public frmAltaElectrodomestico()
        {
            InitializeComponent();
        }
        public frmAltaElectrodomestico(Electrodomestico electrodomestico)
        {
            InitializeComponent();
            this.electrodomestico = electrodomestico;
            Text = "Modificar Electrodomestico";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Electrodomestico electro = new Electrodomestico();
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();
            try
            {
                if (electrodomestico==null)
                    electrodomestico = new Electrodomestico();

                electrodomestico.Codigo = txtCodigo.Text;
                electrodomestico.Nombre = txtNombre.Text;
                electrodomestico.Descripcion = txtDescripcion.Text;
                electrodomestico.ImagenUrl = txtImagenUrl.Text;
                electrodomestico.Precio = int.Parse(txtPrecio.Text);
                electrodomestico.Marcas = (Marca)cbxMarca.SelectedItem;
                electrodomestico.Categorias=(Categoria)cbxCategoria.SelectedItem;

                if (electrodomestico.Id != 0)
                {
                    service.modificar(electrodomestico);
                    MessageBox.Show("Modificado exitosamente!");
                } 
                else
                {
                    service.agregar(electrodomestico);
                    MessageBox.Show("Agregado exitosamente!");
                }


                Close();   
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaElectrodomestico_Load(object sender, EventArgs e)
        {
            MarcaSERVICE marcaSERVICE = new MarcaSERVICE();
            CategoriaSERVICE categoriaSERVICE = new CategoriaSERVICE(); 
            try
            {
                cbxMarca.DataSource = marcaSERVICE.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
                cbxCategoria.DataSource = categoriaSERVICE.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                if(electrodomestico != null)
                {
                    txtCodigo.Text = electrodomestico.Codigo;
                    txtNombre.Text = electrodomestico.Nombre;
                    txtDescripcion.Text = electrodomestico.Descripcion;
                    txtImagenUrl.Text=electrodomestico.ImagenUrl;
                    txtPrecio.Text=electrodomestico.Precio.ToString();
                    cargarImagen(electrodomestico.ImagenUrl);
                    cbxMarca.SelectedValue = electrodomestico.Marcas.Id;
                    cbxCategoria.SelectedValue = electrodomestico.Categorias.Id;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbElectrodomestico.Load(imagen);

            }
            catch (Exception ex)
            {

                pbElectrodomestico.Load("https://cdn.pixabay.com/photo/2017/02/12/21/29/false-2061132_640.png");
            }
        }

    }
}
