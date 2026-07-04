using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace AutoAnki.Core.Config
{
    internal sealed class Configuration
    {
        private static readonly string ConfigPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

        private static readonly Lazy<Configuration> _instance =
            new Lazy<Configuration>(() => LoadInternal());

        public static Configuration Instance => _instance.Value;

        // -----------------------------
        // Your config options
        // -----------------------------
        [Description("This is the name of the picture field in your Anki note template.")]
        public string AnkiPictureFieldName { get; set; } = "Picture";
        [Description("This is the name of the Audio field in your Anki note template.")]
        public string AnkiAudioFieldName { get; set; } = "SentenceAudio";
        public string AnkiConnectIP { get; set; } = "127.0.0.1";
        public string AnkiConnectPort { get; set; } = "8765";
        [Description("Value between 1-100. Default 70. Higher quality = Larger file size.")]
        public int RecordingQuality { get; set; } = 70;
        [Description("Value between 1-100. Default to 5. Higher value = Significantly higher file size.")]
        public int RecordingFramerate { get; set; } = 5;
        [Description("When a new anki card is detected, many last x seconds of video should be shown in the player? Default 120")]
        public int VideoConcatDurationSecs { get; set; } = 120;
        [Description("Override the width of the outputted image. Default 640. Set to 0 to ignore this field")]
        public int ImageOutputWidthOverride { get; set; } = 640;
        [Description("Override the height of the outputted image. Default 360. Set to 0 to ignore this field")]
        public int ImageOutputHeightOverride { get; set; } = 360;
        [Description("When a new card is detected, bring this app to the front")]
        public bool BringToFrontOnNewCard { get; set; } = true;
        [Description("When sending a selection to Anki, send it as a compressed animated apng file")]
        public bool OutputAnimatedAPNG { get; set; } = false;
        [Description("[Directory]Anki Media Directory Path")]
        public string AnkiMediaFolderPath { get; set; } = "C:\\Users\\<user>\\AppData\\Roaming\\Anki2\\User 1\\collection.media";
        [Description("[hidden]")]
        public int PlayerVolume { get; set; } = 100;
        [Description("[hidden]")]
        public int PlayerGain { get; set; } = 0;
        [Description("[hidden]")]
        public int OutputVolume { get; set; } = 100;
        [Description("[hidden]")]
        public int OutputGain { get; set; } = 0;

        // -----------------------------
        // Internal loader
        // -----------------------------
        private static Configuration LoadInternal()
        {
            Configuration cfg;

            if (!File.Exists(ConfigPath))
            {
                // First run → create file
                cfg = new Configuration();
                cfg.Save();
                return cfg;
            }

            string json = File.ReadAllText(ConfigPath);

            cfg = JsonSerializer.Deserialize<Configuration>(json)
                  ?? new Configuration();

            // Auto-save to ensure new fields are written
            cfg.Save();

            return cfg;
        }

        // -----------------------------
        // Save config
        // -----------------------------
        public void Save()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(ConfigPath, json);
        }
    }
}
