using IntelligentControl.Views;
using log4net;
using System;
using System.Windows;

namespace IntelligentControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(App));

        protected override void OnStartup(StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            var loginWindows = new LoginWindow();
            var result = loginWindows.ShowDialog();
            logger.Info($"登录状态: {result ?? false}");
            if (!(result ?? false))
            {
                logger.Info("登录失败, 程序退出");
                Application.Current.Shutdown();
                Environment.Exit(0);
                return;
            }
            logger.Info("登录成功, 开始跳转到主窗口");
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
            base.OnStartup(e);
            new MainWindow().Show();
        }


    }
}
