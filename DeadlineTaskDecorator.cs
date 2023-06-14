using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class DeadlineTaskDecorator : TaskDecorator
    {
        public DeadlineTaskDecorator(UserTask task) : base(task) { }

        public override void DisplayTask()
        {
            base.DisplayTask();
            Console.WriteLine($"\tDeadline: {DueDate.ToShortDateString()}");
        }
    }
}
