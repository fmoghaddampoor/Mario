namespace MarioEngine.ConfigEditor
{
    using System.Windows;

    /// <summary>
    /// Main window. No code-behind logic — all bindings handled by ViewModel via DataContext set in App.OnStartup.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
