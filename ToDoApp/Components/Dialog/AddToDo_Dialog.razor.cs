using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToDoApp.Models;

namespace ToDoApp.Components.Dialog
{
    public partial class AddToDo_Dialog
    {
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        [Parameter] public ToDo? todo { get; set; } = new();

        private int id { get; set; }
        private string name { get; set; } = string.Empty;
        private string description { get; set; } = string.Empty;
        private Priority priority { get; set; } = Priority.Low;
        private List<SubTask> subTask { get; set; } = [];

        protected override void OnInitialized()
        {
            if (todo is not null)
            {
                id = todo!.Id;
                name = todo!.Name;
                description = todo!.Description;
                priority = todo!.Priority;
                subTask = todo!.SubTasks;
            }

        }

        void Submit()
        {
            todo = new()
            {
                Id = id,
                Name = name,
                Description = description,
                Priority = priority,
                SubTasks = subTask

            };
            MudDialog!.Close(DialogResult.Ok(todo));
        }
        void Cancel() => MudDialog!.Cancel();

        private void AddSubTask()
        {
            subTask.Add(new SubTask());
        }

        private void RemoveSubTask(SubTask task)
        {
            subTask.Remove(task);
        }

        private void SubTaskValueChanged(SubTask task, string value)
        {

            todo!.SubTasks.Find(x => x.Id == task.Id)!.Task = value;
        }
    }
}
