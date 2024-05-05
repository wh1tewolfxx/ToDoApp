using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToDoApp.Components.Dialog;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public IDialogService? DialogService { get; set; }
        [Inject]
        public IToDoService? tds { get; set; }
        [Inject]
        public ISnackbar? Snackbar { get; set; }

        private IEnumerable<ToDo>? todos { get; set; }
        protected override void OnInitialized()
        {
            todos = tds!.GetAllToDos();
        }

        private async Task OpenAddToDoDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, MaxWidth = MaxWidth.Medium, NoHeader = true };

            var dialog = await DialogService!.ShowAsync<AddToDo_Dialog>("Add ToDo Item", options);
            var result = await dialog.Result;

            if (result.Canceled)
            {
                return;
            }

            if (tds!.AddToDo((ToDo)result.Data) is null)
            {
                Snackbar!.Add("Error Adding ToDo!", Severity.Error);
            }
        }
        private void Refresh()
        {
            StateHasChanged();
        }
    }
}