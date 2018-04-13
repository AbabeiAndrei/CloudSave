namespace CloudSave
{
    partial class SettingsForm
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
            DisposeComponents(disposing);
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.chkStartOnStartup = new System.Windows.Forms.CheckBox();
            this.tpAccounts = new System.Windows.Forms.TabPage();
            this.flwServices = new System.Windows.Forms.FlowLayoutPanel();
            this.tpLocations = new System.Windows.Forms.TabPage();
            this.dgvLocations = new System.Windows.Forms.DataGridView();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgcLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcMain.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.tpAccounts.SuspendLayout();
            this.tpLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocations)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpSettings);
            this.tcMain.Controls.Add(this.tpAccounts);
            this.tcMain.Controls.Add(this.tpLocations);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(891, 455);
            this.tcMain.TabIndex = 0;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.chkStartOnStartup);
            this.tpSettings.Location = new System.Drawing.Point(4, 30);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(883, 421);
            this.tpSettings.TabIndex = 2;
            this.tpSettings.Text = "General";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // chkStartOnStartup
            // 
            this.chkStartOnStartup.AutoSize = true;
            this.chkStartOnStartup.Location = new System.Drawing.Point(9, 7);
            this.chkStartOnStartup.Name = "chkStartOnStartup";
            this.chkStartOnStartup.Size = new System.Drawing.Size(221, 25);
            this.chkStartOnStartup.TabIndex = 0;
            this.chkStartOnStartup.Text = "Porneste odata cu Windows";
            this.chkStartOnStartup.UseVisualStyleBackColor = true;
            // 
            // tpAccounts
            // 
            this.tpAccounts.Controls.Add(this.flwServices);
            this.tpAccounts.Location = new System.Drawing.Point(4, 30);
            this.tpAccounts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpAccounts.Name = "tpAccounts";
            this.tpAccounts.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpAccounts.Size = new System.Drawing.Size(883, 421);
            this.tpAccounts.TabIndex = 0;
            this.tpAccounts.Text = "Conturi";
            this.tpAccounts.UseVisualStyleBackColor = true;
            // 
            // flwServices
            // 
            this.flwServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwServices.Location = new System.Drawing.Point(4, 5);
            this.flwServices.Name = "flwServices";
            this.flwServices.Size = new System.Drawing.Size(875, 411);
            this.flwServices.TabIndex = 1;
            // 
            // tpLocations
            // 
            this.tpLocations.Controls.Add(this.dgvLocations);
            this.tpLocations.Location = new System.Drawing.Point(4, 30);
            this.tpLocations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpLocations.Name = "tpLocations";
            this.tpLocations.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpLocations.Size = new System.Drawing.Size(883, 421);
            this.tpLocations.TabIndex = 1;
            this.tpLocations.Text = "Locatii";
            this.tpLocations.UseVisualStyleBackColor = true;
            // 
            // dgvLocations
            // 
            this.dgvLocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcLocation});
            this.dgvLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocations.Location = new System.Drawing.Point(4, 5);
            this.dgvLocations.Name = "dgvLocations";
            this.dgvLocations.Size = new System.Drawing.Size(875, 411);
            this.dgvLocations.TabIndex = 0;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 455);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(891, 60);
            this.pnlActions.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(589, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 50);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Ok";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(742, 5);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(145, 50);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Anuleaza";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgcLocation
            // 
            this.dgcLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcLocation.HeaderText = "Location";
            this.dgcLocation.Name = "dgcLocation";
            this.dgcLocation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 515);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.pnlActions);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsForm";
            this.Text = "CloudSave";
            this.tcMain.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.tpAccounts.ResumeLayout(false);
            this.tpLocations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocations)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.TabPage tpAccounts;
        private System.Windows.Forms.TabPage tpLocations;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvLocations;
        private System.Windows.Forms.CheckBox chkStartOnStartup;
        private System.Windows.Forms.FlowLayoutPanel flwServices;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLocation;
    }
}

