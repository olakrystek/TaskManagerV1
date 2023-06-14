using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class SimpleTask : UserTask
    {
        public SimpleTask(int id, string title, string description, DateTime dueDate, TaskPriority priority, List<string> tags, TaskStatus status = TaskStatus.NotStarted)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Tags = tags;
            Status = status;
        }

        public override void DisplayTask()
        {
            Console.WriteLine($"#{Id} {Title}");
        }
    }
}
