using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeteriaApp.Models;
using System.Data.SQLite;

namespace CafeteriaApp.Data
{
    public static class VentaData
    {
        public static void RegistrarVenta(Venta venta)
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"
                    INSERT INTO Ventas
                    (FechaHora, ProductoId, Cantidad, PrecioUnitario, Total, ArqueoId)
                    VALUES
                    (@fecha, @productoId, @cantidad, @precio, @total, @arqueoId)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", venta.FechaHora);
                    cmd.Parameters.AddWithValue("@productoId", venta.ProductoId);
                    cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@precio", venta.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@total", venta.Total);
                    cmd.Parameters.AddWithValue("@arqueoId", venta.ArqueoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Venta> ObtenerVentasPorArqueo(int arqueoId)
        {
            List<Venta> ventas = new List<Venta>();

            using (var conexion = BaseDatos.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT v.Id, v.FechaHora, v.ProductoId, p.Nombre AS NombreProducto, 
                                        v.Cantidad, v.PrecioUnitario, v.Total, v.ArqueoId
                                 FROM Ventas v
                                 JOIN Productos p ON v.ProductoId = p.Id
                                 WHERE v.ArqueoId = @ArqueoId";

                using (var cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ArqueoId", arqueoId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ventas.Add(new Venta
                            {
                                IdVenta = Convert.ToInt32(reader["Id"]),
                                FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                ProductoId = Convert.ToInt32(reader["ProductoId"]),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                PrecioUnitario = Convert.ToDouble(reader["PrecioUnitario"]),
                                Total = Convert.ToDouble(reader["Total"]),
                                ArqueoId = Convert.ToInt32(reader["ArqueoId"])
                            });
                        }
                    }
                }
            }

            return ventas;
        }
    }
}
