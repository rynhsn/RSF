using PMM00200COMMON;
using PMM00200COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMM00200MODEL
{
    public class PMM00200ViewModel : R_ViewModel<PMM00200DTO>
    {
        private PMM00200Model _model = new PMM00200Model();

        public ObservableCollection<PMM00200GridDTO> _UserParamList { get; set; } = new ObservableCollection<PMM00200GridDTO>();
        public PMM00200DTO _UserParam { get; set; } = new PMM00200DTO();

        public string _CUserOperatorSign { get; set; } = "";
        public List<RadioModel> _Options { get; set; } = new List<RadioModel>
        {
            new RadioModel { Value = "=", Text = "(=)" },
            new RadioModel { Value= ">=", Text = "(>=)" },
        };
        public string _UserParamCode { get; set; }
        public bool _Active { get; set; }
        public string _Action { get; set; }

        public async Task GetUserParamList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetUserParamListAsync();
                _UserParamList = new ObservableCollection<PMM00200GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUserParamRecord(PMM00200DTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                _UserParam = R_FrontUtility.ConvertObjectToObject<PMM00200DTO>(loResult);
                _CUserOperatorSign = _UserParam.CUSER_LEVEL_OPERATOR_SIGN;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUserParam(PMM00200DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _UserParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync(ActiveInactiveParam poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.LACTIVE = _Active;
                poParam.CACTION = _Action;
                await _model.GetActiveParamAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }

    public class RadioModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
