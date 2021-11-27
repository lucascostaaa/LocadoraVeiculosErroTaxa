
namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    partial class TelaRegistroLocacaoForm
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
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtFuncionario = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGrupoVeiculos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDataLocacao = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDevolucaoPrevista = new System.Windows.Forms.DateTimePicker();
            this.cmbPlanosCobranca = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbVeiculos = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbCondutores = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtKmVeiculo = new System.Windows.Forms.TextBox();
            this.listTaxas = new System.Windows.Forms.CheckedListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCupom = new System.Windows.Forms.TextBox();
            this.btnAplicarCupom = new System.Windows.Forms.Button();
            this.labelValorPrevisto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Enabled = false;
            this.btnGravar.Location = new System.Drawing.Point(351, 475);
            this.btnGravar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(88, 31);
            this.btnGravar.TabIndex = 13;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(445, 475);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(88, 31);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // txtFuncionario
            // 
            this.txtFuncionario.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.txtFuncionario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFuncionario.Enabled = false;
            this.txtFuncionario.ForeColor = System.Drawing.SystemColors.Info;
            this.txtFuncionario.Location = new System.Drawing.Point(140, 55);
            this.txtFuncionario.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFuncionario.Name = "txtFuncionario";
            this.txtFuncionario.Size = new System.Drawing.Size(147, 23);
            this.txtFuncionario.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "Funcionário:";
            // 
            // cmbGrupoVeiculos
            // 
            this.cmbGrupoVeiculos.BackColor = System.Drawing.Color.LightYellow;
            this.cmbGrupoVeiculos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrupoVeiculos.FormattingEnabled = true;
            this.cmbGrupoVeiculos.Location = new System.Drawing.Point(140, 122);
            this.cmbGrupoVeiculos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbGrupoVeiculos.Name = "cmbGrupoVeiculos";
            this.cmbGrupoVeiculos.Size = new System.Drawing.Size(146, 23);
            this.cmbGrupoVeiculos.TabIndex = 19;
            this.cmbGrupoVeiculos.SelectedIndexChanged += new System.EventHandler(this.cmbGrupoVeiculos_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Grupo de Veículo:";
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Enabled = false;
            this.txtId.ForeColor = System.Drawing.SystemColors.Info;
            this.txtId.Location = new System.Drawing.Point(140, 24);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(67, 23);
            this.txtId.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Id:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 201);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 35;
            this.label7.Text = "Data da Locação:";
            // 
            // txtDataLocacao
            // 
            this.txtDataLocacao.CalendarMonthBackground = System.Drawing.Color.LightYellow;
            this.txtDataLocacao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDataLocacao.Location = new System.Drawing.Point(140, 198);
            this.txtDataLocacao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDataLocacao.Name = "txtDataLocacao";
            this.txtDataLocacao.Size = new System.Drawing.Size(146, 23);
            this.txtDataLocacao.TabIndex = 34;
            this.txtDataLocacao.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(293, 195);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 31);
            this.label8.TabIndex = 37;
            this.label8.Text = "Devolução Prevista:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDevolucaoPrevista
            // 
            this.txtDevolucaoPrevista.CalendarMonthBackground = System.Drawing.Color.LightYellow;
            this.txtDevolucaoPrevista.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDevolucaoPrevista.Location = new System.Drawing.Point(380, 198);
            this.txtDevolucaoPrevista.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDevolucaoPrevista.Name = "txtDevolucaoPrevista";
            this.txtDevolucaoPrevista.Size = new System.Drawing.Size(152, 23);
            this.txtDevolucaoPrevista.TabIndex = 36;
            this.txtDevolucaoPrevista.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cmbPlanosCobranca
            // 
            this.cmbPlanosCobranca.BackColor = System.Drawing.Color.LightYellow;
            this.cmbPlanosCobranca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlanosCobranca.FormattingEnabled = true;
            this.cmbPlanosCobranca.Location = new System.Drawing.Point(140, 158);
            this.cmbPlanosCobranca.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPlanosCobranca.Name = "cmbPlanosCobranca";
            this.cmbPlanosCobranca.Size = new System.Drawing.Size(146, 23);
            this.cmbPlanosCobranca.TabIndex = 40;
            this.cmbPlanosCobranca.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Plano:";
            // 
            // cmbVeiculos
            // 
            this.cmbVeiculos.BackColor = System.Drawing.Color.LightYellow;
            this.cmbVeiculos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVeiculos.FormattingEnabled = true;
            this.cmbVeiculos.Location = new System.Drawing.Point(380, 122);
            this.cmbVeiculos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbVeiculos.Name = "cmbVeiculos";
            this.cmbVeiculos.Size = new System.Drawing.Size(153, 23);
            this.cmbVeiculos.TabIndex = 42;
            this.cmbVeiculos.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(325, 126);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 15);
            this.label9.TabIndex = 41;
            this.label9.Text = "Veículo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 376);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "Cupom:";
            // 
            // cmbClientes
            // 
            this.cmbClientes.BackColor = System.Drawing.Color.LightYellow;
            this.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(140, 88);
            this.cmbClientes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(146, 23);
            this.cmbClientes.TabIndex = 48;
            this.cmbClientes.SelectedIndexChanged += new System.EventHandler(this.cmbClientes_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(89, 92);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 15);
            this.label10.TabIndex = 47;
            this.label10.Text = "Cliente:";
            // 
            // cmbCondutores
            // 
            this.cmbCondutores.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCondutores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondutores.FormattingEnabled = true;
            this.cmbCondutores.Location = new System.Drawing.Point(380, 88);
            this.cmbCondutores.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbCondutores.Name = "cmbCondutores";
            this.cmbCondutores.Size = new System.Drawing.Size(152, 23);
            this.cmbCondutores.TabIndex = 50;
            this.cmbCondutores.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(312, 92);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 15);
            this.label11.TabIndex = 49;
            this.label11.Text = "Condutor:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(35, 289);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 15);
            this.label14.TabIndex = 57;
            this.label14.Text = "Taxas da Locação:";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(318, 151);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 37);
            this.label13.TabIndex = 58;
            this.label13.Text = "Km do Veículo:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKmVeiculo
            // 
            this.txtKmVeiculo.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.txtKmVeiculo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKmVeiculo.Enabled = false;
            this.txtKmVeiculo.ForeColor = System.Drawing.SystemColors.Info;
            this.txtKmVeiculo.Location = new System.Drawing.Point(380, 158);
            this.txtKmVeiculo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtKmVeiculo.Name = "txtKmVeiculo";
            this.txtKmVeiculo.Size = new System.Drawing.Size(152, 23);
            this.txtKmVeiculo.TabIndex = 59;
            // 
            // listTaxas
            // 
            this.listTaxas.CheckOnClick = true;
            this.listTaxas.FormattingEnabled = true;
            this.listTaxas.Location = new System.Drawing.Point(140, 241);
            this.listTaxas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listTaxas.Name = "listTaxas";
            this.listTaxas.Size = new System.Drawing.Size(392, 112);
            this.listTaxas.TabIndex = 60;
            this.listTaxas.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listTaxas_ItemCheck);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 417);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 15);
            this.label12.TabIndex = 61;
            this.label12.Text = "Valor Total Previsto:";
            // 
            // txtCupom
            // 
            this.txtCupom.Location = new System.Drawing.Point(140, 373);
            this.txtCupom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCupom.Name = "txtCupom";
            this.txtCupom.Size = new System.Drawing.Size(186, 23);
            this.txtCupom.TabIndex = 63;
            // 
            // btnAplicarCupom
            // 
            this.btnAplicarCupom.Location = new System.Drawing.Point(334, 369);
            this.btnAplicarCupom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAplicarCupom.Name = "btnAplicarCupom";
            this.btnAplicarCupom.Size = new System.Drawing.Size(199, 31);
            this.btnAplicarCupom.TabIndex = 64;
            this.btnAplicarCupom.Text = "Aplicar Cupom";
            this.btnAplicarCupom.UseVisualStyleBackColor = true;
            this.btnAplicarCupom.Click += new System.EventHandler(this.btnAplicarCupom_Click);
            // 
            // labelValorPrevisto
            // 
            this.labelValorPrevisto.AutoSize = true;
            this.labelValorPrevisto.BackColor = System.Drawing.Color.Transparent;
            this.labelValorPrevisto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelValorPrevisto.Location = new System.Drawing.Point(140, 419);
            this.labelValorPrevisto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelValorPrevisto.Name = "labelValorPrevisto";
            this.labelValorPrevisto.Size = new System.Drawing.Size(35, 13);
            this.labelValorPrevisto.TabIndex = 65;
            this.labelValorPrevisto.Text = "valor";
            // 
            // TelaRegistroLocacaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 527);
            this.Controls.Add(this.labelValorPrevisto);
            this.Controls.Add(this.btnAplicarCupom);
            this.Controls.Add(this.txtCupom);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listTaxas);
            this.Controls.Add(this.txtKmVeiculo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbCondutores);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbClientes);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbVeiculos);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbPlanosCobranca);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDevolucaoPrevista);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDataLocacao);
            this.Controls.Add(this.txtFuncionario);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbGrupoVeiculos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaRegistroLocacaoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Locações";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtFuncionario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGrupoVeiculos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker txtDataLocacao;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker txtDevolucaoPrevista;
        private System.Windows.Forms.ComboBox cmbPlanosCobranca;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbVeiculos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbCondutores;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtKmVeiculo;
        private System.Windows.Forms.CheckedListBox listTaxas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCupom;
        private System.Windows.Forms.Button btnAplicarCupom;
        private System.Windows.Forms.Label labelValorPrevisto;


    }
}