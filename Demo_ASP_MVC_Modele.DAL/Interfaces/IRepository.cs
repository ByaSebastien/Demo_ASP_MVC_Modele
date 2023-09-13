using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        // CRUD 

        //Create
        TKey Add(TEntity entity);

        //Read
        IEnumerable<TEntity> GetAll();
        TEntity GetById(TKey id);

        //Update
        bool Update(TEntity entity);

        //Delete
        bool Delete(TKey id);
    }
}
