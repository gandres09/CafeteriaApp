namespace CafeteriaApp.Forms
{
    partial class FormInicioDia
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.txtSaldoInicial = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnIniciarDia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Poor Richard", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Sienna;
            this.labelTitle.Location = new System.Drawing.Point(65, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(159, 44);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Inicio de Día";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(105, 75);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(98, 20);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "01/01/1999";  // Este texto debería actualizarse dinámicamente
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSaldo.Location = new System.Drawing.Point(50, 110);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(74, 19);
            this.lblSaldo.TabIndex = 2;
            this.lblSaldo.Text = "Saldo inicial:";
            // 
            // txtSaldoInicial
            // 
            this.txtSaldoInicial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSaldoInicial.Location = new System.Drawing.Point(130, 107);
            this.txtSaldoInicial.Name = "txtSaldoInicial";
            this.txtSaldoInicial.Size = new System.Drawing.Size(100, 25);
            this.txtSaldoInicial.TabIndex = 3;
            this.txtSaldoInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaldoInicial_KeyPress);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(50, 180);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(35, 13);
            this.lblMensaje.TabIndex = 4;
            this.lblMensaje.Text = "Error";
            this.lblMensaje.Visible = false;
            // 
            // btnIniciarDia
            // 
            this.btnIniciarDia.BackColor = System.Drawing.Color.Sienna;
            this.btnIniciarDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarDia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnIniciarDia.ForeColor = System.Drawing.Color.White;
            this.btnIniciarDia.Location = new System.Drawing.Point(115, 140);
            this.btnIniciarDia.Name = "btnIniciarDia";
            this.btnIniciarDia.Size = new System.Drawing.Size(90, 30);
            this.btnIniciarDia.TabIndex = 5;
            this.btnIniciarDia.Text = "Iniciar Día";
            this.btnIniciarDia.UseVisualStyleBackColor = false;
            this.btnIniciarDia.Click += new System.EventHandler(this.btnIniciarDia_Click);
            this.btnIniciarDia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaldoInicial_KeyPress);
            // 
            // FormInicioDia
            // 
            this.AcceptButton = this.btnIniciarDia;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 241, 223);
            this.ClientSize = new System.Drawing.Size(300, 230);
            this.Controls.Add(this.btnIniciarDia);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.txtSaldoInicial);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormInicioDia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Día";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.TextBox txtSaldoInicial;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnIniciarDia;
    }
}
