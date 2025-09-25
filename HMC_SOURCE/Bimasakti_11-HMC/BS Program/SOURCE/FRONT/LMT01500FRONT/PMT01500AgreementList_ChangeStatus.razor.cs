using BlazorClientHelper;
using PMT01500Common.Utilities;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.MessageBox;
using PMT01500Common.DTO._1._AgreementList;

namespace PMT01500Front
{
    public partial class PMT01500AgreementList_ChangeStatus
    {
        private PMT01500AgreementListViewModel _viewModel = new();
        [Inject] private IClientHelper? _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.loParameterChangeStatus = R_FrontUtility.ConvertObjectToObject<PMT01500GetHeaderParameterDTO>(poParameter);

                if (!string.IsNullOrEmpty(_viewModel.loParameterChangeStatus.CREF_NO))
                {
                    await _viewModel.GetChangeStatus();
                    await _viewModel.GetComboBoxDataCTransStatus();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Control Function

        private async Task OnClickCloseButton()
        {
            await Close(true, false);
        }


        private async Task OnClickSAveButton()
        {
            var loEx = new R_Exception();

            try
            {
                PMT01500ChangeStatusParameterDTO loParam = new PMT01500ChangeStatusParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.loParameterChangeStatus.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.loParameterChangeStatus.CDEPT_CODE,
                    CTRANS_CODE = _viewModel.loParameterChangeStatus.CTRANS_CODE,
                    CREF_NO = _viewModel.loParameterChangeStatus.CREF_NO,
                    CUSER_ID = _clientHelper.UserId,
                };

                await _viewModel.ProcessChangeStatus(loParam);
                await R_MessageBox.Show("", "Status Changed", R_eMessageBoxButtonType.OK);
                await Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BlankFunction()
        {
            var loException = new R_Exception();

            try
            {
                await R_MessageBox.Show("", "This Function still on Development Process", R_eMessageBoxButtonType.OK);
                //var llTrue = await R_MessageBox.Show("", "You Clicked the Button!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        
        
        #endregion



    }
}
