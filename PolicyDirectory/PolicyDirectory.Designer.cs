namespace PolicyDirectory {
    partial class PolicyDirectory {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.log = new System.Windows.Forms.TextBox();
            this.cloudPortTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.conLabel = new System.Windows.Forms.Label();
            this.cloudIPTextBox = new System.Windows.Forms.TextBox();
            this.conToCloudButton = new System.Windows.Forms.Button();
            this.selectedClientBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.clientInfoBox = new System.Windows.Forms.GroupBox();
            this.clientSpeedBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addrLabel = new System.Windows.Forms.Label();
            this.canReqCheckBox = new System.Windows.Forms.CheckBox();
            this.clientInfoBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.SystemColors.Window;
            this.log.Location = new System.Drawing.Point(12, 134);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(332, 106);
            this.log.TabIndex = 14;
            // 
            // cloudPortTextBox
            // 
            this.cloudPortTextBox.Location = new System.Drawing.Point(12, 67);
            this.cloudPortTextBox.Name = "cloudPortTextBox";
            this.cloudPortTextBox.Size = new System.Drawing.Size(117, 20);
            this.cloudPortTextBox.TabIndex = 29;
            this.cloudPortTextBox.Text = "13000";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(12, 51);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(117, 13);
            this.portLabel.TabIndex = 28;
            this.portLabel.Text = "Port chmury sterowania";
            // 
            // conLabel
            // 
            this.conLabel.AutoSize = true;
            this.conLabel.Location = new System.Drawing.Point(12, 9);
            this.conLabel.Name = "conLabel";
            this.conLabel.Size = new System.Drawing.Size(108, 13);
            this.conLabel.TabIndex = 27;
            this.conLabel.Text = "IP chmury sterowania";
            // 
            // cloudIPTextBox
            // 
            this.cloudIPTextBox.Location = new System.Drawing.Point(12, 28);
            this.cloudIPTextBox.Name = "cloudIPTextBox";
            this.cloudIPTextBox.Size = new System.Drawing.Size(117, 20);
            this.cloudIPTextBox.TabIndex = 26;
            this.cloudIPTextBox.Text = "127.0.0.1";
            // 
            // conToCloudButton
            // 
            this.conToCloudButton.Location = new System.Drawing.Point(12, 93);
            this.conToCloudButton.Name = "conToCloudButton";
            this.conToCloudButton.Size = new System.Drawing.Size(117, 35);
            this.conToCloudButton.TabIndex = 25;
            this.conToCloudButton.Text = "Połącz";
            this.conToCloudButton.UseVisualStyleBackColor = true;
            this.conToCloudButton.Click += new System.EventHandler(this.conToCloudButton_Click);
            // 
            // selectedClientBox
            // 
            this.selectedClientBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectedClientBox.FormattingEnabled = true;
            this.selectedClientBox.Location = new System.Drawing.Point(220, 6);
            this.selectedClientBox.Name = "selectedClientBox";
            this.selectedClientBox.Size = new System.Drawing.Size(118, 21);
            this.selectedClientBox.TabIndex = 32;
            this.selectedClientBox.SelectedIndexChanged += new System.EventHandler(this.selectedClientBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Klienty w sieci";
            // 
            // clientInfoBox
            // 
            this.clientInfoBox.Controls.Add(this.clientSpeedBox);
            this.clientInfoBox.Controls.Add(this.label3);
            this.clientInfoBox.Controls.Add(this.label2);
            this.clientInfoBox.Controls.Add(this.label1);
            this.clientInfoBox.Controls.Add(this.addrLabel);
            this.clientInfoBox.Controls.Add(this.canReqCheckBox);
            this.clientInfoBox.Location = new System.Drawing.Point(144, 28);
            this.clientInfoBox.Name = "clientInfoBox";
            this.clientInfoBox.Size = new System.Drawing.Size(200, 100);
            this.clientInfoBox.TabIndex = 33;
            this.clientInfoBox.TabStop = false;
            this.clientInfoBox.Text = "Informacje o Kliencie";
            // 
            // clientSpeedBox
            // 
            this.clientSpeedBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientSpeedBox.FormattingEnabled = true;
            this.clientSpeedBox.Location = new System.Drawing.Point(125, 69);
            this.clientSpeedBox.Name = "clientSpeedBox";
            this.clientSpeedBox.Size = new System.Drawing.Size(69, 21);
            this.clientSpeedBox.TabIndex = 34;
            this.clientSpeedBox.SelectedIndexChanged += new System.EventHandler(this.clientSpeedBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Maksymalna prędkość\r\nw Mbit/s";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Czy może żądać połączenia?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Adres klienta";
            // 
            // addrLabel
            // 
            this.addrLabel.AutoSize = true;
            this.addrLabel.BackColor = System.Drawing.SystemColors.Info;
            this.addrLabel.ForeColor = System.Drawing.Color.Red;
            this.addrLabel.Location = new System.Drawing.Point(145, 23);
            this.addrLabel.Name = "addrLabel";
            this.addrLabel.Size = new System.Drawing.Size(49, 13);
            this.addrLabel.TabIndex = 2;
            this.addrLabel.Text = "__.__.__";
            this.addrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // canReqCheckBox
            // 
            this.canReqCheckBox.AutoSize = true;
            this.canReqCheckBox.Location = new System.Drawing.Point(179, 45);
            this.canReqCheckBox.Name = "canReqCheckBox";
            this.canReqCheckBox.Size = new System.Drawing.Size(15, 14);
            this.canReqCheckBox.TabIndex = 1;
            this.canReqCheckBox.UseVisualStyleBackColor = true;
            this.canReqCheckBox.CheckedChanged += new System.EventHandler(this.canReqCheckBox_CheckedChanged);
            // 
            // PolicyDirectory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 244);
            this.Controls.Add(this.clientInfoBox);
            this.Controls.Add(this.selectedClientBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cloudPortTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.conLabel);
            this.Controls.Add(this.cloudIPTextBox);
            this.Controls.Add(this.conToCloudButton);
            this.Controls.Add(this.log);
            this.Name = "PolicyDirectory";
            this.Text = "Policy & Directory";
            this.clientInfoBox.ResumeLayout(false);
            this.clientInfoBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.TextBox cloudPortTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label conLabel;
        private System.Windows.Forms.TextBox cloudIPTextBox;
        private System.Windows.Forms.Button conToCloudButton;
        private System.Windows.Forms.ComboBox selectedClientBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox clientInfoBox;
        private System.Windows.Forms.ComboBox clientSpeedBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label addrLabel;
        private System.Windows.Forms.CheckBox canReqCheckBox;
    }
}

