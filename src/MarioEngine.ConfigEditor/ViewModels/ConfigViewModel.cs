namespace MarioEngine.ConfigEditor.ViewModels
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MarioEngine.Core.Config;

    /// <summary>
    /// MVVM ViewModel for the game configuration editor.
    /// Exposes all GameConfig properties as bindable fields with change notification.
    /// Uses CommunityToolkit.Mvvm source generators for commands and observable properties.
    /// </summary>
    internal sealed partial class ConfigViewModel : ObservableObject
    {
        /// <summary>Config manager for loading and saving game settings.</summary>
        private readonly ConfigManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigViewModel"/> class.
        /// </summary>
        /// <param name="manager">The config manager to load/save settings.</param>
        public ConfigViewModel(ConfigManager manager)
        {
            _manager = manager;
        }

        /// <summary>Gets or sets the display width in pixels.</summary>
        [ObservableProperty]
        private int _width = 1920;

        /// <summary>Gets or sets the display height in pixels.</summary>
        [ObservableProperty]
        private int _height = 1080;

        /// <summary>Gets or sets a value indicating whether fullscreen mode is enabled.</summary>
        [ObservableProperty]
        private bool _fullscreen = true;

        /// <summary>Gets or sets a value indicating whether vertical sync is enabled.</summary>
        [ObservableProperty]
        private bool _vSync = true;

        /// <summary>Gets or sets the target FPS cap (0 = unlimited).</summary>
        [ObservableProperty]
        private int _fpsCap = 60;

        /// <summary>Gets or sets the MSAA sample count (0, 2, 4, 8).</summary>
        [ObservableProperty]
        private int _msaaSamples;

        /// <summary>Gets or sets the master volume percentage (0-100).</summary>
        [ObservableProperty]
        private int _masterVolumePercent = 80;

        /// <summary>Gets or sets the music volume percentage (0-100).</summary>
        [ObservableProperty]
        private int _musicVolumePercent = 70;

        /// <summary>Gets or sets the SFX volume percentage (0-100).</summary>
        [ObservableProperty]
        private int _sfxVolumePercent = 60;

        /// <summary>Gets or sets the Seq log server URL.</summary>
        [ObservableProperty]
        private string _seqUrl = string.Empty;

        /// <summary>Gets or sets the Grafana Loki log server URL.</summary>
        [ObservableProperty]
        private string _lokiUrl = string.Empty;

        /// <summary>Gets or sets the horizontal sensitivity percentage (25-200).</summary>
        [ObservableProperty]
        private int _horizontalSensitivityPercent = 100;

        /// <summary>Gets or sets the vertical sensitivity percentage (25-200).</summary>
        [ObservableProperty]
        private int _verticalSensitivityPercent = 100;

        /// <summary>Gets or sets the controller dead zone percentage (5-30).</summary>
        [ObservableProperty]
        private int _deadZonePercent = 15;

        /// <summary>Gets or sets a value indicating whether auto-run is enabled.</summary>
        [ObservableProperty]
        private bool _autoRun;

        /// <summary>Gets or sets a value indicating whether controller vibration is enabled.</summary>
        [ObservableProperty]
        private bool _vibrationEnabled = true;

        /// <summary>Gets or sets the splash screen display duration in seconds.</summary>
        [ObservableProperty]
        private float _splashDuration = 3f;

        /// <summary>Gets or sets the number of starting lives.</summary>
        [ObservableProperty]
        private int _startingLives = 3;

        /// <summary>Gets or sets a value indicating whether the FPS overlay is shown.</summary>
        [ObservableProperty]
        private bool _showFps;

        /// <summary>Gets or sets the difficulty level (Easy, Normal, Hard).</summary>
        [ObservableProperty]
        private string _difficulty = "Normal";

        /// <summary>Gets or sets the language code (en, de, ja, fr, es).</summary>
        [ObservableProperty]
        private string _language = "en";

        /// <summary>Gets or sets the status bar text displayed at the bottom of the window.</summary>
        [ObservableProperty]
        private string _statusText = "Ready";

        /// <summary>
        /// Loads values from the ConfigManager into the ViewModel properties.
        /// </summary>
        public void LoadFromManager()
        {
            var c = _manager.Config;
            Width = c.Video.Width;
            Height = c.Video.Height;
            Fullscreen = c.Video.Fullscreen;
            VSync = c.Video.VSync;
            FpsCap = c.Video.FpsCap;
            MsaaSamples = c.Video.MSAASamples;

            MasterVolumePercent = (int)(c.Audio.MasterVolume * 100);
            MusicVolumePercent = (int)(c.Audio.MusicVolume * 100);
            SfxVolumePercent = (int)(c.Audio.SfxVolume * 100);
            SeqUrl = c.Audio.SeqUrl;
            LokiUrl = c.Audio.LokiUrl;

            HorizontalSensitivityPercent = (int)(c.Input.HorizontalSensitivity * 100);
            VerticalSensitivityPercent = (int)(c.Input.VerticalSensitivity * 100);
            DeadZonePercent = (int)(c.Input.DeadZone * 100);
            AutoRun = c.Input.AutoRun;
            VibrationEnabled = c.Input.VibrationEnabled;

            SplashDuration = c.Gameplay.SplashDuration;
            StartingLives = c.Gameplay.StartingLives;
            ShowFps = c.Gameplay.ShowFps;
            Difficulty = c.Gameplay.Difficulty;
            Language = c.Gameplay.Language;

            StatusText = "Loaded";
        }

        /// <summary>
        /// Writes ViewModel values back to ConfigManager and saves to disk.
        /// </summary>
        [RelayCommand]
        private void Save()
        {
            var c = _manager.Config;
            c.Video.Width = Width;
            c.Video.Height = Height;
            c.Video.Fullscreen = Fullscreen;
            c.Video.VSync = VSync;
            c.Video.FpsCap = FpsCap;
            c.Video.MSAASamples = MsaaSamples;

            c.Audio.MasterVolume = MasterVolumePercent / 100f;
            c.Audio.MusicVolume = MusicVolumePercent / 100f;
            c.Audio.SfxVolume = SfxVolumePercent / 100f;
            c.Audio.SeqUrl = SeqUrl;
            c.Audio.LokiUrl = LokiUrl;

            c.Input.HorizontalSensitivity = HorizontalSensitivityPercent / 100f;
            c.Input.VerticalSensitivity = VerticalSensitivityPercent / 100f;
            c.Input.DeadZone = DeadZonePercent / 100f;
            c.Input.AutoRun = AutoRun;
            c.Input.VibrationEnabled = VibrationEnabled;

            c.Gameplay.SplashDuration = SplashDuration;
            c.Gameplay.StartingLives = StartingLives;
            c.Gameplay.ShowFps = ShowFps;
            c.Gameplay.Difficulty = Difficulty;
            c.Gameplay.Language = Language;

            _manager.Save();
            StatusText = "Saved successfully";
        }

        /// <summary>
        /// Resets all settings to defaults and clears the config file.
        /// </summary>
        [RelayCommand]
        private void ResetDefaults()
        {
            _manager.Config = new GameConfig();
            LoadFromManager();
            StatusText = "Reset to defaults";
        }

        /// <summary>
        /// Closes the configuration window.
        /// </summary>
        [RelayCommand]
        private void CloseWindow()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
