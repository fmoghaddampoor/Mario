namespace MarioEngine.ConfigEditor
{
    using System.IO;
    using System.Windows;
    using MarioEngine.ConfigEditor.ViewModels;
    using MarioEngine.Core.Config;

    /// <summary>
    /// Main window for the game configuration editor.
    /// Sets up the MVVM ViewModel and loads existing config.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// Loads the configuration and sets the ViewModel as data context.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            var iniPath = Path.Combine(System.AppContext.BaseDirectory, "app.ini");
            var manager = ConfigManager.Load();

            if (File.Exists(iniPath))
            {
                manager.MergeIniFile(iniPath);
            }

            var viewModel = new ConfigViewModel(manager);
            viewModel.LoadFromManager();
            DataContext = viewModel;
        }
    }
}
