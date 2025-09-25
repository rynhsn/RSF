using BlazorClientHelper;
using PMM01000COMMON;
using PMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Attributes;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Helpers;

namespace PMM01000FRONT
{
    public partial class PMM01002 : R_Page
    {
        private PMM01002ViewModel _viewModel = new PMM01002ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] private R_ILocalizer<PMM01000FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private R_ComboBox<PMM01000UniversalDTO, string> Charges_Type;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Property = (PMM01000DTOPropety)poParameter;
                await PrintUtility_RateType_ServiceGetListRecord();

                await Charges_Type.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task PrintUtility_RateType_ServiceGetListRecord()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new PMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _viewModel.GetRateTypeList(loParam);

                if (_viewModel.RateTypeList.Count > 0)
                {
                    var loData = _viewModel.RateTypeList.FirstOrDefault();
                    _viewModel.Data.CCHARGE_TYPE_FROM = loData.CCODE;
                    _viewModel.Data.CCHARGE_TYPE_TO = loData.CCODE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            bool loValidate = false;

            try
            {
                var loParam = new PMM01000PrintParamDTO();
                loParam = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(loParam.CCHARGE_TYPE_FROM))
                {
                    loEx.Add("", _localizer["_ValidatePrintParamFrom"]);
                    loValidate = true;
                }

                if (string.IsNullOrWhiteSpace(loParam.CCHARGE_TYPE_TO))
                {
                    loEx.Add("", _localizer["_ValidatePrintParamTo"]);
                    loValidate |= true;
                }

                if (loValidate == false)
                {
                    // set Param
                    loParam.CCOMPANY_ID = clientHelper.CompanyId;
                    loParam.CUSER_ID = clientHelper.UserId;
                    loParam.CPROPERTY_ID = _viewModel.Property.CPROPERTY_ID;
                    loParam.CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME;
                    loParam.CREPORT_CULTURE = clientHelper.ReportCulture;

                    await _reportService.GetReport(
                        "R_DefaultServiceUrlPM",
                        "PM",
                        "rpt/PMM01000Print/AllUtilityChargesPost",
                        "rpt/PMM01000Print/AllStreamUtilityChargesGet",
                        loParam);

                    await this.Close(true, true);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
