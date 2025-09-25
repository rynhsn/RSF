using LMM07500MODEL;
using PMM07500COMMON;
using PMM07500COMMON.DTO_s;
using PMM07500COMMON.DTO_s.stamp_code;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMM07500MODEL.View_Models
{
    public class PMM07500ViewModel : R_ViewModel<PMM07500GridDTO>
    {
        private PMM07500Model _model = new PMM07500Model();
        private PMM07500InitModel _initModel = new PMM07500InitModel();
        public ObservableCollection<PMM07500GridDTO> StampRateList { get; set; } = new ObservableCollection<PMM07500GridDTO>();

        public PMM07500GridDTO StampRate { get; set; } = new PMM07500GridDTO();
        public List<PropertyDTO> PropertyList { get; set; } = new List<PropertyDTO>();
        public List<CurrencyDTO> CurrencyList { get; set; } = new List<CurrencyDTO>();
        public string PropertyId { get; set; } = "";

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _initModel.GetPropertyListAsync();
                if (loResult.Count > 0)
                {
                    PropertyList = new List<PropertyDTO>(loResult);
                    PropertyId = PropertyList.FirstOrDefault().CPROPERTY_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _initModel.GetCurrencyListAsync();
                if (loResult.Count > 0)
                {
                    foreach (var item in loResult)
                    {
                        item.CCURRENCY_DISPLAY = $"{item.CCURRENCY_CODE} - {item.CCURRENCY_NAME}";
                    }
                    CurrencyList = new List<CurrencyDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStampCodeListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMM07500ContextConstant.CPROPERTY_ID, PropertyId);
                var loResult = await _model.GetStampListAsync();

                foreach (var item in loResult)
                {
                    item.CCURRENCY_DISPLAY = $"{item.CCURRENCY_CODE} - {item.CCURRENCY_NAME}";
                }

                StampRateList = new ObservableCollection<PMM07500GridDTO>(loResult)??new ObservableCollection<PMM07500GridDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStampCodeRecordAsync(PMM07500GridDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                loResult.CCURRENCY_DISPLAY = $"{loResult.CCURRENCY_CODE} - {loResult.CCURRENCY_NAME}";

                StampRate = R_FrontUtility.ConvertObjectToObject<PMM07500GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveStampCodeAsync(PMM07500GridDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                poParam.CCURRENCY_CODE= poParam.CCURRENCY_DISPLAY.Split(" - ")[0];
                switch (poCRUDMode)
                {
                    case eCRUDMode.NormalMode:
                        break;
                    case eCRUDMode.AddMode:
                        poParam.CACTION = "NEW";
                        poParam.CREC_ID = "";
                        break;
                    case eCRUDMode.EditMode:
                        poParam.CACTION = "EDIT";
                        break;
                    case eCRUDMode.DeleteMode:
                        break;
                    default:
                        break;
                }
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                loResult.CCURRENCY_DISPLAY = $"{loResult.CCURRENCY_CODE} - {loResult.CCURRENCY_NAME}";
                StampRate = R_FrontUtility.ConvertObjectToObject<PMM07500GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteStampCodeAsync(PMM07500GridDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CPROPERTY_ID = PropertyId;
                await _model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
