using Microsoft.AspNetCore.Components;
using PMI00300COMMON.DrillDownParameter;
using PMI00300COMMON.DTO;
using PMI00300MODEL.ViewModel;
using PMT00300Front;
using PMT00500FRONT;
using PMT01300FRONT;
using PMT01600Front;
using PMT01900Front;
using PMT02500Front;
using PMT02600FRONT;
using PMT02700Front;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Extensions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMI00300FRONT
{
    public partial class PMI00300AgreementForm : R_Page
    {
        [Inject] private R_ILocalizer<PMI00300FrontResources.Resources_Dummy_Class> _localizer { get; set; } = null!;

        private PMI00300AgreementFormViewModel loViewModel = new();
        private R_ConductorGrid _conductorGridRef = new();
        private R_Grid<PMI00300GetAgreementFormListDTO> _gridRef = new();

        private PMI00300AgreementFormDetailParameterDTO loTabParam = new();
        private int PageSize = 10;
        private bool IsDrillDownEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loTabParam = (PMI00300AgreementFormDetailParameterDTO)poParameter;
                loViewModel.loDisplayProcess = R_FrontUtility.ConvertObjectToObject<PMI00300AgreementFormDisplayProcessDTO>(loTabParam);
                await loViewModel.InitialProcess();
                await _gridRef.R_RefreshGrid(null);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region BUTTON METHODS

        private async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Detail_DrillDown(R_BeforeOpenDetailEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                DrillDownParameterDTO loDrillDownParameter = new DrillDownParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID,
                    CDEPT_CODE = loViewModel.loSelectedAgreementForm.CDEPT_CODE,
                    CREF_NO = loViewModel.loSelectedAgreementForm.CAGREEMENT_NO,
                    CTRANS_CODE = loViewModel.loSelectedAgreementForm.CTRANS_CODE.Trim(),
                    CALLER_ACTION = "VIEW",
                    CCALLER_ACTION = "VIEW",
                };

                eventArgs.FormAccess = R_eFormAccess.View.ToDescription();

                eventArgs.Parameter = loDrillDownParameter;

                switch (loDrillDownParameter.CTRANS_CODE)
                {
                    case "802030":
                        eventArgs.TargetPageType = typeof(PMT02500Agreement);
                        break;

                    case "802032":
                        eventArgs.TargetPageType = typeof(PMT02610);
                        break;

                    case "802033":
                        eventArgs.TargetPageType = typeof(PMT02700Agreement);
                        break;

                    case "802061":
                        eventArgs.TargetPageType = typeof(PMT01310);
                        break;

                    case "802062":
                        eventArgs.TargetPageType = typeof(PMT01600LOI);
                        break;

                    case "802063":
                        eventArgs.TargetPageType = typeof(PMT01900LOI);
                        break;

                    case "802010":
                        eventArgs.TargetPageType = typeof(PMT00300LOIInfo_UnitInfo);
                        break;

                    case "802013":
                        eventArgs.TargetPageType = typeof(PMT00300LOIInfo_UnitInfo);
                        break;

                    case "802020":
                        eventArgs.TargetPageType = typeof(PMT00510);
                        break;

                    default:
                        R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayExceptionAsync(loEx);
        }

        private void R_After_Open_Detail_DrillDown(R_AfterOpenDetailEventArgs eventArgs)
        {
        }

        private void R_Before_Open_Popup_HandOver(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new PMI00300HandOverChecklistParamDTO()
                {
                    CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID.Trim(),
                    CHO_DEPT_CODE = loViewModel.loSelectedAgreementForm.CHO_DEPT_CODE.Trim(),
                    CHO_REF_NO = loViewModel.loSelectedAgreementForm.CHO_REF_NO.Trim(),
                    CHO_TRANS_CODE = loViewModel.loSelectedAgreementForm.CHO_TRANS_CODE.Trim(),
                    CUNIT_ID = loViewModel.loDisplayProcess.CUNIT_ID.Trim(),
                    CBUILDING_ID = loViewModel.loDisplayProcess.CBUILDING_ID.Trim(),
                    CFLOOR_ID = loViewModel.loDisplayProcess.CFLOOR_ID.Trim(),
                };
                eventArgs.PageTitle = _localizer["_Hochecklist"];
                eventArgs.TargetPageType = typeof(PMI00300HandoverChecklist);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayExceptionAsync(loEx);
        }

        #endregion BUTTON METHODS

        #region GRID METHOD

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await loViewModel.GetAgreementFormListAsync();
                if (loViewModel.loAgreementFormList.Count == 0)
                {
                    loViewModel.loSelectedAgreementForm = new PMI00300GetAgreementFormListDTO();
                    await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.OK);
                    IsDrillDownEnabled = false;
                }
                else if (loViewModel.loAgreementFormList.Count > 0)
                {
                    loViewModel.loSelectedAgreementForm = loViewModel.loAgreementFormList.FirstOrDefault() ?? new();
                    IsDrillDownEnabled = true;
                }
                eventArgs.ListEntityResult = loViewModel.loAgreementFormList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    loViewModel.loSelectedAgreementForm = eventArgs.Data != null ? (PMI00300GetAgreementFormListDTO)eventArgs.Data : new();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion GRID METHOD
    }
}