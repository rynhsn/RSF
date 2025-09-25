using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using GLB00700FrontResources;

namespace GLB00700FRONT
{
    public partial class GLB00701 : R_Page
    {
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        protected override Task R_Init_From_Master(object poParameter)
        {
            return base.R_Init_From_Master(poParameter);
        }
        public async Task Onclick_YesButtonAsync()
        {
            await Close(true, null);
        }
        public async Task Onclick_NoButtonAsync()
        {
            await Close(false, null);
        }
    }
}
