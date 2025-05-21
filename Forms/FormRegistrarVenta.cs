using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeteriaApp.Data;
using CafeteriaApp.Models;

namespace CafeteriaApp.Forms
{
    public partial class FormRegistrarVenta : Form
    {
        private int arqueoIdActual = -1;
        private Label labelStockDisponible;


        public FormRegistrarVenta(int arqueoId)
        {
            InitializeComponent();
            arqueoIdActual = arqueoId;
            CargarProductos();
        }

        private void CargarProductos()
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id, Nombre, Precio FROM Productos WHERE Estado = 'Activo'";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBoxProductos.Items.Add(new Producto
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Precio = (decimal)reader.GetDouble(2)
                        });
                    }
                }
            }

            comboBoxProductos.DisplayMember = "Nombre";
            comboBoxProductos.ValueMember = "Id";
        }

        private void comboBoxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProductos.SelectedItem is Producto producto)
            {
                textBoxPrecio.Text = producto.Precio.ToString("0.00");
                CalcularTotal();
                MostrarStockDisponible(producto.Id); // ✅ Actualiza el stock al seleccionar
            }
        }


        private void numericUpDownCantidad_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (comboBoxProductos.SelectedItem is Producto producto)
            {
                int cantidad = (int)numericUpDownCantidad.Value;
                decimal total = cantidad * producto.Precio;
                labelTotal.Text = $"Total: ${total:0.00}";
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!(comboBoxProductos.SelectedItem is Producto producto))
            {
                MessageBox.Show("Debe seleccionar un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)numericUpDownCantidad.Value;

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = BaseDatos.ObtenerConexion())
                {
                    conn.Open();

                    // Obtener el stock actual desde la base de datos
                    int stockActual = 0;
                    string consultaStock = "SELECT Stock FROM Productos WHERE Id = @id";
                    using (var cmdStock = new SQLiteCommand(consultaStock, conn))
                    {
                        cmdStock.Parameters.AddWithValue("@id", producto.Id);
                        object result = cmdStock.ExecuteScalar();
                        if (result != null)
                            stockActual = Convert.ToInt32(result);
                    }

                    decimal precio = producto.Precio;
                    decimal total = cantidad * precio;

                    // Registrar la venta
                    string sql = @"INSERT INTO Ventas (FechaHora, ProductoId, Cantidad, PrecioUnitario, Total, ArqueoId)
                           VALUES (@fecha, @productoId, @cantidad, @precio, @total, @arqueoId)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@productoId", producto.Id);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@arqueoId", arqueoIdActual);
                        cmd.ExecuteNonQuery();
                    }

                    // Actualizar el stock
                    string actualizarStock = "UPDATE Productos SET Stock = Stock - @cantidad WHERE Id = @id";
                    using (var cmdUpdate = new SQLiteCommand(actualizarStock, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@cantidad", cantidad);
                        cmdUpdate.Parameters.AddWithValue("@id", producto.Id);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Venta registrada exitosamente.");
                MostrarStockDisponible(producto.Id); // ✅ Actualiza luego de venta

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar la venta:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarStockDisponible(int productoId)
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Stock FROM Productos WHERE Id = @id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", productoId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int stock = Convert.ToInt32(result);
                        labelStockDisponible.Text = $"Stock disponible: {stock}";
                    }
                    else
                    {
                        labelStockDisponible.Text = "Stock no encontrado.";
                    }
                }
            }
        }



    }
}