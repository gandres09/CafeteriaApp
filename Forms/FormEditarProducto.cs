using CafeteriaApp.Data;
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
using CafeteriaApp.Forms;
using System.IO;

namespace CafeteriaApp.Forms
{
    public partial class FormEditarProducto : Form
    {
        private int productoId;

        public FormEditarProducto(int id, string nombre, double precio, int stock, string estado)
        {
            InitializeComponent();
            productoId = id;
            txtNombre.Text = nombre;
            txtPrecio.Text = precio.ToString();
            txtStock.Text = stock.ToString();
            cmbEstado.SelectedItem = estado;

            lblMensaje.Visible = false; // Ocultar mensaje al iniciar
            timerMensaje.Tick += timerMensaje_Tick;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombre.Text.Trim();

            if (!double.TryParse(txtPrecio.Text, out double nuevoPrecio))
            {
                MostrarMensaje("Precio inválido.", true);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int nuevoStock))
            {
                MostrarMensaje("Stock inválido.", true);
                return;
            }

            string estadoNuevo = cmbEstado.SelectedItem?.ToString() ?? "Activo";

            try
            {
                using (SQLiteConnection conn = BaseDatos.ObtenerConexion())
                {
                    conn.Open();
                    string sql = "UPDATE Productos SET Nombre = @nombre, Precio = @precio, Stock = @stock, Estado = @estado WHERE Id = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@precio", nuevoPrecio);
                        cmd.Parameters.AddWithValue("@stock", nuevoStock);
                        cmd.Parameters.AddWithValue("@estado", estadoNuevo);
                        cmd.Parameters.AddWithValue("@id", productoId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MostrarMensaje("Producto actualizado correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al actualizar: " + ex.Message, true);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = esError ? Color.Red : Color.Green;
            lblMensaje.Visible = true;
            timerMensaje.Start();
        }

        private void timerMensaje_Tick(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            timerMensaje.Stop();

            // Si fue un mensaje exitoso, cerrar el formulario
            if (lblMensaje.ForeColor == Color.Green)
            {
                this.Close();
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
        }
    }
}

