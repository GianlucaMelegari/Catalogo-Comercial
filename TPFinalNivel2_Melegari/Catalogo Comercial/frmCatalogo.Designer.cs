namespace Catalogo_Comercial
{
    partial class frmCatalogo
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
            this.dgvCatalogo = new System.Windows.Forms.DataGridView();
            this.pbElectrodomestico = new System.Windows.Forms.PictureBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbElectrodomestico)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCatalogo
            // 
            this.dgvCatalogo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCatalogo.Location = new System.Drawing.Point(12, 21);
            this.dgvCatalogo.Name = "dgvCatalogo";
            this.dgvCatalogo.Size = new System.Drawing.Size(583, 345);
            this.dgvCatalogo.TabIndex = 0;
            this.dgvCatalogo.SelectionChanged += new System.EventHandler(this.dgvCatalogo_SelectionChanged);
            // 
            // pbElectrodomestico
            // 
            this.pbElectrodomestico.Location = new System.Drawing.Point(626, 21);
            this.pbElectrodomestico.Name = "pbElectrodomestico";
            this.pbElectrodomestico.Size = new System.Drawing.Size(257, 345);
            this.pbElectrodomestico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbElectrodomestico.TabIndex = 1;
            this.pbElectrodomestico.TabStop = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(12, 397);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmCatalogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 432);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.pbElectrodomestico);
            this.Controls.Add(this.dgvCatalogo);
            this.Name = "frmCatalogo";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbElectrodomestico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCatalogo;
        private System.Windows.Forms.PictureBox pbElectrodomestico;
        private System.Windows.Forms.Button btnAgregar;
    }
}

