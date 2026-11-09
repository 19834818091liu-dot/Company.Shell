using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Sqlite
{
    public abstract class RepositoryBase
    {
        protected static SqliteDbContext sqliteDbContext { get; } = new Lazy<SqliteDbContext>().Value;
    }
}
