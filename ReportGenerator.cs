using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class ReportGenerator
    {
        public static double CalculateCompletionPercentage(User user)
        {
            int completedTasks = user.TaskList.GetTasks().Count(t => t.Status == TaskStatus.Completed);
            int totalTasks = user.TaskList.GetTasks().Count;
            if (totalTasks > 0)
            {
                return (double)completedTasks / totalTasks * 100;
            }
            else
            {
                return 0;
            }
        }

        public static double GetAveragePriority(User user)
        {
            if (user.TaskList.GetTasks().Count > 0)
            {
                return user.TaskList.GetTasks().Average(t => (int)t.Priority);
            }
            else
            {
                return 0;
            }
        }

        public static string GetMostCommonTag(User user)
        {
            if (user.TaskList.GetTasks().Count > 0)
            {
                return user.TaskList.GetTasks().SelectMany(t => t.Tags).GroupBy(t => t).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key ?? "";
            }
            else
            {
                return "";
            }
        }

        public static string GenerateSummaryReport(User user)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Summary report for {user.Username}");
            sb.AppendLine($"Total tasks: {user.TaskList.GetTasks().Count}");
            sb.AppendLine($"Completed tasks: {user.TaskList.GetTasks().Count(t => t.Status == TaskStatus.Completed)}");
            sb.AppendLine($"In progress tasks: {user.TaskList.GetTasks().Count(t => t.Status == TaskStatus.InProgress)}");
            sb.AppendLine($"Not started tasks: {user.TaskList.GetTasks().Count(t => t.Status == TaskStatus.NotStarted)}");
            sb.AppendLine($"Completion percentage: {CalculateCompletionPercentage(user):F2}%");
            sb.AppendLine($"Average priority: {GetAveragePriority(user):F2}");
            sb.AppendLine($"Most common tag: {GetMostCommonTag(user)}");
            return sb.ToString();
        }

        public static void DisplaySummaryReport(User user)
        {
            if (user != null)
            {
                string report = GenerateSummaryReport(user);

                Console.WriteLine("********** Summary Report **********");

                Console.WriteLine(report);

                Console.WriteLine("************************************");
                Console.WriteLine();
            }
            else
            {
                throw new ArgumentNullException("User cannot be null.");
            }
        }

        public static void ExportReport(string report, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName + ".txt"))
                {
                    sw.Write(report);
                }
                Console.WriteLine("Report exported successfully.");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine();
            }
        }

        public static void ExportTaskList(User user, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName + ".csv"))
                {
                    foreach (UserTask task in user.TaskList.GetTasks())
                    {
                        string title = $"\"{task.Title}\"";
                        string description = $"\"{task.Description}\"";
                        string tags = $"\"{string.Join(";", task.Tags)}\"";

                        string line = $"{task.Id},{title},{description},{task.DueDate},{task.Priority},{task.Status},{tags}";
                        sw.WriteLine(line);
                    }
                }
                Console.WriteLine("Task list exported successfully.");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine();
            }
        }
    }
}
