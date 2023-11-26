using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(" select A.Id , Codigo, Nombre, A.Descripcion,Precio,ImagenUrl , C.Descripcion  as 'Categoria' , M.Descripcion as 'Marca', A.IdCategoria, A.IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M where IdCategoria = C.Id and IdMarca = M.Id and A.Precio > 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("imagenUrl")))) ;
                     aux.ImagenUrl = (string)datos.Lector["imagenUrl"];
                   // aux.IdCategoria = (int)datos.Lector["IdCategoria"];
                    //aux.IdMarca = (int)datos.Lector["IdMarca"];
                    
                    
                    
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    listaArticulos.Add(aux);
                }

                return listaArticulos;
                
                
            }

            catch (Exception ex)
            { 
                throw ex;

            }
            finally
            {
                datos.cerrarConexion();

            }

          
        }
        public List<Articulo> listarBajados()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(" select A.Id , Codigo, Nombre, A.Descripcion,Precio,ImagenUrl , C.Descripcion  as 'Categoria' , M.Descripcion as 'Marca', A.IdCategoria, A.IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M where IdCategoria = C.Id and IdMarca = M.Id and A.Precio = 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("imagenUrl")))) ;
                    aux.ImagenUrl = (string)datos.Lector["imagenUrl"];
                    // aux.IdCategoria = (int)datos.Lector["IdCategoria"];
                    //aux.IdMarca = (int)datos.Lector["IdMarca"];



                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    listaArticulos.Add(aux);
                }

                return listaArticulos;


            }

            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                datos.cerrarConexion();

            }


        }

        public void agregarArticulo(Articulo nuevo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, Precio, IdCAtegoria, IdMarca) values (@Codigo, @Nombre, @Descripcion, @ImagenUrl, @Precio, @IdCategoria, @IdMarca )");
                accesoDatos.setearParametro("@Codigo", nuevo.Codigo);
                accesoDatos.setearParametro("@Nombre", nuevo.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevo.Descripcion);
                accesoDatos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                accesoDatos.setearParametro("@Precio", nuevo.Precio);
                accesoDatos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                accesoDatos.setearParametro("@IdMarca", nuevo.Marca.Id);

                accesoDatos.ejecutarAccion();
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

        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descr, IdMarca = @idmarca, IdCategoria = @idcategoria, ImagenUrl = @img, Precio = @precio where Id = @id");
                datos.setearParametro("@codigo", modificar.Codigo);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@descr", modificar.Descripcion);
                datos.setearParametro("@idmarca", modificar.Marca.Id);
                datos.setearParametro("@idcategoria", modificar.Categoria.Id);
                datos.setearParametro("@img", modificar.ImagenUrl);
                datos.setearParametro("@precio", modificar.Precio);
                datos.setearParametro("@id", modificar.Id);

                datos.ejecutarAccion();
            }
            catch ( Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void dardebaja(int id )
        {
            try
            {
                AccesoDatos datos=new AccesoDatos();
                datos.setearConsulta("update ARTICULOS set Precio = 0 where id = @id");
                datos.setearParametro("@id", id );
                datos.ejecutarAccion();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void activarArticulo(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update ARTICULOS set Precio = 1 where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
