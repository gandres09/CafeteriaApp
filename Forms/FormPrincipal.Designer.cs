using System.Drawing;
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
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.btnConsultarProductos = new System.Windows.Forms.Button();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnInicioDia = new System.Windows.Forms.Button();
            this.btnCierreDia = new System.Windows.Forms.Button();
            this.btnResumenDia = new System.Windows.Forms.Button();
            this.pnlEstadoDia = new System.Windows.Forms.Panel();
            this.lblEstadoDia = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultarProductos
            // 
            this.btnConsultarProductos.Location = new System.Drawing.Point(12, 526);
            this.btnConsultarProductos.Name = "btnConsultarProductos";
            this.btnConsultarProductos.Size = new System.Drawing.Size(136, 23);
            this.btnConsultarProductos.TabIndex = 0;
            this.btnConsultarProductos.Text = "Consultar Producto";
            this.btnConsultarProductos.UseVisualStyleBackColor = true;
            this.btnConsultarProductos.Click += new System.EventHandler(this.btnConsultarProductos_Click);
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Location = new System.Drawing.Point(12, 497);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(136, 23);
            this.btnAgregarProducto.TabIndex = 1;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnInicioDia
            // 
            this.btnInicioDia.Location = new System.Drawing.Point(41, 193);
            this.btnInicioDia.Name = "btnInicioDia";
            this.btnInicioDia.Size = new System.Drawing.Size(75, 23);
            this.btnInicioDia.TabIndex = 3;
            this.btnInicioDia.Text = "Iniciar Dia";
            this.btnInicioDia.UseVisualStyleBackColor = true;
            this.btnInicioDia.Click += new System.EventHandler(this.btnInicioDia_Click);
            // 
            // btnCierreDia
            // 
            this.btnCierreDia.Location = new System.Drawing.Point(122, 193);
            this.btnCierreDia.Name = "btnCierreDia";
            this.btnCierreDia.Size = new System.Drawing.Size(75, 23);
            this.btnCierreDia.TabIndex = 4;
            this.btnCierreDia.Text = "Cerrar Dia";
            this.btnCierreDia.UseVisualStyleBackColor = true;
            this.btnCierreDia.Click += new System.EventHandler(this.btnCierreDia_Click);
            // 
            // btnResumenDia
            // 
            this.btnResumenDia.Location = new System.Drawing.Point(41, 222);
            this.btnResumenDia.Name = "btnResumenDia";
            this.btnResumenDia.Size = new System.Drawing.Size(150, 30);
            this.btnResumenDia.TabIndex = 5;
            this.btnResumenDia.Text = "Resumen del Día";
            this.btnResumenDia.UseVisualStyleBackColor = true;
            this.btnResumenDia.Click += new System.EventHandler(this.btnResumenDia_Click);
            // 
            // pnlEstadoDia
            // 
            this.pnlEstadoDia.BackColor = System.Drawing.Color.Gray;
            this.pnlEstadoDia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEstadoDia.Location = new System.Drawing.Point(35, 167);
            this.pnlEstadoDia.Name = "pnlEstadoDia";
            this.pnlEstadoDia.Size = new System.Drawing.Size(20, 20);
            this.pnlEstadoDia.TabIndex = 6;
            this.pnlEstadoDia.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlEstadoDia_Paint);
            // 
            // lblEstadoDia
            // 
            this.lblEstadoDia.AutoSize = true;
            this.lblEstadoDia.Location = new System.Drawing.Point(61, 172);
            this.lblEstadoDia.Name = "lblEstadoDia";
            this.lblEstadoDia.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoDia.TabIndex = 7;
            this.lblEstadoDia.Text = "label1";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(697, 526);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 8;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnRegistrarVenta
            // 
            this.btnRegistrarVenta.Location = new System.Drawing.Point(256, 30);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Size = new System.Drawing.Size(75, 38);
            this.btnRegistrarVenta.TabIndex = 9;
            this.btnRegistrarVenta.Text = "Venta General";
            this.btnRegistrarVenta.UseVisualStyleBackColor = true;
            this.btnRegistrarVenta.Click += new System.EventHandler(this.btnRegistrarVenta_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(241)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnRegistrarVenta);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblEstadoDia);
            this.Controls.Add(this.btnCierreDia);
            this.Controls.Add(this.btnInicioDia);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.btnConsultarProductos);
            this.Controls.Add(this.btnResumenDia);
            this.Controls.Add(this.pnlEstadoDia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConsultarProductos;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnInicioDia;
        private System.Windows.Forms.Button btnCierreDia;
        private System.Windows.Forms.Button btnResumenDia;
        private System.Windows.Forms.Panel pnlEstadoDia;
        private Label lblEstadoDia;
        private Button btnSalir;
        private Button btnRegistrarVenta;
    }
}

