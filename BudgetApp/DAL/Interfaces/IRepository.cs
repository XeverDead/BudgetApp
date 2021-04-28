using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(long id);

        IEnumerable<T> GetAll();

        T Update(T model);

        T Add(T model);
    }
}
