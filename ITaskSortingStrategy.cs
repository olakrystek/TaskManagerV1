using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    interface ITaskSortingStrategy
    {
        IEnumerable<UserTask> Sort(IEnumerable<UserTask> tasks);
    }
}
