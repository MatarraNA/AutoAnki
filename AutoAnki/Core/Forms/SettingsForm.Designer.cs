namespace AutoAnki
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
            components = new System.ComponentModel.Container();
            AutoAnkiForm = new ReaLTaiizor.Forms.SpaceForm();
            crownSaveBtn = new ReaLTaiizor.Controls.CrownButton();
            cancelBtn = new ReaLTaiizor.Controls.CrownButton();
            settingsPanel = new ReaLTaiizor.Docking.Crown.CrownDockPanel();
            spaceMaximize1 = new ReaLTaiizor.Controls.SpaceMaximize();
            spaceMinimize1 = new ReaLTaiizor.Controls.SpaceMinimize();
            spaceClose1 = new ReaLTaiizor.Controls.SpaceClose();
            uiUpdateTimer = new System.Windows.Forms.Timer(components);
            AutoAnkiForm.SuspendLayout();
            SuspendLayout();
            // 
            // AutoAnkiForm
            // 
            AutoAnkiForm.BackColor = Color.FromArgb(42, 42, 42);
            AutoAnkiForm.BorderStyle = FormBorderStyle.None;
            AutoAnkiForm.Controls.Add(crownSaveBtn);
            AutoAnkiForm.Controls.Add(cancelBtn);
            AutoAnkiForm.Controls.Add(settingsPanel);
            AutoAnkiForm.Controls.Add(spaceMaximize1);
            AutoAnkiForm.Controls.Add(spaceMinimize1);
            AutoAnkiForm.Controls.Add(spaceClose1);
            AutoAnkiForm.Customization = "Kioq/yAgIP8qKir/Kioq/xwcHP/+/v7/Kysr/xkZGf8=";
            AutoAnkiForm.Dock = DockStyle.Fill;
            AutoAnkiForm.Font = new Font("Verdana", 8F);
            AutoAnkiForm.Image = null;
            AutoAnkiForm.Location = new Point(0, 0);
            AutoAnkiForm.MinimumSize = new Size(200, 25);
            AutoAnkiForm.Movable = true;
            AutoAnkiForm.Name = "AutoAnkiForm";
            AutoAnkiForm.NoRounding = false;
            AutoAnkiForm.Padding = new Padding(5, 25, 5, 5);
            AutoAnkiForm.Sizable = false;
            AutoAnkiForm.Size = new Size(370, 480);
            AutoAnkiForm.SmartBounds = true;
            AutoAnkiForm.StartPosition = FormStartPosition.CenterScreen;
            AutoAnkiForm.TabIndex = 0;
            AutoAnkiForm.Text = " Settings";
            AutoAnkiForm.TransparencyKey = Color.Purple;
            AutoAnkiForm.Transparent = false;
            // 
            // crownSaveBtn
            // 
            crownSaveBtn.Location = new Point(12, 449);
            crownSaveBtn.Name = "crownSaveBtn";
            crownSaveBtn.Padding = new Padding(5);
            crownSaveBtn.Size = new Size(166, 23);
            crownSaveBtn.TabIndex = 7;
            crownSaveBtn.Text = "Save";
            crownSaveBtn.Click += crownSaveBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(192, 449);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Padding = new Padding(5);
            cancelBtn.Size = new Size(166, 23);
            cancelBtn.TabIndex = 6;
            cancelBtn.Text = "Cancel";
            cancelBtn.Click += cancelBtn_Click;
            // 
            // settingsPanel
            // 
            settingsPanel.AutoScroll = true;
            settingsPanel.BackColor = Color.FromArgb(60, 63, 65);
            settingsPanel.Location = new Point(12, 37);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(346, 406);
            settingsPanel.TabIndex = 5;
            // 
            // spaceMaximize1
            // 
            spaceMaximize1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            spaceMaximize1.Customization = "G4qM/3LEtP8yMjL/Kioq/yPJzP8bioz//v7+/yMjI/8qKir/";
            spaceMaximize1.DefaultAnchor = true;
            spaceMaximize1.DefaultLocation = true;
            spaceMaximize1.Enabled = false;
            spaceMaximize1.Font = new Font("Verdana", 8F);
            spaceMaximize1.Image = null;
            spaceMaximize1.Location = new Point(320, 3);
            spaceMaximize1.Name = "spaceMaximize1";
            spaceMaximize1.NoRounding = false;
            spaceMaximize1.Size = new Size(23, 21);
            spaceMaximize1.TabIndex = 4;
            spaceMaximize1.Text = "+";
            spaceMaximize1.Transparent = false;
            spaceMaximize1.WindowState = FormWindowState.Normal;
            // 
            // spaceMinimize1
            // 
            spaceMinimize1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            spaceMinimize1.Customization = "G4qM/3LEtP8yMjL/Kioq/yPJzP8bioz//v7+/yMjI/8qKir/";
            spaceMinimize1.DefaultAnchor = true;
            spaceMinimize1.DefaultLocation = true;
            spaceMinimize1.Font = new Font("Verdana", 8F);
            spaceMinimize1.Image = null;
            spaceMinimize1.Location = new Point(296, 3);
            spaceMinimize1.Name = "spaceMinimize1";
            spaceMinimize1.NoRounding = false;
            spaceMinimize1.Size = new Size(23, 21);
            spaceMinimize1.TabIndex = 3;
            spaceMinimize1.Text = "_";
            spaceMinimize1.Transparent = false;
            spaceMinimize1.WindowState = FormWindowState.Normal;
            // 
            // spaceClose1
            // 
            spaceClose1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            spaceClose1.Customization = "DQ/S/xhh8/8yMjL/Kioq/x5/9/8ND9L//v7+/yMjI/8qKir/";
            spaceClose1.DefaultAnchor = true;
            spaceClose1.DefaultLocation = true;
            spaceClose1.Font = new Font("Verdana", 8F);
            spaceClose1.Image = null;
            spaceClose1.Location = new Point(344, 3);
            spaceClose1.Name = "spaceClose1";
            spaceClose1.NoRounding = false;
            spaceClose1.Size = new Size(23, 21);
            spaceClose1.TabIndex = 2;
            spaceClose1.Text = "x";
            spaceClose1.Transparent = false;
            // 
            // uiUpdateTimer
            // 
            uiUpdateTimer.Enabled = true;
            uiUpdateTimer.Interval = 50;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 480);
            Controls.Add(AutoAnkiForm);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimumSize = new Size(261, 61);
            Name = "SettingsForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "themeForm1";
            TransparencyKey = Color.Purple;
            AutoAnkiForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.SpaceForm AutoAnkiForm;
        private System.Windows.Forms.Timer uiUpdateTimer;
        private ReaLTaiizor.Controls.SpaceMinimize spaceMinimize1;
        private ReaLTaiizor.Controls.SpaceClose spaceClose1;
        private ReaLTaiizor.Controls.SpaceMaximize spaceMaximize1;
        private ReaLTaiizor.Docking.Crown.CrownDockPanel settingsPanel;
        private ReaLTaiizor.Controls.CrownButton crownSaveBtn;
        private ReaLTaiizor.Controls.CrownButton cancelBtn;
    }
}