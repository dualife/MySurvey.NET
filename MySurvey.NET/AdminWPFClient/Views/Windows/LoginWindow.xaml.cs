using AdminWPFClient.Extensions;
using AdminWPFClient.ViewModels;
using System.Windows;

namespace AdminWPFClient.Windows
{
    /// <summary>
    /// Description for LoginWindow.
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the LoginWindow class.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();

            this.PasswordBx.PasswordChanged += PasswordBx_PasswordChanged;
            this.connectBtn.Click += ConnectBtn_Click; 
        }

        private void PasswordBx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var VM = this.DataContext as LoginViewModel;

            VM.PasswordHash = this.PasswordBx.SecurePassword;
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            var res = this.PasswordBx.SecurePassword.GetUnsecureString();
        }
    }
}