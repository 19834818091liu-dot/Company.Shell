using Company.DataBase.Base.Models;
using Company.DataBase.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Sqlite
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public int Delete(UserEntity entity)
        {
            sqliteDbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return sqliteDbContext.SaveChanges();
        }

        public List<UserEntity> FindAll(string keyword)
        {
            return sqliteDbContext.Users.Where(t => t.UserName.Contains(keyword)).ToList();
        }

        public UserEntity Get(int id)
        {
            return sqliteDbContext.Users.Find(id);
        }

        public List<UserEntity> GetAll()
        {
            return sqliteDbContext.Users.ToList();
        }

        public int Insert(UserEntity entity)
        {
            sqliteDbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return sqliteDbContext.SaveChanges();
        }

        public int SaveChanged()
        {
            return sqliteDbContext.SaveChanges();
        }

        public UserEntity Select(string keyword)
        {
            return sqliteDbContext.Users.ToList().Find(t => t.UserName == keyword);

        }

        public int UpDate(UserEntity entity)
        {
           sqliteDbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return sqliteDbContext.SaveChanges();
        }
    }
}
