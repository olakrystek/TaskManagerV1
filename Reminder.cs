using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class Reminder
    {
        private readonly User _user;

        public Reminder(User user)
        {
            _user = user;
        }

        public void ShowTasksDueToday()
        {
            List<UserTask> tasks = _user.TaskList.GetTasksDueToday();

            if (tasks.Count == 0)
            {
                Console.WriteLine("You have no tasks due today. Enjoy your day!\n");
            }
            else
            {
                Console.WriteLine($"You have {tasks.Count} tasks due today:");
                foreach (UserTask task in tasks)
                {
                    task.DisplayTask();
                    Console.WriteLine();
                }
            }
        }
    }
}
