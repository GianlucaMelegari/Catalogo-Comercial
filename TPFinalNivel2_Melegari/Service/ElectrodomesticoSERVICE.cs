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
                comando.CommandText = "Select Codigo,Nombre,A.Descripcion,ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, Precio From ARTICULOS A, MARCAS M, CATEGORIAS C Where M.Id=A.IdMarca AND C.Id=A.IdCategoria";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Electrodomestico aux = new Electrodomestico();
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Marcas = new Marca();
                    aux.Marcas.Descripcion = (string)lector["Marca"];
                    aux.Categorias=new Categoria();
                    aux.Categorias.Descripcion=(string)lector["Categoria"];
                    aux.Precio = (int)(decimal)lector["Precio"];

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
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre,Descripcion,Precio) values (@Codigo,@Nombre,@Descripcion,@Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);

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

        public void modificar(Electrodomestico modificar)
        {

        }
    }
}
