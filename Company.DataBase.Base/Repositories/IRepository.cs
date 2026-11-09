using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Base.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);   //根据id获取

        int UpDate(T entity);   //增加实体

        int Delete(T entity); //删除实体

        int Insert(T entity);       //插入实体

        T Select(string keyword);    //根据关键字查找实体

        List<T> GetAll();         //获取所有实体

        List<T> FindAll(string keyword);      //根据关键字查找所有包含该关键字的实体列表

        int SaveChanged();   
    }
}
