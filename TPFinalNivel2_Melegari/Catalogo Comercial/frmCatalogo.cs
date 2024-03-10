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

namespace Catalogo_Comercial
{
    public partial class frmCatalogo : Form
    {
        private List<Electrodomestico> listaElectrodomestico;
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();
            listaElectrodomestico = service.listar();
            dgvCatalogo.DataSource = listaElectrodomestico;
            dgvCatalogo.Columns["ImagenUrl"].Visible = false;
            cargarImagen(listaElectrodomestico[0].ImagenUrl);


        }

        private void dgvCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            Electrodomestico seleccionado = (Electrodomestico)dgvCatalogo.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);    


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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaElectrodomestico alta = new frmAltaElectrodomestico();
            alta.ShowDialog();
        }
    }
}
