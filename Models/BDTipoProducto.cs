using System.Data.SqlClient;

namespace POOII_CL3.Models
{
    public class BDTipoProducto
    {
        // CONEXION CON LA BASE DE DATOS
        string cadenaConexion = "Data Source=MINAMI-EED;" +
            "Initial Catalog=POOCL3;" +
            "Integrated Security=True;";

        public List<TipoProducto> ObtenerTodos()
        {
            List<TipoProducto> listaTiposProductos = new List<TipoProducto>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SELECT * FROM TipoProducto", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TipoProducto tipoProducto = new TipoProducto();

                tipoProducto.id = dr.GetInt32(0);
                tipoProducto.tipo = dr.GetString(1);

                listaTiposProductos.Add(tipoProducto);
            }
            return listaTiposProductos;
        }
    }
}
