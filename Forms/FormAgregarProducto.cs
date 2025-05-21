using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeteriaApp.Data;
using System.Data.SQLite;

namespace CafeteriaApp.Forms
{
    public partial class FormAgregarProducto : Form
    {
        public FormAgregarProducto()
        {
            InitializeComponent();
            lblMensaje.Visible = false;
            timerMensaje.Tick += timerMensaje_Tick;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            string nombre = txtNombre.Text.Trim();

            if (!double.TryParse(txtPrecio.Text, out double precio))
            {
                MostrarMensaje("Precio inválido.", true);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MostrarMensaje("Stock inválido.", true);
                return;
            }

            try
            {
                using (SQLiteConnection conn = BaseDatos.ObtenerConexion())
                {
                    conn.Open();
                    string sql = "INSERT INTO Productos (Nombre, Precio, Stock, Estado) VALUES (@nombre, @precio, @stock, @estado)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@estado", "Activo");
                        cmd.ExecuteNonQuery();
                    }
                }

                MostrarMensaje("Producto guardado correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar: " + ex.Message, true);
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
