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
using Negocio;

namespace WindowsFormsApp3
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private List<Articulo> listaArticulos;
        private void Form4_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articulos = new ArticuloNegocio();
            listaArticulos = articulos.listarBajados();
            dataGridView1.DataSource = listaArticulos;
            dataGridView1.ReadOnly = true;

            try
            {
                dataGridView1.Columns["ImagenUrl"].Visible = false;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Descripcion"].Visible = false;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Precio"].Visible= false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo articulo = new Articulo();

                articulo = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                articuloNegocio.activarArticulo(articulo.Id);
                ArticuloNegocio articulos = new ArticuloNegocio();
                listaArticulos = articulos.listarBajados();
                dataGridView1.DataSource = listaArticulos;

            }
            catch( Exception ex ) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltro;


            if (textBox1.Text.Length >= 2)
            {
                listaFiltro = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(textBox1.Text.ToUpper()));
            }
            else
            {
                listaFiltro = listaArticulos;
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaFiltro;

            try
            {
                dataGridView1.Columns["ImagenUrl"].Visible = false;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Descripcion"].Visible = false;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Precio"].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }


           
        }
    }
}
