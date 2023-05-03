using IntelligentControl.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IntelligentControl.Views
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        private void My_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Point point = Mouse.GetPosition(sender as Grid);
            if (point.Y <= 15)
            {
                this.DragMove();
            }
        }
    }
}
