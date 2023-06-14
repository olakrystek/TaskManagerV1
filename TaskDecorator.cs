using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    abstract class TaskDecorator : UserTask
    {
        protected UserTask _decoratedTask;

        public TaskDecorator(UserTask task)
        {
            _decoratedTask = task;
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            Status = task.Status;
            Tags = task.Tags;
        }

        public override void DisplayTask()
        {
            _decoratedTask.DisplayTask();
        }
    }
}
