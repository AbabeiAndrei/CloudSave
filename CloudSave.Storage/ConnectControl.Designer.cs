namespace CloudSave.Storage
{
    partial class ConnectControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbLocalStorage = new System.Windows.Forms.GroupBox();
            this.pbGoogleDriveLogo = new System.Windows.Forms.PictureBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.gbLocalStorage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGoogleDriveLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLocalStorage
            // 
            this.gbLocalStorage.Controls.Add(this.pbGoogleDriveLogo);
            this.gbLocalStorage.Controls.Add(this.btnSelectPath);
            this.gbLocalStorage.Controls.Add(this.lblPath);
            this.gbLocalStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLocalStorage.Location = new System.Drawing.Point(0, 0);
            this.gbLocalStorage.Name = "gbLocalStorage";
            this.gbLocalStorage.Size = new System.Drawing.Size(333, 97);
            this.gbLocalStorage.TabIndex = 2;
            this.gbLocalStorage.TabStop = false;
            this.gbLocalStorage.Text = "Local Storage";
            // 
            // pbGoogleDriveLogo
            // 
            this.pbGoogleDriveLogo.Image = global::CloudSave.Storage.Properties.Resources.FH63XNDI504TRGD_LARGE;
            this.pbGoogleDriveLogo.Location = new System.Drawing.Point(7, 25);
            this.pbGoogleDriveLogo.Name = "pbGoogleDriveLogo";
            this.pbGoogleDriveLogo.Size = new System.Drawing.Size(151, 64);
            this.pbGoogleDriveLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGoogleDriveLogo.TabIndex = 2;
            this.pbGoogleDriveLogo.TabStop = false;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPath.Location = new System.Drawing.Point(165, 51);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(161, 40);
            this.btnSelectPath.TabIndex = 1;
            this.btnSelectPath.Text = "Conectare";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(160, 25);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 13);
            this.lblPath.TabIndex = 0;
            // 
            // ConnectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbLocalStorage);
            this.Name = "ConnectControl";
            this.Size = new System.Drawing.Size(333, 97);
            this.Load += new System.EventHandler(this.ConnectControl_Load);
            this.gbLocalStorage.ResumeLayout(false);
            this.gbLocalStorage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGoogleDriveLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLocalStorage;
        private System.Windows.Forms.PictureBox pbGoogleDriveLogo;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
    }
}
