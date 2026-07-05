using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private async void UpdateForm_Load(object sender, EventArgs e)
        {
            // handle zip extraction and relaunch logic
            await ExtractUpdateZipAsync(Program.UPDATE_ZIP_PATH, updateProgressBar);

            // now relaunch the main app
            try
            {
                Process.Start(Program.EXE_PATH);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to restart main application:\n" + ex.Message);
            }

            // Safely delete ZIP with retry
            const int maxZipDeleteRetries = 10;
            const int zipDeleteDelayMs = 150;
            for (int i = 0; i < maxZipDeleteRetries; i++)
            {
                try
                {
                    if (File.Exists(Program.UPDATE_ZIP_PATH))
                    {
                        File.Delete(Program.UPDATE_ZIP_PATH);
                    }
                    break; // success
                }
                catch (IOException)
                {
                    await Task.Delay(zipDeleteDelayMs);
                }
            }

            // after that, relaunch the exe and kill updater process
            Application.Exit();
        }

        public static async Task ExtractUpdateZipAsync(string zipPath, ProgressBar updateProgressBar)
        {
            string appDir = AppContext.BaseDirectory;

            string updaterName = Path.GetFileName(Process.GetCurrentProcess().MainModule!.FileName);

            string mainExePath = Program.EXE_PATH;
            string mainExeName = Path.GetFileNameWithoutExtension(mainExePath);

            // Kill running main app
            foreach (var proc in Process.GetProcessesByName(mainExeName))
            {
                try
                {
                    proc.Kill();
                    await proc.WaitForExitAsync();
                }
                catch { }
            }

            using ZipArchive archive = ZipFile.OpenRead(zipPath);

            var fileEntries = archive.Entries.Where(e => !string.IsNullOrEmpty(e.Name)).ToList();
            int totalFiles = fileEntries.Count;
            int processed = 0;

            updateProgressBar.Minimum = 0;
            updateProgressBar.Maximum = totalFiles;
            updateProgressBar.Value = 0;

            foreach (var entry in fileEntries)
            {
                if (entry.Name.Equals(updaterName, StringComparison.OrdinalIgnoreCase))
                {
                    processed++;
                    updateProgressBar.Value = processed;
                    continue;
                }

                string destinationPath = Path.Combine(appDir, entry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                // 🔥 Retry until file is unlocked
                const int maxRetries = 10;
                const int delayMs = 200;

                for (int i = 0; i < maxRetries; i++)
                {
                    try
                    {
                        using (var entryStream = entry.Open())
                        using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await entryStream.CopyToAsync(fileStream);
                        }

                        break; // success
                    }
                    catch (IOException)
                    {
                        // File still locked → wait and retry
                        await Task.Delay(delayMs);
                    }
                }

                processed++;
                updateProgressBar.Value = processed;

                await Task.Yield();
            }
        }
    }
}
