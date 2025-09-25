using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using PMM09000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000MODEL.ViewModel
{
    public class PMM09000AmortizationEntryViewModel : R_ViewModel<PMM09000EntryHeaderDetailDTO>
    {
        private PMM09000CRUDModel _CRUDModel = new PMM09000CRUDModel();
        private PMM09000ListModel _ListModel = new PMM09000ListModel();

        public PMM09000DbParameterDTO oParameter = new PMM09000DbParameterDTO();
        public List<PeriodMonthDTO>? GetMonthList;
        PMM09000EntryHeaderDTO _AmortizationDetail = new PMM09000EntryHeaderDTO(); //optional

        public PMM09000EntryHeaderDetailDTO _EntityHeaderDetail = new PMM09000EntryHeaderDetailDTO();

        public ObservableCollection<PMM09000AmortizationChargesDTO> _AmortizationChargesList =
       new ObservableCollection<PMM09000AmortizationChargesDTO>();

        public ObservableCollection<PMM09000AmortizationChargesDTO> _TempAmortizationChargesList =
       new ObservableCollection<PMM09000AmortizationChargesDTO>();
        public List<ChargesDTO> _ChargesList = new List<ChargesDTO>();
        public PMM09000AmortizationChargesDTO _oAmorCharges = new PMM09000AmortizationChargesDTO();

        public List<RadionButtonDTO> _UnitOptionList = new List<RadionButtonDTO>
                {
                    new RadionButtonDTO { Id = "U", Name = R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), $"_labelUnit")
                },
                    new RadionButtonDTO { Id = "O",  Name = R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), $"_labelOtherUnit")
                }
                };
        public string _UnitOptionValueEntry = "U";
        public string _ChargeTypeValue = "";
        #region HeaderDetail
        public async Task GetEntityHeaderDetail(PMM09000EntryHeaderDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                //Result From Back
                var loResult = await _CRUDModel.R_ServiceGetRecordAsync(poEntity);
                if (loResult != null)
                {
                    loResult.Header.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.Header.CSTART_DATE!);
                    loResult.Header.DEND_DATE = ConvertStringToDateTimeFormat(loResult.Header.CEND_DATE!);
                    loResult.Header.CBUILDING_ID = loResult.Header.CBUILDING_ID;

                    if (!string.IsNullOrEmpty(loResult?.Header?.CREF_NO))
                    {
                        loResult.Header.CREF_NO = loResult.Header.CREF_NO.Trim();
                    }
                    PMM09000DbParameterDTO loParam = new PMM09000DbParameterDTO()
                    {
                        CPROPERTY_ID = poEntity.Header.CPROPERTY_ID,
                        CUNIT_OPTION = poEntity.Header.CUNIT_OPTION,
                        CBUILDING_ID = poEntity.Header.CBUILDING_ID,
                        CTRANS_TYPE = poEntity.Header.CTRANS_TYPE,
                        CREF_NO = poEntity.Header.CREF_NO
                    };

                    var listAmortizationCharges = loResult.Details; //await GetAmortizationChargesList(loParam);
                    if (listAmortizationCharges.Any())
                    {
                        listAmortizationCharges = listAmortizationCharges.Select(item =>
                        {
                            item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!);
                            item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!);
                            return item;
                        }).ToList();
                    }
                    _AmortizationChargesList = new ObservableCollection<PMM09000AmortizationChargesDTO>(listAmortizationCharges);

                    _EntityHeaderDetail.Header = loResult.Header;
                    _EntityHeaderDetail.Details = listAmortizationCharges;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion
        #region CRUDBASE
        public async Task ServiceDelete(PMM09000EntryHeaderDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = poEntity.Header;
                loData.CSTART_DATE = ConvertDateTimeToStringFormat(loData.DSTART_DATE);
                loData.CEND_DATE = ConvertDateTimeToStringFormat(loData.DEND_DATE);
                loData.CCUT_OF_PRD = loData.IPERIOD_YEAR.ToString() + loData.CPERIOD_MONTH;

                poEntity.Details = _AmortizationChargesList.ToList();
                var loAmorDetail = poEntity.Details;
                foreach (var item in loAmorDetail)
                {
                    item.CSTART_DATE = ConvertDateTimeToStringFormat(item.DSTART_DATE);
                    item.CEND_DATE = ConvertDateTimeToStringFormat(item.DEND_DATE);

                }
                await _CRUDModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMM09000EntryHeaderDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.Header.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.Header.DSTART_DATE);
                poNewEntity.Header.CEND_DATE = ConvertDateTimeToStringFormat(poNewEntity.Header.DEND_DATE);

                if (poNewEntity.Header.IPERIOD_YEAR.ToString() != "0")
                {
                    poNewEntity.Header.CCUT_OF_PRD = poNewEntity.Header.IPERIOD_YEAR.ToString() + poNewEntity.Header.CPERIOD_MONTH;

                }
                else
                {
                    poNewEntity.Header.CCUT_OF_PRD = "";
                }

                poNewEntity.Details = _AmortizationChargesList.ToList();

                var loAmorDetail = poNewEntity.Details;
                foreach (var item in loAmorDetail)
                {
                    item.CSTART_DATE = ConvertDateTimeToStringFormat(item.DSTART_DATE);
                    item.CEND_DATE = ConvertDateTimeToStringFormat(item.DEND_DATE);
                }

                var loResult = await _CRUDModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                if (loResult != null)
                {
                    loResult.Header.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.Header.CSTART_DATE!);
                    loResult.Header.DEND_DATE = ConvertStringToDateTimeFormat(loResult.Header.CEND_DATE!);

                }
                _EntityHeaderDetail = loResult!;

                var loTempAmorDetail = loResult.Details;
                foreach (var item in loTempAmorDetail)
                {
                    item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!);
                    item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!);
                }
                _AmortizationChargesList = new ObservableCollection<PMM09000AmortizationChargesDTO>(loTempAmorDetail);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Validation
        public void ValidationHeader(PMM09000EntryHeaderDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loDataHeader = poEntity.Header;

                if (string.IsNullOrEmpty(loDataHeader.CPROPERTY_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationProperty");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(loDataHeader.CBUILDING_ID) && loDataHeader.CUNIT_OPTION == "U")
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationBuilding");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(loDataHeader.CTENANT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationTenant");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(loDataHeader.CAMORTIZATION_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationAmortizationNo");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(loDataHeader.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationDepartment");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(loDataHeader.CREF_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationLOI");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(loDataHeader.CTRANS_DEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationDepartmentTransaction");
                    loEx.Add(loErr);
                }
                if (loDataHeader.DSTART_DATE == null && loDataHeader.LCUT_OFF_PRD)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationStartDate");
                    loEx.Add(loErr);
                }
                if (loDataHeader.DEND_DATE == null && loDataHeader.LCUT_OFF_PRD)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationEndDate");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationDetail(PMM09000AmortizationChargesDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(poEntity.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationCharge");
                    loEx.Add(loErr);
                }
                if (poEntity.DSTART_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationStartDateGrid");
                    loEx.Add(loErr);
                }
                if (poEntity.DEND_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationEndDateGrid");
                    loEx.Add(loErr);
                }
                if (poEntity.DSTART_DATE > poEntity.DEND_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationStartDateMoreThanEndDate");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Amortization Schedule
        public void GetAmortizationChargesList()
        {
            R_Exception loException = new R_Exception();
            try
            {

                var loDataDetails = _EntityHeaderDetail.Details;
                _AmortizationChargesList = new ObservableCollection<PMM09000AmortizationChargesDTO>(loDataDetails);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public void GetAmorCharges(PMM09000AmortizationChargesDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var temp = poEntity;
                _oAmorCharges = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region ChargesType
        public async Task ChargesTypeList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _ListModel.GetChargesTypeListAsyncModel();
                if (loResult.Data.Any())
                {
                    _ChargesList = loResult.Data!;
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
        public List<PeriodMonthDTO> GetMonth()
        {
            var tempMonthList
             = new List<PeriodMonthDTO>();
            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                PeriodMonthDTO month = new PeriodMonthDTO { Id = monthId };
                tempMonthList.Add(month);
            }
            GetMonthList = tempMonthList;
            return tempMonthList;
        }
        public DateTime? ConvertStringToDateTimeFormat(string pcEntity)
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
        public string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return "";
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }

    }
}
