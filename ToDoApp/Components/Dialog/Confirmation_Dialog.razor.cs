using Microsoft.AspNetCore.Components;
using MudBlazor;
using Color = MudBlazor.Color;

namespace ToDoApp.Components.Dialog
{
    public partial class Confirmation_Dialog
    {
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        [Parameter] public string ContentText { get; set; } = string.Empty;

        [Parameter] public string ButtonText { get; set; } = string.Empty;

        [Parameter] public Color Color { get; set; }

        void Submit() => MudDialog!.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog!.Cancel();
    }
}
