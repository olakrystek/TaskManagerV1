using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class StatusTaskDecorator : TaskDecorator
    {
        public StatusTaskDecorator(UserTask task) : base(task) { }

        public override void DisplayTask()
        {
            base.DisplayTask();
            Console.WriteLine($"\tStatus: {Status}");

            switch (Status)
            {
                case TaskStatus.NotStarted:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case TaskStatus.InProgress:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case TaskStatus.Completed:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.WriteLine("\t***");

            Console.ResetColor();

        }
    }
}
