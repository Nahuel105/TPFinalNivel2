using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {


        public List<Marca> listar()
        {
            List<Marca> marcas = new List<Marca>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("select Id, Descripcion from Marcas");
                accesoDatos.ejecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Marca aux = new Marca();

                    aux.Id = (int)accesoDatos.Lector["Id"];
                    aux.Descripcion = (string)accesoDatos.Lector["Descripcion"];

                    marcas.Add(aux);
                }
                return marcas;

            }
            catch (Exception ex)
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