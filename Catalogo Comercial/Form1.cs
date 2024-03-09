using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catalogo_Comercial
{
    public partial class Form1 : Form
    {
        private List<Electrodomestico> listaElectrodomestico;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();
            listaElectrodomestico = service.listar();
            dgvCatalogo.DataSource = listaElectrodomestico;
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
    }
}
