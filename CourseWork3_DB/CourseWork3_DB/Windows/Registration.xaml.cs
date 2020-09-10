using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CourseWork3_DB
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration
    {
        private bool _loginExists;

        public Registration()
        {
            InitializeComponent();
        }

        private async void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            if (_loginExists)
            {
                MessageBox.Show("This login is already taken");
                return;
            }
            var login = LoginTextBox.Text;
            if(login.Length < 5)
            {
                MessageBox.Show("Login must have at least 5 symbols");
                return;
            }

            var password = PasswordTextBox.Password;
            if (!string.IsNullOrEmpty(password))
            {
                await using var applicationContext = new ApplicationContext();
                var newUser = new User
                {
                    Name = login,
                    Password = password
                };
                applicationContext.Users.Add(newUser);
                await applicationContext.SaveChangesAsync();
                MessageBox.Show("User successfully created");
                Close();
                return;
            }

            MessageBox.Show("Please, enter the password");
        }

        private async void LoginChecked(object sender, TextChangedEventArgs e)
        {   
            var login = LoginTextBox.Text;
            if (login.Length < 5)
            {
                WarningLabel.Content = "Login should have at least 5 symbols";
                WarningLabel.Visibility = Visibility.Visible;
                return;
            }
            await using var applicationContext = new ApplicationContext();
            _loginExists = await applicationContext.Users.AnyAsync(p => p.Name == LoginTextBox.Text);
            if (_loginExists)
            {
                WarningLabel.Content = "Login is already taken";
                WarningLabel.Visibility = Visibility.Visible;
                return;
            }

            WarningLabel.Visibility = Visibility.Hidden;
        }
    }
}