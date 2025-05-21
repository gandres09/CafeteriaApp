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
    public partial class FormInicioDia : Form
    {
        public bool InicioExitoso { get; private set; } = false;
        public FormInicioDia()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        // iniciar dia con boton enter
        private void txtSaldoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnIniciarDia_Click(sender, e);
            }
        }

        private void btnIniciarDia_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (!double.TryParse(txtSaldoInicial.Text, out double saldoInicial))
            {
                lblMensaje.Text = "Saldo inválido.";
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Visible = true;
                return;
            }

            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string insertSql = @"INSERT INTO ArqueoDiario 
            (Fecha, FechaHoraInicio, SaldoInicial) 
            VALUES (@fecha, @fechaHora, @saldo)";

                using (var cmdInsert = new SQLiteCommand(insertSql, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@fecha", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmdInsert.Parameters.AddWithValue("@fechaHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmdInsert.Parameters.AddWithValue("@saldo", saldoInicial);
                    cmdInsert.ExecuteNonQuery();
                }

                lblMensaje.Text = "Inicio de día registrado.";
                lblMensaje.ForeColor = Color.Green;
                lblMensaje.Visible = true;
                InicioExitoso = true;
                this.Close();
            }
        }


    }
}
