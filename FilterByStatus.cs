using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class FilterByStatus : ITaskFilteringStrategy
    {
        private readonly TaskStatus _status;

        public FilterByStatus(TaskStatus status)
        {
            _status = status;
        }

        public IEnumerable<UserTask> Filter(IEnumerable<UserTask> tasks)
        {
            return tasks.Where(t => t.Status == _status);
        }
    }
}
