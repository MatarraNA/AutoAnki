using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;

namespace AutoAnki.Core.API
{
    public class FfmpegAPI
    {
        private static FfmpegAPI? _instance = null;
        public static FfmpegAPI Instance => _instance ??= new FfmpegAPI();

        public static readonly string CONCAT_FILE = "current_concat.mp4";

        private readonly string _ffmpegPath = "ffmpeg.exe";

        // ------------------------------------------------------------
        // GET DURATION USING WINDOWS SHELL (Explorer metadata)
        // ------------------------------------------------------------
        private double GetDurationSeconds(string file)
        {
            try
            {
                using (var shell = ShellObject.FromParsingName(file))
                {
                    var durationProp = shell.Properties.System.Media.Duration.Value;

                    if (durationProp.HasValue)
                    {
                        long ticks = (long)durationProp.Value;
                        return ticks / 10_000_000.0;
                    }
                }
            }
            catch { }

            return 0;
        }

        /// <summary>
        /// Ensure an mp4 is fully ready and file is unlocked
        /// </summary>
        /// <param name="path"></param>
        private void WaitForFileReady(string path)
        {
            long lastSize = -1;

            for (int i = 0; i < 40; i++) // up to ~2 seconds
            {
                try
                {
                    var info = new FileInfo(path);
                    long size = info.Length;

                    // If size is stable AND duration is valid → file is ready
                    if (size > 0 && size == lastSize)
                    {
                        double dur = GetDurationSeconds(path);
                        if (dur > 0.1)
                            return;
                    }

                    lastSize = size;
                }
                catch
                {
                    // file still locked
                }

                System.Threading.Thread.Sleep(50);
            }
        }


        // ------------------------------------------------------------
        // SELECT ONLY LAST X SECONDS OF SEGMENTS (newest → oldest)
        // ------------------------------------------------------------
        private string[] SelectLastXSeconds(string inputDirectory, double lastSeconds)
        {
            var files = Directory.GetFiles(inputDirectory, "segment_*.mp4")
                                 .OrderByDescending(f => File.GetLastWriteTime(f))
                                 .ToList();

            files = files.Where(f => new FileInfo(f).Length > 1024).ToList();

            if (files.Count == 0)
                throw new Exception("No valid segments found.");

            double accumulated = 0;
            var selected = new System.Collections.Generic.List<string>();

            foreach (var file in files)
            {
                double dur = GetDurationSeconds(file);
                selected.Add(file);
                accumulated += dur;

                if (accumulated >= lastSeconds)
                    break;
            }

            // reverse → chronological order
            selected.Reverse();

            return selected.ToArray();
        }

        // ------------------------------------------------------------
        // CONCAT ONLY THE SELECTED SEGMENTS (synchronous)
        // ------------------------------------------------------------
        private string ConcatSegments(string outputFile, string[] segments)
        {
            if (File.Exists(outputFile)) File.Delete(outputFile);

            string listPath = Path.Combine(Path.GetDirectoryName(outputFile)!, "concat_list.txt");
            var utf8NoBom = new UTF8Encoding(false);

            using (var writer = new StreamWriter(listPath, false, utf8NoBom))
            {
                foreach (var file in segments)
                {
                    string full = Path.GetFullPath(file)
                                      .Replace("\\", "/")
                                      .Replace("'", "'\\''");

                    writer.WriteLine($"file '{full}'");
                }
            }

            // Wait for last segment metadata to flush
            var lastSeg = segments.LastOrDefault();
            if(File.Exists(lastSeg)) WaitForFileReady(lastSeg);

            string args =
                $"-err_detect ignore_err -f concat -safe 0 -i \"{listPath}\" " +
                "-c copy " +
                "-movflags +faststart " +
                "-avoid_negative_ts make_zero " +
                $"\"{outputFile}\"";


            RunFFmpeg(args);

            File.Delete(listPath);
            return outputFile;
        }

        // ------------------------------------------------------------
        // TRIM FINAL OUTPUT TO EXACT X SECONDS (from the END)
        // ------------------------------------------------------------
        private void TrimFinalOutput(string inputFile, string outputFile, double lastSeconds)
        {
            if (File.Exists(outputFile)) File.Delete(outputFile);

            double totalDuration = GetDurationSeconds(inputFile);
            double start = Math.Max(0, totalDuration - lastSeconds);

            string args =
                $"-ss {start} -t {lastSeconds} -i \"{inputFile}\" " +
                "-c copy " +
                "-movflags +faststart " +
                "-avoid_negative_ts make_zero " +
                $"\"{outputFile}\"";

            RunFFmpeg(args);
        }

        // ------------------------------------------------------------
        // PUBLIC API: CONCAT LAST X SECONDS (synchronous)
        // ------------------------------------------------------------
        public string ConcatLastXSeconds(string outputFile, string inputDirectory, double lastSeconds)
        {
            if (!Directory.Exists(inputDirectory)) return "";

            var selected = SelectLastXSeconds(inputDirectory, lastSeconds);

            string tempConcat = Path.Combine(inputDirectory, "temp_concat.mp4");
            ConcatSegments(tempConcat, selected);

            TrimFinalOutput(tempConcat, outputFile, lastSeconds);

            WaitForFileReady(outputFile);

            File.Delete(tempConcat);
            return outputFile;
        }

        // ------------------------------------------------------------
        // EXTRACT AUDIO (unchanged)
        // ------------------------------------------------------------
        public void ExtractAudioRegionToOpus(
            string inputMp4,
            TimeSpan startOffset,
            TimeSpan endOffset,
            string outputOpusPath,
            int volumePercent,
            int gainDb
        )
        {
            if (!File.Exists(inputMp4))
                throw new FileNotFoundException("Input video not found.", inputMp4);

            if (endOffset <= startOffset)
                throw new ArgumentException("End offset must be greater than start offset.");

            double durationSeconds = (endOffset - startOffset).TotalSeconds;

            if (durationSeconds <= 0.05)
                throw new ArgumentException("Duration too small to extract audio.");

            if (File.Exists(outputOpusPath))
                File.Delete(outputOpusPath);

            double linearVolume = Math.Clamp(volumePercent, 0, 100) / 100.0;
            string audioFilter = $"volume={linearVolume},volume={gainDb}dB";

            string args =
                $"-ss {startOffset} -t {durationSeconds} -i \"{inputMp4}\" " +
                "-vn " +
                "-c:a libopus " +
                "-b:a 32k " +
                "-ac 1 " +
                "-ar 48000 " +
                $"-filter:a \"{audioFilter}\" " +
                $"\"{outputOpusPath}\" -y";

            RunFFmpeg(args);
        }

        // ------------------------------------------------------------
        // EXTRACT VIDEO REGION AS APNG (unchanged)
        // ------------------------------------------------------------
        public void ExtractVideoRegionToApng(
            string inputMp4,
            TimeSpan startOffset,
            TimeSpan endOffset,
            string outputApngPath,
            int width = 0,
            int height = 0
        )
        {
            if (!File.Exists(inputMp4))
                throw new FileNotFoundException("Input video not found.", inputMp4);

            if (endOffset <= startOffset)
                throw new ArgumentException("End offset must be greater than start offset.");

            double durationSeconds = (endOffset - startOffset).TotalSeconds;

            if (durationSeconds <= 0.05)
                throw new ArgumentException("Duration too small to extract video.");

            if (File.Exists(outputApngPath))
                File.Delete(outputApngPath);

            int w = width > 0 ? width : -1;
            int h = height > 0 ? height : -1;

            string scaleFilter = $"scale={w}:{h}";

            string args =
                $"-ss {startOffset} -t {durationSeconds} -i \"{inputMp4}\" " +
                $"-vf \"fps=5,{scaleFilter}\" " +
                "-pix_fmt rgb8 " +
                "-compression_level 9 " +
                "-pred mixed " +
                "-plays 0 " +
                "-f apng " +
                $"\"{outputApngPath}\" -y";

            RunFFmpeg(args);
        }

        // ------------------------------------------------------------
        // EXTRACT FIRST FRAME AS WEBP (unchanged)
        // ------------------------------------------------------------
        public void ExtractFirstFrameToWebp(
            string inputMp4,
            TimeSpan startOffset,
            string outputWebpPath,
            int width = 0,
            int height = 0
        )
        {
            if (!File.Exists(inputMp4))
                throw new FileNotFoundException("Input video not found.", inputMp4);

            if (File.Exists(outputWebpPath))
                File.Delete(outputWebpPath);

            string scaleFilter = "";
            if (width > 0 || height > 0)
            {
                int w = width > 0 ? width : -1;
                int h = height > 0 ? height : -1;
                scaleFilter = $"-vf scale={w}:{h} ";
            }

            string args =
                $"-ss {startOffset} -i \"{inputMp4}\" " +
                "-vframes 1 " +
                "-an " +
                scaleFilter +
                "-lossless 0 " +
                "-compression_level 6 " +
                "-quality 80 " +
                $"\"{outputWebpPath}\" -y";

            RunFFmpeg(args);
        }

        // ------------------------------------------------------------
        // INTERNAL FFmpeg RUNNER (unchanged)
        // ------------------------------------------------------------
        private void RunFFmpeg(string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _ffmpegPath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                };

                using var proc = Process.Start(psi);
                string stderr = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (proc.ExitCode != 0)
                    throw new Exception($"FFmpeg failed ({proc.ExitCode}):\n{stderr}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: Error occurred when concating clips.\n\n{ex.Message}");
                Clipboard.SetText(ex.Message);
            }
        }
    }
}
