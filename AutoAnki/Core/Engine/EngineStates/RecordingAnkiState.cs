using AutoAnki.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.Engine.EngineStates
{
    public class RecordingAnkiState(AnkiStateMachine machine, MainForm ankiForm) : AnkiBaseState(machine, ankiForm)
    {
        private string _currentHandlerSelection = "";

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            // attempt capture if need
            AttemptCapture();

            // ensure current capture is still a valid window
            CheckCurrentCaptureValid();

            // check if update should continue running
            if( !CheckAnki() ) return;

            // finally update UI text
            UpdateUI();
        }

        private void CheckCurrentCaptureValid()
        {
            if (string.IsNullOrWhiteSpace(_currentHandlerSelection) || !ScreenCaptureAPI.Instance.IsRecording ) return;

            // handler exists, check it among open windwos
            bool exists = false;
            foreach( var window in WindowsAPI.GetOpenWindows() )
            {
                if (_currentHandlerSelection.Contains(window.Hwnd.ToString())) exists = true;
            }

            if (exists) return;

            // doesnt exist, stop recording if recording
            ScreenCaptureAPI.Instance.StopRecording();
        }

        public override void Exit()
        {
            base.Exit();

            if( ScreenCaptureAPI.Instance.IsRecording )
            {
                // ensure recording always stops on state change
                ScreenCaptureAPI.Instance.StopRecording();
            }

            // update UI on exit
            UpdateUI();
        }

        private bool CheckAnki()
        {
            if( AnkiAPI.GetVersion() < 1 ) // not connected anymore
            {
                Machine.ChangeState(new CheckAnkiState(Machine, AnkiForm));
                return false;
            }

            // check if anki card came in
            if ( AnkiAPI.GetLatestNoteId() > 0 )
            {
                Machine.ChangeState(new SendToAnkiState(Machine, AnkiForm));
                return false;
            }

            return true;
        }

        private void UpdateUI()
        {
            StringBuilder builder = new StringBuilder();

            bool isRecording = ScreenCaptureAPI.Instance.IsRecording;

            string recordingUnicode =
                isRecording
                    ? new[] { "🔴", "🟠", "🟡", "🟠" }[(StateUptimeMS / 500) % 4]
                    : "⚪";

            builder.AppendLine($"Capturing State: {isRecording} {recordingUnicode}");
            builder.AppendLine($"Recording Uptime: {ScreenCaptureAPI.Instance.RecordingUptime:hh\\:mm\\:ss}");

            AnkiForm.CaptureInfoText = builder.ToString();

            if( isRecording )
            {
                AnkiForm.StatusText = $"Currently Capturing {_currentHandlerSelection} - Awaiting new Anki Card...";
            }
            else
            {
                AnkiForm.StatusText = $"Not Currently Capturing... Select a Window or refresh the list and try again.";
            }
        }


        private void AttemptCapture()
        {
            string selectedText = AnkiForm.HwindComboSelectionText;
            if (ScreenCaptureAPI.Instance.IsRecording)
            {
                // already recording, check if handler changed
                if (AnkiForm.HwindComboSelectionText == _currentHandlerSelection) return;

                // check if its the no handler state
                if( AnkiForm.HwindComboSelectionText.Where(x=>char.IsNumber(x)).Count() < 1 ) // handler would contain atleast 1 digit
                {
                    ScreenCaptureAPI.Instance.StopRecording();
                    return;
                }

                // handler changed, stop and start recording with new handler
                var handlers = WindowsAPI.GetOpenWindows();
                var hwind = handlers.Where(x => selectedText.Contains(x.Hwnd.ToString())).FirstOrDefault();
                if (hwind == null) return;

                // hwind exists, start rec
                ScreenCaptureAPI.Instance.StopRecording();
                _currentHandlerSelection = AnkiForm.HwindComboSelectionText;
                ScreenCaptureAPI.Instance.RecordWindow(hwind.Hwnd);
                return;
            }

            // makes it here, it never started recording, startit now
            var handlers2 = WindowsAPI.GetOpenWindows();
            var hwind2 = handlers2.Where(x => selectedText.Contains(x.Hwnd.ToString())).FirstOrDefault();
            if (hwind2 == null) return;

            // hwind exists, start rec
            _currentHandlerSelection = AnkiForm.HwindComboSelectionText;
            ScreenCaptureAPI.Instance.RecordWindow(hwind2.Hwnd);
        }
    }
}
