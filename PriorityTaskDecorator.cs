using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class PriorityTaskDecorator : TaskDecorator
    {
        public PriorityTaskDecorator(UserTask task) : base(task) { }

        public override void DisplayTask()
        {
            base.DisplayTask();
            Console.WriteLine($"\tPriority: {Priority}");

            switch (Priority)
            {
                case TaskPriority.Low:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case TaskPriority.Medium:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case TaskPriority.High:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.WriteLine("\t!!!"); // Add some exclamation marks to emphasize the priority

            Console.ResetColor(); // Reset the text color to default

        }
    }
}
