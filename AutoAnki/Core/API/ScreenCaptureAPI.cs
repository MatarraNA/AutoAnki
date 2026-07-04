using AutoAnki.Core.Config;
using Microsoft.WindowsAPICodePack.Shell;
using ScreenRecorderLib;

namespace AutoAnki.Core.API
{
    public class ScreenCaptureAPI
    {
        private static ScreenCaptureAPI? _instance;
        public static ScreenCaptureAPI Instance => _instance ??= new ScreenCaptureAPI();

        public static string OUTPUT_DIRECTORY => Path.Combine(Directory.GetCurrentDirectory(), "ScreenOut");

        private Recorder? _recorder;
        private DateTime _recordingStartTime;
        private bool _isRecording;
        private WindowRecordingSource? _currentSource;

        private IntPtr _currentHwnd;

        public void RecordWindow(IntPtr hwnd)
        {
            if (_isRecording)
                throw new InvalidOperationException("A recording is already in progress.");

            _currentHwnd = hwnd;

            Directory.CreateDirectory(OUTPUT_DIRECTORY);

            // recording limiter
            var files = Directory.GetFiles(OUTPUT_DIRECTORY, "segment_*.mp4")
                                 .OrderBy(f => File.GetCreationTimeUtc(f))
                                 .ToList();

            while (files.Count > 5) // keep max 5 total
            {
                File.Delete(files[0]);
                files.RemoveAt(0);
            }

            // ------------------------------------------------------------
            // CREATE NEW SEGMENT
            // ------------------------------------------------------------
            string filePath = Path.Combine(OUTPUT_DIRECTORY, $"segment_{DateTime.Now.Ticks}.mp4");
            StartNewSegment(filePath);

            _recordingStartTime = DateTime.Now;
        }


        // ------------------------------------------------------------
        // INTERNAL: START A NEW SEGMENT (FILE PATH)
        // ------------------------------------------------------------
        private void StartNewSegment(string filePath)
        {
            _currentSource = new WindowRecordingSource(_currentHwnd)
            {
                IsBorderRequired = false
            };

            var options = new RecorderOptions
            {
                SourceOptions = new SourceOptions
                {
                    RecordingSources = new List<RecordingSourceBase> { _currentSource }
                },

                OutputOptions = new OutputOptions
                {
                    RecorderMode = RecorderMode.Video,
                },

                AudioOptions = new AudioOptions
                {
                    IsAudioEnabled = true,
                    IsOutputDeviceEnabled = true,
                    IsInputDeviceEnabled = false
                },

                VideoEncoderOptions = new VideoEncoderOptions
                {
                    Quality = Configuration.Instance.RecordingQuality,
                    Framerate = Configuration.Instance.RecordingFramerate,
                    IsFixedFramerate = true,
                    IsFragmentedMp4Enabled = true,
                    IsMp4FastStartEnabled = true,
                    Encoder = new H264VideoEncoder
                    {
                        BitrateMode = H264BitrateControlMode.Quality,
                        EncoderProfile = H264Profile.Baseline
                    },
                },

                MouseOptions = new MouseOptions
                {
                    IsMousePointerEnabled = true
                }
            };

            _recorder = Recorder.CreateRecorder(options);

            // NOW start recording
            _recorder.Record(filePath);
            _isRecording = true;
        }


        // ------------------------------------------------------------
        // PUBLIC: STOP RECORDING COMPLETELY
        // ------------------------------------------------------------
        public void StopRecording()
        {
            if (!_isRecording || _recorder == null)
                return;

            _recorder.Stop();
            _recorder.Dispose();
            _recorder = null;
            _isRecording = false;
        }

        /// <summary>
        /// Cleans up mp4s from previous sessions that don't have valid metadata / werent saved correctly
        /// </summary>
        public static void Cleanup()
        {
            if (!Directory.Exists(OUTPUT_DIRECTORY)) return;
            var files = Directory.GetFiles(OUTPUT_DIRECTORY, "segment_*.mp4").ToList();
            foreach( var file in files )
            {
                if (HasWindowsDuration(file)) continue;

                // no valid metadata, delete it
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
        }

        public static bool HasWindowsDuration(string file)
        {
            try
            {
                using (var shell = ShellObject.FromParsingName(file))
                {
                    var durationProp = shell.Properties.System.Media.Duration.Value;

                    if (durationProp.HasValue)
                    {
                        long ticks = (long)durationProp.Value;
                        double seconds = ticks / 10_000_000.0;

                        return seconds > 0.1;
                    }
                }
            }
            catch { }

            return false;
        }

        public bool IsRecording => _isRecording;
        public TimeSpan RecordingUptime => IsRecording ? DateTime.Now - _recordingStartTime : TimeSpan.Zero;
    }
}