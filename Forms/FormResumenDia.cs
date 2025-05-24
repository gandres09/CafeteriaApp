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

                            // Guardar TotalVentas en la base de datos
                            string updateSql = "UPDATE ArqueoDiario SET TotalVentas = @totalVentas WHERE Id = @arqueoId";
                            using (var updateCmd = new SQLiteCommand(updateSql, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@totalVentas", totalVentas);
                                updateCmd.Parameters.AddWithValue("@arqueoId", arqueoId);
                                updateCmd.ExecuteNonQuery();
                            }

                            string updateVentasSql = "UPDATE ArqueoDiario SET TotalVentas = @totalVentas WHERE Id = @arqueoId";
                            using (var updateCmd = new SQLiteCommand(updateVentasSql, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@totalVentas", totalVentas);
                                updateCmd.Parameters.AddWithValue("@arqueoId", arqueoId);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Calcular diferencia correctamente
                            if (dt.Rows[0]["SaldoInicial"] != DBNull.Value && dt.Rows[0]["TotalVentas"] != DBNull.Value && dt.Rows[0]["SaldoFinal"] != DBNull.Value)
                            {
                                double saldoInicial = Convert.ToDouble(dt.Rows[0]["SaldoInicial"]);
                                double saldoFinal = Convert.ToDouble(dt.Rows[0]["SaldoFinal"]);
                                double diferencia = saldoFinal - (saldoInicial + totalVentas);
                                string updateDiffSql = "UPDATE ArqueoDiario SET Diferencia = @diferencia WHERE Id = @arqueoId";
                                using (var diffCmd = new SQLiteCommand(updateDiffSql, conn))
                                {
                                    diffCmd.Parameters.AddWithValue("@diferencia", diferencia);
                                    diffCmd.Parameters.AddWithValue("@arqueoId", arqueoId);
                                    diffCmd.ExecuteNonQuery();
                                }
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
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvVentas.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvVentas.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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

                    // Hoja Resumen
                    for (int i = 0; i < dgvResumen.Columns.Count; i++)
                    {
                        hojaResumen.Cell(1, i + 1).Value = dgvResumen.Columns[i].HeaderText;
                        hojaResumen.Cell(1, i + 1).Style.Font.Bold = true;
                    }

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

                    // Hoja Ventas
                    DataTable ventasData = ObtenerVentasDelDia(dtpFiltro.Value.Date);
                    if (ventasData != null)
                    {
                        var hojaVentas = workbook.Worksheets.Add("Ventas");
                        for (int i = 0; i < ventasData.Columns.Count; i++)
                        {
                            hojaVentas.Cell(1, i + 1).Value = ventasData.Columns[i].ColumnName;
                            hojaVentas.Cell(1, i + 1).Style.Font.Bold = true;
                        }

                        for (int i = 0; i < ventasData.Rows.Count; i++)
                        {
                            for (int j = 0; j < ventasData.Columns.Count; j++)
                            {
                                var cell = hojaVentas.Cell(i + 2, j + 1);
                                object value = ventasData.Rows[i][j];
                                string colName = ventasData.Columns[j].ColumnName;

                                if (colName == "FechaHora" && DateTime.TryParse(value.ToString(), out DateTime fechaHora))
                                {
                                    cell.Value = fechaHora;
                                    cell.Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                                }
                                else if ((colName == "PrecioUnitario" || colName == "Total") &&
                                         double.TryParse(value.ToString(), out double num))
                                {
                                    cell.Value = num;
                                    cell.Style.NumberFormat.Format = "$#,##0.00";
                                }
                                else
                                {
                                    cell.Value = value?.ToString();
                                }
                            }
                        }

                        // Hoja Productos por Cantidad
                        var hojaProductosOrdenados = workbook.Worksheets.Add("Productos x Cantidad");
                        var productosAgrupados = ventasData.AsEnumerable()
                            .GroupBy(row => row.Field<string>("Producto"))
                            .Select(g => new
                            {
                                Producto = g.Key,
                                Cantidad = g.Sum(r => Convert.ToInt32(r["Cantidad"])),
                                Total = g.Sum(r => Convert.ToDouble(r["Total"]))
                            })
                            .OrderByDescending(x => x.Cantidad)
                            .ToList();

                        hojaProductosOrdenados.Cell(1, 1).Value = "Producto";
                        hojaProductosOrdenados.Cell(1, 2).Value = "Cantidad";
                        hojaProductosOrdenados.Cell(1, 3).Value = "Total Vendido";
                        hojaProductosOrdenados.Range("A1:C1").Style.Font.Bold = true;

                        for (int i = 0; i < productosAgrupados.Count; i++)
                        {
                            hojaProductosOrdenados.Cell(i + 2, 1).Value = productosAgrupados[i].Producto;
                            hojaProductosOrdenados.Cell(i + 2, 2).Value = productosAgrupados[i].Cantidad;
                            hojaProductosOrdenados.Cell(i + 2, 3).Value = productosAgrupados[i].Total;
                            hojaProductosOrdenados.Cell(i + 2, 3).Style.NumberFormat.Format = "$#,##0.00";
                        }

                        // Hoja Productos por Hora
                        var hojaPorHora = workbook.Worksheets.Add("Productos x Hora");
                        hojaPorHora.Cell(1, 1).Value = "Hora";
                        hojaPorHora.Cell(1, 2).Value = "Producto";
                        hojaPorHora.Cell(1, 3).Value = "Cantidad";
                        hojaPorHora.Cell(1, 4).Value = "Total";
                        hojaPorHora.Range("A1:D1").Style.Font.Bold = true;

                        var ordenadas = ventasData.AsEnumerable()
                            .OrderBy(r => Convert.ToDateTime(r["FechaHora"]))
                            .ToList();

                        for (int i = 0; i < ordenadas.Count; i++)
                        {
                            DateTime fecha = Convert.ToDateTime(ordenadas[i]["FechaHora"]);
                            hojaPorHora.Cell(i + 2, 1).Value = fecha;
                            hojaPorHora.Cell(i + 2, 1).Style.DateFormat.Format = "HH:mm";
                            hojaPorHora.Cell(i + 2, 2).Value = ordenadas[i]["Producto"].ToString();
                            hojaPorHora.Cell(i + 2, 3).Value = Convert.ToInt32(ordenadas[i]["Cantidad"]);
                            hojaPorHora.Cell(i + 2, 4).Value = Convert.ToDouble(ordenadas[i]["Total"]);
                            hojaPorHora.Cell(i + 2, 4).Style.NumberFormat.Format = "$#,##0.00";
                        }

                        // Hoja Ventas por Media Hora
                        var hojaMediaHora = workbook.Worksheets.Add("Ventas por Media Hora");
                        hojaMediaHora.Cell(1, 1).Value = "Rango Horario";
                        hojaMediaHora.Cell(1, 2).Value = "Clientes";
                        hojaMediaHora.Cell(1, 3).Value = "Total Vendido";
                        hojaMediaHora.Cell(1, 4).Value = "Promedio Ticket";
                        hojaMediaHora.Range("A1:D1").Style.Font.Bold = true;

                        var grupos = ventasData.AsEnumerable()
                            .Select(r => new
                            {
                                FechaHora = Convert.ToDateTime(r["FechaHora"]),
                                Total = Convert.ToDouble(r["Total"])
                            })
                            .GroupBy(r =>
                            {
                                var fecha = r.FechaHora;
                                int minutos = fecha.Minute < 30 ? 0 : 30;
                                DateTime inicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, minutos, 0);
                                DateTime fin = inicio.AddMinutes(30);
                                return new { Inicio = inicio, Fin = fin };
                            })
                            .OrderBy(g => g.Key.Inicio);

                        int filaExcel = 2;
                        foreach (var grupo in grupos)
                        {
                            var rango = grupo.Key;
                            int clientes = grupo.Count();
                            double total = grupo.Sum(x => x.Total);
                            double promedio = clientes > 0 ? total / clientes : 0;

                            hojaMediaHora.Cell(filaExcel, 1).Value = $"{rango.Inicio:HH:mm} - {rango.Fin:HH:mm}";
                            hojaMediaHora.Cell(filaExcel, 2).Value = clientes;
                            hojaMediaHora.Cell(filaExcel, 3).Value = total;
                            hojaMediaHora.Cell(filaExcel, 3).Style.NumberFormat.Format = "$#,##0.00";
                            hojaMediaHora.Cell(filaExcel, 4).Value = promedio;
                            hojaMediaHora.Cell(filaExcel, 4).Style.NumberFormat.Format = "$#,##0.00";

                            filaExcel++;
                        }
                    }

                    // Autoajuste de todas las hojas
                    foreach (var ws in workbook.Worksheets)
                        ws.Columns().AdjustToContents();



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

        private DataTable ObtenerVentasDelDia(DateTime fecha)
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT v.FechaHora, p.Nombre AS Producto, v.Cantidad, v.PrecioUnitario, v.Total 
        FROM Ventas v
        JOIN Productos p ON v.ProductoId = p.Id
        WHERE DATE(v.FechaHora) = @Fecha 
        ORDER BY v.FechaHora ASC";

            using (var conn = BaseDatos.ObtenerConexion()) // Usamos la conexión correcta
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("yyyy-MM-dd"));
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dt); // ← Aquí corregiste el error anterior de 'wadapter'
                    }
                }
            }

            return dt;
        }





    }
}
