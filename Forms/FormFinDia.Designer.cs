namespace CafeteriaApp.Forms
{
    partial class FormFinDia
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
            this.lblSaldoFinal = new System.Windows.Forms.Label();
            this.txtSaldoFinal = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnCerrarDia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Poor Richard", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Sienna;
            this.labelTitle.Location = new System.Drawing.Point(70, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(144, 44);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Fin de Día";
            // 
            // lblSaldoFinal
            // 
            this.lblSaldoFinal.AutoSize = true;
            this.lblSaldoFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldoFinal.Location = new System.Drawing.Point(50, 90);
            this.lblSaldoFinal.Name = "lblSaldoFinal";
            this.lblSaldoFinal.Size = new System.Drawing.Size(73, 19);
            this.lblSaldoFinal.TabIndex = 1;
            this.lblSaldoFinal.Text = "Saldo final:";
            // 
            // txtSaldoFinal
            // 
            this.txtSaldoFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoFinal.Location = new System.Drawing.Point(130, 87);
            this.txtSaldoFinal.Name = "txtSaldoFinal";
            this.txtSaldoFinal.Size = new System.Drawing.Size(100, 25);
            this.txtSaldoFinal.TabIndex = 2;
            this.txtSaldoFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaldoFinal_KeyPress);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(50, 140);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(35, 13);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Text = "Error";
            this.lblMensaje.Visible = false;
            // 
            // btnCerrarDia
            // 
            this.btnCerrarDia.BackColor = System.Drawing.Color.Sienna;
            this.btnCerrarDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarDia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrarDia.ForeColor = System.Drawing.Color.White;
            this.btnCerrarDia.Location = new System.Drawing.Point(110, 170);
            this.btnCerrarDia.Name = "btnCerrarDia";
            this.btnCerrarDia.Size = new System.Drawing.Size(90, 30);
            this.btnCerrarDia.TabIndex = 4;
            this.btnCerrarDia.Text = "Cerrar Día";
            this.btnCerrarDia.UseVisualStyleBackColor = false;
            this.btnCerrarDia.Click += new System.EventHandler(this.btnCerrarDia_Click);
            // 
            // FormFinDia
            // 
            this.AcceptButton = this.btnCerrarDia;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 241, 223);
            this.ClientSize = new System.Drawing.Size(300, 230);
            this.Controls.Add(this.btnCerrarDia);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.txtSaldoFinal);
            this.Controls.Add(this.lblSaldoFinal);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormFinDia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fin de Día";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lblSaldoFinal;
        private System.Windows.Forms.TextBox txtSaldoFinal;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnCerrarDia;
    }
}
