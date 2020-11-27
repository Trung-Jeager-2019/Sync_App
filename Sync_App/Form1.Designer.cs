namespace Sync_App
{
    partial class Form_Sync
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
            this.button_Sync = new System.Windows.Forms.Button();
            this.button_Thoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Sync
            // 
            this.button_Sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Sync.Location = new System.Drawing.Point(69, 96);
            this.button_Sync.Name = "button_Sync";
            this.button_Sync.Size = new System.Drawing.Size(501, 129);
            this.button_Sync.TabIndex = 0;
            this.button_Sync.Text = "Sync";
            this.button_Sync.UseVisualStyleBackColor = true;
            this.button_Sync.Click += new System.EventHandler(this.button_Sync_Click);
            // 
            // button_Thoat
            // 
            this.button_Thoat.Location = new System.Drawing.Point(704, 124);
            this.button_Thoat.Name = "button_Thoat";
            this.button_Thoat.Size = new System.Drawing.Size(187, 72);
            this.button_Thoat.TabIndex = 1;
            this.button_Thoat.Text = "Thoát";
            this.button_Thoat.UseVisualStyleBackColor = true;
            this.button_Thoat.Click += new System.EventHandler(this.button_Thoat_Click);
            // 
            // Form_Sync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 332);
            this.Controls.Add(this.button_Thoat);
            this.Controls.Add(this.button_Sync);
            this.Name = "Form_Sync";
            this.Text = "Form_Sync";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Sync;
        private System.Windows.Forms.Button button_Thoat;
    }
}

