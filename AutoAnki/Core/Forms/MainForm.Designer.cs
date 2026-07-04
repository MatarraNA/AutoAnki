namespace AutoAnki
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            AutoAnkiForm = new ReaLTaiizor.Forms.SpaceForm();
            videoPanel = new Panel();
            settingsBtn = new ReaLTaiizor.Controls.SpaceButton();
            crownGroupBox2 = new ReaLTaiizor.Controls.CrownGroupBox();
            captureInfoTextBox = new ReaLTaiizor.Controls.CrownTextBox();
            refreshHwindBtn = new ReaLTaiizor.Controls.SpaceButton();
            hwindComboBox = new ReaLTaiizor.Controls.CrownComboBox();
            crownGroupBox1 = new ReaLTaiizor.Controls.CrownGroupBox();
            ankiInfoTextBox = new ReaLTaiizor.Controls.CrownTextBox();
            stateLabel = new ReaLTaiizor.Controls.SpaceLabel();
            spaceMaximize1 = new ReaLTaiizor.Controls.SpaceMaximize();
            spaceMinimize1 = new ReaLTaiizor.Controls.SpaceMinimize();
            spaceClose1 = new ReaLTaiizor.Controls.SpaceClose();
            playbackPanel = new Panel();
            sendToAnkiBtn = new ReaLTaiizor.Controls.SpaceButton();
            skipAnkiCardBtn = new ReaLTaiizor.Controls.SpaceButton();
            groupBox2 = new GroupBox();
            outputVolLabl = new ReaLTaiizor.Controls.SpaceLabel();
            outputVolumeBar = new ReaLTaiizor.Controls.DungeonTrackBar();
            outputGainBar = new ReaLTaiizor.Controls.DungeonTrackBar();
            outputGainLabl = new ReaLTaiizor.Controls.SpaceLabel();
            timelineTrackBar = new ReaLTaiizor.Controls.DungeonTrackBar();
            groupBox1 = new GroupBox();
            volLabel = new ReaLTaiizor.Controls.SpaceLabel();
            playerVolumeBar = new ReaLTaiizor.Controls.DungeonTrackBar();
            gainTrackBar = new ReaLTaiizor.Controls.DungeonTrackBar();
            playerGainLabel = new ReaLTaiizor.Controls.SpaceLabel();
            endLabl = new ReaLTaiizor.Controls.SpaceLabel();
            startTimeLabl = new ReaLTaiizor.Controls.SpaceLabel();
            endTimeTxtBox = new ReaLTaiizor.Controls.CrownTextBox();
            startTimeTxtBox = new ReaLTaiizor.Controls.CrownTextBox();
            playBtn = new ReaLTaiizor.Controls.SpaceButton();
            timelineLabel = new ReaLTaiizor.Controls.SpaceLabel();
            uiUpdateTimer = new System.Windows.Forms.Timer(components);
            AutoAnkiForm.SuspendLayout();
            crownGroupBox2.SuspendLayout();
            crownGroupBox1.SuspendLayout();
            playbackPanel.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // AutoAnkiForm
            // 
            AutoAnkiForm.BackColor = Color.FromArgb(42, 42, 42);
            AutoAnkiForm.BorderStyle = FormBorderStyle.None;
            AutoAnkiForm.Controls.Add(videoPanel);
            AutoAnkiForm.Controls.Add(settingsBtn);
            AutoAnkiForm.Controls.Add(crownGroupBox2);
            AutoAnkiForm.Controls.Add(crownGroupBox1);
            AutoAnkiForm.Controls.Add(stateLabel);
            AutoAnkiForm.Controls.Add(spaceMaximize1);
            AutoAnkiForm.Controls.Add(spaceMinimize1);
            AutoAnkiForm.Controls.Add(spaceClose1);
            AutoAnkiForm.Controls.Add(playbackPanel);
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
            AutoAnkiForm.Size = new Size(1280, 720);
            AutoAnkiForm.SmartBounds = true;
            AutoAnkiForm.StartPosition = FormStartPosition.CenterScreen;
            AutoAnkiForm.TabIndex = 0;
            AutoAnkiForm.Text = " AutoAnki";
            AutoAnkiForm.TransparencyKey = Color.Purple;
            AutoAnkiForm.Transparent = false;
            // 
            // videoPanel
            // 
            videoPanel.Location = new Point(280, 55);
            videoPanel.Name = "videoPanel";
            videoPanel.Size = new Size(988, 566);
            videoPanel.TabIndex = 11;
            // 
            // settingsBtn
            // 
            settingsBtn.Customization = "Kioq/zIyMv8yMjL/Kioq/y8vL/8nJyf//v7+/yMjI/8qKir/";
            settingsBtn.Font = new Font("Verdana", 10F);
            settingsBtn.Image = null;
            settingsBtn.Location = new Point(1154, 28);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.NoRounding = false;
            settingsBtn.Size = new Size(114, 27);
            settingsBtn.TabIndex = 2;
            settingsBtn.Text = "⚙ Settings ⚙";
            settingsBtn.TextAlignment = HorizontalAlignment.Center;
            settingsBtn.Transparent = false;
            settingsBtn.Click += settingsBtn_Click;
            // 
            // crownGroupBox2
            // 
            crownGroupBox2.BorderColor = Color.FromArgb(51, 51, 51);
            crownGroupBox2.Controls.Add(captureInfoTextBox);
            crownGroupBox2.Controls.Add(refreshHwindBtn);
            crownGroupBox2.Controls.Add(hwindComboBox);
            crownGroupBox2.Location = new Point(8, 36);
            crownGroupBox2.Name = "crownGroupBox2";
            crownGroupBox2.Size = new Size(262, 234);
            crownGroupBox2.TabIndex = 8;
            crownGroupBox2.TabStop = false;
            crownGroupBox2.Text = "Capture Info";
            // 
            // captureInfoTextBox
            // 
            captureInfoTextBox.BackColor = Color.FromArgb(69, 73, 74);
            captureInfoTextBox.BorderStyle = BorderStyle.FixedSingle;
            captureInfoTextBox.Font = new Font("Consolas", 10F);
            captureInfoTextBox.ForeColor = Color.FromArgb(220, 220, 220);
            captureInfoTextBox.Location = new Point(6, 46);
            captureInfoTextBox.Multiline = true;
            captureInfoTextBox.Name = "captureInfoTextBox";
            captureInfoTextBox.ReadOnly = true;
            captureInfoTextBox.Size = new Size(250, 182);
            captureInfoTextBox.TabIndex = 1;
            captureInfoTextBox.WordWrap = false;
            // 
            // refreshHwindBtn
            // 
            refreshHwindBtn.Customization = "Kioq/zIyMv8yMjL/Kioq/y8vL/8nJyf//v7+/yMjI/8qKir/";
            refreshHwindBtn.Font = new Font("Verdana", 12F);
            refreshHwindBtn.Image = null;
            refreshHwindBtn.Location = new Point(235, 19);
            refreshHwindBtn.Name = "refreshHwindBtn";
            refreshHwindBtn.NoRounding = false;
            refreshHwindBtn.Size = new Size(21, 21);
            refreshHwindBtn.TabIndex = 1;
            refreshHwindBtn.Text = "⟳";
            refreshHwindBtn.TextAlignment = HorizontalAlignment.Center;
            refreshHwindBtn.Transparent = false;
            refreshHwindBtn.Click += refreshHwindBtn_Click;
            // 
            // hwindComboBox
            // 
            hwindComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            hwindComboBox.FormattingEnabled = true;
            hwindComboBox.Location = new Point(6, 19);
            hwindComboBox.Name = "hwindComboBox";
            hwindComboBox.Size = new Size(223, 21);
            hwindComboBox.TabIndex = 0;
            hwindComboBox.SelectedIndexChanged += hwindComboBox_SelectedIndexChanged;
            // 
            // crownGroupBox1
            // 
            crownGroupBox1.BorderColor = Color.FromArgb(51, 51, 51);
            crownGroupBox1.Controls.Add(ankiInfoTextBox);
            crownGroupBox1.Location = new Point(8, 276);
            crownGroupBox1.Name = "crownGroupBox1";
            crownGroupBox1.Size = new Size(262, 430);
            crownGroupBox1.TabIndex = 7;
            crownGroupBox1.TabStop = false;
            crownGroupBox1.Text = "Anki Info";
            // 
            // ankiInfoTextBox
            // 
            ankiInfoTextBox.BackColor = Color.FromArgb(69, 73, 74);
            ankiInfoTextBox.BorderStyle = BorderStyle.FixedSingle;
            ankiInfoTextBox.Font = new Font("Consolas", 10F);
            ankiInfoTextBox.ForeColor = Color.FromArgb(220, 220, 220);
            ankiInfoTextBox.Location = new Point(6, 19);
            ankiInfoTextBox.Multiline = true;
            ankiInfoTextBox.Name = "ankiInfoTextBox";
            ankiInfoTextBox.ReadOnly = true;
            ankiInfoTextBox.Size = new Size(250, 405);
            ankiInfoTextBox.TabIndex = 0;
            ankiInfoTextBox.WordWrap = false;
            // 
            // stateLabel
            // 
            stateLabel.Customization = "/v7+/yoqKv8=";
            stateLabel.Font = new Font("Verdana", 12F, FontStyle.Bold);
            stateLabel.Image = null;
            stateLabel.Location = new Point(280, 36);
            stateLabel.Name = "stateLabel";
            stateLabel.NoRounding = false;
            stateLabel.Size = new Size(720, 19);
            stateLabel.TabIndex = 6;
            stateLabel.Text = "no state";
            stateLabel.TextAlignment = HorizontalAlignment.Left;
            stateLabel.Transparent = false;
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
            spaceMaximize1.Location = new Point(1230, 3);
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
            spaceMinimize1.Location = new Point(1206, 3);
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
            spaceClose1.Location = new Point(1254, 3);
            spaceClose1.Name = "spaceClose1";
            spaceClose1.NoRounding = false;
            spaceClose1.Size = new Size(23, 21);
            spaceClose1.TabIndex = 2;
            spaceClose1.Text = "x";
            spaceClose1.Transparent = false;
            // 
            // playbackPanel
            // 
            playbackPanel.Controls.Add(sendToAnkiBtn);
            playbackPanel.Controls.Add(skipAnkiCardBtn);
            playbackPanel.Controls.Add(groupBox2);
            playbackPanel.Controls.Add(timelineTrackBar);
            playbackPanel.Controls.Add(groupBox1);
            playbackPanel.Controls.Add(endLabl);
            playbackPanel.Controls.Add(startTimeLabl);
            playbackPanel.Controls.Add(endTimeTxtBox);
            playbackPanel.Controls.Add(startTimeTxtBox);
            playbackPanel.Controls.Add(playBtn);
            playbackPanel.Controls.Add(timelineLabel);
            playbackPanel.Location = new Point(270, 627);
            playbackPanel.Name = "playbackPanel";
            playbackPanel.Size = new Size(1010, 93);
            playbackPanel.TabIndex = 27;
            // 
            // sendToAnkiBtn
            // 
            sendToAnkiBtn.Customization = "Kioq/zIyMv8yMjL/Kioq/y8vL/8nJyf//v7+/yMjI/8qKir/";
            sendToAnkiBtn.Font = new Font("Verdana", 8F);
            sendToAnkiBtn.Image = null;
            sendToAnkiBtn.Location = new Point(713, 27);
            sendToAnkiBtn.Name = "sendToAnkiBtn";
            sendToAnkiBtn.NoRounding = false;
            sendToAnkiBtn.Size = new Size(120, 52);
            sendToAnkiBtn.TabIndex = 37;
            sendToAnkiBtn.Text = "Send To Anki";
            sendToAnkiBtn.TextAlignment = HorizontalAlignment.Center;
            sendToAnkiBtn.Transparent = false;
            // 
            // skipAnkiCardBtn
            // 
            skipAnkiCardBtn.Customization = "Kioq/zIyMv8yMjL/Kioq/y8vL/8nJyf//v7+/yMjI/8qKir/";
            skipAnkiCardBtn.Font = new Font("Verdana", 8F);
            skipAnkiCardBtn.Image = null;
            skipAnkiCardBtn.Location = new Point(587, 27);
            skipAnkiCardBtn.Name = "skipAnkiCardBtn";
            skipAnkiCardBtn.NoRounding = false;
            skipAnkiCardBtn.Size = new Size(120, 52);
            skipAnkiCardBtn.TabIndex = 36;
            skipAnkiCardBtn.Text = "Skip Current Card";
            skipAnkiCardBtn.TextAlignment = HorizontalAlignment.Center;
            skipAnkiCardBtn.Transparent = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(outputVolLabl);
            groupBox2.Controls.Add(outputVolumeBar);
            groupBox2.Controls.Add(outputGainBar);
            groupBox2.Controls.Add(outputGainLabl);
            groupBox2.FlatStyle = FlatStyle.Flat;
            groupBox2.ForeColor = Color.Gainsboro;
            groupBox2.Location = new Point(199, 23);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(182, 56);
            groupBox2.TabIndex = 35;
            groupBox2.TabStop = false;
            groupBox2.Text = "Output Settings";
            // 
            // outputVolLabl
            // 
            outputVolLabl.Customization = "/v7+/yoqKv8=";
            outputVolLabl.Font = new Font("Verdana", 7F);
            outputVolLabl.Image = null;
            outputVolLabl.Location = new Point(6, 13);
            outputVolLabl.Name = "outputVolLabl";
            outputVolLabl.NoRounding = false;
            outputVolLabl.Size = new Size(80, 18);
            outputVolLabl.TabIndex = 19;
            outputVolLabl.Text = "Vol. 100%";
            outputVolLabl.TextAlignment = HorizontalAlignment.Center;
            outputVolLabl.Transparent = false;
            // 
            // outputVolumeBar
            // 
            outputVolumeBar.BorderColor = Color.FromArgb(200, 200, 200);
            outputVolumeBar.DrawValueString = false;
            outputVolumeBar.EmptyBackColor = Color.FromArgb(221, 221, 221);
            outputVolumeBar.FillBackColor = Color.FromArgb(217, 99, 50);
            outputVolumeBar.JumpToMouse = false;
            outputVolumeBar.Location = new Point(6, 28);
            outputVolumeBar.Maximum = 100;
            outputVolumeBar.Minimum = 1;
            outputVolumeBar.MinimumSize = new Size(47, 22);
            outputVolumeBar.Name = "outputVolumeBar";
            outputVolumeBar.Size = new Size(80, 22);
            outputVolumeBar.TabIndex = 20;
            outputVolumeBar.Text = "outputVolumeBar";
            outputVolumeBar.ThumbBackColor = Color.FromArgb(244, 244, 244);
            outputVolumeBar.ThumbBorderColor = Color.FromArgb(180, 180, 180);
            outputVolumeBar.Value = 1;
            outputVolumeBar.ValueDivison = ReaLTaiizor.Controls.DungeonTrackBar.ValueDivisor.By1;
            outputVolumeBar.ValueToSet = 1F;
            // 
            // outputGainBar
            // 
            outputGainBar.BorderColor = Color.FromArgb(200, 200, 200);
            outputGainBar.DrawValueString = false;
            outputGainBar.EmptyBackColor = Color.FromArgb(221, 221, 221);
            outputGainBar.FillBackColor = Color.FromArgb(217, 99, 50);
            outputGainBar.JumpToMouse = false;
            outputGainBar.Location = new Point(92, 28);
            outputGainBar.Maximum = 40;
            outputGainBar.Minimum = 0;
            outputGainBar.MinimumSize = new Size(47, 22);
            outputGainBar.Name = "outputGainBar";
            outputGainBar.Size = new Size(80, 22);
            outputGainBar.TabIndex = 24;
            outputGainBar.Text = "outputGainBar";
            outputGainBar.ThumbBackColor = Color.FromArgb(244, 244, 244);
            outputGainBar.ThumbBorderColor = Color.FromArgb(180, 180, 180);
            outputGainBar.Value = 10;
            outputGainBar.ValueDivison = ReaLTaiizor.Controls.DungeonTrackBar.ValueDivisor.By1;
            outputGainBar.ValueToSet = 10F;
            // 
            // outputGainLabl
            // 
            outputGainLabl.Customization = "/v7+/yoqKv8=";
            outputGainLabl.Font = new Font("Verdana", 7F);
            outputGainLabl.Image = null;
            outputGainLabl.Location = new Point(92, 13);
            outputGainLabl.Name = "outputGainLabl";
            outputGainLabl.NoRounding = false;
            outputGainLabl.Size = new Size(80, 18);
            outputGainLabl.TabIndex = 23;
            outputGainLabl.Text = "Gain +1";
            outputGainLabl.TextAlignment = HorizontalAlignment.Center;
            outputGainLabl.Transparent = false;
            // 
            // timelineTrackBar
            // 
            timelineTrackBar.BorderColor = Color.FromArgb(200, 200, 200);
            timelineTrackBar.DrawValueString = false;
            timelineTrackBar.EmptyBackColor = Color.FromArgb(221, 221, 221);
            timelineTrackBar.FillBackColor = Color.FromArgb(217, 99, 50);
            timelineTrackBar.JumpToMouse = true;
            timelineTrackBar.Location = new Point(11, 2);
            timelineTrackBar.Maximum = 10000;
            timelineTrackBar.Minimum = 0;
            timelineTrackBar.MinimumSize = new Size(47, 22);
            timelineTrackBar.Name = "timelineTrackBar";
            timelineTrackBar.Size = new Size(988, 22);
            timelineTrackBar.TabIndex = 27;
            timelineTrackBar.Text = "timelineTrackBar";
            timelineTrackBar.ThumbBackColor = Color.FromArgb(244, 244, 244);
            timelineTrackBar.ThumbBorderColor = Color.FromArgb(180, 180, 180);
            timelineTrackBar.Value = 5;
            timelineTrackBar.ValueDivison = ReaLTaiizor.Controls.DungeonTrackBar.ValueDivisor.By1;
            timelineTrackBar.ValueToSet = 5F;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(volLabel);
            groupBox1.Controls.Add(playerVolumeBar);
            groupBox1.Controls.Add(gainTrackBar);
            groupBox1.Controls.Add(playerGainLabel);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.ForeColor = Color.Gainsboro;
            groupBox1.Location = new Point(11, 23);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(182, 56);
            groupBox1.TabIndex = 34;
            groupBox1.TabStop = false;
            groupBox1.Text = "Playback Settings";
            // 
            // volLabel
            // 
            volLabel.Customization = "/v7+/yoqKv8=";
            volLabel.Font = new Font("Verdana", 7F);
            volLabel.Image = null;
            volLabel.Location = new Point(6, 13);
            volLabel.Name = "volLabel";
            volLabel.NoRounding = false;
            volLabel.Size = new Size(80, 18);
            volLabel.TabIndex = 19;
            volLabel.Text = "Vol. 100%";
            volLabel.TextAlignment = HorizontalAlignment.Center;
            volLabel.Transparent = false;
            // 
            // playerVolumeBar
            // 
            playerVolumeBar.BorderColor = Color.FromArgb(200, 200, 200);
            playerVolumeBar.DrawValueString = false;
            playerVolumeBar.EmptyBackColor = Color.FromArgb(221, 221, 221);
            playerVolumeBar.FillBackColor = Color.FromArgb(217, 99, 50);
            playerVolumeBar.JumpToMouse = false;
            playerVolumeBar.Location = new Point(6, 28);
            playerVolumeBar.Maximum = 100;
            playerVolumeBar.Minimum = 1;
            playerVolumeBar.MinimumSize = new Size(47, 22);
            playerVolumeBar.Name = "playerVolumeBar";
            playerVolumeBar.Size = new Size(80, 22);
            playerVolumeBar.TabIndex = 20;
            playerVolumeBar.Text = "volSlider";
            playerVolumeBar.ThumbBackColor = Color.FromArgb(244, 244, 244);
            playerVolumeBar.ThumbBorderColor = Color.FromArgb(180, 180, 180);
            playerVolumeBar.Value = 1;
            playerVolumeBar.ValueDivison = ReaLTaiizor.Controls.DungeonTrackBar.ValueDivisor.By1;
            playerVolumeBar.ValueToSet = 1F;
            // 
            // gainTrackBar
            // 
            gainTrackBar.BorderColor = Color.FromArgb(200, 200, 200);
            gainTrackBar.DrawValueString = false;
            gainTrackBar.EmptyBackColor = Color.FromArgb(221, 221, 221);
            gainTrackBar.FillBackColor = Color.FromArgb(217, 99, 50);
            gainTrackBar.JumpToMouse = false;
            gainTrackBar.Location = new Point(92, 28);
            gainTrackBar.Maximum = 40;
            gainTrackBar.Minimum = 0;
            gainTrackBar.MinimumSize = new Size(47, 22);
            gainTrackBar.Name = "gainTrackBar";
            gainTrackBar.Size = new Size(80, 22);
            gainTrackBar.TabIndex = 24;
            gainTrackBar.Text = "gainTrackBar";
            gainTrackBar.ThumbBackColor = Color.FromArgb(244, 244, 244);
            gainTrackBar.ThumbBorderColor = Color.FromArgb(180, 180, 180);
            gainTrackBar.Value = 10;
            gainTrackBar.ValueDivison = ReaLTaiizor.Controls.DungeonTrackBar.ValueDivisor.By1;
            gainTrackBar.ValueToSet = 10F;
            // 
            // playerGainLabel
            // 
            playerGainLabel.Customization = "/v7+/yoqKv8=";
            playerGainLabel.Font = new Font("Verdana", 7F);
            playerGainLabel.Image = null;
            playerGainLabel.Location = new Point(92, 13);
            playerGainLabel.Name = "playerGainLabel";
            playerGainLabel.NoRounding = false;
            playerGainLabel.Size = new Size(80, 18);
            playerGainLabel.TabIndex = 23;
            playerGainLabel.Text = "Gain +1";
            playerGainLabel.TextAlignment = HorizontalAlignment.Center;
            playerGainLabel.Transparent = false;
            // 
            // endLabl
            // 
            endLabl.Customization = "/v7+/yoqKv8=";
            endLabl.Font = new Font("Verdana", 8F);
            endLabl.Image = null;
            endLabl.Location = new Point(848, 59);
            endLabl.Name = "endLabl";
            endLabl.NoRounding = false;
            endLabl.Size = new Size(45, 20);
            endLabl.TabIndex = 33;
            endLabl.Text = "End:";
            endLabl.TextAlignment = HorizontalAlignment.Left;
            endLabl.Transparent = false;
            // 
            // startTimeLabl
            // 
            startTimeLabl.Customization = "/v7+/yoqKv8=";
            startTimeLabl.Font = new Font("Verdana", 8F);
            startTimeLabl.Image = null;
            startTimeLabl.Location = new Point(848, 27);
            startTimeLabl.Name = "startTimeLabl";
            startTimeLabl.NoRounding = false;
            startTimeLabl.Size = new Size(45, 20);
            startTimeLabl.TabIndex = 32;
            startTimeLabl.Text = "Start:";
            startTimeLabl.TextAlignment = HorizontalAlignment.Left;
            startTimeLabl.Transparent = false;
            // 
            // endTimeTxtBox
            // 
            endTimeTxtBox.BackColor = Color.FromArgb(69, 73, 74);
            endTimeTxtBox.BorderStyle = BorderStyle.FixedSingle;
            endTimeTxtBox.ForeColor = Color.FromArgb(220, 220, 220);
            endTimeTxtBox.Location = new Point(899, 59);
            endTimeTxtBox.Name = "endTimeTxtBox";
            endTimeTxtBox.ReadOnly = true;
            endTimeTxtBox.Size = new Size(100, 20);
            endTimeTxtBox.TabIndex = 31;
            // 
            // startTimeTxtBox
            // 
            startTimeTxtBox.BackColor = Color.FromArgb(69, 73, 74);
            startTimeTxtBox.BorderStyle = BorderStyle.FixedSingle;
            startTimeTxtBox.ForeColor = Color.FromArgb(220, 220, 220);
            startTimeTxtBox.Location = new Point(899, 27);
            startTimeTxtBox.Name = "startTimeTxtBox";
            startTimeTxtBox.ReadOnly = true;
            startTimeTxtBox.Size = new Size(100, 20);
            startTimeTxtBox.TabIndex = 30;
            // 
            // playBtn
            // 
            playBtn.Customization = "Kioq/zIyMv8yMjL/Kioq/y8vL/8nJyf//v7+/yMjI/8qKir/";
            playBtn.Font = new Font("Segoe UI Symbol", 16F);
            playBtn.Image = null;
            playBtn.Location = new Point(485, 27);
            playBtn.Name = "playBtn";
            playBtn.NoRounding = false;
            playBtn.Size = new Size(40, 40);
            playBtn.TabIndex = 29;
            playBtn.Text = "▶";
            playBtn.TextAlignment = HorizontalAlignment.Center;
            playBtn.Transparent = false;
            // 
            // timelineLabel
            // 
            timelineLabel.Customization = "/v7+/yoqKv8=";
            timelineLabel.Font = new Font("Verdana", 8F);
            timelineLabel.Image = null;
            timelineLabel.Location = new Point(455, 68);
            timelineLabel.Name = "timelineLabel";
            timelineLabel.NoRounding = false;
            timelineLabel.Size = new Size(98, 22);
            timelineLabel.TabIndex = 28;
            timelineLabel.Text = "0:00 / 10:50";
            timelineLabel.TextAlignment = HorizontalAlignment.Center;
            timelineLabel.Transparent = false;
            // 
            // uiUpdateTimer
            // 
            uiUpdateTimer.Enabled = true;
            uiUpdateTimer.Interval = 50;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(AutoAnkiForm);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(261, 61);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AutoAnki";
            TransparencyKey = Color.Purple;
            AutoAnkiForm.ResumeLayout(false);
            crownGroupBox2.ResumeLayout(false);
            crownGroupBox2.PerformLayout();
            crownGroupBox1.ResumeLayout(false);
            crownGroupBox1.PerformLayout();
            playbackPanel.ResumeLayout(false);
            playbackPanel.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.SpaceForm AutoAnkiForm;
        private System.Windows.Forms.Timer uiUpdateTimer;
        private ReaLTaiizor.Controls.SpaceMinimize spaceMinimize1;
        private ReaLTaiizor.Controls.SpaceClose spaceClose1;
        private ReaLTaiizor.Controls.SpaceMaximize spaceMaximize1;
        private ReaLTaiizor.Controls.SpaceLabel stateLabel;
        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox2;
        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox1;
        private ReaLTaiizor.Controls.CrownComboBox hwindComboBox;
        private ReaLTaiizor.Controls.SpaceButton refreshHwindBtn;
        private ReaLTaiizor.Controls.SpaceButton settingsBtn;
        private ReaLTaiizor.Controls.CrownTextBox ankiInfoTextBox;
        private ReaLTaiizor.Controls.CrownTextBox captureInfoTextBox;
        private Panel videoPanel;
        private Panel playbackPanel;
        private GroupBox groupBox2;
        private ReaLTaiizor.Controls.SpaceLabel outputVolLabl;
        private ReaLTaiizor.Controls.DungeonTrackBar outputVolumeBar;
        private ReaLTaiizor.Controls.DungeonTrackBar outputGainBar;
        private ReaLTaiizor.Controls.SpaceLabel outputGainLabl;
        private ReaLTaiizor.Controls.DungeonTrackBar timelineTrackBar;
        private GroupBox groupBox1;
        private ReaLTaiizor.Controls.SpaceLabel volLabel;
        private ReaLTaiizor.Controls.DungeonTrackBar playerVolumeBar;
        private ReaLTaiizor.Controls.DungeonTrackBar gainTrackBar;
        private ReaLTaiizor.Controls.SpaceLabel playerGainLabel;
        private ReaLTaiizor.Controls.SpaceLabel endLabl;
        private ReaLTaiizor.Controls.SpaceLabel startTimeLabl;
        private ReaLTaiizor.Controls.CrownTextBox endTimeTxtBox;
        private ReaLTaiizor.Controls.CrownTextBox startTimeTxtBox;
        private ReaLTaiizor.Controls.SpaceButton playBtn;
        private ReaLTaiizor.Controls.SpaceLabel timelineLabel;
        private ReaLTaiizor.Controls.SpaceButton sendToAnkiBtn;
        private ReaLTaiizor.Controls.SpaceButton skipAnkiCardBtn;
    }
}