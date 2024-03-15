using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using Service;

namespace Servicio
{
    public class ElectrodomesticoSERVICE
    {
        public List<Electrodomestico> listar()
        {
            List<Electrodomestico> lista = new List<Electrodomestico>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Codigo,Nombre,A.Descripcion,ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria, A.Id From ARTICULOS A, MARCAS M, CATEGORIAS C Where M.Id=A.IdMarca AND C.Id=A.IdCategoria";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Electrodomestico aux = new Electrodomestico();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    //validamos la Url en caso de null
                    if(!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];

                    aux.Marcas = new Marca();
                    aux.Marcas.Id = (int)lector["IdMarca"];
                    aux.Marcas.Descripcion = (string)lector["Marca"];

                    aux.Categorias=new Categoria();
                    aux.Categorias.Id = (int)lector["IdCategoria"];
                    aux.Categorias.Descripcion=(string)lector["Categoria"];
                    aux.Precio = (decimal)lector["Precio"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void agregar(Electrodomestico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre,Descripcion,Precio,IdMarca,IdCategoria, ImagenUrl) values (@Codigo,@Nombre,@Descripcion,@Precio,@IdMarca,@IdCategoria,@ImagenUrl)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@IdMarca", nuevo.Marcas.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categorias.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);


                datos.ejecutarAccion();

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

        public void modificar(Electrodomestico electro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,IdCategoria=@IdCategoria,ImagenUrl=@ImagenUrl,Precio=@Precio where Id=@Id");
                datos.setearParametro("@Codigo", electro.Codigo);
                datos.setearParametro("@Nombre", electro.Nombre);
                datos.setearParametro("@Descripcion", electro.Descripcion);
                datos.setearParametro("@IdMarca", electro.Marcas.Id);
                datos.setearParametro("@IdCategoria", electro.Categorias.Id);
                datos.setearParametro("@ImagenUrl", electro.ImagenUrl);
                datos.setearParametro("@Precio", electro.Precio);
                datos.setearParametro("@Id", electro.Id );

                datos.ejecutarAccion();

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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id =@Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Electrodomestico> filtrar(string campo, string criterio, string filtro)
        {
            List<Electrodomestico> lista = new List<Electrodomestico>();
            AccesoDatos datos = new AccesoDatos();
            

            try
            {
                string consulta = "Select Codigo,Nombre,A.Descripcion,ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria, A.Id From ARTICULOS A, MARCAS M, CATEGORIAS C Where M.Id = A.IdMarca AND C.Id = A.IdCategoria AND ";
                
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro ;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtro ;
                            break;
                        default:
                            consulta += "Precio = " + filtro ;
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Electrodomestico aux = new Electrodomestico();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //validamos la Url en caso de null
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marcas = new Marca();
                    aux.Marcas.Id = (int)datos.Lector["IdMarca"];
                    aux.Marcas.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categorias = new Categoria();
                    aux.Categorias.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categorias.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
