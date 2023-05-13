using log4net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void My_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Point point = Mouse.GetPosition(sender as Grid);
            if (point.Y <= 15)
            {
                this.DragMove();
            }
        }


        private void LabelClick(object sender, RoutedEventArgs e) {
            if (sender is null) {
                return;
            }
            var label = sender as Label;
            if (label is null) {
                return;
            }
            label.Tag = 1;
            var parent = label.Parent;
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++) {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (object.ReferenceEquals(child, label)) {
                    continue;
                }
                if (child is Label tempLabel)
                {
                    tempLabel.Tag = -1;
                }

            }
        }

    }
}
