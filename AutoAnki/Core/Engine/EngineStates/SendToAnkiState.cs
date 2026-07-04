using AutoAnki.Core.API;
using AutoAnki.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.Engine.EngineStates
{
    public class SendToAnkiState : AnkiBaseState
    {
        private long _currentCardId = 0;
        private NoteInfo? _currentCardNoteInfo;

        public SendToAnkiState(AnkiStateMachine machine, MainForm ankiForm) : base(machine, ankiForm) { }

        public override void Enter()
        {
            base.Enter();

            GatherCardInfo();
            EnterUpdateUI();
            HandleClipsConcat();
        }

        public override void Exit()
        {
            base.Exit();

            AnkiForm.SetPlaybackControlsActive(false);
            AnkiForm.SetMediaPlayerClipAndPlay("");
            AnkiForm.AnkiInfoText = "";
        }

        private void HandleClipsConcat()
        {
            FfmpegAPI.Instance.ConcatLastXSeconds(FfmpegAPI.CONCAT_FILE, ScreenCaptureAPI.OUTPUT_DIRECTORY, Configuration.Instance.VideoConcatDurationSecs);

            if (File.Exists(FfmpegAPI.CONCAT_FILE))
            {
                // update our media player
                AnkiForm.SetMediaPlayerClipAndPlay(FfmpegAPI.CONCAT_FILE);
            }
        }

        public void OnSendToAnkiBtn()
        {
            // ensure timestamps are valid
            bool successStart = TimeSpan.TryParseExact(AnkiForm.StartOffsetText, @"m\:ss\.fff", null, out var startTs);
            bool successEnd = TimeSpan.TryParseExact(AnkiForm.EndOffsetText, @"m\:ss\.fff", null, out var endTs);
        
            if( !successEnd || !successStart || endTs < startTs || startTs <= TimeSpan.Zero || endTs <= TimeSpan.Zero )
            {
                MessageBox.Show("Error: Start / End times are invalid. Please set valid Start / End offsets.");
                return;
            }
            
            // ensure media path valid
            if( !Directory.Exists(Configuration.Instance.AnkiMediaFolderPath) )
            {
                MessageBox.Show("Error: Media folder path is invalid. Please set it in the settings.");
                return;
            }

            // times are valid, send to anki now
            var outputAudioFileName = $"{_currentCardId}-{DateTime.Now.Ticks}.opus";
            var outputImageFileName = $"{_currentCardId}-{DateTime.Now.Ticks}.{(Configuration.Instance.OutputAnimatedAPNG ? "apng" : "webp")}";
            var outputAudioPath = Path.Combine(Configuration.Instance.AnkiMediaFolderPath, outputAudioFileName);
            var outputImagePath = Path.Combine(Configuration.Instance.AnkiMediaFolderPath, outputImageFileName);
            FfmpegAPI.Instance.ExtractAudioRegionToOpus(FfmpegAPI.CONCAT_FILE, startTs, endTs, outputAudioPath, Configuration.Instance.OutputVolume, Configuration.Instance.OutputGain);

            if (Configuration.Instance.OutputAnimatedAPNG)
                FfmpegAPI.Instance.ExtractVideoRegionToApng(FfmpegAPI.CONCAT_FILE, startTs, endTs, outputImagePath, Configuration.Instance.ImageOutputWidthOverride, Configuration.Instance.ImageOutputHeightOverride);
            else
                FfmpegAPI.Instance.ExtractFirstFrameToWebp(FfmpegAPI.CONCAT_FILE, startTs, outputImagePath, Configuration.Instance.ImageOutputWidthOverride, Configuration.Instance.ImageOutputHeightOverride);

            // update card to include info
            AnkiAPI.UpdateNoteFields(_currentCardId, 
                new Dictionary<string, string> 
                {
                    { Configuration.Instance.AnkiAudioFieldName, $"[sound:{outputAudioFileName}]" },
                    { Configuration.Instance.AnkiPictureFieldName, $"<img src=\"{outputImageFileName}\">" },
                });

            // finally go back to recording state
            Machine.ChangeState(new RecordingAnkiState(Machine, AnkiForm));
        }

        public void OnSkipBtn()
        {
            // update card to include info
            AnkiAPI.UpdateNoteFields(_currentCardId,
                new Dictionary<string, string>
                {
                    { Configuration.Instance.AnkiPictureFieldName, $"skipped" },
                });

            // finally go back to recording state
            Machine.ChangeState(new RecordingAnkiState(Machine, AnkiForm));
        }

        private void GatherCardInfo()
        {
            // gather latest note
            _currentCardId = AnkiAPI.GetLatestNoteId() ?? 0;
            if (_currentCardId == 0)
            {
                Machine.ChangeState(new CheckAnkiState(Machine, AnkiForm));
                return;
            }

            // gather card info
            var info = AnkiAPI.GetNoteById(_currentCardId);
            if( info == null )
            {
                Machine.ChangeState(new CheckAnkiState(Machine, AnkiForm));
                return;
            }

            _currentCardNoteInfo = info;
        }

        private void EnterUpdateUI()
        {
            AnkiForm.StatusText = "New card detected...";
            AnkiForm.ForceToFront();

            StringBuilder ankiInfo = new StringBuilder();
            ankiInfo.AppendLine($"Card Id: {_currentCardId}");
            ankiInfo.AppendLine($"");
            if( _currentCardNoteInfo == null )
            {
                // something went really wrong
                Machine.ChangeState(new CheckAnkiState(Machine, AnkiForm));
                return;
            }
            foreach( var field in _currentCardNoteInfo.Fields) 
            {
                if (field.Value.Value.Length < 1) continue;
                string value = field.Value.Value;
                if (value.Length > 12) value = value.Substring(0, 12) + "...";
                ankiInfo.AppendLine($"{field.Key}: {value}");
            }
            AnkiForm.AnkiInfoText = ankiInfo.ToString();

            AnkiForm.SetPlaybackControlsActive(true);
        }

        public override void Update()
        {
            base.Update();

            // poll anki API to see if its still connected
            if (AnkiAPI.GetVersion() < 1)
            {
                Machine.ChangeState(new CheckAnkiState(Machine, AnkiForm));
                return;
            }
        }
    }
}
