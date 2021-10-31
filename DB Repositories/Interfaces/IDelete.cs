using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Repositories.Interfaces
{
    interface IDelete<T>
        where T : class
    {
        void Delete(T ent);
    }
}
