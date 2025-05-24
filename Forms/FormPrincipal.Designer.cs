using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CafeteriaApp
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.btnConsultarProductos = new System.Windows.Forms.Button();
            this.btnInicioDia = new System.Windows.Forms.Button();
            this.btnCierreDia = new System.Windows.Forms.Button();
            this.btnResumenDia = new System.Windows.Forms.Button();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pnlEstadoDia = new System.Windows.Forms.Panel();
            this.lblEstadoDia = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Sienna;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(672, 52);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Panel Principal - Cafetería";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(189, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.BackColor = System.Drawing.Color.Peru;
            this.btnAgregarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarProducto.FlatAppearance.BorderSize = 0;
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarProducto.Location = new System.Drawing.Point(17, 217);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(189, 30);
            this.btnAgregarProducto.TabIndex = 2;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.UseVisualStyleBackColor = false;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // btnConsultarProductos
            // 
            this.btnConsultarProductos.BackColor = System.Drawing.Color.Peru;
            this.btnConsultarProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarProductos.FlatAppearance.BorderSize = 0;
            this.btnConsultarProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarProductos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnConsultarProductos.ForeColor = System.Drawing.Color.White;
            this.btnConsultarProductos.Location = new System.Drawing.Point(17, 256);
            this.btnConsultarProductos.Name = "btnConsultarProductos";
            this.btnConsultarProductos.Size = new System.Drawing.Size(189, 30);
            this.btnConsultarProductos.TabIndex = 3;
            this.btnConsultarProductos.Text = "Consultar Producto";
            this.btnConsultarProductos.UseVisualStyleBackColor = false;
            this.btnConsultarProductos.Click += new System.EventHandler(this.btnConsultarProductos_Click);
            // 
            // btnInicioDia
            // 
            this.btnInicioDia.BackColor = System.Drawing.Color.Peru;
            this.btnInicioDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInicioDia.FlatAppearance.BorderSize = 0;
            this.btnInicioDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicioDia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInicioDia.ForeColor = System.Drawing.Color.White;
            this.btnInicioDia.Location = new System.Drawing.Point(223, 124);
            this.btnInicioDia.Name = "btnInicioDia";
            this.btnInicioDia.Size = new System.Drawing.Size(56, 26);
            this.btnInicioDia.TabIndex = 5;
            this.btnInicioDia.Text = "Iniciar Día";
            this.btnInicioDia.UseVisualStyleBackColor = false;
            this.btnInicioDia.Click += new System.EventHandler(this.btnInicioDia_Click);
            // 
            // btnCierreDia
            // 
            this.btnCierreDia.BackColor = System.Drawing.Color.Peru;
            this.btnCierreDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCierreDia.FlatAppearance.BorderSize = 0;
            this.btnCierreDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCierreDia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCierreDia.ForeColor = System.Drawing.Color.White;
            this.btnCierreDia.Location = new System.Drawing.Point(287, 124);
            this.btnCierreDia.Name = "btnCierreDia";
            this.btnCierreDia.Size = new System.Drawing.Size(56, 26);
            this.btnCierreDia.TabIndex = 6;
            this.btnCierreDia.Text = "Cerrar Día";
            this.btnCierreDia.UseVisualStyleBackColor = false;
            this.btnCierreDia.Click += new System.EventHandler(this.btnCierreDia_Click);
            // 
            // btnResumenDia
            // 
            this.btnResumenDia.BackColor = System.Drawing.Color.Sienna;
            this.btnResumenDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResumenDia.FlatAppearance.BorderSize = 0;
            this.btnResumenDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResumenDia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResumenDia.ForeColor = System.Drawing.Color.White;
            this.btnResumenDia.Location = new System.Drawing.Point(223, 159);
            this.btnResumenDia.Name = "btnResumenDia";
            this.btnResumenDia.Size = new System.Drawing.Size(120, 52);
            this.btnResumenDia.TabIndex = 7;
            this.btnResumenDia.Text = "Resumen del Día";
            this.btnResumenDia.UseVisualStyleBackColor = false;
            this.btnResumenDia.Click += new System.EventHandler(this.btnResumenDia_Click);
            // 
            // btnRegistrarVenta
            // 
            this.btnRegistrarVenta.BackColor = System.Drawing.Color.Sienna;
            this.btnRegistrarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarVenta.FlatAppearance.BorderSize = 0;
            this.btnRegistrarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarVenta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRegistrarVenta.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarVenta.Location = new System.Drawing.Point(223, 64);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Size = new System.Drawing.Size(120, 43);
            this.btnRegistrarVenta.TabIndex = 4;
            this.btnRegistrarVenta.Text = "Venta General";
            this.btnRegistrarVenta.UseVisualStyleBackColor = false;
            this.btnRegistrarVenta.Click += new System.EventHandler(this.btnRegistrarVenta_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Sienna;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(574, 451);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(77, 26);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // pnlEstadoDia
            // 
            this.pnlEstadoDia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.pnlEstadoDia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEstadoDia.Location = new System.Drawing.Point(223, 230);
            this.pnlEstadoDia.Name = "pnlEstadoDia";
            this.pnlEstadoDia.Size = new System.Drawing.Size(21, 21);
            this.pnlEstadoDia.TabIndex = 8;
            this.pnlEstadoDia.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlEstadoDia_Paint);
            // 
            // lblEstadoDia
            // 
            this.lblEstadoDia.AutoSize = true;
            this.lblEstadoDia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEstadoDia.ForeColor = System.Drawing.Color.Sienna;
            this.lblEstadoDia.Location = new System.Drawing.Point(253, 233);
            this.lblEstadoDia.Name = "lblEstadoDia";
            this.lblEstadoDia.Size = new System.Drawing.Size(96, 19);
            this.lblEstadoDia.TabIndex = 9;
            this.lblEstadoDia.Text = "Estado del Día";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.label1.Location = new System.Drawing.Point(212, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.label2.Location = new System.Drawing.Point(212, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.label3.Location = new System.Drawing.Point(225, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.label4.Location = new System.Drawing.Point(234, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "1";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(672, 486);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.btnConsultarProductos);
            this.Controls.Add(this.btnRegistrarVenta);
            this.Controls.Add(this.btnInicioDia);
            this.Controls.Add(this.btnCierreDia);
            this.Controls.Add(this.btnResumenDia);
            this.Controls.Add(this.pnlEstadoDia);
            this.Controls.Add(this.lblEstadoDia);
            this.Controls.Add(this.btnSalir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel Principal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        


        #endregion

        private Label lblTitulo;
        private PictureBox pictureBox1;
        private Button btnAgregarProducto;
        private Button btnConsultarProductos;
        private Button btnInicioDia;
        private Button btnCierreDia;
        private Button btnResumenDia;
        private Panel pnlEstadoDia;
        private Label lblEstadoDia;
        private Button btnSalir;
        private Button btnRegistrarVenta;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
