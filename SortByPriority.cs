using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class SortByPriority : ITaskSortingStrategy
    {
        public IEnumerable<UserTask> Sort(IEnumerable<UserTask> tasks)
        {
            return tasks.OrderByDescending(t => t.Priority);
        }
    }
}
