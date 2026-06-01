namespace MarioEngine.ConfigEditor.ViewModels
{
    using System;
    using CommunityToolkit.Mvvm.Input;
    using MarioEngine.Core.Config;

    /// <summary>
    /// Contains load/save/reset/close logic for the ConfigViewModel.
    /// </summary>
    internal sealed partial class ConfigViewModel
    {
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
