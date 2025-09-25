using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMModel.ViewModel;
using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using PMM09000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000MODEL.ViewModel
{
    public class PMM09000AmortizationViewModel : R_ViewModel<PMM09000AmortizationDTO>
    {
        private PMM09000ListModel _GetListModel = new PMM09000ListModel();
        private GlobalFunctionViewModel _viewModelGlobal = new GlobalFunctionViewModel();
        private PMM09000CRUDModel _CRUDModel = new PMM09000CRUDModel();
        public List<PropertyDTO> _PropertyList =
           new List<PropertyDTO>();
        public ObservableCollection<PMM09000AmortizationDTO> _AmortizationList =
            new ObservableCollection<PMM09000AmortizationDTO>();
        public ObservableCollection<PMM09000AmortizationSheduleDetailDTO> _AmortizationScheduleList =
            new ObservableCollection<PMM09000AmortizationSheduleDetailDTO>();
        public ObservableCollection<PMM09000BuildingDTO> _BuildingList =
            new ObservableCollection<PMM09000BuildingDTO>();
        public ObservableCollection<ChargesDTO> _ChargesList =
            new ObservableCollection<ChargesDTO>();

        public List<RadionButtonDTO> _UnitOptionList = new List<RadionButtonDTO>
                {
                    new RadionButtonDTO { Id = "U", Name = R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), $"_labelUnit")
                },
                    new RadionButtonDTO { Id = "O",  Name = R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), $"_labelOtherUnit")
                }
                };
        public PropertyDTO _PropertyValue = new PropertyDTO();
        public PMM09000BuildingDTO _BuildingValue = new PMM09000BuildingDTO();
        public PMM09000AmortizationDTO _AmortizationValue = new PMM09000AmortizationDTO();
        public string _UnitOptionValue = "U";

        public bool lPropertyExist = true;
        public bool _dropdownProperty = true;
        public bool _enabledTabEntry = true;
        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO>? loReturn = new List<PropertyDTO>();
            try
            {
                List<PropertyDTO> loResult = await _viewModelGlobal.GetPropertyList();
                if (loResult.Any())
                {
                    _PropertyList = loResult;
                    _PropertyValue = _PropertyList[0];
                    lPropertyExist = true;
                    _enabledTabEntry = true;
                }
                else
                {
                    lPropertyExist = false;
                    _enabledTabEntry = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #region Building

        public async Task GetBuildingList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _PropertyValue.CPROPERTY_ID);
                var loResult = await _GetListModel.GetBuldingListAsyncModel();
                _BuildingList = new ObservableCollection<PMM09000BuildingDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion

        public async Task GetAmortizationList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _PropertyValue.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_OPTION, _UnitOptionValue);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, _BuildingValue.CBUILDING_ID);
                var loResult = await _GetListModel.GetAmortizationListAsyncModel();

                if (loResult.Data.Any())
                {
                    loResult.Data = loResult.Data!.Select(item =>
                    {
                        item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!);
                        item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!);
                        return item;
                    }).ToList();

                }
                _AmortizationList = new ObservableCollection<PMM09000AmortizationDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Amortization Schedule
        public async Task GetAmortizationScheduleList(PMM09000DbParameterDTO loParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, loParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_OPTION, loParameter.CUNIT_OPTION);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, loParameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_TYPE, loParameter.CTRANS_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, loParameter.CREF_NO);

                var loResult = await _GetListModel.GetAmortizationScheduleListAsyncModel();
                if (loResult.Data.Any())
                {
                    loResult.Data = loResult.Data!.Select(item =>
                    {
                        item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!);
                        item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!);
                        return item;
                    }).ToList();

                }
                _AmortizationScheduleList = new ObservableCollection<PMM09000AmortizationSheduleDetailDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion
        #region Amortization ChargesType
        public async Task GetChargesTypeList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _GetListModel.GetChargesTypeListAsyncModel();
                if (loResult.Data.Any())
                {
                    _ChargesList = new ObservableCollection<ChargesDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion
        #region Button Close-Reopen
        public async Task<UpdateStatusDTO> ProcessUpdateAmortization(PMM09000DbParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            UpdateStatusDTO loReturn = new UpdateStatusDTO();
            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {
                    var loResult = await _CRUDModel.UpdateAmortizationStatusAsyncModel(poParameter);
                    loReturn = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
        #endregion
        private DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
            }
            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
            return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
        }
    }
}
