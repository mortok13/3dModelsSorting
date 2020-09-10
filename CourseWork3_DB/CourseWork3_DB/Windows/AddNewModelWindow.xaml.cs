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
using Microsoft.Win32;
using System.IO;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CourseWork3_DB.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace CourseWork3_DB
{
    /// <summary>
    /// Логика взаимодействия для AddNewModel.xaml
    /// </summary>
    public partial class AddNewModelWindow
    {
        public AddNewModelWindow()
        {
            InitializeComponent();
            Enum.GetNames(typeof(ModelType))
                .ToList()
                .ForEach(x => TypeOfModelComboBox.Items.Add(x));
        }

        private async void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var path = ModelPathTextBox.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("File does not exist");
                return;
            }

            var name = ModelNameTextBox.Text;
            var type = TypeOfModelComboBox.Text;
            var category = CategoryOfModelComboBox.Text;
            var status = StatusOfModelComboBox.Text;
            var group = GroupOfModelComboBox.Text;

            if (new[]
            {
                name,
                type,
                category,
                status,
                path
            }.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show("Please, enter all fields marked by '*'");
                return;
            }

            await using var dbContext = new ApplicationContext();
            dbContext.Models.Add(new Model
            {
                Name = name,
                Type = type,
                Category = category,
                Status = status,
                Group = await dbContext.Groups
                    .FirstOrDefaultAsync(x => x.Name.Equals(group)),
                Path = path,
                UserId = UserProvider.Instance.User.Id,
            });

            await dbContext.SaveChangesAsync();
            Hide();
            await MainWindowProvider.Instance.Window.ModelsDataGrid.ReloadModelsTableAsync();
        }


        private void ChooseFileButtonClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().GetValueOrDefault(false))
            {
                ModelPathTextBox.Text = openFileDialog.FileName;
            }
        }

        private async void PopulateGroups(object sender, EventArgs e)
        {
            await using var dbContext = new ApplicationContext();
            foreach (var name in await dbContext.Groups
                .Where(x => x.User.Equals(UserProvider.Instance.User))
                .Select(x => x.Name)
                .ToArrayAsync())
            {
                GroupOfModelComboBox.Items.Add(name);
            }
        }
    }
}