using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FacultyManagementSystem.UI.View.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlBindablePasswordBox.xaml
    /// </summary>
    public partial class UserControlBindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(UserControlBindablePasswordBox));

        public SecureString Password
        {             
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public UserControlBindablePasswordBox()
        {
            InitializeComponent();

            passwordBox.PasswordChanged += (s, e) =>
            {
                Password = passwordBox.SecurePassword;
            };
        }
    }
}
