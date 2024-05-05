using LiteDB;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class SubTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Task { get; set; } = string.Empty;
        public bool isComplete { get; set; } = false;
    }
    public enum Priority
    {
        Low,
        Medium,
        High,
        Hot
    }
    public class ToDo
    {
        [BsonId(true)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SubTask> SubTasks { get; set; } = [];
        public Priority Priority { get; set; } = Priority.Low;
    }
}
