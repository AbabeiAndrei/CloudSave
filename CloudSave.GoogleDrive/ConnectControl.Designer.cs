namespace CloudSave.GoogleDrive
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
            this.gbGoogleDrive = new System.Windows.Forms.GroupBox();
            this.pbGoogleDriveLogo = new System.Windows.Forms.PictureBox();
            this.btnGoogleDriveConnect = new System.Windows.Forms.Button();
            this.lblGoogleDriveState = new System.Windows.Forms.Label();
            this.gbGoogleDrive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGoogleDriveLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // gbGoogleDrive
            // 
            this.gbGoogleDrive.Controls.Add(this.pbGoogleDriveLogo);
            this.gbGoogleDrive.Controls.Add(this.btnGoogleDriveConnect);
            this.gbGoogleDrive.Controls.Add(this.lblGoogleDriveState);
            this.gbGoogleDrive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGoogleDrive.Location = new System.Drawing.Point(0, 0);
            this.gbGoogleDrive.Name = "gbGoogleDrive";
            this.gbGoogleDrive.Size = new System.Drawing.Size(332, 95);
            this.gbGoogleDrive.TabIndex = 1;
            this.gbGoogleDrive.TabStop = false;
            this.gbGoogleDrive.Text = "Google Drive";
            // 
            // pbGoogleDriveLogo
            // 
            this.pbGoogleDriveLogo.Location = new System.Drawing.Point(7, 25);
            this.pbGoogleDriveLogo.Name = "pbGoogleDriveLogo";
            this.pbGoogleDriveLogo.Size = new System.Drawing.Size(151, 64);
            this.pbGoogleDriveLogo.TabIndex = 2;
            this.pbGoogleDriveLogo.TabStop = false;
            // 
            // btnGoogleDriveConnect
            // 
            this.btnGoogleDriveConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoogleDriveConnect.Location = new System.Drawing.Point(164, 49);
            this.btnGoogleDriveConnect.Name = "btnGoogleDriveConnect";
            this.btnGoogleDriveConnect.Size = new System.Drawing.Size(161, 40);
            this.btnGoogleDriveConnect.TabIndex = 1;
            this.btnGoogleDriveConnect.Text = "Conectare";
            this.btnGoogleDriveConnect.UseVisualStyleBackColor = true;
            this.btnGoogleDriveConnect.Click += new System.EventHandler(this.btnGoogleDriveConnect_Click);
            // 
            // lblGoogleDriveState
            // 
            this.lblGoogleDriveState.AutoSize = true;
            this.lblGoogleDriveState.Location = new System.Drawing.Point(160, 25);
            this.lblGoogleDriveState.Name = "lblGoogleDriveState";
            this.lblGoogleDriveState.Size = new System.Drawing.Size(63, 13);
            this.lblGoogleDriveState.TabIndex = 0;
            this.lblGoogleDriveState.Text = "Neconectat";
            // 
            // ConnectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbGoogleDrive);
            this.Name = "ConnectControl";
            this.Size = new System.Drawing.Size(332, 95);
            this.gbGoogleDrive.ResumeLayout(false);
            this.gbGoogleDrive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGoogleDriveLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGoogleDrive;
        private System.Windows.Forms.PictureBox pbGoogleDriveLogo;
        private System.Windows.Forms.Button btnGoogleDriveConnect;
        private System.Windows.Forms.Label lblGoogleDriveState;
    }
}
