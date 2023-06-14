using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    public enum TaskPriority
    {
        Undefined = 0,
        Normal = 1,
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    abstract class UserTask
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Undefined;
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        public List<string> Tags { get; set; } = new List<string>();

        public bool IsDueToday()
        {
            return DueDate.Date == DateTime.Today;
        }

        public abstract void DisplayTask();
    }
}
