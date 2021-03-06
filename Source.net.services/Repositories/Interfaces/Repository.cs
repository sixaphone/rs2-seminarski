﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Source.net.services.Repositories.Interfaces
{
    public interface Repository<T, TFilter>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(TFilter filter);
        T Get(int id);
        T Add(T model);
        T Update(T model);
        T Delete(int id);
        void BulkInsert(ICollection<T> entities);
    }
}
