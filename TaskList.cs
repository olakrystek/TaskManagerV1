using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class TaskList
    {
        private readonly List<UserTask> _tasks;
        private ITaskSortingStrategy _sortingStrategy; // The current sorting strategy
        private ITaskFilteringStrategy _filteringStrategy; // The current filtering strategy
        private static int _lastId; // The last used id for tasks in this list

        public TaskList()
        {
            _tasks = new List<UserTask>();
            _lastId = 0;

        }

        public static int GenerateNewId()
        {
            _lastId++;
            return _lastId;
        }

        public void UpdateLastId()
        {
            if (_tasks.Any())
            {
                _lastId = _tasks.Max(t => t.Id);
            }
        }

        public void AddTask(UserTask task)
        {
            _tasks.Add(task);
        }

        public void RemoveTask(UserTask task)
        {
            _tasks.Remove(task);
        }

        public void EditTask(int id, string title, string description, DateTime dueDate, TaskPriority priority, TaskStatus status, List<string> tags)
        {
            UserTask task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.Title = title;
                task.Description = description;
                task.DueDate = dueDate;
                task.Priority = priority;
                task.Status = status;
                task.Tags = tags;
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

        }

        public void DisplayAllTasks()
        {
            foreach (UserTask task in _tasks)
            {
                task.DisplayTask();
                Console.WriteLine();
            }
        }

        public List<UserTask> GetTasks()
        {
            return _tasks;
        }

        public List<UserTask> GetTasksDueToday()
        {
            return _tasks.Where(t => t.IsDueToday()).ToList();
        }

        // Set the sorting strategy for the list of tasks
        public void SetSortingStrategy(ITaskSortingStrategy strategy)
        {
            _sortingStrategy = strategy;
        }

        // Set the filtering strategy for the list of tasks
        public void SetFilteringStrategy(ITaskFilteringStrategy strategy)
        {
            _filteringStrategy = strategy;
        }

        public IEnumerable<UserTask> ApplyStrategies()
        {
            IEnumerable<UserTask> result = _tasks;

            if (_sortingStrategy != null)
            {
                result = _sortingStrategy.Sort(result);
            }

            if (_filteringStrategy != null)
            {
                result = _filteringStrategy.Filter(result);
            }

            return result;
        }

    }
}
