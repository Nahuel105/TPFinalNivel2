using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private List<Articulo> listaArticulos = new List<Articulo>();
        private List<Categoria> listaCategoria = new List<Categoria>();
        private List<Marca> listaMarca = new List<Marca>();
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void botonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articulos = new ArticuloNegocio();
            listaArticulos = articulos.listar();
            dataGridView1.DataSource = listaArticulos;
            
            ocultarFilas();

           

        }

        private void ocultarFilas()
        {
            try
            {
                dataGridView1.Columns["ImagenUrl"].Visible = false;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Descripcion"].Visible = false;

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        private void cargarimagen(string imagen)
        {
            try
            {
                pictureBox1.Load(imagen);
            }
            catch (Exception ex)
            {
                pictureBox1.Load("https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                cargarimagen(seleccionado.ImagenUrl);
            }
        }

        private void botonAgregar_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            ArticuloNegocio articulos = new ArticuloNegocio();
            listaArticulos = articulos.listar();
            dataGridView1.DataSource = listaArticulos;
            ocultarFilas();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
    {
               
                string descripcion = dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                
                MessageBox.Show(descripcion, nombre, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void botonModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

            
            Form3 modificar = new Form3(seleccionado);
            if (modificar.ShowDialog() == DialogResult.OK)
            {
                
                ArticuloNegocio articulos = new ArticuloNegocio();
                listaArticulos = articulos.listar();
                dataGridView1.DataSource = listaArticulos;
                ocultarFilas();
            }
        }

        private void botonEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo = new Articulo();
            try
            {
             DialogResult respuesta =  MessageBox.Show("Esto eliminara el articulo por completo", "Eliminando", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.OK)
                {
                    articulo = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                    articuloNegocio.eliminar(articulo.Id);
                   

                    ArticuloNegocio articulos = new ArticuloNegocio();
                    listaArticulos = articulos.listar();
                    dataGridView1.DataSource = listaArticulos;
                    ocultarFilas();

                }
                
               
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void botonbajar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulonegocio = new ArticuloNegocio();
            Articulo articulo = new Articulo();

         DialogResult resultado =    MessageBox.Show("Esta a punto de dar de baja el articulo indefinidamente", "Inhabilitando", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);


            try
            {
                if(resultado == DialogResult.OK)
                {
                   
                    articulo = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                    articulonegocio.dardebaja(articulo.Id);
                    ArticuloNegocio articulos = new ArticuloNegocio();
                    listaArticulos = articulos.listar();
                    dataGridView1.DataSource = listaArticulos;
                    ocultarFilas();
                }
                
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            ArticuloNegocio articulos = new ArticuloNegocio();
            listaArticulos = articulos.listar();
            dataGridView1.DataSource = listaArticulos;
        }

     

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltro;


            if (textBoxFiltro.Text.Length >=  2)
            {
                listaFiltro = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(textBoxFiltro.Text.ToUpper()));
            }
            else
            {
                listaFiltro = listaArticulos;
            }


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaFiltro;
            ocultarFilas();


        }

        private void textBoxCod_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltro;


            if (textBoxCod.Text.Length >=  2)
            {
                listaFiltro = listaArticulos.FindAll(x => x.Codigo.ToUpper().Contains(textBoxCod.Text.ToUpper()));
            }
            else
            {
                listaFiltro = listaArticulos;
            }


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaFiltro;
            ocultarFilas();

        }

       
    }
}
