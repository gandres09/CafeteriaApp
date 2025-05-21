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
using CafeteriaApp.Forms;

namespace CafeteriaApp
{
    public partial class FormPrincipal : Form
    {
        private FormAgregarProducto formAgregarProducto;
        private FormConsultarProducto formConsultarProducto;
        private FormInicioDia formInicioDia;
        private FormFinDia formFinDia;
        private bool diaIniciado = false;
        private bool diaCerrado = false;
        private FormResumenDia formResumenDia;
        private FormularioVentas formVentas;

        public FormPrincipal()
        {
            InitializeComponent();
            VerificarEstadoDia();
            ActualizarBotonesEstadoDia();  // <-- Aquí actualizamos los botones al iniciar
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (formAgregarProducto == null || formAgregarProducto.IsDisposed)
            {
                formAgregarProducto = new FormAgregarProducto();
                formAgregarProducto.FormClosed += (s, args) =>
                {
                    btnAgregarProducto.Enabled = true;
                    formAgregarProducto = null;
                };
                formAgregarProducto.Show();
                btnAgregarProducto.Enabled = false;
            }
        }

        private void btnConsultarProductos_Click(object sender, EventArgs e)
        {
            if (formConsultarProducto == null || formConsultarProducto.IsDisposed)
            {
                formConsultarProducto = new FormConsultarProducto();
                formConsultarProducto.FormClosed += (s, args) =>
                {
                    btnConsultarProductos.Enabled = true;
                    formConsultarProducto = null;
                };
                formConsultarProducto.Show();
                btnConsultarProductos.Enabled = false;
            }
        }

        private void btnInicioDia_Click(object sender, EventArgs e)
        {
            if (DiaYaIniciadoSinCerrar())
            {
                MessageBox.Show("Ya hay un día iniciado que no fue cerrado. Por favor cierre el día actual antes de iniciar uno nuevo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (formInicioDia == null || formInicioDia.IsDisposed)
            {
                formInicioDia = new FormInicioDia();
                formInicioDia.FormClosed += (s, args) =>
                {
                    btnInicioDia.Enabled = true;
                    if (formInicioDia.InicioExitoso)
                    {
                        diaIniciado = true;
                        diaCerrado = false;
                    }
                    formInicioDia = null;
                    ActualizarEstadoDia();
                    ActualizarBotonesEstadoDia(); // <--- Actualizamos botones luego de iniciar día
                };
                formInicioDia.Show();
                btnInicioDia.Enabled = false;
            }
        }

        private void btnCierreDia_Click(object sender, EventArgs e)
        {
            if (!diaIniciado)
            {
                MessageBox.Show("Debe iniciar el día primero.");
                return;
            }

            if (formFinDia == null || formFinDia.IsDisposed)
            {
                formFinDia = new FormFinDia();
                formFinDia.FormClosed += (s, args) =>
                {
                    btnCierreDia.Enabled = true;
                    if (formFinDia.CierreExitoso)
                    {
                        diaCerrado = true;
                        diaIniciado = false; // Cerró el día, ya no está iniciado
                    }
                    formFinDia = null;
                    ActualizarEstadoDia();
                    ActualizarBotonesEstadoDia(); // <--- Actualizamos botones luego de cerrar día
                };
                formFinDia.Show();
                btnCierreDia.Enabled = false;
            }
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (diaIniciado && !diaCerrado)
            {
                MessageBox.Show("Debe cerrar el día antes de salir.");
                e.Cancel = true;
            }
        }

        private void btnResumenDia_Click(object sender, EventArgs e)
        {
            if (formResumenDia == null || formResumenDia.IsDisposed)
            {
                formResumenDia = new FormResumenDia();
                formResumenDia.FormClosed += (s, args) =>
                {
                    btnResumenDia.Enabled = true;
                    formResumenDia = null;
                };
                formResumenDia.Show();
                btnResumenDia.Enabled = false;
            }
        }

        private void pnlEstadoDia_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pnlEstadoDia.Width - 1, pnlEstadoDia.Height - 1);
            pnlEstadoDia.Region = new Region(path);
        }

        private void ActualizarEstadoDia()
        {
            if (!diaIniciado && !diaCerrado)
            {
                lblEstadoDia.Text = "Estado del día: No iniciado";
                lblEstadoDia.ForeColor = Color.Gray;
                pnlEstadoDia.BackColor = Color.Gray;
            }
            else if (diaIniciado && !diaCerrado)
            {
                lblEstadoDia.Text = "Estado del día: En curso";
                lblEstadoDia.ForeColor = Color.Green;
                pnlEstadoDia.BackColor = Color.Green;
            }
            else if (diaCerrado)
            {
                lblEstadoDia.Text = "Estado del día: Cerrado";
                lblEstadoDia.ForeColor = Color.Red;
                pnlEstadoDia.BackColor = Color.Red;
            }
        }



        private void VerificarEstadoDia()
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"
            SELECT FechaHoraInicio, FechaHoraCierre 
            FROM ArqueoDiario 
            ORDER BY FechaHoraInicio DESC 
            LIMIT 1";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var fechaInicio = reader["FechaHoraInicio"] as DateTime?;
                            var fechaCierre = reader["FechaHoraCierre"] as DateTime?;

                            if (fechaInicio != null && fechaCierre == null)
                            {
                                // Día iniciado y no cerrado
                                diaIniciado = true;
                                diaCerrado = false;
                            }
                            else if (fechaInicio != null && fechaCierre != null)
                            {
                                // Día cerrado
                                diaIniciado = false;
                                diaCerrado = true;
                            }
                        }
                        else
                        {
                            // No hay registros: día no iniciado ni cerrado
                            diaIniciado = false;
                            diaCerrado = false;
                        }
                    }
                }
            }

            ActualizarEstadoDia();
        }


        private bool DiaYaIniciadoSinCerrar()
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"
                SELECT COUNT(*) 
                FROM ArqueoDiario 
                WHERE FechaHoraCierre IS NULL";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // NUEVO método para actualizar el estado de los botones de inicio y cierre
        private void ActualizarBotonesEstadoDia()
        {
            if (DiaYaIniciadoSinCerrar())
            {
                btnInicioDia.Enabled = false; // Ya hay día iniciado, no se puede iniciar otro
                btnCierreDia.Enabled = true;  // Se puede cerrar el día
            }
            else
            {
                btnInicioDia.Enabled = true;  // No hay día iniciado, se puede iniciar
                btnCierreDia.Enabled = false; // No hay día para cerrar
            }
        }

        //Boton Salir que no deja cerrar si el dia esta iniciado
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (diaIniciado && !diaCerrado)
            {
                MessageBox.Show("Debe cerrar el día antes de salir.");
            }
            else
            {
                this.Close();
            }
        }

        private int ObtenerArqueoIdActivo()
        {
            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id FROM ArqueoDiario WHERE FechaHoraCierre IS NULL ORDER BY FechaHoraInicio DESC LIMIT 1";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {

            if (!diaIniciado || diaCerrado)
            {
                MessageBox.Show("Debe tener el día iniciado y no cerrado para registrar ventas.");
                return;
            }

            int arqueoId = ObtenerArqueoIdActivo();
            if (arqueoId == -1)
            {
                MessageBox.Show("No se encontró un arqueo activo.");
                return;
            }

            var formVenta = new FormularioVentas(arqueoId);
            formVenta.ShowDialog();

        }
    }
}
