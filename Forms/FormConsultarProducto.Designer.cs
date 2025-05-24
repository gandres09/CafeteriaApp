using System.Windows.Forms;

namespace CafeteriaApp.Forms
{
    partial class FormConsultarProducto
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dataGridViewProductos = new DataGridView();
            this.txtBuscar = new TextBox();
            this.btnBuscar = new Button();
            this.label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).BeginInit();
            this.SuspendLayout();

            // 
            // dataGridViewProductos
            // 
            this.dataGridViewProductos.AllowUserToAddRows = false;
            this.dataGridViewProductos.AllowUserToDeleteRows = false;
            this.dataGridViewProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductos.Location = new System.Drawing.Point(12, 99);
            this.dataGridViewProductos.MultiSelect = false;
            this.dataGridViewProductos.Name = "dataGridViewProductos";
            this.dataGridViewProductos.ReadOnly = true;
            this.dataGridViewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductos.Size = new System.Drawing.Size(560, 150);
            this.dataGridViewProductos.TabIndex = 0;
            this.dataGridViewProductos.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewProductos_CellDoubleClick);

            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(33, 73);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(173, 20);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.KeyDown += new KeyEventHandler(this.txtBuscar_KeyDown);

            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(212, 71);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.button1_Click);

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poor Richard", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(113, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 44);
            this.label1.TabIndex = 3;
            this.label1.Text = "Consulta";

            // 
            // FormConsultarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dataGridViewProductos);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormConsultarProducto";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Consultar Productos";
            this.Load += new System.EventHandler(this.FormConsultarProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewProductos;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label label1;
    }
}
