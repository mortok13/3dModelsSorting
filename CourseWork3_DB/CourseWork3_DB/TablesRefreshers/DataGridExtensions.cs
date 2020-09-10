using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace CourseWork3_DB
{
    public static class DataGridExtensions
    {
        public static async Task ReloadModelsTableAsync(this DataGrid dataGrid)
        {
            await using var dbContext = new ApplicationContext();
            var models = await dbContext.Models
                .GetModelsWithoutUserAsync(x => x.User.Id.Equals(UserProvider.Instance.User.Id));
            dataGrid.ItemsSource = models;
        }
        
        public static async Task ReloadGroupTableAsync(this DataGrid dataGrid)
        {
            await using var dbContext = new ApplicationContext();
            var group = await dbContext.Groups
                .Where(x => x.User.Id.Equals(UserProvider.Instance.User.Id))
                .OfType<IShortGroup>()
                .ToArrayAsync();
            dataGrid.ItemsSource = group;
        }
    }
}