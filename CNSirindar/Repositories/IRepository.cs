using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNSirindar.Models;

namespace CNSirindar.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : TableDbConventions
        where TKey : struct
    {
        bool Create(TEntity entity);
        TEntity Read(TKey? key);
        bool Update(TEntity entity);
        bool Delete(TKey? key);

        List<TEntity> List();
    }

    public class RepositoryFactory
    {
        public static T Get<T>() where T : class
        {
            return Activator.CreateInstance(Type.GetType(System.Configuration.ConfigurationManager.AppSettings[typeof(T).ToString()])) as T;
        }
    }
}
