using AutoAnki.Core.API;
using AutoAnki.Core.Config;
using AutoAnki.Core.Engine;
using AutoAnki.Core.Engine.EngineStates;
using AutoAnki.Core.Forms.Controls;
using Mpv.NET.Player;
using System.Diagnostics;
using System.Numerics;
using System.Security.Policy;

namespace AutoAnki
{
    public partial class MainForm : Form
    {
        protected AnkiStateMachine? _ankiStateMachine;

        const int TimelineResolution = 10000;

        private Marker _startMarker = new Marker(Color.LimeGreen);
        private Marker _endMarker = new Marker(Color.Red);
        private bool _userDragging = false;
        private MpvPlayer _mpvPlayer;
        private long _lastScrubTime = 0;

        public MainForm()
        {
            InitializeComponent();

            ScreenCaptureAPI.Cleanup();
            UIInit();
            StateMachineInit();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await CheckForUpdate();            
        }

        private async Task CheckForUpdate()
        {
            // check latest app version on launch
            if (await GithubAPI.IsUpdateAvailableByTimestampAsync())
            {
                var latest = await GithubAPI.GetLatestReleaseAsync();
                var resp = MessageBox.Show($"New version of AnkiInsert is available, update to latest?\n\n{latest.Body}", "Update Available", MessageBoxButtons.YesNo);
                if (resp == DialogResult.Yes)
                {
                    // said yes, download update
                    await GithubAPI.DownloadLatestAssetAsync();

                    // after download, launch updater exe
                    string mainExePath = Process.GetCurrentProcess().MainModule!.FileName;
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "Updater.exe",
                        Arguments = $"\"{GithubAPI.UPDATE_ZIP_PATH}\" \"{mainExePath}\"",
                        UseShellExecute = false
                    });

                    // terminate self while update occurs
                    Application.Exit();
                }
            }
        }


        private async void UIInit()
        {
            // initialize markers
            this.Controls.Add(_startMarker);
            this.Controls.Add(_endMarker);
            playbackPanel.Controls.Add(_startMarker);
            playbackPanel.Controls.Add(_endMarker);
            _startMarker.Visible = false;
            _startMarker.BringToFront();
            _endMarker.Visible = false;
            _endMarker.BringToFront();

            _mpvPlayer = new MpvPlayer(videoPanel.Handle)
            {
                KeepOpen = KeepOpen.Always,
                Loop = false,
                Volume = 50
            };
            _mpvPlayer.API.SetPropertyString("hr-seek", "yes");

            _mpvPlayer.PositionChanged += (s, e) =>
            {
                UI(() =>
                {
                    TimeSpan posSpan = TimeSpan.Zero;
                    TimeSpan durSpan = TimeSpan.Zero;
                    try
                    {
                        posSpan = _mpvPlayer.Position;
                        durSpan = _mpvPlayer.Duration;
                    }
                    catch { }

                    if (!_userDragging)
                    {
                        timelineTrackBar.Maximum = TimelineResolution;
                        double ratio = posSpan.TotalMilliseconds /
                            durSpan.TotalMilliseconds;
                        timelineTrackBar.Value = (int)(ratio * TimelineResolution);
                    }

                    string pos = $"{(int)posSpan.TotalMinutes}:{posSpan.Seconds:00}";
                    string dur = $"{(int)durSpan.TotalMinutes}:{durSpan.Seconds:00}";
                    timelineLabel.Text = $"{pos} / {dur}";

                    // stop at endTime if set
                    bool success = TimeSpan.TryParseExact(endTimeTxtBox.Text, @"m\:ss\.fff", null, out var endTs);
                    if (success)
                    {
                        if (posSpan.TotalSeconds >= endTs.TotalSeconds)
                            _mpvPlayer.Pause();
                    }
                });
            };

            timelineTrackBar.MouseDown += (s, e) =>
            {
                if (!_mpvPlayer.IsMediaLoaded) return;
                if (e.Button == MouseButtons.Right)
                {
                    // Convert mouse X → trackbar value
                    double ratio = (double)e.X / timelineTrackBar.Width;
                    ratio = Math.Max(0, Math.Min(1, ratio));

                    timelineTrackBar.Value = (int)(ratio * TimelineResolution);

                    // Begin drag just like left click
                    _userDragging = true;
                    _mpvPlayer.Pause();
                }
                else if (e.Button == MouseButtons.Left)
                {
                    _userDragging = true;
                    _mpvPlayer.Pause();
                }
            };

            timelineTrackBar.MouseUp += (s, e) =>
            {
                if (!_mpvPlayer.IsMediaLoaded) return;

                _userDragging = false;

                double ratio = (double)timelineTrackBar.Value / TimelineResolution;

                _mpvPlayer.Position = TimeSpan.FromMilliseconds(
                    ratio * _mpvPlayer.Duration.TotalMilliseconds
                );

                _mpvPlayer.Resume();

                // Compute thumb position
                int trackLeft = timelineTrackBar.Left + 6;
                int trackWidth = timelineTrackBar.Width - 12;
                int thumbX = trackLeft + (int)(ratio * trackWidth);

                int markerY = timelineTrackBar.Top + (timelineTrackBar.Height / 2) - (_startMarker.Height / 2);

                // Compute timestamp from ratio, not mpv
                double ms = ratio * _mpvPlayer.Duration.TotalMilliseconds;
                TimeSpan ts = TimeSpan.FromMilliseconds(ms);
                string textboxTxtPos = $"{(int)ts.TotalMinutes}:{ts.Seconds:00}.{ts.Milliseconds:000}";

                if (e.Button == MouseButtons.Left)
                {
                    _startMarker.Location = new Point(thumbX - (_startMarker.Width / 2), markerY);
                    _startMarker.Visible = true;
                    startTimeTxtBox.Text = textboxTxtPos;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    _endMarker.Location = new Point(thumbX - (_endMarker.Width / 2), markerY);
                    _endMarker.Visible = true;
                    endTimeTxtBox.Text = textboxTxtPos;
                }
            };

            timelineTrackBar.ValueChanged += () =>
            {
                if (_userDragging)
                {
                    // only if valid media is loaded
                    if (!_mpvPlayer.IsMediaLoaded) return;

                    long now = Environment.TickCount64;

                    // 100 ms cooldown
                    if (now - _lastScrubTime < 100)
                        return;

                    _lastScrubTime = now;

                    double ratio = (double)timelineTrackBar.Value / TimelineResolution;

                    _mpvPlayer.Position = TimeSpan.FromMilliseconds(
                        ratio * _mpvPlayer.Duration.TotalMilliseconds
                    );
                }
            };

            sendToAnkiBtn.Click += (s, e) => 
            {
                // only valid if correct state
                if( _ankiStateMachine?.CurrentBaseState is SendToAnkiState sendState )
                {
                    sendState.OnSendToAnkiBtn();
                }
            };
            skipAnkiCardBtn.Click += (s, e) =>
            {
                // only valid if correct state
                if (_ankiStateMachine?.CurrentBaseState is SendToAnkiState sendState)
                {
                    sendState.OnSkipBtn();
                }
            };

            _mpvPlayer.MediaPaused += (s, e) => 
            {
                UI(() => playBtn.Text = "▶"); // its currently paused, so show PLAY button
            };
            _mpvPlayer.MediaResumed += (s, e) =>
            {
                UI(()=>playBtn.Text = "❚❚"); // its currently resumed, so show PAUSE button
            };

            playerVolumeBar.ValueChanged += () => 
            {
                int volume = Configuration.Instance.PlayerVolume = Math.Clamp(playerVolumeBar.Value, 1, 100);
                Configuration.Instance.Save();
                volLabel.Text = $"Vol. {volume}%";
                _mpvPlayer.Volume = volume;
            };
            playerVolumeBar.Value = Configuration.Instance.PlayerVolume;

            gainTrackBar.ValueChanged += () =>
            {
                int mid = gainTrackBar.Maximum / 2;

                // Convert trackbar → real gain
                int realGain = gainTrackBar.Value - mid;

                // Store REAL gain in config
                Configuration.Instance.PlayerGain = realGain;
                Configuration.Instance.Save();

                // Apply to mpv
                _mpvPlayer.API.SetPropertyString("af", $"lavfi=[volume={realGain}dB]");

                playerGainLabel.Text = $"Gain {(realGain < 0 ? realGain.ToString() : "+" + realGain)}";
            };
            int playerGainMid = gainTrackBar.Maximum / 2;
            gainTrackBar.Value = Configuration.Instance.PlayerGain + playerGainMid;

            outputVolumeBar.ValueChanged += () =>
            {
                int volume = Configuration.Instance.OutputVolume = Math.Clamp(outputVolumeBar.Value, 1, 100);
                Configuration.Instance.Save();
                outputVolLabl.Text = $"Vol. {volume}%";
            };
            outputVolumeBar.Value = Configuration.Instance.OutputVolume;

            outputGainBar.ValueChanged += () =>
            {
                int mid = outputGainBar.Maximum / 2;

                // Convert trackbar → real gain
                int realGain = outputGainBar.Value - mid;

                // Store REAL gain in config
                Configuration.Instance.OutputGain = realGain;
                Configuration.Instance.Save();

                outputGainLabl.Text = $"Gain {(realGain < 0 ? realGain.ToString() : "+" + realGain)}";
            };
            int outputGainMid = outputGainBar.Maximum / 2;
            outputGainBar.Value = Configuration.Instance.OutputGain + outputGainMid;

            playBtn.Click += (s, e) =>
            {
                if (_mpvPlayer.IsPlaying)
                {
                    _mpvPlayer.Pause();
                    return;
                }

                // Start at A if defined
                bool success = TimeSpan.TryParseExact(startTimeTxtBox.Text, @"m\:ss\.fff", null, out var ts);
                if (success && ts < _mpvPlayer.Duration)
                    _mpvPlayer.Position = ts;

                _mpvPlayer.Resume();
            };

            SetPlaybackControlsActive(false);

            // hwind box population
            refreshHwindBtn_Click(refreshHwindBtn, EventArgs.Empty);
        }

        private void StateMachineInit()
        {
            _ankiStateMachine = new AnkiStateMachine();

            // Start background update loop
            _ankiStateMachine.Start();

            // initial state
            _ankiStateMachine.ChangeState(new CheckAnkiState(_ankiStateMachine, this));
        }

        #region UI Event Handlers
        private void refreshHwindBtn_Click(object sender, EventArgs e)
        {
            // populate combox box
            List<string> list = new List<string>()
            {
                "Select a Capture Window"
            };
            foreach (var window in WindowsAPI.GetOpenWindows())
            {
                list.Add($"<{window.Hwnd}> {window.ProcessName}");
            }
            hwindComboBox.DataSource = list;
        }

        private void hwindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _hwindSelectedText = hwindComboBox.Text;
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }
        #endregion

        #region UI Wrappers
        public string StatusText
        {
            get => UI(() => stateLabel.Text);
            set => UI(() => stateLabel.Text = value);
        }
        public string CaptureInfoText
        {
            get => UI(() => captureInfoTextBox.Text);
            set => UI(() => captureInfoTextBox.Text = value);
        }
        public string StartOffsetText
        {
            get => UI(() => startTimeTxtBox.Text);
            set => UI(() => startTimeTxtBox.Text = value);
        }
        public string EndOffsetText
        {
            get => UI(() => endTimeTxtBox.Text);
            set => UI(() => endTimeTxtBox.Text = value);
        }
        public string AnkiInfoText
        {
            get => UI(() => ankiInfoTextBox.Text);
            set => UI(() => ankiInfoTextBox.Text = value);
        }
        public void SetMediaPlayerClipAndPlay(string clip)
        {
            UI(() =>
            {
                try
                {
                    _mpvPlayer.API.Command("playlist-clear");

                    // Defer load by one UI tick
                    this.BeginInvoke(new Action(() =>
                    {
                        if (File.Exists(clip))
                        {
                            _mpvPlayer.API.Command("loadfile", clip);
                        }
                        else
                        {
                            _mpvPlayer.API.Command("loadfile", "null");
                        }
                    }));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("mpv load error: " + ex);
                }
            });
        }

        public void SetPlaybackControlsActive(bool active)
        {
            UI(() =>
            {
                timelineTrackBar.Enabled = active;
                timelineTrackBar.Value = 0;
                playBtn.Enabled = active;
                sendToAnkiBtn.Enabled = active;
                skipAnkiCardBtn.Enabled = active;

                if( !active )
                {
                    // hide the markers
                    _startMarker.Visible = false;
                    _endMarker.Visible = false;
                }

                // zero out labels
                startTimeTxtBox.Text = "";
                endTimeTxtBox.Text = "";
            });
        }

        public void ForceToFront()
        {
            UI(() =>
            {
                TopMost = true;
                Activate();
                BringToFront();
                TopMost = false;
            });
        }
        private string _hwindSelectedText = "";
        public string HwindComboSelectionText
        {
            get
            {
                return _hwindSelectedText;
            }
        }

        private void UI(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private T UI<T>(Func<T> func)
        {
            if (InvokeRequired)
                return (T)Invoke(func);
            else
                return func();
        }
        #endregion
    }
}
