using Microsoft.AspNetCore.Components;
using PMI00300COMMON.DTO;
using PMI00300MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMI00300FRONT
{
    public partial class PMI00300HandoverChecklist : R_Page
    {
        [Inject] private R_ILocalizer<PMI00300FrontResources.Resources_Dummy_Class> _localizer { get; set; } = null!;
        private PMI00300HandOverChecklist_ViewModel _viewModel = new();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poParameter != null)
                {
                    _viewModel.loParam = R_FrontUtility.ConvertObjectToObject<PMI00300HandOverChecklistParamDTO>(poParameter) ?? new();
                    await _viewModel.GetList_HandOverChecklist(_localizer);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
    }
}
