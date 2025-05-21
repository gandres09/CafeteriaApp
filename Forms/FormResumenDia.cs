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
using CafeteriaApp.Models;
using ClosedXML.Excel;
using ClosedXML.Excel;
using System.IO;

namespace CafeteriaApp.Forms
{
    public partial class FormResumenDia : Form
    {

        public FormResumenDia()
        {
            InitializeComponent();
            dtpFiltro.Value = DateTime.Today;
            CargarResumen(DateTime.Today);
            dgvResumen.CellFormatting += dgvResumen_CellFormatting;
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarResumen(dtpFiltro.Value);
        }

        private void CargarResumen(DateTime fecha)
        {
            string fechaStr = fecha.ToString("yyyy-MM-dd");

            using (var conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT Fecha, SaldoInicial, TotalVentas, SaldoFinal, Diferencia FROM ArqueoDiario WHERE Fecha = @fecha";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", fechaStr);

                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int arqueoId = -1;

                        if (dt.Rows.Count == 0)
                        {
                            dt.Columns.Clear();
                            dt.Columns.Add("Fecha", typeof(string));
                            dt.Columns.Add("SaldoInicial", typeof(double));
                            dt.Columns.Add("TotalVentas", typeof(double));
                            dt.Columns.Add("SaldoFinal", typeof(double));
                            dt.Columns.Add("Diferencia", typeof(double));

                            DataRow fila = dt.NewRow();
                            fila["Fecha"] = fecha.ToString("dd/MM/yyyy");
                            fila["SaldoInicial"] = DBNull.Value;
                            fila["TotalVentas"] = DBNull.Value;
                            fila["SaldoFinal"] = DBNull.Value;
                            fila["Diferencia"] = DBNull.Value;
                            dt.Rows.Add(fila);

                            dgvVentas.DataSource = null;
                        }
                        else
                        {
                            // Obtener ID del arqueo para cargar ventas
                            string idSql = "SELECT Id FROM ArqueoDiario WHERE Fecha = @fecha LIMIT 1";
                            using (var idCmd = new SQLiteCommand(idSql, conn))
                            {
                                idCmd.Parameters.AddWithValue("@fecha", fechaStr);
                                object result = idCmd.ExecuteScalar();
                                if (result != null) arqueoId = Convert.ToInt32(result);
                            }

                            // Calcular total de ventas reales de la tabla Ventas
                            double totalVentas = 0;
                            string sumSql = "SELECT SUM(Total) FROM Ventas WHERE ArqueoId = @arqueoId";
                            using (var sumCmd = new SQLiteCommand(sumSql, conn))
                            {
                                sumCmd.Parameters.AddWithValue("@arqueoId", arqueoId);
                                object totalVentasObj = sumCmd.ExecuteScalar();
                                if (totalVentasObj != DBNull.Value && totalVentasObj != null)
                                    totalVentas = Convert.ToDouble(totalVentasObj);
                            }

                            // Actualizar columna TotalVentas
                            dt.Rows[0]["TotalVentas"] = totalVentas;

                            // Calcular diferencia correctamente
                            if (dt.Rows[0]["SaldoInicial"] != DBNull.Value && dt.Rows[0]["TotalVentas"] != DBNull.Value && dt.Rows[0]["SaldoFinal"] != DBNull.Value)
                            {
                                double saldoInicial = Convert.ToDouble(dt.Rows[0]["SaldoInicial"]);
                                double saldoFinal = Convert.ToDouble(dt.Rows[0]["SaldoFinal"]);
                                double diferencia = saldoFinal - (saldoInicial + totalVentas);
                                dt.Rows[0]["Diferencia"] = diferencia;
                            }

                            CargarVentasDelArqueo(conn, arqueoId);
                        }

                        dgvResumen.DataSource = dt;
                        dgvResumen.ClearSelection();
                    }
                }
            }
        }

        private void dgvResumen_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string colName = dgvResumen.Columns[e.ColumnIndex].Name;

            if (colName == "SaldoInicial" || colName == "TotalVentas" || colName == "SaldoFinal" || colName == "Diferencia")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
                else if (double.TryParse(e.Value.ToString(), out double valor))
                {
                    e.Value = "$" + valor.ToString("0.00");
                    e.FormattingApplied = true;
                }
            }
            else if (colName == "Fecha")
            {
                if (e.Value != null && e.Value != DBNull.Value && DateTime.TryParse(e.Value.ToString(), out DateTime fecha))
                {
                    e.Value = fecha.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
            }
        }

        private void CargarVentasDelArqueo(SQLiteConnection conn, int arqueoId)
        {
            string sql = @"SELECT V.FechaHora, P.Nombre AS Producto, V.Cantidad, V.PrecioUnitario, V.Total
                           FROM Ventas V
                           JOIN Productos P ON V.ProductoId = P.Id
                           WHERE V.ArqueoId = @arqueoId
                           ORDER BY V.FechaHora";

            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@arqueoId", arqueoId);
                using (var adapter = new SQLiteDataAdapter(cmd))
                {
                    DataTable dtVentas = new DataTable();
                    adapter.Fill(dtVentas);
                    dgvVentas.DataSource = dtVentas;

                    dgvVentas.Columns["FechaHora"].HeaderText = "Fecha";
                    dgvVentas.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                    dgvVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                }
            }
        }


        // ...

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvResumen.DataSource == null)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Workbook|*.xlsx";
            saveFileDialog.Title = "Guardar archivo Excel";
            saveFileDialog.FileName = "Resumen_" + dtpFiltro.Value.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var hojaResumen = workbook.Worksheets.Add("Resumen del Día");

                    // Encabezados
                    for (int i = 0; i < dgvResumen.Columns.Count; i++)
                    {
                        hojaResumen.Cell(1, i + 1).Value = dgvResumen.Columns[i].HeaderText;
                        hojaResumen.Cell(1, i + 1).Style.Font.Bold = true;
                    }

                    // Cuerpo de resumen con formato
                    for (int i = 0; i < dgvResumen.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvResumen.Columns.Count; j++)
                        {
                            var cellValue = dgvResumen.Rows[i].Cells[j].Value;
                            var cell = hojaResumen.Cell(i + 2, j + 1);

                            if (cellValue == null || cellValue == DBNull.Value)
                            {
                                cell.Value = "";
                                continue;
                            }

                            string columnName = dgvResumen.Columns[j].Name;

                            if (columnName == "Fecha" && DateTime.TryParse(cellValue.ToString(), out DateTime fecha))
                            {
                                cell.Value = fecha;
                                cell.Style.DateFormat.Format = "dd/MM/yyyy";
                            }
                            else if ((columnName == "SaldoInicial" || columnName == "TotalVentas" || columnName == "SaldoFinal" || columnName == "Diferencia") &&
                                     double.TryParse(cellValue.ToString(), out double valor))
                            {
                                cell.Value = valor;
                                cell.Style.NumberFormat.Format = "$#,##0.00";
                            }
                            else
                            {
                                cell.Value = cellValue.ToString();
                            }
                        }
                    }

                    // Exportar Ventas
                    if (dgvVentas.DataSource != null)
                    {
                        var hojaVentas = workbook.Worksheets.Add("Ventas");

                        for (int i = 0; i < dgvVentas.Columns.Count; i++)
                        {
                            hojaVentas.Cell(1, i + 1).Value = dgvVentas.Columns[i].HeaderText;
                            hojaVentas.Cell(1, i + 1).Style.Font.Bold = true;
                        }

                        for (int i = 0; i < dgvVentas.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvVentas.Columns.Count; j++)
                            {
                                var cellValue = dgvVentas.Rows[i].Cells[j].Value;
                                var cell = hojaVentas.Cell(i + 2, j + 1);

                                if (cellValue == null || cellValue == DBNull.Value)
                                {
                                    cell.Value = "";
                                    continue;
                                }

                                string columnName = dgvVentas.Columns[j].Name;

                                if (columnName == "FechaHora" && DateTime.TryParse(cellValue.ToString(), out DateTime fechaHora))
                                {
                                    cell.Value = fechaHora;
                                    cell.Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                                }
                                else if ((columnName == "PrecioUnitario" || columnName == "Total") &&
                                         double.TryParse(cellValue.ToString(), out double valor))
                                {
                                    cell.Value = valor;
                                    cell.Style.NumberFormat.Format = "$#,##0.00";
                                }
                                else
                                {
                                    cell.Value = cellValue.ToString();
                                }
                            }
                        }
                    }

                    try
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Archivo exportado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                    }
                }
            }
        }

    }
}
