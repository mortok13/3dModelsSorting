using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CourseWork3_DB.Utilities;

namespace CourseWork3_DB.Windows
{
    /// <summary>
    /// Логика взаимодействия для GroupsWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        private readonly DelayedLoader<Group> _delayedLoader = new DelayedLoader<Group>();

        public GroupsWindow()
        {
            InitializeComponent();
        }

        private async void FillTableWhenInizialized(object sender, EventArgs e)
        {
            await GroupDataGrid.ReloadGroupTableAsync();
        }

        private async void DeleteGroupButtonClick(object sender, RoutedEventArgs e)
        {
            if (GroupDataGrid.SelectedIndex == -1)
            {
                return;
            }

            await using var dbContext = new ApplicationContext();
            var selectedRow = GroupDataGrid.SelectedItem as IShortGroup ??
                              throw new ArgumentNullException(nameof(GroupDataGrid.SelectedItem));
            foreach (var model in await dbContext.Models
                .Include(x => x.Group)
                .Where(x => x.Group.Id == selectedRow.Id)
                .ToArrayAsync())
            {
                model.Group = null;
            }

            dbContext.Groups.Remove(new Group
            {
                Id = selectedRow.Id
            });
            await dbContext.SaveChangesAsync();
            await Task.WhenAll(GroupDataGrid.ReloadGroupTableAsync(),
                MainWindowProvider.Instance.Window.ModelsDataGrid.ReloadModelsTableAsync());
        }

        private void AddNewButtonClick(object sender, RoutedEventArgs e)
        {
            var createNewGroupWindow = new CreateNewGroupWindow
            {
                Owner = this
            };
            createNewGroupWindow.ShowDialog();
        }

        private void SearchGroupByName(object sender, TextChangedEventArgs e)
        {
            _delayedLoader
                .ScheduleDelayedLoading(GroupDataGrid,
                    context => context.Groups,
                    (set, token) => set
                        .Where(x => x.User.Id.Equals(UserProvider.Instance.User.Id) &&
                                    x.Name.Contains(GroupNameTextBox.Text))
                        .OfType<IShortGroup>()
                        .ToListAsync(token));
        }
    }
}