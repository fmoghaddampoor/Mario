namespace MarioEngine.ConfigEditor.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using MarioEngine.Core.Config;

    /// <summary>
    /// MVVM ViewModel for the game configuration editor — properties partial.
    /// Exposes all GameConfig settings as bindable fields with change notification.
    /// Uses CommunityToolkit.Mvvm source generators for observable properties.
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
        [ObservableProperty] private int _width = 1920;

        /// <summary>Gets or sets the display height in pixels.</summary>
        [ObservableProperty] private int _height = 1080;

        /// <summary>Gets or sets a value indicating whether fullscreen mode is enabled.</summary>
        [ObservableProperty] private bool _fullscreen = true;

        /// <summary>Gets or sets a value indicating whether vertical sync is enabled.</summary>
        [ObservableProperty] private bool _vSync = true;

        /// <summary>Gets or sets the target FPS cap (0 = unlimited).</summary>
        [ObservableProperty] private int _fpsCap = 60;

        /// <summary>Gets or sets the MSAA sample count (0, 2, 4, 8).</summary>
        [ObservableProperty] private int _msaaSamples;

        /// <summary>Gets or sets the master volume percentage (0-100).</summary>
        [ObservableProperty] private int _masterVolumePercent = 80;

        /// <summary>Gets or sets the music volume percentage (0-100).</summary>
        [ObservableProperty] private int _musicVolumePercent = 70;

        /// <summary>Gets or sets the SFX volume percentage (0-100).</summary>
        [ObservableProperty] private int _sfxVolumePercent = 60;

        /// <summary>Gets or sets the Seq log server URL.</summary>
        [ObservableProperty] private string _seqUrl = string.Empty;

        /// <summary>Gets or sets the Grafana Loki log server URL.</summary>
        [ObservableProperty] private string _lokiUrl = string.Empty;

        /// <summary>Gets or sets the horizontal sensitivity percentage (25-200).</summary>
        [ObservableProperty] private int _horizontalSensitivityPercent = 100;

        /// <summary>Gets or sets the vertical sensitivity percentage (25-200).</summary>
        [ObservableProperty] private int _verticalSensitivityPercent = 100;

        /// <summary>Gets or sets the controller dead zone percentage (5-30).</summary>
        [ObservableProperty] private int _deadZonePercent = 15;

        /// <summary>Gets or sets a value indicating whether auto-run is enabled.</summary>
        [ObservableProperty] private bool _autoRun;

        /// <summary>Gets or sets a value indicating whether controller vibration is enabled.</summary>
        [ObservableProperty] private bool _vibrationEnabled = true;

        /// <summary>Gets or sets the splash screen display duration in seconds.</summary>
        [ObservableProperty] private float _splashDuration = 3f;

        /// <summary>Gets or sets the number of starting lives.</summary>
        [ObservableProperty] private int _startingLives = 3;

        /// <summary>Gets or sets a value indicating whether the FPS overlay is shown.</summary>
        [ObservableProperty] private bool _showFps;

        /// <summary>Gets or sets the difficulty level (Easy, Normal, Hard).</summary>
        [ObservableProperty] private string _difficulty = "Normal";

        /// <summary>Gets or sets the language code (en, de, ja, fr, es).</summary>
        [ObservableProperty] private string _language = "en";

        /// <summary>Gets or sets the status bar text displayed at the bottom of the window.</summary>
        [ObservableProperty] private string _statusText = "Ready";
    }
}
