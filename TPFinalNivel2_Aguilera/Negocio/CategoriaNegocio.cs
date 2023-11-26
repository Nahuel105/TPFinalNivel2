using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> categorias = new List<Categoria>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("select Id, Descripcion from CATEGORIAS");
                accesoDatos.ejecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)accesoDatos.Lector["Id"];
                    aux.Descripcion = (string)accesoDatos.Lector["Descripcion"];

                    categorias.Add(aux);
                }
                return categorias;

            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                accesoDatos.cerrarConexion();

            }
        }
    }
}
