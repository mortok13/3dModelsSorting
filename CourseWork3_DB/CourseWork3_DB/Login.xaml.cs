using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork3_DB
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            
            
        }

        private async void HandleLoginButtonClick(object sender, RoutedEventArgs e)
        {   
            var login = LoginTextBox.Text;
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Login is empty");
                return;
            }

            var password = PasswordTableBox.Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is empty");
                return;
            }

            if(login.Length < 5)
            {
                MessageBox.Show("User does not exist");
                return;
            }

            await using var dbContext = new ApplicationContext();
            if (await dbContext.Users.AnyAsync(x => x.Name == login && x.Password == password))
            {
                
                await UserProvider.Instance.InitializeAsync(dbContext, login, password);
                var mainWindow = new MainWindow();
                mainWindow.Show();
                MainWindowProvider.Instance.Initialize(mainWindow);
                Close();
                return;
            }

            MessageBox.Show("User does not exist");
        }

        private void MouseEnterRegisterLabel(object sender, MouseEventArgs e)
        {
            RegisterTableBox.TextDecorations = TextDecorations.Underline;
            RegisterTableBox.Foreground = Brushes.RoyalBlue;
        }

        private void MouseLeaveRegisterLabel(object sender, MouseEventArgs e)
        {
            RegisterTableBox.TextDecorations = null;
            RegisterTableBox.Foreground = Brushes.Black;
        }

        private void RegisterButtonClick(object sender, MouseButtonEventArgs e)
        {
            var registrationWin = new Registration();
            registrationWin.ShowDialog();
        }
    }
}
