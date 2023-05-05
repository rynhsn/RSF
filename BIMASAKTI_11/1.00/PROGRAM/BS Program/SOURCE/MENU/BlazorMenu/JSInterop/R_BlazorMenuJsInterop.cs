using Microsoft.JSInterop;

namespace BlazorMenu.JSInterop
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class R_BlazorMenuJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public R_BlazorMenuJsInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "/js/R_BlazorMenuJsInterop.js").AsTask());
        }

        public async ValueTask<string> Prompt(string message)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        public async Task SelectTextHandler(string id)
        {
            var module = await _moduleTask.Value;

            await module.InvokeVoidAsync("selectText", id);
        }

        public async Task setValueByIdHandler(object id, object value)
        {
            var module = await _moduleTask.Value;

            await module.InvokeVoidAsync("setValueById", id, value);
        }

        public async Task scrollToSelectedRowHandler(object gridSelector)
        {
            var module = await _moduleTask.Value;

            await module.InvokeVoidAsync("scrollToSelectedRow", "." + gridSelector);
        }
    }
}