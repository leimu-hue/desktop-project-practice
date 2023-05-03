using log4net;
using System.Windows;

namespace IntelligentControl.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow()
        {
            logger.Info("main window was init");
            InitializeComponent();
        }
    }
}
