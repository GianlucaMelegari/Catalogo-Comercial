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
        public frmAltaElectrodomestico()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Electrodomestico electro = new Electrodomestico();
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();
            try
            {
                electro.Codigo = txtCodigo.Text;
                electro.Nombre = txtNombre.Text;
                electro.Descripcion = txtDescripcion.Text;
                electro.Precio = int.Parse(txtPrecio.Text);
                electro.Marcas = (Marca)cbxMarca.SelectedItem;
                electro.Categorias=(Categoria)cbxCategoria.SelectedItem;

                service.agregar(electro);
                MessageBox.Show("Agregado exitosamente!");
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
                cbxCategoria.DataSource = categoriaSERVICE.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
