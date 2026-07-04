using AutoAnki.Core.API;
using AutoAnki.Core.Config;
using AutoAnki.Core.Engine;
using AutoAnki.Core.Engine.EngineStates;
using Microsoft.VisualBasic.Devices;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Docking.Crown;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace AutoAnki
{
    public partial class SettingsForm : Form
    {
        private readonly ToolTip _tip = new ToolTip();

        public SettingsForm()
        {
            InitializeComponent();

            GenerateSettingsUI(Configuration.Instance, settingsPanel);
        }

        public void GenerateSettingsUI(object config, CrownDockPanel panel)
        {
            panel.Controls.Clear();

            int y = 10;

            var props = config.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                string name = prop.Name;
                Type type = prop.PropertyType;

                // ⭐ Read [Description]
                var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                string description = descAttr?.Description ?? name;

                if (description.Contains("[hidden]")) continue;

                // Label
                var lbl = new CrownLabel
                {
                    Text = name,
                    Left = 10,
                    Top = y + 4,
                    AutoSize = true
                };
                panel.Controls.Add(lbl);

                Control ctrl = null;

                // ⭐ Directory picker
                if (description.Contains("[Directory]", StringComparison.OrdinalIgnoreCase))
                {
                    var txt = new CrownTextBox
                    {
                        Name = "txt" + name,
                        Left = 180,
                        Top = y,
                        Width = 105
                    };

                    var btn = new CrownButton
                    {
                        Text = "...",
                        Left = txt.Left + txt.Width + 6,
                        Top = y,
                        Width = 30,
                        Height = txt.Height
                    };

                    btn.Click += (s, e) =>
                    {
                        using var dialog = new FolderBrowserDialog();
                        dialog.Description = description;

                        if (Directory.Exists(txt.Text))
                            dialog.SelectedPath = txt.Text;

                        if (dialog.ShowDialog() == DialogResult.OK)
                            txt.Text = dialog.SelectedPath;
                    };

                    panel.Controls.Add(txt);
                    panel.Controls.Add(btn);

                    _tip.SetToolTip(lbl, description);
                    _tip.SetToolTip(txt, description);
                    _tip.SetToolTip(btn, description);

                    y += 35;
                    continue;
                }

                // ⭐ Normal controls
                if (type == typeof(string))
                {
                    ctrl = new CrownTextBox
                    {
                        Name = "txt" + name,
                        Left = 180,
                        Top = y,
                        Width = 140
                    };
                }
                else if (type == typeof(bool))
                {
                    ctrl = new CrownCheckBox
                    {
                        Name = "chk" + name,
                        Left = 180,
                        Top = y,
                        Width = 140
                    };
                }
                else if (type == typeof(int))
                {
                    ctrl = new CrownNumeric
                    {
                        Name = "num" + name,
                        Left = 180,
                        Top = y,
                        Width = 140,
                        Minimum = 0,
                        Maximum = 999999,
                    };
                    ctrl.MouseWheel += (s,e) => { ((HandledMouseEventArgs)e!).Handled = true; };
                }

                if (ctrl != null)
                {
                    panel.Controls.Add(ctrl);

                    _tip.SetToolTip(lbl, description);
                    _tip.SetToolTip(ctrl, description);

                    y += 35;
                }
            }

            LoadConfigToUI(config, panel);
        }


        public void LoadConfigToUI(object config, CrownDockPanel panel)
        {
            foreach (var prop in config.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance))
            {
                string name = prop.Name;
                object value = prop.GetValue(config);

                // Find the generated control
                Control ctrl = panel.Controls
                    .Cast<Control>()
                    .FirstOrDefault(c => c.Name.EndsWith(name));

                if (ctrl == null)
                    continue;

                switch (ctrl)
                {
                    case CrownTextBox tb:
                        tb.Text = value?.ToString() ?? "";
                        break;

                    case CrownCheckBox cb:
                        cb.Checked = value is bool b && b;
                        break;

                    case CrownNumeric nud:
                        nud.Value = Convert.ToDecimal(value);
                        break;
                }
            }
        }

        public void SaveUIToConfig(object config, CrownDockPanel panel)
        {
            foreach (var prop in config.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance))
            {
                string name = prop.Name;

                // Find the generated control
                Control ctrl = panel.Controls
                    .Cast<Control>()
                    .FirstOrDefault(c => c.Name.EndsWith(name));

                if (ctrl == null)
                    continue;

                // Assign value based on control type
                if (ctrl is CrownTextBox tb)
                {
                    prop.SetValue(config, tb.Text);
                }
                else if (ctrl is CrownCheckBox cb)
                {
                    prop.SetValue(config, cb.Checked);
                }
                else if (ctrl is CrownNumeric nud)
                {
                    // Convert to the property type
                    if (prop.PropertyType == typeof(int))
                        prop.SetValue(config, (int)nud.Value);
                    else if (prop.PropertyType == typeof(double))
                        prop.SetValue(config, (double)nud.Value);
                    else if (prop.PropertyType == typeof(decimal))
                        prop.SetValue(config, nud.Value);
                }
            }
        }

        private void crownSaveBtn_Click(object sender, EventArgs e)
        {
            SaveUIToConfig(Configuration.Instance, settingsPanel);
            Configuration.Instance.Save();
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
