namespace apideneme
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSoru = new System.Windows.Forms.TextBox();
            this.txtCevap = new System.Windows.Forms.RichTextBox();
            this.btnGonder = new System.Windows.Forms.Button();
            this.btnDosyaSec = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSoru
            // 
            this.txtSoru.Location = new System.Drawing.Point(55, 51);
            this.txtSoru.Name = "txtSoru";
            this.txtSoru.Size = new System.Drawing.Size(363, 20);
            this.txtSoru.TabIndex = 0;
            this.txtSoru.TextChanged += new System.EventHandler(this.txtSoru_TextChanged);
            // 
            // txtCevap
            // 
            this.txtCevap.Location = new System.Drawing.Point(55, 212);
            this.txtCevap.Name = "txtCevap";
            this.txtCevap.Size = new System.Drawing.Size(478, 201);
            this.txtCevap.TabIndex = 1;
            this.txtCevap.Text = "";
            // 
            // btnGonder
            // 
            this.btnGonder.Location = new System.Drawing.Point(55, 169);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(119, 37);
            this.btnGonder.TabIndex = 2;
            this.btnGonder.Text = "Sor";
            this.btnGonder.UseVisualStyleBackColor = true;
            this.btnGonder.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDosyaSec
            // 
            this.btnDosyaSec.Location = new System.Drawing.Point(55, 118);
            this.btnDosyaSec.Name = "btnDosyaSec";
            this.btnDosyaSec.Size = new System.Drawing.Size(119, 29);
            this.btnDosyaSec.TabIndex = 3;
            this.btnDosyaSec.Text = "gorsel\\dosyasec";
            this.btnDosyaSec.UseVisualStyleBackColor = true;
            this.btnDosyaSec.Click += new System.EventHandler(this.btnDosyaSec_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(258, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(219, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDosyaSec);
            this.Controls.Add(this.btnGonder);
            this.Controls.Add(this.txtCevap);
            this.Controls.Add(this.txtSoru);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSoru;
        private System.Windows.Forms.RichTextBox txtCevap;
        private System.Windows.Forms.Button btnGonder;
        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

