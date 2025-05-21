using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace CafeteriaApp.Data
{
    public static class BaseDatos
    {
        private static string ruta = "cafeteria.db";
        private static string cadenaConexion = $"Data Source={ruta};Version=3;";

        public static SQLiteConnection ObtenerConexion()
        {
            return new SQLiteConnection(cadenaConexion);
        }

        public static void Inicializar()
        {
            if (!File.Exists(ruta))
            {
                SQLiteConnection.CreateFile(ruta);
            }

            using (var conexion = ObtenerConexion())
            {
                conexion.Open();

                // Tabla de productos  
                string queryProductos = @"  
                       CREATE TABLE IF NOT EXISTS Productos (  
                           Id INTEGER PRIMARY KEY AUTOINCREMENT,  
                           Nombre TEXT NOT NULL,  
                           Precio REAL NOT NULL,  
                           Stock INTEGER NOT NULL,  
                           Estado TEXT NOT NULL DEFAULT 'Activo'  
                       );";
                new SQLiteCommand(queryProductos, conexion).ExecuteNonQuery();

                // Tabla de arqueos múltiples por día  
                string queryArqueo = @"  
                       CREATE TABLE IF NOT EXISTS ArqueoDiario (  
                           Id INTEGER PRIMARY KEY AUTOINCREMENT,  
                           Fecha TEXT NOT NULL,  
                           FechaHoraInicio TEXT NOT NULL,  
                           FechaHoraCierre TEXT,  
                           SaldoInicial REAL NOT NULL,  
                           SaldoFinal REAL,  
                           TotalVentas REAL,  
                           Diferencia REAL  
                       );";
                new SQLiteCommand(queryArqueo, conexion).ExecuteNonQuery();

                // Tabla de ventas  
                string queryVentas = @"  
                       CREATE TABLE IF NOT EXISTS Ventas (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FechaHora DATETIME NOT NULL,
                            ProductoId INTEGER NOT NULL,
                            Cantidad INTEGER NOT NULL,
                            PrecioUnitario REAL NOT NULL,
                            Total REAL NOT NULL,
                            ArqueoId INTEGER NOT NULL,
                            FOREIGN KEY (ProductoId) REFERENCES Productos(Id),
                            FOREIGN KEY (ArqueoId) REFERENCES ArqueoDiario(Id)
);
";
                new SQLiteCommand(queryVentas, conexion).ExecuteNonQuery();
            }
        }
    }
}
