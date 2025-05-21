using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CafeteriaApp.Data
{
    public static class ArqueoData
    {
        /// <summary>
        /// Devuelve el Id del arqueo del día en curso (sin cierre), o -1 si no hay ninguno.
        /// </summary>
        public static int ObtenerArqueoAbiertoId()
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"
                    SELECT Id 
                    FROM ArqueoDiario 
                    WHERE Fecha = @hoy 
                      AND FechaHoraCierre IS NULL
                    LIMIT 1";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hoy", DateTime.Today.ToString("yyyy-MM-dd"));
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        /// <summary>
        /// Suma el monto de cada venta al campo TotalVentas del arqueo.
        /// </summary>
        public static void ActualizarTotalVentas(int arqueoId, double montoVenta)
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"
                    UPDATE ArqueoDiario
                    SET TotalVentas = IFNULL(TotalVentas, 0) + @monto
                    WHERE Id = @id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@monto", montoVenta);
                    cmd.Parameters.AddWithValue("@id", arqueoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
