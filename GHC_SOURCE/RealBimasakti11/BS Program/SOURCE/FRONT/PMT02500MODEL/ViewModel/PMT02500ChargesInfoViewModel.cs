using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT02500Common.DTO._4._Charges_Info;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Front;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT02500Model.ViewModel
{
    public class PMT02500ChargesInfoViewModel : R_ViewModel<PMT02500FrontChargesInfoDetailDTO>
    {
        #region From Back
        private readonly PMT02500ChargesInfoModel _modelPMT02500ChargesInfoModel = new PMT02500ChargesInfoModel();
        public ObservableCollection<PMT02500ChargesInfoListDTO> loListChargesInfo = new ObservableCollection<PMT02500ChargesInfoListDTO>();
        public PMT02500FrontChargesInfoDetailDTO loEntityChargesInfo = new PMT02500FrontChargesInfoDetailDTO();
        public PMT02500ChargesInfoHeaderDTO loEntityChargesInfoHeader = new PMT02500ChargesInfoHeaderDTO();
        public PMT02500ChargesInfoParameterActiveDTO loParameterProcessActive = new PMT02500ChargesInfoParameterActiveDTO();
        public List<PMT02500ComboBoxDTO> loComboBoxDataCFEE_METHOD { get; set; } = new List<PMT02500ComboBoxDTO>();
        public List<PMT02500ComboBoxDTO> loComboBoxDataCINVOICE_PERIOD { get; set; } = new List<PMT02500ComboBoxDTO>();
        public PMT02500GetHeaderParameterDTO loParameterList = new PMT02500GetHeaderParameterDTO();

        #region for Cal Unit

        public ObservableCollection<PMT02500FrontChargesInfo_FeeCalculationDetailDTO> loListChargesInfoCalUnit = new ObservableCollection<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>();
        public ObservableCollection<PMT02500FrontChargesInfo_FeeCalculationDetailDTO> loTempListChargesInfoCalUnit = new ObservableCollection<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>();

        #endregion

        #endregion

        #region DataCBillingMode

        public List<PMT02500ComboBoxDTO> loRadioGroupDataCBILLING_MODE = new List<PMT02500ComboBoxDTO>
        {
            new PMT02500ComboBoxDTO { CCODE = "01", CDESCRIPTION = "DP" },
            new PMT02500ComboBoxDTO { CCODE = "02", CDESCRIPTION = "By Period" },
        };

        #endregion

        #region For Front
        public bool LTAXABLE = false;
        public PMT02500ControlYMD _oControlYMD = new PMT02500ControlYMD();

        public string _cCurrencyCode = "";
        public string _cTempSeqNo = "";

        public decimal _nTempFEE_AMT = 0;


        #endregion

        #region ChargesInfo

        public async Task GetChargesInfoHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500ChargesInfoModel.GetChargesInfoHeaderAsync(poParameter: loParameterList);
                    loResult.CUNIT_ID = loParameterList.CCHARGE_MODE == "02" ? loParameterList.CUNIT_ID : "";
                    loResult.CUNIT_NAME = loParameterList.CCHARGE_MODE == "02" ? loParameterList.CUNIT_NAME : "";
                    loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                    loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                    loEntityChargesInfoHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesInfoList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT02500ChargesInfoModel.GetChargesInfoListAsync(poParameter: loParameterList);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DSTART_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                        }
                    }
                    loListChargesInfo = new ObservableCollection<PMT02500ChargesInfoListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesInfoCalUnitList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(_cTempSeqNo))
                {
                    var loParam = new PMT02500GetHeaderParameterChargesInfoCalUnitDTO()
                    {
                        CPROPERTY_ID = loParameterList.CPROPERTY_ID,
                        CDEPT_CODE = loParameterList.CDEPT_CODE,
                        CTRANS_CODE = loParameterList.CTRANS_CODE,
                        CREF_NO = loParameterList.CREF_NO,
                        CSEQ_NO = _cTempSeqNo,
                    };
                    var loResult = await _modelPMT02500ChargesInfoModel.GetChargesInfoCalUnitListAsync(poParameter: loParam);
                    loListChargesInfoCalUnit = new ObservableCollection<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT02500FrontChargesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT02500ChargesInfoModel.R_ServiceGetRecordAsync(ConvertToEntityBack(poEntity));
                loEntityChargesInfo = ConvertToEntityFront(loResult);
                //CalculateYMD(poEntity.DSTART_DATE: Data.DSTART_DATE, poEntity.DEND_DATE: Data.DEND_DATE);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500FrontChargesInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                    poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                    poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                    poNewEntity.CREF_NO = loParameterList.CREF_NO;
                }

                poNewEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poNewEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poNewEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poNewEntity.CUNIT_ID = loParameterList.CUNIT_ID;


                var loResult = await _modelPMT02500ChargesInfoModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityChargesInfo = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500FrontChargesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poEntity.CUNIT_ID = loParameterList.CUNIT_ID;
                // Validation Before Delete
                await _modelPMT02500ChargesInfoModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCFEE_METHOD()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500ChargesInfoModel.GetComboBoxDataCFEE_METHODAsync();
                loComboBoxDataCFEE_METHOD = new List<PMT02500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCINVOICE_PERIOD()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500ChargesInfoModel.GetComboBoxDataCINVOICE_PERIODAsync();
                loComboBoxDataCINVOICE_PERIOD = new List<PMT02500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessChangeStatusChargesInfoActive()
        {
            R_Exception loEx = new R_Exception();
            PMT02500ChargesInfoParameterActiveDTO? loParam = null;

            try
            {
                loParam = new PMT02500ChargesInfoParameterActiveDTO()
                {
                    CPROPERTY_ID = Data.CPROPERTY_ID,
                    CDEPT_CODE = Data.CDEPT_CODE,
                    CTRANS_CODE = Data.CTRANS_CODE,
                    CREF_NO = Data.CREF_NO,
                    CSEQ_NO = Data.CSEQ_NO,
                    LACTIVE = !Data.LACTIVE,
                };
                var loResult = await _modelPMT02500ChargesInfoModel.ProcessChangeStatusChargesInfoActiveAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion

        #region Utilities

        private PMT02500FrontChargesInfoDetailDTO ConvertToEntityFront(PMT02500ChargesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT02500FrontChargesInfoDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT02500FrontChargesInfoDetailDTO>(poEntity);
                    loReturn.DSTART_DATE = ConvertStringToDateTimeFormat(poEntity.CSTART_DATE!);
                    loReturn.DEND_DATE = ConvertStringToDateTimeFormat(poEntity.CEND_DATE!);
                    CalculateYMD(poEntity: ref loReturn);

                }

            }

            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private PMT02500ChargesInfoDetailDTO ConvertToEntityBack(PMT02500FrontChargesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT02500ChargesInfoDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT02500ChargesInfoDetailDTO>(poEntity);
                    loReturn.CSTART_DATE = ConvertDateTimeToStringFormat(poEntity.DSTART_DATE!);
                    loReturn.CEND_DATE = ConvertDateTimeToStringFormat(poEntity.DEND_DATE!);
                }

            }

            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }

        public DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }

        public string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }

        private void CalculateYMD(ref PMT02500FrontChargesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT02500FrontChargesInfoDetailDTO loData = poEntity;

            try
            {
                if (poEntity.DSTART_DATE >= poEntity.DEND_DATE)
                {
                    loData.IYEARS = loData.IMONTHS = 0;
                    loData.IDAYS = 1;
                    goto EndBlocks;
                }
                DateTime dValueEndDate = poEntity.DEND_DATE!.Value.AddDays(1);

                int liChecker = poEntity.DEND_DATE!.Value.Day - poEntity.DSTART_DATE!.Value.Day;


                loData.IDAYS = dValueEndDate.Day - poEntity.DSTART_DATE!.Value.Day;
                if (loData.IDAYS < 0)
                {
                    DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                    int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                    loData.IDAYS = liTempDayinMonth + loData.IDAYS;
                    if (loData.IDAYS < 0) { loException.Add("ErrDev", "Value is negative!"); }
                    loData.IMONTHS = dValueEndDateForHandleDay.Month - poEntity.DSTART_DATE!.Value.Month;
                    if (loData.IMONTHS < 0)
                    {
                        loData.IMONTHS = 12 + loData.IMONTHS;
                        DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        loData.IYEARS = dValueEndDateForHandleMonth.Year - poEntity.DSTART_DATE!.Value.Year;
                        if (loData.IYEARS < 0)
                        {
                            loData.IYEARS = 0;
                        }
                    }

                }
                else
                {
                    loData.IMONTHS = dValueEndDate.Month - poEntity.DSTART_DATE!.Value.Month;
                    if (loData.IMONTHS < 0)
                    {
                        loData.IMONTHS = 12 + loData.IMONTHS;
                        DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        loData.IYEARS = dValueEndDateForHandleMonth.Year - poEntity.DSTART_DATE!.Value.Year;
                        if (loData.IYEARS < 0)
                        {
                            loData.IYEARS = 0;
                        }
                    }
                    else
                    {
                        loData.IYEARS = dValueEndDate.Year - poEntity.DSTART_DATE!.Value.Year;
                    }
                }

            }

            //loData.IYEAR = dValueEndDate.Year - poEntity.DSTART_DATE!.Value.Year;
            //loData.IMONTH = dValueEndDate.Month - poEntity.DSTART_DATE!.Value.Month;}
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlocks:

            loException.ThrowExceptionIfErrors();
        }

        #endregion


    }
}