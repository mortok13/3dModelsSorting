using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using CourseWork3_DB.Utilities;
using CourseWork3_DB.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CourseWork3_DB
{
    public partial class MainWindow
    {
        private readonly DelayedLoader<Model> _delayedLoader = new DelayedLoader<Model>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadModels(object sender, EventArgs e)
        {
            await ModelsDataGrid.ReloadModelsTableAsync();
        }

        private void AddNewButtonClick(object sender, RoutedEventArgs e)
        {
            var addNewModel = new AddNewModelWindow();
            addNewModel.ShowDialog();
        }

        private void ChangeUserButtonClick(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }

        private void OpenGroupWindowButtonClick(object sender, RoutedEventArgs e)
        {
            var groupWindow = new GroupsWindow();
            groupWindow.ShowDialog();
        }

        private async void DeleteElementButtonClick(object sender, RoutedEventArgs e)
        {
            if (ModelsDataGrid.SelectedIndex == -1)
            {
                return;
            }

            await using var dbContext = new ApplicationContext();
            var selectedRow = ModelsDataGrid.SelectedItem;
            var model = selectedRow as IModelWithoutUser ?? throw new ArgumentException(nameof(selectedRow));
            dbContext.Models.Remove(new Model
            {
                Id = model.Id
            });
            await dbContext.SaveChangesAsync();
            await ModelsDataGrid.ReloadModelsTableAsync();
        }

        private void SearchModelByName(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _delayedLoader
                .ScheduleDelayedLoading(ModelsDataGrid,
                    dbContext => dbContext.Models,
                    (dbSet, token) => dbSet
                        .GetModelsWithoutUserAsync(x =>
                            x.User.Id.Equals(UserProvider.Instance.User.Id) &&
                            x.Name.Contains(ModelNameTextBox.Text), token));
        }


        private void OpenFileButtonClick(object sender, RoutedEventArgs e)
        {
            if (ModelsDataGrid.SelectedIndex == -1)
            {
                return;
            }

            var selectedRow = ModelsDataGrid.SelectedItem as IModelWithoutUser
                              ?? throw new ArgumentException(nameof(ModelsDataGrid.SelectedItem));
            var path = selectedRow.Path;
            if (!File.Exists(path))
            {
                MessageBox.Show("File does not exist or path is changed");
                return;
            }
            Process.Start("explorer.exe", Path.GetDirectoryName(path));
        }
    }
}