namespace Updater
{
    partial class UpdateForm
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
            UpdateFormSpace = new ReaLTaiizor.Forms.SpaceForm();
            SuspendLayout();
            // 
            // UpdateFormSpace
            // 
            UpdateFormSpace.BackColor = Color.FromArgb(42, 42, 42);
            UpdateFormSpace.BorderStyle = FormBorderStyle.None;
            UpdateFormSpace.Customization = "Kioq/yAgIP8qKir/Kioq/xwcHP/+/v7/Kysr/xkZGf8=";
            UpdateFormSpace.Dock = DockStyle.Fill;
            UpdateFormSpace.Font = new Font("Verdana", 8F);
            UpdateFormSpace.Image = null;
            UpdateFormSpace.Location = new Point(0, 0);
            UpdateFormSpace.MinimumSize = new Size(200, 25);
            UpdateFormSpace.Movable = true;
            UpdateFormSpace.Name = "UpdateFormSpace";
            UpdateFormSpace.NoRounding = false;
            UpdateFormSpace.Padding = new Padding(5, 25, 5, 5);
            UpdateFormSpace.Sizable = true;
            UpdateFormSpace.Size = new Size(300, 100);
            UpdateFormSpace.SmartBounds = true;
            UpdateFormSpace.StartPosition = FormStartPosition.WindowsDefaultLocation;
            UpdateFormSpace.TabIndex = 0;
            UpdateFormSpace.Text = "Update in progress...";
            UpdateFormSpace.TransparencyKey = Color.Purple;
            UpdateFormSpace.Transparent = false;
            // 
            // UpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 100);
            Controls.Add(UpdateFormSpace);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UpdateForm";
            Text = "UpdateForm";
            TransparencyKey = Color.Purple;
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.SpaceForm UpdateFormSpace;
    }
}
