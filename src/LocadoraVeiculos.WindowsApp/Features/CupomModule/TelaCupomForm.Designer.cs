
namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    partial class TelaCupomForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbParceiros = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDataValidade = new System.Windows.Forms.DateTimePicker();
            this.txtValorMinimo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbParceiros
            // 
            this.cmbParceiros.BackColor = System.Drawing.Color.LightYellow;
            this.cmbParceiros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParceiros.FormattingEnabled = true;
            this.cmbParceiros.Location = new System.Drawing.Point(124, 156);
            this.cmbParceiros.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbParceiros.Name = "cmbParceiros";
            this.cmbParceiros.Size = new System.Drawing.Size(147, 23);
            this.cmbParceiros.TabIndex = 56;
            this.cmbParceiros.SelectedValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(67, 159);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 15);
            this.label10.TabIndex = 55;
            this.label10.Text = "Parceiro:";
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNome.Location = new System.Drawing.Point(124, 58);
            this.txtNome.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(393, 23);
            this.txtNome.TabIndex = 54;
            this.txtNome.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 61);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 53;
            this.label5.Text = "Nome:";
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Enabled = false;
            this.txtId.ForeColor = System.Drawing.SystemColors.Info;
            this.txtId.Location = new System.Drawing.Point(124, 25);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(67, 23);
            this.txtId.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 51;
            this.label1.Text = "Id:";
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Enabled = false;
            this.btnGravar.Location = new System.Drawing.Point(335, 337);
            this.btnGravar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(88, 31);
            this.btnGravar.TabIndex = 50;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(429, 337);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(88, 31);
            this.btnCancelar.TabIndex = 49;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtValor.Location = new System.Drawing.Point(124, 91);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(147, 23);
            this.txtValor.TabIndex = 58;
            this.txtValor.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "Valor:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 127);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 60;
            this.label7.Text = "Data de Validade:";
            // 
            // txtDataValidade
            // 
            this.txtDataValidade.CalendarMonthBackground = System.Drawing.Color.LightYellow;
            this.txtDataValidade.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDataValidade.Location = new System.Drawing.Point(124, 123);
            this.txtDataValidade.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDataValidade.Name = "txtDataValidade";
            this.txtDataValidade.Size = new System.Drawing.Size(146, 23);
            this.txtDataValidade.TabIndex = 59;
            this.txtDataValidade.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // txtValorMinimo
            // 
            this.txtValorMinimo.BackColor = System.Drawing.Color.White;
            this.txtValorMinimo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValorMinimo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtValorMinimo.Location = new System.Drawing.Point(370, 91);
            this.txtValorMinimo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtValorMinimo.Name = "txtValorMinimo";
            this.txtValorMinimo.Size = new System.Drawing.Size(147, 23);
            this.txtValorMinimo.TabIndex = 62;
            this.txtValorMinimo.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 61;
            this.label3.Text = "Valor Mínimo:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.LightYellow;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Valor Fixo",
            "Percentual do Valor"});
            this.cmbTipo.Location = new System.Drawing.Point(124, 188);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(146, 23);
            this.cmbTipo.TabIndex = 64;
            this.cmbTipo.SelectedValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 63;
            this.label4.Text = "Tipo:";
            // 
            // TelaCupomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 394);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValorMinimo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDataValidade);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbParceiros);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaCupomForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Cupons";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbParceiros;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker txtDataValidade;
        private System.Windows.Forms.TextBox txtValorMinimo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label4;
    }
}