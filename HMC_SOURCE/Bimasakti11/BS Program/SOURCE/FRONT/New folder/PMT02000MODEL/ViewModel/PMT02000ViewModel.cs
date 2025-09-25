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
        public ObservableCollection<PMT02000LOIDetailListDTO> _listDetail = new ObservableCollection<PMT02000LOIDetailListDTO>();
        public PMT02000LOIHeader_DetailDTO _EntityHeaderDetail = new PMT02000LOIHeader_DetailDTO();
        public PMT02000LOIHeader_DetailDTO _TempEntityHeaderDetail = new PMT02000LOIHeader_DetailDTO();

        public PMT02000LOIDetailListDTO _EntityDetail = new PMT02000LOIDetailListDTO();

        public List<PMT02000GetMonthDTO>? GetMonthList;
        public string PropertyValueID = "";
        public bool _isBtnEnabled;
        public string VAR_LOI_TRANS_CODE = "802041";
        public string VAR_HO_TRANS_CODE = "802130";
        public string _defaultMonth = DateTime.Now.Month.ToString("D2");
        public int _defaultYear = DateTime.Now.Year;

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.PropertyListStreamAsyncModel();
                PropertyList = loResult.Data!;
                if (PropertyList.Count > 0)
                {
                    PropertyValueID = PropertyList[0].CPROPERTY_ID!;
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
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, PropertyValueID);
                if (lcTab == "HO")
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, VAR_HO_TRANS_CODE); //////
                }
                else if ((lcTab == "LOI"))
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, VAR_HO_TRANS_CODE);//VAR_LOI_TRANS_CODE);
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

        #region GetDetail
        public async Task GetHeaderDetail(PMT02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loParam = new PMT02000DBParameter()
                {
                    CPROPERTY_ID = poParameter.CPROPERTY_ID,
                    CTRANS_CODE = poParameter.CTRANS_CODE,
                    CDEPT_CODE = poParameter.CDEPT_CODE,
                    CREF_NO = poParameter.CREF_NO,
                    CBUILDING_ID = poParameter.CBUILDING_ID,
                    CFLOOR_ID = poParameter.CFLOOR_ID,
                    CUNIT_ID = poParameter.CUNIT_ID,
                    CSAVEMODE = poParameter.CSAVEMODE,
                };

                _LOIHeader = await _model.GetLOIHeaderModel(loParam);

                _LOIHeader.DHO_REF_DATE = ConvertStringToDateTimeFormat(_LOIHeader.CHO_REF_DATE!);
                _LOIHeader.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(_LOIHeader.CHAND_OVER_DATE!);
                _LOIHeader.DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(_LOIHeader.CHO_ACTUAL_DATE!);
                _LOIHeader.DHO_PLAN_START_DATE = ConvertStringToDateTimeFormat(_LOIHeader.CHO_PLAN_START_DATE!);
                _LOIHeader.DHO_PLAN_END_DATE = ConvertStringToDateTimeFormat(_LOIHeader.CHO_PLAN_END_DATE!);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetLOIDetailList(PMT02000DBParameter loParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, loParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, loParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, VAR_LOI_TRANS_CODE); //////
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, loParam.CREF_NO); //////
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBUILDING_ID, loParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFLOOR_ID, loParam.CFLOOR_ID); //////
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUNIT_ID, loParam.CUNIT_ID);

                var loResult = await _model.LOIListDetailStreamAsyncModel();

                if (loResult != null)
                {
                    _listDetail = new ObservableCollection<PMT02000LOIDetailListDTO>(loResult.Data);

                    foreach (var item in _listDetail)
                    {
                        if (!string.IsNullOrWhiteSpace(item.CYEAR))
                        {
                            item.IYEAR = int.Parse(item.CYEAR);
                        }
                        else
                        {
                            item.IYEAR = _defaultYear;
                        }
                        if (string.IsNullOrWhiteSpace(item.CMONTH))
                        {
                            item.CMONTH = _defaultMonth;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region Submit Redraft
        public async Task SubmitRedraft(string lcType)
        {
            R_Exception loException = new R_Exception();
            try
            {
                PMT02000DBParameter poParameter = R_FrontUtility.ConvertObjectToObject<PMT02000DBParameter>(_CurrentLOI);

                switch (lcType)
                {
                    case "Submit":
                        poParameter.VAR_NEW_STATUS = "01";
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
                // Validation Before Delete
                //poEntity.CHO_REF_DATE = ConvertDateTimeToStringFormat(poEntity.DHO_REF_DATE);
                //poEntity.CHO_ACTUAL_DATE = ConvertDateTimeToStringFormat(poEntity.DHO_ACTUAL_DATE);
                //poEntity.CHO_PLAN_START_DATE = ConvertDateTimeToStringFormat(poEntity.DHO_PLAN_START_DATE);
                //poEntity.CHO_PLAN_END_DATE = ConvertDateTimeToStringFormat(poEntity.DHO_PLAN_END_DATE);
                //poEntity.VAR_LOI_TRANS_CODE = VAR_LOI_TRANS_CODE;

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

        #region PopUP
        public async Task ServiceSave(PMT02000LOIHeader_DetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _EntityHeaderDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetEntity(PMT02000LOIHeader_DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                if (loResult != null)
                {
                    if (poEntity.CSAVEMODE == "NEW")
                    {
                        loResult.DHO_REF_DATE = DateTime.Now;
                    }
                    var loTempResult = ConvertDataHeaderToFront(loResult);

                    loTempResult.ListDetail = _listDetail.ToList();
                    _EntityHeaderDetail = loTempResult;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validation
        public void ValidationHeader(PMT02000LOIHeader_DetailDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {

                if (string.IsNullOrEmpty(poEntity.CHO_REF_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2001");
                    loEx.Add(loErr);
                }
                if (poEntity.DHO_REF_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2002");
                    loEx.Add(loErr);
                }

                if (poEntity.DHO_ACTUAL_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2003");
                    loEx.Add(loErr);
                }

                if (poEntity.DHO_PLAN_START_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2004");
                    loEx.Add(loErr);
                }

                if (poEntity.DHO_PLAN_END_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2005");
                    loEx.Add(loErr);
                }
                if (poEntity.DHO_PLAN_START_DATE > poEntity.DHO_PLAN_END_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2006");
                    loEx.Add(loErr);
                }
                if (poEntity.NHO_ACTUAL_SIZE < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2008");
                    loEx.Add(loErr);
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationDetail(PMT02000LOIDetailListDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (poEntity.IYEAR <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2009");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(poEntity.CMONTH))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2010");
                    loEx.Add(loErr);
                }

                if (poEntity.IMETER_START <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "2011");
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

        public void GetEntityDetail(PMT02000LOIDetailListDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                _EntityDetail = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        #region Utility
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
            PMT02000LOIHeader_DetailDTO loReturn = null;
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
                    CFLOOR_ID = loParam.CFLOOR_ID,
                    CUNIT_ID = loParam.CUNIT_ID,
                    CSAVEMODE = loParam.CSAVEMODE
                };

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loReturn!;

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
