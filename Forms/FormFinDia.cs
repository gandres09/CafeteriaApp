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

//este es un comentario de prueba

namespace CafeteriaApp.Forms
{
    public partial class FormFinDia : Form
    {
        public bool CierreExitoso { get; private set; } = false;
        public FormFinDia()
        {
            InitializeComponent();
        }

        // cerrar dia con boton enter
        private void txtSaldoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnCerrarDia_Click(sender, e);
            }
        }

        private void btnCerrarDia_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (!double.TryParse(txtSaldoFinal.Text, out double saldoFinal))
            {
                lblMensaje.Text = "Saldo inválido.";
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Visible = true;
                return;
            }

            double totalVentas = ObtenerTotalVentasHoy();

            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();

                string selectSql = @"
            SELECT Id, SaldoInicial 
            FROM ArqueoDiario 
            WHERE SaldoFinal IS NULL 
            ORDER BY FechaHoraInicio DESC 
            LIMIT 1";

                long idArqueo = -1;
                double saldoInicial = 0;

                using (var cmd = new SQLiteCommand(selectSql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idArqueo = reader.GetInt64(0);
                            saldoInicial = reader.GetDouble(1);
                        }
                        else
                        {
                            lblMensaje.Text = "No hay un arqueo iniciado.";
                            lblMensaje.ForeColor = Color.Red;
                            lblMensaje.Visible = true;
                            return;
                        }
                    }
                }

                double diferencia = saldoFinal - (saldoInicial + totalVentas);

                string updateSql = @"
            UPDATE ArqueoDiario 
            SET SaldoFinal = @final, 
                TotalVentas = @ventas, 
                Diferencia = @diferencia,
                FechaHoraCierre = @cierre
            WHERE Id = @id";

                using (var cmd = new SQLiteCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@final", saldoFinal);
                    cmd.Parameters.AddWithValue("@ventas", totalVentas);
                    cmd.Parameters.AddWithValue("@diferencia", diferencia);
                    cmd.Parameters.AddWithValue("@cierre", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@id", idArqueo);
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.Text = "Día cerrado correctamente.";
                lblMensaje.ForeColor = Color.Green;
                lblMensaje.Visible = true;
                CierreExitoso = true;
                this.Close();
            }
        }

        private double ObtenerTotalVentasHoy()
        {
            // TODO: Reemplazar esta simulación con la suma real desde la tabla de ventas
            return 0; // Por ahora, si no tenés sistema de ventas activo
        }
    }
}
