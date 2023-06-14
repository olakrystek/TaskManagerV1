using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class FilterByTag : ITaskFilteringStrategy
    {
        private readonly string _tag;

        public FilterByTag(string tag)
        {
            _tag = tag;
        }

        public IEnumerable<UserTask> Filter(IEnumerable<UserTask> tasks)
        {
            return tasks.Where(t => t.Tags.Contains(_tag));
        }
    }
}
