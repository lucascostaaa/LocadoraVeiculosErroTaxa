
namespace LocadoraVeiculos.WindowsApp
{
    partial class TelaPrincipalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.condutoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funcionáriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.veículosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cuponsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parceirosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locacoesVeiculosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolboxAcoes = new System.Windows.Forms.ToolStrip();
            this.btnInserirNovo = new System.Windows.Forms.ToolStripButton();
            this.btnConcluir = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEnviarEmail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.labelTipoCadastro = new System.Windows.Forms.ToolStripLabel();
            this.panelRegistros = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelRodape = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.toolboxAcoes.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrosToolStripMenuItem,
            this.vendasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1025, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            this.cadastrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesMenuItem,
            this.condutoresToolStripMenuItem,
            this.funcionáriosToolStripMenuItem,
            this.veículosToolStripMenuItem1,
            this.cuponsToolStripMenuItem,
            this.parceirosToolStripMenuItem});
            this.cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            this.cadastrosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // clientesMenuItem
            // 
            this.clientesMenuItem.Name = "clientesMenuItem";
            this.clientesMenuItem.Size = new System.Drawing.Size(142, 22);
            this.clientesMenuItem.Text = "Clientes";
            this.clientesMenuItem.Click += new System.EventHandler(this.locacoesVeiculosMenuItem_Click);
            // 
            // condutoresToolStripMenuItem
            // 
            this.condutoresToolStripMenuItem.Name = "condutoresToolStripMenuItem";
            this.condutoresToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.condutoresToolStripMenuItem.Text = "Condutores";
            // 
            // funcionáriosToolStripMenuItem
            // 
            this.funcionáriosToolStripMenuItem.Name = "funcionáriosToolStripMenuItem";
            this.funcionáriosToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.funcionáriosToolStripMenuItem.Text = "Funcionários";
            // 
            // veículosToolStripMenuItem1
            // 
            this.veículosToolStripMenuItem1.Name = "veículosToolStripMenuItem1";
            this.veículosToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.veículosToolStripMenuItem1.Text = "Veículos";
            // 
            // cuponsToolStripMenuItem
            // 
            this.cuponsToolStripMenuItem.Name = "cuponsToolStripMenuItem";
            this.cuponsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.cuponsToolStripMenuItem.Text = "Cupons";
            this.cuponsToolStripMenuItem.Click += new System.EventHandler(this.cuponsMenuItem_Click);
            // 
            // parceirosToolStripMenuItem
            // 
            this.parceirosToolStripMenuItem.Name = "parceirosToolStripMenuItem";
            this.parceirosToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.parceirosToolStripMenuItem.Text = "Parceiros";
            this.parceirosToolStripMenuItem.Click += new System.EventHandler(this.parceirosMenuItem_Click);
            // 
            // vendasToolStripMenuItem
            // 
            this.vendasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locacoesVeiculosMenuItem});
            this.vendasToolStripMenuItem.Name = "vendasToolStripMenuItem";
            this.vendasToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.vendasToolStripMenuItem.Text = "Locações";
            // 
            // locacoesVeiculosMenuItem
            // 
            this.locacoesVeiculosMenuItem.Name = "locacoesVeiculosMenuItem";
            this.locacoesVeiculosMenuItem.Size = new System.Drawing.Size(117, 22);
            this.locacoesVeiculosMenuItem.Text = "Veículos";
            this.locacoesVeiculosMenuItem.Click += new System.EventHandler(this.locacoesVeiculosMenuItem_Click);
            // 
            // toolboxAcoes
            // 
            this.toolboxAcoes.Enabled = false;
            this.toolboxAcoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInserirNovo,
            this.btnConcluir,
            this.btnEditar,
            this.btnExcluir,
            this.toolStripSeparator1,
            this.btnEnviarEmail,
            this.toolStripSeparator2,
            this.labelTipoCadastro});
            this.toolboxAcoes.Location = new System.Drawing.Point(0, 24);
            this.toolboxAcoes.Name = "toolboxAcoes";
            this.toolboxAcoes.Size = new System.Drawing.Size(1025, 43);
            this.toolboxAcoes.TabIndex = 1;
            this.toolboxAcoes.Text = "toolStrip1";
            // 
            // btnInserirNovo
            // 
            this.btnInserirNovo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInserirNovo.Image = global::LocadoraVeiculos.WindowsApp.Properties.Resources.outline_add_circle_outline_black_24dp;
            this.btnInserirNovo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInserirNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserirNovo.Name = "btnInserirNovo";
            this.btnInserirNovo.Padding = new System.Windows.Forms.Padding(6);
            this.btnInserirNovo.Size = new System.Drawing.Size(40, 40);
            this.btnInserirNovo.Click += new System.EventHandler(this.btnInserirNovo_Click);
            // 
            // btnConcluir
            // 
            this.btnConcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConcluir.Image = global::LocadoraVeiculos.WindowsApp.Properties.Resources.outline_check_circle_black_24dp;
            this.btnConcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConcluir.Name = "btnConcluir";
            this.btnConcluir.Padding = new System.Windows.Forms.Padding(6);
            this.btnConcluir.Size = new System.Drawing.Size(40, 40);
            this.btnConcluir.Text = "Agrupar";
            this.btnConcluir.Click += new System.EventHandler(this.btnConcluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::LocadoraVeiculos.WindowsApp.Properties.Resources.outline_mode_edit_black_24dp;
            this.btnEditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Padding = new System.Windows.Forms.Padding(6);
            this.btnEditar.Size = new System.Drawing.Size(40, 40);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcluir.Image = global::LocadoraVeiculos.WindowsApp.Properties.Resources.outline_delete_black_24dp;
            this.btnExcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Padding = new System.Windows.Forms.Padding(6);
            this.btnExcluir.Size = new System.Drawing.Size(40, 40);
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnEnviarEmail
            // 
            this.btnEnviarEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnviarEmail.Image = global::LocadoraVeiculos.WindowsApp.Properties.Resources.outline_mail_black_24dp;
            this.btnEnviarEmail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEnviarEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviarEmail.Name = "btnEnviarEmail";
            this.btnEnviarEmail.Padding = new System.Windows.Forms.Padding(6);
            this.btnEnviarEmail.Size = new System.Drawing.Size(40, 40);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // labelTipoCadastro
            // 
            this.labelTipoCadastro.Name = "labelTipoCadastro";
            this.labelTipoCadastro.Size = new System.Drawing.Size(174, 40);
            this.labelTipoCadastro.Text = "Cadastro Selecionado: Nenhum";
            // 
            // panelRegistros
            // 
            this.panelRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRegistros.Location = new System.Drawing.Point(14, 81);
            this.panelRegistros.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelRegistros.Name = "panelRegistros";
            this.panelRegistros.Size = new System.Drawing.Size(997, 600);
            this.panelRegistros.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelRodape});
            this.statusStrip1.Location = new System.Drawing.Point(0, 697);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1025, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelRodape
            // 
            this.labelRodape.Name = "labelRodape";
            this.labelRodape.Size = new System.Drawing.Size(73, 17);
            this.labelRodape.Text = "Tudo Ok!  ;-)";
            // 
            // TelaPrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 719);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelRegistros);
            this.Controls.Add(this.toolboxAcoes);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TelaPrincipalForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Locadora de Veículos";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolboxAcoes.ResumeLayout(false);
            this.toolboxAcoes.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesMenuItem;
        private System.Windows.Forms.ToolStrip toolboxAcoes;
        private System.Windows.Forms.ToolStripButton btnInserirNovo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel labelTipoCadastro;
        private System.Windows.Forms.Panel panelRegistros;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelRodape;
        private System.Windows.Forms.ToolStripButton btnConcluir;
        private System.Windows.Forms.ToolStripMenuItem vendasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locacoesVeiculosMenuItem;
        private System.Windows.Forms.ToolStripMenuItem condutoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem funcionáriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem veículosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cuponsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parceirosToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnEnviarEmail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

