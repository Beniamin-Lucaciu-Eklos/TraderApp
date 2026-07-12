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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, IDisposable
    {
        public LoginView()
        {
            InitializeComponent();

            pbPassword.PasswordChanged += PbPassword_PasswordChanged;
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
                typeof(LoginView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback: OnPasswordPropertyChanged));

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LoginView viewLogin)
            {
                viewLogin.AssignPassword();
            }
        }

        public void Dispose()
        {
            pbPassword.PasswordChanged -= PbPassword_PasswordChanged;
        }
    }
}
