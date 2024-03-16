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
            cargar();
            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");


        }

        private void dgvCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvCatalogo.CurrentRow != null)
            {
                Electrodomestico seleccionado = (Electrodomestico)dgvCatalogo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);    
            }

        }

        private void cargar()
        {
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();

            try
            {
                listaElectrodomestico = service.listar();
                dgvCatalogo.DataSource = listaElectrodomestico;
                ocultarColumnas();
                cargarImagen(listaElectrodomestico[0].ImagenUrl);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void ocultarColumnas()
        {
            dgvCatalogo.Columns["ImagenUrl"].Visible = false;
            dgvCatalogo.Columns["Id"].Visible = false;

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
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Electrodomestico seleccionado;
            seleccionado = (Electrodomestico)dgvCatalogo.CurrentRow.DataBoundItem;

            frmAltaElectrodomestico modificar = new frmAltaElectrodomestico(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();
            Electrodomestico seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad queres eliminarlo?","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Electrodomestico)dgvCatalogo.CurrentRow.DataBoundItem;
                    service.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        /*private void btnFiltrarRapido_Click(object sender, EventArgs e)
        {
            List<Electrodomestico> listaFiltrada;
            string filtro = txtFiltrarRapido.Text;

            if(filtro != "")
            {
                listaFiltrada = listaElectrodomestico.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Categorias.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            } 
            else
            {
                listaFiltrada = listaElectrodomestico;
            }
 
            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = listaFiltrada;
            ocultarColumnas();
        }*/

        private void txtFiltrarRapido_TextChanged(object sender, EventArgs e)
        {
            
                List<Electrodomestico> listaFiltrada;
                string filtro = txtFiltrarRapido.Text;

                if (filtro.Length >= 3)
                {
                    listaFiltrada = listaElectrodomestico.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Categorias.Descripcion.ToUpper().Contains(filtro.ToUpper()));

                }
                else
                {
                    listaFiltrada = listaElectrodomestico;
                }

                dgvCatalogo.DataSource = null;
                dgvCatalogo.DataSource = listaFiltrada;
                ocultarColumnas();

            
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if(opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            } 
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }

        private bool validarFiltro()
        {
            if(cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return true;
            }
            
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return true;
            }

            if(cboCampo.SelectedItem.ToString()== "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para buscar.");
                    return true;

                }
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Solo numeros para filtar por el campo Precio");
                    return true;
                }
            }

            return false;
        
        }

        private bool soloNumeros(string cadena)
        {
            foreach(char caracter in cadena)
            {
                if(!(char.IsNumber(caracter)))
                    return false;
            }

            return true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ElectrodomesticoSERVICE service = new ElectrodomesticoSERVICE();
            try
            {
                if (validarFiltro())
                    return;

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                dgvCatalogo.DataSource = service.filtrar(campo,criterio,filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
