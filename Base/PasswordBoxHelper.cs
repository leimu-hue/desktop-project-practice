using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IntelligentControl.Base
{
    internal class PasswordBoxHelper
    {
        /// <summary>
        /// 使用添加附加属性的方式来绑定密码框中的密码信息
        ///     包含两个附加属性：password、attach
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("PasswordContent", typeof(string), typeof(PasswordBoxHelper),
                new PropertyMetadata(new PropertyChangedCallback(OnPasswordPropertyChanged)));

        public static string GetPasswordContent(DependencyObject obj)
        {
            return (string) obj.GetValue(PasswordProperty);
        }

        public static void SetPasswordContent(DependencyObject obj, string password) {
            obj.SetValue(PasswordProperty, password);
        }


        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(string),
            typeof(PasswordBoxHelper), new PropertyMetadata(new PropertyChangedCallback(OnAttachChanged)));

        public static string GetAttach(DependencyObject obj)
        {
            return (string)obj.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject obj, string attach)
        {
            obj.SetValue(AttachProperty, attach);
        }

        private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null)
            {
                return;
            }
            // 这里才是最重要的一部，把Pb_PasswordChanged事件加载到passwordChanged事件中
            // passwordBox.PasswordChanged += Pb_PasswordChanged;
        }

        static bool _isUpdating = false;

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null)
            {
                return;
            }
            passwordBox.PasswordChanged -= Pb_PasswordChanged;
            var password = (string) e.NewValue;
            if (!_isUpdating) {
                passwordBox.Password = password;
            }
            passwordBox.PasswordChanged += Pb_PasswordChanged;
        }

        private static void Pb_PasswordChanged(object sender, RoutedEventArgs e) {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null) {
                return;
            }
            _isUpdating = true;
            SetPasswordContent(passwordBox, passwordBox.Password);
            _isUpdating = false;
        }

    }
}
