using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Repositories.Interfaces
{
    interface ICreate<T>
        where T : class
    {
        void Create(T newEntity);
    }
}
