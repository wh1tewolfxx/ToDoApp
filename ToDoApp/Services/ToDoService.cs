using LiteDB;
using System.Diagnostics;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface IToDoService
    {
        public BsonValue AddToDo(ToDo todo);
        public bool UpdateToDo(ToDo todo);
        IEnumerable<ToDo> GetAllToDos();
        bool DeleteToDobyId(int id);
    }
    internal class ToDoService : IToDoService
    {
        private readonly LiteDatabase _database;
        private readonly ILiteCollection<ToDo> _todos;
        private readonly ConnectionString _connectionString;

        public ToDoService(string filePath)
        {
            Debug.WriteLine($"FilePath: {filePath}");

            _connectionString = new ConnectionString(filePath)
            {
                Connection = ConnectionType.Shared
            };
            _database = new LiteDatabase(_connectionString);
            //_database.GetCollection<ToDo>("todos").DeleteAll();
            _todos = _database.GetCollection<ToDo>("todos");
        }

        public BsonValue AddToDo(ToDo todo)
        {
            return _todos.Insert(todo);
        }

        public bool DeleteToDobyId(int id)
        {
            return _todos.Delete(id);
        }

        public IEnumerable<ToDo> GetAllToDos()
        {
            return _todos.FindAll();
        }

        public bool UpdateToDo(ToDo todo)
        {
            return _todos.Update(todo);
        }
    }
}
