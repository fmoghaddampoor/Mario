namespace MarioEngine.ConfigEditor
{
    using System.IO;
    using System.Windows;
    using MarioEngine.ConfigEditor.ViewModels;
    using MarioEngine.Core.Config;

    /// <summary>
    /// Application entry point. Creates the ViewModel and assigns it to MainWindow.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Called on startup. Loads configuration and sets the main window data context.
        /// </summary>
        /// <param name="e">Startup event arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var iniPath = Path.Combine(System.AppContext.BaseDirectory, "app.ini");
            var manager = ConfigManager.Load();

            if (File.Exists(iniPath))
            {
                manager.MergeIniFile(iniPath);
            }

            var viewModel = new ConfigViewModel(manager);
            viewModel.LoadFromManager();

            var window = new MainWindow();
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
