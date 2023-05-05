using BlazorMenu.Shared.Tabs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Menu;

namespace BlazorMenu.Shared
{
    public partial class MainBody : R_IMainBody
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        public List<R_Tab> Tabs { get; set; } = new();

        [CascadingParameter(Name = "currentTabMenu")]
        public MenuTab CurrentTab { get; set; }

        private int ActiveTabIndex;

        protected override void OnInitialized()
        {
            if (CurrentTab != null)
                R_AddTab(CurrentTab.Title, CurrentTab.Body, plSetActive: true, peFormModel: R_eFormModel.MainForm, pcFormAccess: CurrentTab.Access);
        }

        public void R_AddTab(string pcTitle, RenderFragment poBody, bool plSetActive = false, bool plCloseable = false, object poParameter = null, R_eFormModel peFormModel = R_eFormModel.None, string pcFormAccess = "V")
        {
            Tabs.ForEach(x =>
            {
                x.IsActive = false;
            });

            R_Tab loNewTab = null;
            var selTab = Tabs.FirstOrDefault(m => m.Title == pcTitle || string.IsNullOrEmpty(m.Title));
            if (selTab == null)
            {
                loNewTab = new R_Tab
                {
                    Title = pcTitle,
                    IsActive = true,
                    Body = poBody,
                    Closeable = plCloseable,
                    Parameter = poParameter,
                    FormModel = peFormModel,
                    Access = pcFormAccess
                };

                Tabs.Add(loNewTab);
            }
            else
            {
                if (string.IsNullOrEmpty(selTab.Title))
                    selTab.Title = pcTitle;

                selTab.IsActive = true;
            }

            if (plSetActive && loNewTab != null)
            {
                ActivateTab(loNewTab);
            }

            if (!plSetActive && loNewTab != null)
            {
                StateHasChanged();
            }
        }

        private async Task OnTabClose(R_Tab poTab)
        {
            if (poTab.Close != null)
            {
                var llResult = await poTab.Close(true, null);

                if (!llResult)
                    return;
            }

            var liTabIndex = Tabs.IndexOf(poTab);
            Tabs.Remove(poTab);

            if (poTab.IsActive)
            {
                R_Tab activeTab = null;
                if (liTabIndex > 0)
                    activeTab = Tabs[liTabIndex - 1];
                else if (Tabs.Count > 0)
                    activeTab = Tabs[0];

                if (activeTab != null)
                    await ActivateTab(activeTab);
            }
        }

        private Task ActivateTab(R_Tab poTab)
        {
            if (ActiveTabIndex != Tabs.IndexOf(poTab))
            {
                ActiveTabIndex = Tabs.IndexOf(poTab);
                StateHasChanged();
            }

            return Task.CompletedTask;
        }
    }
}
