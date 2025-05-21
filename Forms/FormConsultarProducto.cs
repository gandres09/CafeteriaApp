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

namespace CafeteriaApp.Forms
{
    public partial class FormConsultarProducto : Form
    {
        public FormConsultarProducto()
        {
            InitializeComponent();
        }
        private void CargarProductos(string filtro = "")
        {
            // Limpiar columnas y filas del DataGridView manualmente
            dataGridViewProductos.Rows.Clear();
            dataGridViewProductos.Columns.Clear();

            // Definir columnas (asegúrate que coincidan con los nombres reales de tu tabla)
            dataGridViewProductos.Columns.Add("Id", "ID");
            dataGridViewProductos.Columns.Add("Nombre", "Nombre");
            dataGridViewProductos.Columns.Add("Precio", "Precio");
            dataGridViewProductos.Columns.Add("Stock", "Stock");
            dataGridViewProductos.Columns.Add("Estado", "Estado");

            using (SQLiteConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string consulta = "SELECT Id, Nombre, Precio, Stock, Estado FROM Productos WHERE Nombre LIKE @filtro ORDER BY Nombre ASC";

                using (SQLiteCommand cmd = new SQLiteCommand(consulta, conn))
                {
                    cmd.Parameters.AddWithValue("@filtro", $"%{filtro}%");

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridViewProductos.Rows.Add(
                                reader["Id"].ToString(),
                                reader["Nombre"].ToString(),
                                reader["Precio"].ToString(),
                                reader["Stock"].ToString(),
                                reader["Estado"].ToString()
                            );
                        }
                    }
                }
            }

            dataGridViewProductos.ClearSelection(); // Opcional
        }



        private void FormConsultarProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            btnBuscar.Click += button1_Click;
            dataGridViewProductos.AllowUserToAddRows = false;
        }

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Click en encabezado

            DataGridViewRow fila = dataGridViewProductos.Rows[e.RowIndex];

            // Validar que todas las celdas estén completas
            if (fila.Cells["Id"].Value == null || fila.Cells["Nombre"].Value == null ||
                fila.Cells["Precio"].Value == null || fila.Cells["Stock"].Value == null ||
                fila.Cells["Estado"].Value == null)
            {
                MessageBox.Show("La fila seleccionada no tiene todos los datos necesarios.");
                return;
            }

            try
            {
                int id = Convert.ToInt32(fila.Cells["Id"].Value);
                string nombre = fila.Cells["Nombre"].Value.ToString();
                double precio = Convert.ToDouble(fila.Cells["Precio"].Value);
                int stock = Convert.ToInt32(fila.Cells["Stock"].Value);
                string estado = fila.Cells["Estado"].Value.ToString();

                FormEditarProducto editarForm = new FormEditarProducto(id, nombre, precio, stock, estado);
                editarForm.FormClosed += (s, args) => CargarProductos(); // refrescar al cerrar
                editarForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ventana de edición: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            CargarProductos(txtBuscar.Text.Trim());

        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // evita el "beep"
                CargarProductos(txtBuscar.Text.Trim());
            }
        }
    }
}
