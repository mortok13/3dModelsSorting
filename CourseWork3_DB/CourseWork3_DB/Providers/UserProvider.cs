using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseWork3_DB
{
    public class UserProvider
    {
        public static UserProvider Instance { get; } = new UserProvider();

        private UserProvider()
        { }

        private User _user;

        public User User => _user ?? throw new InvalidOperationException("There is no user.");

        public async Task InitializeAsync(ApplicationContext applicationContext, string login, string password)
        {
            _user = await applicationContext.Users
                .FirstAsync(x => x.Name == login && x.Password == password);
        }
    }
}