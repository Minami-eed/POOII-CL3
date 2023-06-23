using System.Data;
using System.Data.SqlClient;

namespace POOII_CL3.Models
{
    public class BDProducto
    {
        // CONEXION CON LA BASE DE DATOS
        string cadenaConexion = "Data Source=MINAMI-EED;" +
            "Initial Catalog=POOCL3;" +
            "Integrated Security=True;";

        // LISTAR -----------------------------------------------------------------------------------------
        public List<Producto> ObtenerTodos()
        {
            List<Producto> listaProducto = new List<Producto>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            string sql = "select p.id, p.nombre, tp.tipo, p.precio, p.fecha " +
                        "from Producto p " +
                        "inner join TipoProducto tp " +
                        "on p.idtipo=tp.id";
            SqlCommand cmd = new SqlCommand(sql, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Producto producto = new Producto();

                producto.id = dr.GetInt32(0);
                producto.nombre = dr.GetString(1);
                producto.idtipo = dr.GetString(2);
                producto.precio = dr.GetDecimal(3);
                producto.fecha = dr.GetDateTime(4).Date;

                listaProducto.Add(producto);
            }
            return listaProducto;
        }

        // BUSCAR POR ID -----------------------------------------------------------------------------------
        public Producto BuscarPorId(int id)
        {
            Producto? producto = null;
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("sp_GetProductoByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                producto = new Producto();
                producto.id = dr.GetInt32(0);
                producto.nombre = dr.GetString(1);
                producto.idtipo = dr.GetString(2);
                producto.precio = dr.GetDecimal(3);
                producto.fecha = dr.GetDateTime(4).Date;
            }
            con.Close();

            return producto;
        }


        // CREAR -------------------------------------------------------------------------------------------
        public int Crear(string nombre, int tipo, decimal precio, DateTime fecha)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("sp_AddProducto", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Agregar los parámetros al comando
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@idtipo", tipo);
            cmd.Parameters.AddWithValue("@precio", precio);
            cmd.Parameters.AddWithValue("@fecha", fecha.Date);

            con.Open();

            // Ejecutar el comando y obtener el número de filas afectadas
            int filasAfectadas = cmd.ExecuteNonQuery();

            con.Close();

            return filasAfectadas;
        }

        // ACTUALIZAR --------------------------------------------------------------------------------------
        public int Actualizar(Producto producto)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("sp_UpdateProducto", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Agregar los parámetros al comando
            cmd.Parameters.AddWithValue("@nombre", producto.nombre);
            cmd.Parameters.AddWithValue("@idtipo", producto.idtipo);
            cmd.Parameters.AddWithValue("@precio", producto.precio);
            cmd.Parameters.AddWithValue("@fecha", producto.fecha.Date);
            cmd.Parameters.AddWithValue("@id", producto.id); // Agregar el parámetro ID

            con.Open();

            // Ejecutar el comando y obtener el número de filas afectadas
            int filasAfectadas = cmd.ExecuteNonQuery();

            con.Close();

            return filasAfectadas;
        }

        // ELIMINAR ----------------------------------------------------------------------------------------
        public int Eliminar(int id)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("sp_DeleteProducto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int nroClientes = cmd.ExecuteNonQuery();
            con.Close();
            return nroClientes;
        }
    }
}
