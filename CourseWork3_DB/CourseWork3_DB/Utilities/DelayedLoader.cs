using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace CourseWork3_DB.Utilities
{
    public class DelayedLoader<TTable>
        where TTable : class
    {
        private CancellationTokenSource _tokenSource;
        private Task _currentTask;
        
        public void ScheduleDelayedLoading<TResult>(DataGrid finalContainer,
            Func<ApplicationContext, DbSet<TTable>> tableSelector,
            Func<DbSet<TTable>, CancellationToken, Task<TResult>> queryFunction)
            where TResult : IEnumerable
        {
            async Task SearchAsync(CancellationToken token = default)
            {
                await Task.Delay(1000, token);
                await using var dbContext = new ApplicationContext();
                var table = tableSelector(dbContext);
                var result = await queryFunction(table, token);
                finalContainer.ItemsSource = result;
            }

            if (_currentTask != null && _currentTask.Status == TaskStatus.WaitingForActivation)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();
            _currentTask = SearchAsync(_tokenSource.Token);
        }
    }

}