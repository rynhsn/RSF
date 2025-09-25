using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Utility;
using PMT02000FrontResources;
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

namespace PMT02000MODEL.ViewModel
{
    public class PMT02000ViewModel : R_ViewModel<PMT02000LOIHeader_DetailDTO>
    {
        private PMT02000LOIModel _model = new PMT02000LOIModel();
        public List<PMT02000PropertyDTO> PropertyList { get; set; } = new List<PMT02000PropertyDTO>();
        public ObservableCollection<PMT02000LOIDTO> LOIList =
            new ObservableCollection<PMT02000LOIDTO>();
        public PMT02000LOIDTO _CurrentLOI = new PMT02000LOIDTO();
        public PMT02000LOIHeader _LOIHeader = new PMT02000LOIHeader();
        public ObservableCollection<PMT02000LOIHandOverUnitDTO> _listDetailUnit = new ObservableCollection<PMT02000LOIHandOverUnitDTO>();
        public ObservableCollection<PMT02000LOIHandoverUtilityDTO> _listDetailUtilityFiltered = new ObservableCollection<PMT02000LOIHandoverUtilityDTO>();
        public ObservableCollection<PMT02000LOIHandoverUtilityDTO> _listDetailUtilityUnfiltered = new ObservableCollection<PMT02000LOIHandoverUtilityDTO>();


        public PMT02000LOIHeader_DetailDTO _EntityHeaderDetail = new PMT02000LOIHeader_DetailDTO();
        public PMT02000LOIHeader_DetailDTO _TempEntityHeaderDetail = new PMT02000LOIHeader_DetailDTO();

        public PMT02000LOIHandoverUtilityDTO _EntityUtility = new PMT02000LOIHandoverUtilityDTO();
        public PMT02000LOIHandOverUnitDTO _EntityUnit = new PMT02000LOIHandOverUnitDTO();

        public List<PMT02000GetMonthDTO>? GetMonthList;
        public PMT02000VarGsmTransactionCodeDTO oVarGSMTransactionCode = new PMT02000VarGsmTransactionCodeDTO();
        public PMT02000PropertyDTO _PropertyWithName = new PMT02000PropertyDTO();
        public PMT02000LOIHeader_DetailDTO _oParamGetHeader = new PMT02000LOIHeader_DetailDTO();
        public PMT02000LOIHeader _oParamHeaderPopUp = new PMT02000LOIHeader();
        public PMT02000DBParameter oParamUtility = new PMT02000DBParameter();
        public string _ChargeType = "";
        public bool _isBtnEnabled;
        public string VAR_LOI_TRANS_CODE = "802061";
        public string VAR_HO_TRANS_CODE = "802130";
        public string _defaultMonth = DateTime.Now.Month.ToString("D2");
        public int _defaultYear = DateTime.Now.Year;

        public bool lEnableIBlock;
        public bool lEnableMeter;

        public bool lControlButtonRedraft;
        public bool lControlButtonSubmit;
        public bool lControlBtnEditDelete;
        public bool lPropertyExist = true;
        public eCRUDMode eModeUtility;
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.PropertyListStreamAsyncModel();
                PropertyList = loResult.Data!;
                if (PropertyList.Count > 0)
                {
                    _PropertyWithName = PropertyList[0];
                    lPropertyExist = true;
                }
                else
                {
                    lPropertyExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllList(string lcTab)
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _PropertyWithName.CPROPERTY_ID);
                if (lcTab == "HO")
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, VAR_HO_TRANS_CODE); //////
                }
                else if ((lcTab == "LOI"))
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, VAR_LOI_TRANS_CODE);//VAR_LOI_TRANS_CODE);
                }

                var loResult = await _model.LOIListStreamAsyncModel();
                LOIList = new ObservableCollection<PMT02000LOIDTO>(loResult.Data);

                if (LOIList.Count > 0)
                {
                    _isBtnEnabled = true;
                    loResult.Data = loResult.Data!
                        .Select(item =>
                        {
                            item.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(item.CHAND_OVER_DATE!);
                            return item;
                        }).ToList();

                    _CurrentLOI = LOIList.FirstOrDefault();
                }
                else
                {
                    _isBtnEnabled = false;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Submit Redraft DELETE
        public async Task SubmitRedraft(string lcType)
        {
            R_Exception loException = new R_Exception();
            try
            {
                PMT02000DBParameter poParameter = R_FrontUtility.ConvertObjectToObject<PMT02000DBParameter>(_CurrentLOI);

                switch (lcType)
                {
                    case "Submit":
                        poParameter.VAR_NEW_STATUS = "10";
                        break;
                    case "Redraft":
                        poParameter.VAR_NEW_STATUS = "00";
                        break;
                }
                await _model.ProcessSubmitRedraftAsyncMosel(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(PMT02000LOIHeader_DetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //AFTER CR
                poEntity.VAR_TRANS_CODE = VAR_HO_TRANS_CODE;

                poEntity.CSAVEMODE = "DELETE";

                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Header

        public async Task GetEntityHeaderDetail(PMT02000LOIHeader_DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                //Result From Back
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                if (loResult != null)
                {
                    if (poEntity.CSAVEMODE == "NEW")
                    {
                        loResult.DHO_REF_DATE = DateTime.Now;
                    }
                    //JUST FOR HEADER on UI
                    _LOIHeader = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(loResult);

                    //Assign Name from UI
                    _LOIHeader.CPROPERTY_NAME = _oParamHeaderPopUp.CPROPERTY_NAME;

                    //FOR HEADER AND DETAIL
                    var loTempResult = ConvertDataHeaderToFront(loResult);

                    // oParamUtility = R_FrontUtility.ConvertObjectToObject<PMT02000DBParameter>(loTempResult);

                    _EntityHeaderDetail = loTempResult;

                    //Cek Unit
                    if (_EntityHeaderDetail.ListUnit.Any())
                    {
                        _listDetailUnit = new ObservableCollection<PMT02000LOIHandOverUnitDTO>(_EntityHeaderDetail.ListUnit);

                    }
                    else
                    {
                        _listDetailUnit = new ObservableCollection<PMT02000LOIHandOverUnitDTO>();
                    }
                    //CEK Utility
                    if (_EntityHeaderDetail.ListUtility.Any())
                    {
                        _listDetailUtilityUnfiltered = new ObservableCollection<PMT02000LOIHandoverUtilityDTO>(_EntityHeaderDetail.ListUtility);

                    }
                    else
                    {
                        _listDetailUtilityUnfiltered = new ObservableCollection<PMT02000LOIHandoverUtilityDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task ServiceSave(PMT02000LOIHeader_DetailDTO poParam, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.VAR_LOI_TRANS_CODE = VAR_LOI_TRANS_CODE;
                poParam.VAR_TRANS_CODE = VAR_HO_TRANS_CODE;
                poParam.ListUnit = _listDetailUnit.ToList();
                poParam.ListUtility = _listDetailUtilityUnfiltered.ToList();
                var loResult = await _model.R_ServiceSaveAsync(poParam, peCRUDMode);
                _EntityHeaderDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationHeader(PMT02000LOIHeader_DetailDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {

                if (string.IsNullOrEmpty(poEntity.CHO_REF_NO) && !(oVarGSMTransactionCode.LINCREMENT_FLAG))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationHORefNo");
                    loEx.Add(loErr);
                }
                if (poEntity.DHO_REF_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationHORefDate");
                    loEx.Add(loErr);
                }

                if (poEntity.DHO_ACTUAL_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationHODate");
                    loEx.Add(loErr);
                }
                if ( poEntity.DHO_ACTUAL_DATE > DateTime.Now)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationHODateMoreThanNow");
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
        #region Unit
        public void GetLOIUnitList(PMT02000LOIHeader_DetailDTO loParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (loParameter != null)
                {
                    _listDetailUnit = new ObservableCollection<PMT02000LOIHandOverUnitDTO>(loParameter.ListUnit);
                }
                else
                {
                    _listDetailUnit = new ObservableCollection<PMT02000LOIHandOverUnitDTO>();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public void GetEntityUnit(PMT02000LOIHandOverUnitDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                _EntityUnit = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public void ValidationUnit(PMT02000LOIHandOverUnitDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (poEntity.NACTUAL_AREA_SIZE < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationActualSize");
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
        #region Utility
        public void GetLOIUtilityList(PMT02000DBParameter poParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                _listDetailUtilityFiltered = poParam != null ? new ObservableCollection<PMT02000LOIHandoverUtilityDTO>(_listDetailUtilityUnfiltered
                                                .Where(x => x.CUNIT_ID == poParam.CUNIT_ID)
                                                .Select(item =>
                                                {
                                                    if (int.TryParse(item.CYEAR, out int parsedYear))
                                                    {
                                                        item.IYEAR = parsedYear;
                                                    }
                                                    else
                                                    {
                                                        item.IYEAR = DateTime.Now.Year;
                                                    }
                                                    if (string.IsNullOrWhiteSpace(item.CMONTH))
                                                    {
                                                        item.CMONTH = DateTime.Now.ToString("MM");
                                                    }
                                                    return item;
                                                }).ToList())
                                                : new ObservableCollection<PMT02000LOIHandoverUtilityDTO>();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public void GetEntityUtility(PMT02000LOIHandoverUtilityDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                _EntityUtility = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void ValidationUtility(PMT02000LOIHandoverUtilityDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (poEntity.IYEAR <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationStartYear");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(poEntity.CMONTH))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationStartMonth");
                    loEx.Add(loErr);
                }

                if ((poEntity.NMETER_START < 0 && _ChargeType == "03") || (poEntity.NMETER_START < 0 && _ChargeType == "04"))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationMeterStart");
                    loEx.Add(loErr);
                    goto
                    EndBlock;
                }
                if ((poEntity.NBLOCK1_START < 0 && _ChargeType == "01") || (poEntity.NBLOCK1_START < 0 && _ChargeType == "02"))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidatonBlock1Start");
                    loEx.Add(loErr);
                    goto
                   EndBlock;
                }
                if ((poEntity.NBLOCK2_START < 0 && _ChargeType == "01") || (poEntity.NBLOCK2_START < 0 && _ChargeType == "02"))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidatonBlock2Start");
                    loEx.Add(loErr);
                    goto
                   EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region Helper
        public async Task GetVAR_GSM_TRANSACTION_CODE()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetVAR_GSM_TRANSACTION_CODEAsync();
                oVarGSMTransactionCode = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void GetMonth()
        {
            GetMonthList = new List<PMT02000GetMonthDTO>();
            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                string monthName = R_FrontUtility.R_GetMessage(typeof(Resources_PMT02000_Class), $"_labelMonth{i}");
                PMT02000GetMonthDTO month = new PMT02000GetMonthDTO { Id = monthId, Name = monthName };
                GetMonthList.Add(month);
            }
            _defaultMonth = DateTime.Now.Month.ToString("D2");

        }
        public PMT02000LOIHeader_DetailDTO ConvertDataHeaderToFront(PMT02000LOIHeader_DetailDTO loParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loParam.DHO_REF_DATE = ConvertStringToDateTimeFormat(loParam.CHO_REF_DATE!);
                loParam.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(loParam.CHAND_OVER_DATE!);
                loParam.DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(loParam.CHO_ACTUAL_DATE!);
                loParam.DHO_PLAN_START_DATE = ConvertStringToDateTimeFormat(loParam.CHO_PLAN_START_DATE!);
                loParam.DHO_PLAN_END_DATE = ConvertStringToDateTimeFormat(loParam.CHO_PLAN_END_DATE!);
                //_EntityDetail = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loParam;

        }
        public PMT02000LOIHeader_DetailDTO ConvertDataHeaderToBack(PMT02000LOIHeader_DetailDTO loParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loParam.CHO_REF_DATE = ConvertDateTimeToStringFormat(loParam.DHO_REF_DATE!);
                loParam.CHAND_OVER_DATE = ConvertDateTimeToStringFormat(loParam.DHAND_OVER_DATE!);
                loParam.CHO_ACTUAL_DATE = ConvertDateTimeToStringFormat(loParam.DHO_ACTUAL_DATE!);
                loParam.CHO_PLAN_START_DATE = ConvertDateTimeToStringFormat(loParam.DHO_PLAN_START_DATE!);
                loParam.CHO_PLAN_END_DATE = ConvertDateTimeToStringFormat(loParam.DHO_PLAN_END_DATE!);
                //_EntityDetail = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loParam;

        }
        public PMT02000LOIHeader_DetailDTO ConvertDTO(PMT02000LOIHeader loParam)
        {
            PMT02000LOIHeader_DetailDTO loReturn = new PMT02000LOIHeader_DetailDTO();
            var loEx = new R_Exception();
            try
            {
                loReturn = new PMT02000LOIHeader_DetailDTO()
                {
                    CPROPERTY_ID = loParam.CPROPERTY_ID,
                    CTRANS_CODE = loParam.CTRANS_CODE,
                    CDEPT_CODE = loParam.CDEPT_CODE,
                    CREF_NO = loParam.CREF_NO,
                    CBUILDING_ID = loParam.CBUILDING_ID,
                    CSAVEMODE = loParam.CSAVEMODE
                };

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;

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
        public string ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (ptEntity == DateTime.MinValue)
            {
                // Jika DateTime adalah DateTime.MinValue, kembalikan string kosong
                return ""; // atau null, tergantung pada kebutuhan Anda
            }

            // Format DateTime ke string "yyyyMMdd"
            return ptEntity?.ToString("yyyyMMdd")!;
        }
        #endregion
    }
}
