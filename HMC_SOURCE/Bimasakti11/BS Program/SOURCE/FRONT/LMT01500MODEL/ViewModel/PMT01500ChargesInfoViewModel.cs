using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT01500Common.DTO._4._Charges_Info;
using PMT01500Common.Utilities;
using PMT01500Common.Utilities.Front;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT01500Model.ViewModel
{
    public class PMT01500ChargesInfoViewModel : R_ViewModel<PMT01500FrontChargesInfoDetailDTO>
    {
        #region From Back
        private readonly PMT01500ChargesInfoModel _modelPMT01500ChargesInfoModel = new PMT01500ChargesInfoModel();
        public ObservableCollection<PMT01500ChargesInfoListDTO> loListChargesInfo = new ObservableCollection<PMT01500ChargesInfoListDTO>();
        public PMT01500FrontChargesInfoDetailDTO loEntityChargesInfo = new PMT01500FrontChargesInfoDetailDTO();
        public PMT01500ChargesInfoHeaderDTO loEntityChargesInfoHeader = new PMT01500ChargesInfoHeaderDTO();
        public PMT01500ChargesInfoParameterActiveDTO loParameterProcessActive = new PMT01500ChargesInfoParameterActiveDTO();
        public List<PMT01500ComboBoxDTO> loComboBoxDataCFEE_METHOD { get; set; } = new List<PMT01500ComboBoxDTO>();
        public List<PMT01500ComboBoxDTO> loComboBoxDataCINVOICE_PERIOD { get; set; } = new List<PMT01500ComboBoxDTO>();
        public PMT01500GetHeaderParameterDTO loParameterList = new PMT01500GetHeaderParameterDTO();

        #region for Cal Unit
        public ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO> loListChargesInfoCalUnit = new ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>();
        public ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO> loTempListChargesInfoCalUnit = new ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>();

        #endregion

        #endregion

        #region DataCBillingMode
        public List<PMT01500ComboBoxDTO> loRadioGroupDataCBILLING_MODE = new List<PMT01500ComboBoxDTO>
        {
            new PMT01500ComboBoxDTO { CCODE = "01", CDESCRIPTION = "DP" },
            new PMT01500ComboBoxDTO { CCODE = "02", CDESCRIPTION = "By Period" },
        };

        #endregion

        #region For Front
        public PMT01500ControlYMD _oControlYMD = new PMT01500ControlYMD();

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
                    var loResult = await _modelPMT01500ChargesInfoModel.GetChargesInfoHeaderAsync(poParameter: loParameterList);
                    loResult.CUNIT_ID = loParameterList.CCHARGE_MODE == "02" ? loParameterList.CUNIT_ID : "";
                    loResult.CUNIT_NAME = loParameterList.CCHARGE_MODE == "02" ? loParameterList.CUNIT_NAME : "";
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
                    var loResult = await _modelPMT01500ChargesInfoModel.GetChargesInfoListAsync(poParameter: loParameterList);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DSTART_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = (DateTime)ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                        }
                    }
                    loListChargesInfo = new ObservableCollection<PMT01500ChargesInfoListDTO>(loResult);
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
                    var loParam = new PMT01500GetHeaderParameterChargesInfoCalUnitDTO()
                    {
                        CPROPERTY_ID = loParameterList.CPROPERTY_ID,
                        CDEPT_CODE = loParameterList.CDEPT_CODE,
                        CTRANS_CODE = loParameterList.CTRANS_CODE,
                        CREF_NO = loParameterList.CREF_NO,
                        CSEQ_NO = _cTempSeqNo,
                    };
                    var loResult = await _modelPMT01500ChargesInfoModel.GetChargesInfoCalUnitListAsync(poParameter: loParam);
                    loListChargesInfoCalUnit = new ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT01500FrontChargesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT01500ChargesInfoModel.R_ServiceGetRecordAsync(ConvertToEntityBack(poEntity));

                loEntityChargesInfo = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01500FrontChargesInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
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


                var loResult = await _modelPMT01500ChargesInfoModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityChargesInfo = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01500FrontChargesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poEntity.CUNIT_ID = loParameterList.CUNIT_ID;
                // Validation Before Delete
                await _modelPMT01500ChargesInfoModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
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
                var loResult = await _modelPMT01500ChargesInfoModel.GetComboBoxDataCFEE_METHODAsync();
                loComboBoxDataCFEE_METHOD = new List<PMT01500ComboBoxDTO>(loResult);
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
                var loResult = await _modelPMT01500ChargesInfoModel.GetComboBoxDataCINVOICE_PERIODAsync();
                loComboBoxDataCINVOICE_PERIOD = new List<PMT01500ComboBoxDTO>(loResult);
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
            PMT01500ChargesInfoParameterActiveDTO? loParam = null;

            try
            {
                loParam = new PMT01500ChargesInfoParameterActiveDTO()
                {
                    CPROPERTY_ID = Data.CPROPERTY_ID,
                    CDEPT_CODE = Data.CDEPT_CODE,
                    CTRANS_CODE = Data.CTRANS_CODE,
                    CREF_NO = Data.CREF_NO,
                    CSEQ_NO = Data.CSEQ_NO,
                    LACTIVE = !Data.LACTIVE,
                };
                var loResult = await _modelPMT01500ChargesInfoModel.ProcessChangeStatusChargesInfoActiveAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion


        #region Utilities

        private PMT01500FrontChargesInfoDetailDTO ConvertToEntityFront(PMT01500ChargesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01500FrontChargesInfoDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500FrontChargesInfoDetailDTO>(poEntity);
                    loReturn.DSTART_DATE = ConvertStringToDateTimeFormat(poEntity.CSTART_DATE!);
                    loReturn.DEND_DATE = ConvertStringToDateTimeFormat(poEntity.CEND_DATE!);
                }

            }

            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private PMT01500ChargesInfoDetailDTO ConvertToEntityBack(PMT01500FrontChargesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01500ChargesInfoDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500ChargesInfoDetailDTO>(poEntity);
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

        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
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

        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
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

        #endregion


    }
}