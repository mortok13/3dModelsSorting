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
using System.Linq;

namespace CourseWork3_DB.Windows
{
    /// <summary>
    /// Логика взаимодействия для CreateNewGroupWindows.xaml
    /// </summary>
    public partial class CreateNewGroupWindow : Window
    {
        public CreateNewGroupWindow()
        {
            InitializeComponent();
        }

        private async void CreateNewGroupButtonClick(object sender, RoutedEventArgs e)
        {
            var groupName = NameTextBox.Text;
            if (string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("Please, enter a name of group");
                return;
            }
            await using var dbContext = new ApplicationContext();
            dbContext.Groups.Add(new Group
            {
                Name = groupName,
                UserId = UserProvider.Instance.User.Id
            });
            await dbContext.SaveChangesAsync();
            var groupsWindow = Owner as GroupsWindow ?? throw new ArgumentNullException(nameof(Owner));
            await groupsWindow.GroupDataGrid.ReloadGroupTableAsync();
            Close();
        }
    }
}
