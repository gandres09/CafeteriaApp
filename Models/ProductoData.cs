using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using CafeteriaApp.Models;

namespace CafeteriaApp.Data
{
    public static class ProductoData
    {
        public static List<Producto> ObtenerTodos()
        {
            var lista = new List<Producto>();
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Productos WHERE Estado = 'Activo'";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Estado = reader["Estado"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public static void DescontarStock(int productoId, int cantidad)
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = "UPDATE Productos SET Stock = Stock - @cantidad WHERE Id = @id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@id", productoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
