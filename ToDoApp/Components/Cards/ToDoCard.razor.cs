using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToDoApp.Components.Dialog;
using ToDoApp.Models;
using ToDoApp.Services;
using Color = MudBlazor.Color;



namespace ToDoApp.Components.Cards
{
    public partial class ToDoCard
    {
        [Inject]
        public IDialogService? DialogService { get; set; }
        [Inject]
        public IToDoService? tds {get; set;}
        [Inject]
        public ISnackbar? Snackbar { get; set; }
        [Inject]
        public  NavigationManager? navigationManager { get; set; }
        [Parameter]
        public EventCallback OnRefresh { get; set; }           
        [Parameter]
        public ToDo? todo { get; set; }

        public Double Progress { get; set; } = 0;
        private async Task DeleteToDo()
        {
            var parameters = new DialogParameters<Confirmation_Dialog>
            {
                { x => x.ContentText, $"Are you sure you want to delete the todo item, {todo!.Name}?" },
                { x => x.ButtonText, "Delete" },
                { x => x.Color, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = false };

            var dialog = await DialogService!.ShowAsync<Confirmation_Dialog>("Delete", parameters, options);
            var result = await dialog.Result;

            if (result.Canceled)
            {
                return;
            }
            if (tds!.DeleteToDobyId(todo.Id))
            {                               
                UpdateProgress();
                await OnRefresh.InvokeAsync();
            }
            else
            {
                Snackbar!.Add("Error Deleting ToDo!", Severity.Error);
            }
        }

        private void UpdateProgress()
        {
            Double total = todo!.SubTasks.Count();
            Double amountTasksComplete = todo!.SubTasks.Count(t => t.isComplete == true);

            if(total > 0)
            {
                Progress = Convert.ToInt32((amountTasksComplete / total) * 100.00);
                StateHasChanged();
            }
             
        }

        private async Task EditToDo()
        {
            var parameters = new DialogParameters<AddToDo_Dialog>
            {
                { x => x.todo, todo }
            };
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, MaxWidth = MaxWidth.Medium, NoHeader = true };

            var dialog = await DialogService!.ShowAsync<AddToDo_Dialog>("Update ToDo Item", parameters, options);
            var result = await dialog.Result;

            if (result.Canceled)
            {
                return;
            }
            if (tds!.UpdateToDo((ToDo)result.Data))
            {
                UpdateProgress();
                await OnRefresh.InvokeAsync();
            }
            else 
            {
                Snackbar!.Add("Error Updating ToDo!", Severity.Error);
            }
        }

        private async Task ChangeTaskCompletion(SubTask task, bool value)
        {

            todo!.SubTasks.Find(x => x.Id == task.Id)!.isComplete = value;
         
            if (tds!.UpdateToDo(todo!))
            {
                UpdateProgress();
                await OnRefresh.InvokeAsync();
            }

            
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UpdateProgress();
        }
    }
}
