using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class TagTaskDecorator : TaskDecorator
    {
        public TagTaskDecorator(UserTask task) : base(task) { }

        public override void DisplayTask()
        {
            base.DisplayTask();
            Console.WriteLine($"\tTags: {string.Join(", ", Tags)}");
        }
    }
}
