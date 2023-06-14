using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class ComplexTaskDecorator : TaskDecorator
    {
        public ComplexTaskDecorator(UserTask task) : base(task) { }

        public override void DisplayTask()
        {
            base.DisplayTask();
            Console.WriteLine($"\tDescription: {Description}");
            Console.WriteLine($"\tDate: {DueDate.ToShortDateString()}");
            Console.WriteLine($"\tStatus: {Status}");
        }
    }
}
