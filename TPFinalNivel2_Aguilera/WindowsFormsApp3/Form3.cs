using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
using System.Configuration;

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
       
        private Articulo articuloMod = null;
        private OpenFileDialog archivo = null;

        public Form3()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
        }
        public Form3(Articulo articuloMod)
        {
            InitializeComponent();
            this.articuloMod = articuloMod;
            Text = "Modificar Articulo";
        }



        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio nuevo = new ArticuloNegocio();

            try
            {
                if (articuloMod == null)
                    articuloMod = new Articulo();
                

                articuloMod.Codigo = textBoxCod.Text;
                articuloMod.Descripcion = textBoxDescr.Text;
                decimal nuevoPrecio;
                if (decimal.TryParse(textBoxPrecio.Text, out nuevoPrecio))
                {
                    articuloMod.Precio = nuevoPrecio;
                }
               
                articuloMod.Nombre = textBoxNombre.Text;
                articuloMod.ImagenUrl = textBoxImg.Text;
                articuloMod.Categoria = (Categoria)comboBoxCategoria.SelectedItem;
                articuloMod.Marca = (Marca)comboBoxMarca.SelectedItem;




                if (articuloMod.Id != 0)
                {
                    if (string.IsNullOrEmpty(textBoxCod.Text))
                    {
                        errorProvider1.SetError(textBoxCod, "Rellenar campo");


                    }
                    else if (string.IsNullOrEmpty(textBoxNombre.Text))
                    {
                        errorProvider1.SetError(textBoxNombre, "Rellenar campo");

                    }
                    else if (string.IsNullOrEmpty(textBoxDescr.Text))
                    {
                        errorProvider1.SetError(textBoxDescr, "Rellenar campo");
                    }
                    else if (string.IsNullOrEmpty(textBoxPrecio.Text)) 
                    {
                        errorProvider1.SetError(textBoxPrecio, "Rellenar campo");
                    }
                    else
                    {
                        errorProvider1.SetError(textBoxCod, "");
                        errorProvider1.SetError(textBoxNombre, "");
                        errorProvider1.SetError(textBoxDescr, "");
                        errorProvider1.SetError(textBoxPrecio, "");
                        nuevo.modificar(articuloMod);
                        MessageBox.Show("Modificado correctamente");
                        Close();
                    }
                }

                if (articuloMod.Id == 0)
                {
                    if (string.IsNullOrEmpty(textBoxCod.Text))
                    {
                        errorProvider1.SetError(textBoxCod, "Rellenar campo");


                    }
                    else if (string.IsNullOrEmpty(textBoxNombre.Text))
                    {
                        errorProvider1.SetError(textBoxNombre, "Rellenar campo");

                    }
                    else if (string.IsNullOrEmpty(textBoxDescr.Text))
                    {
                        errorProvider1.SetError(textBoxDescr, "Rellenar campo");
                    }
                    else if (string.IsNullOrEmpty(textBoxPrecio.Text))
                    {
                        errorProvider1.SetError(textBoxPrecio, "Rellenar campo");
                    }
                    else
                    {
                        errorProvider1.SetError(textBoxCod, "");
                        errorProvider1.SetError(textBoxNombre, "");
                        errorProvider1.SetError(textBoxDescr, "");
                        errorProvider1.SetError(textBoxPrecio, "");
                        nuevo.agregarArticulo(articuloMod);
                        MessageBox.Show("Agregado correctamente");
                        Close();
                    }
                }

                if(archivo != null && !(textBoxImg.Text.ToUpper().Contains("HTTP")))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["imagenes-proyecto"] + archivo.SafeFileName);
                }


                

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }
        }

       
        

        private void Form3_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();

            
            try
            {
                comboBoxCategoria.DataSource = categoria.listar();
                comboBoxCategoria.ValueMember = "Id";
                comboBoxCategoria.DisplayMember = "Descripcion";
                comboBoxMarca.DataSource = marca.listar();
                comboBoxMarca.ValueMember = "Id";
                comboBoxMarca.DisplayMember = "Descripcion";



                if (articuloMod != null)
                {
                    textBoxCod.Text = articuloMod.Codigo;
                    textBoxNombre.Text = articuloMod.Nombre;
                    textBoxDescr.Text = articuloMod.Descripcion;
                    textBoxImg.Text = articuloMod.ImagenUrl;
                    cargarimagen(articuloMod.ImagenUrl);
                    textBoxPrecio.Text = articuloMod.Precio.ToString();
                    comboBoxCategoria.SelectedValue = articuloMod.Categoria.Id;
                    comboBoxMarca.SelectedValue = articuloMod.Marca.Id;
                }

            }catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }


        }

        private void textBoxImg_Leave(object sender, EventArgs e)
        {

            cargarimagen(textBoxImg.Text);
            
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

        private void buttonagregarimg_Click(object sender, EventArgs e)
        {
              archivo = new OpenFileDialog();
            archivo.Filter = "Archivos de imagen|*.avif;*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                textBoxImg.Text = archivo.FileName;
                cargarimagen(archivo.FileName);

               // File.Copy(archivo.FileName, ConfigurationManager.AppSettings["imagenes-proyecto"] + archivo.SafeFileName);
            }

        }

        private void textBoxPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.' && e.KeyChar != ',') ||
                    (e.KeyChar == '.' && (sender as TextBox).Text.Contains(".") ||
                     e.KeyChar == ',' && (sender as TextBox).Text.Contains(",")))
            {
                
                e.Handled = true;
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            textBoxCod.Text = "";
            textBoxDescr.Text = "";
            textBoxNombre.Text = "";
            textBoxImg.Text = "";
            textBoxPrecio.Text = "";

        }
    }
}
