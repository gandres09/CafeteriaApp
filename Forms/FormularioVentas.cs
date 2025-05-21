using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeteriaApp.Models;
using CafeteriaApp.Data;

namespace CafeteriaApp
{
    public partial class FormularioVentas : Form
    {

        private BindingList<VentaDetalle> carrito = new BindingList<VentaDetalle>();

        public FormularioVentas(int arqueoId)
        {
            InitializeComponent();

            carrito = new BindingList<VentaDetalle>();
            carrito.ListChanged += (s, e) => ActualizarTotal();

            CargarProductos();
            ConfigurarColumnas();
            dgvCarrito.AutoGenerateColumns = false;
            ActualizarCarrito();
        }

        private void ConfigurarColumnas()
        {


            dgvCarrito.Columns.Clear();

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",
                Name = "NombreProducto"
            });

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Name = "Cantidad"
            });

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Precio Unitario",
                DataPropertyName = "PrecioUnitario",
                Name = "PrecioUnitario"
            });

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Subtotal",
                DataPropertyName = "Total",
                Name = "Subtotal"
            });
        }


        private void CargarProductos()
        {
            var productos = ProductoData.ObtenerTodos(); // Asegúrate de tener este método implementado
            cmbProductos.DataSource = productos;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Id";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem == null || !int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Seleccione un producto y una cantidad válida.");
                return;
            }

            var producto = (Producto)cmbProductos.SelectedItem;
            //if (cantidad > producto.Stock)
            //{
            //    MessageBox.Show("No hay suficiente stock.");
            //    return;
            //}

            carrito.Add(new VentaDetalle
            {
                ProductoId = producto.Id,
                NombreProducto = producto.Nombre,
                PrecioUnitario = (double)producto.Precio,
                Cantidad = cantidad
            });

            ActualizarCarrito();
        }

        private void dgvCarrito_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            dgvCarrito.Columns["Cantidad"].ReadOnly = false;

            // Validar índice de fila
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            try
            {
                var row = dgvCarrito.Rows[e.RowIndex];
                if (dgvCarrito.Columns[e.ColumnIndex].Name == "Cantidad")
                {
                    if (int.TryParse(row.Cells["Cantidad"].Value?.ToString(), out int cantidad) && cantidad > 0)
                    {
                        if (decimal.TryParse(row.Cells["PrecioUnitario"].Value?.ToString(), out decimal precio))
                        {
                            row.Cells["Subtotal"].Value = cantidad * precio;
                            ActualizarTotal();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cantidad inválida. Debe ser un número entero mayor a 0.");
                        row.Cells["Cantidad"].Value = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar el total: " + ex.Message);
            }
        }



        private void ActualizarCarrito()
        {
            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = carrito;

            lblTotalVenta.Text = "Total: $" + carrito.Sum(x => x.Total).ToString("0.00");
        }

        private void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            if (!carrito.Any())
            {
                MessageBox.Show("Agregue productos al carrito antes de confirmar.");
                return;
            }

            int arqueoId = ArqueoData.ObtenerArqueoAbiertoId(); // Te ayudo a implementarlo luego
            if (arqueoId == -1)
            {
                MessageBox.Show("No hay un arqueo abierto.");
                return;
            }

            foreach (var item in carrito)
            {
                VentaData.RegistrarVenta(new Venta
                {
                    FechaHora = DateTime.Now,
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    Total = item.Total,
                    ArqueoId = arqueoId
                });

                ProductoData.DescontarStock(item.ProductoId, item.Cantidad); // Te ayudo a hacer esta función
                ArqueoData.ActualizarTotalVentas(arqueoId, item.Total); // Esta también
            }

            MessageBox.Show("Venta registrada correctamente.");
            carrito.Clear();
            ActualizarCarrito();
            CargarProductos(); // Recargar productos para actualizar stock
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count > 0)
            {
                var item = dgvCarrito.SelectedRows[0].DataBoundItem as VentaDetalle;
                if (item != null)
                {
                    carrito.Remove(item);
                    ActualizarCarrito();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.");
            }
        }



        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCarrito.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }
            lblTotalVenta.Text = $"Total: ${total}";
        }



    }
}

