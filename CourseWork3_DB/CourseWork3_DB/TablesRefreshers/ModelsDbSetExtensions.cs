using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseWork3_DB
{
    public static class ModelsDbSetExtensions
    {
        public static Task<IModelWithoutUser[]> GetModelsWithoutUserAsync(this DbSet<Model> modelsTable,
            Expression<Func<Model, bool>> whereFilterPredicate, CancellationToken token = default)
            => modelsTable
                .Where(whereFilterPredicate)
                .Include(x => x.Group)
                .OfType<IModelWithoutUser>()
                .ToArrayAsync(cancellationToken: token);
    }
}