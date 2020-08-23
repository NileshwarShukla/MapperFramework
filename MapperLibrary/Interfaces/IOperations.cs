using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperLibrary
{
    interface IOperations<T>
    {
        object AssignValue(T o);
    }
}
