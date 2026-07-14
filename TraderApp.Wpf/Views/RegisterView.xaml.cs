using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TraderApp.Wpf.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl, IDisposable
    {
        public RegisterView()
        {
            InitializeComponent();

            pbPassword.PasswordChanged += PbPassword_PasswordChanged;
            confirmPassword.PasswordChanged += ConfirmPassword_PasswordChanged;
        }

        private void PbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = pbPassword.Password;
        }

        public void AssignPassword()
        {
            if (pbPassword.Password != Password)
                pbPassword.Password = Password;
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password),
                typeof(string),
                typeof(RegisterView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback: OnPasswordPropertyChanged));

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RegisterView viewLogin)
            {
                viewLogin.AssignPassword();
            }
        }

        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ConfirmPassword = confirmPassword.Password;
        }

        public void AssignConfirmPassword()
        {
            if (confirmPassword.Password != ConfirmPassword)
                confirmPassword.Password = ConfirmPassword;
        }

        public string ConfirmPassword
        {
            get { return (string)GetValue(ConfirmPasswordProperty); }
            set { SetValue(ConfirmPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfirmPasswordProperty =
            DependencyProperty.Register(nameof(ConfirmPassword),
                typeof(string),
                typeof(RegisterView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback: OnConfirmPasswordPropertyChanged));

        private static void OnConfirmPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RegisterView viewLogin)
            {
                viewLogin.AssignConfirmPassword();
            }
        }

        public void Dispose()
        {
            pbPassword.PasswordChanged -= PbPassword_PasswordChanged;
            confirmPassword.PasswordChanged -= ConfirmPassword_PasswordChanged;
        }
    }
}
