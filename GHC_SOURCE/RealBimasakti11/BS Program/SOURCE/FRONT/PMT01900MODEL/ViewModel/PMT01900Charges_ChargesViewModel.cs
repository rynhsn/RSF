
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using R_BlazorFrontEnd;
using PMT01900Common.DTO.CRUDBase;
using BaseAOC_BS11Common.DTO.Response.GridList;
using BaseAOC_BS11Common.DTO.Response.List;
using BaseAOC_BS11Common.DTO.Front;
using PMT01900Common.DTO.Front;
using BaseAOC_BS11Model;
using BaseAOC_BS11Common.DTO.Request.Request.GridList;
using BaseAOC_BS11Common.Service;
using System.Linq;

namespace PMT01900Model.ViewModel
{
    public class PMT01900Charges_ChargesViewModel : R_ViewModel<PMT01900Charges_ChagesInfoDetailDTO>
    {
        #region From Back
        private readonly BaseAOCGetDataListUtilityModel _baseListModel = new BaseAOCGetDataListUtilityModel();
        private readonly BaseAOCGetDataGridListModel _baseGridModel = new BaseAOCGetDataGridListModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();

        private readonly PMT01900Charges_ChagesInfoDetailModel _model = new PMT01900Charges_ChagesInfoDetailModel();
        public ObservableCollection<BaseAOCResponseAgreementChargesListDTO> loListChargesInfo = new ObservableCollection<BaseAOCResponseAgreementChargesListDTO>();
        public PMT01900Charges_ChagesInfoDetailDTO loEntityChargesInfo = new PMT01900Charges_ChagesInfoDetailDTO();
        public List<BaseAOCResponseComboBoxDTO> loComboBoxDataCFEE_METHOD { get; set; } = new List<BaseAOCResponseComboBoxDTO>();
        //    public List<BaseAOCResponseComboBoxDTO> loComboBoxDataCINVOICE_PERIOD { get; set; } = new List<BaseAOCResponseComboBoxDTO>();
        public PMT01900ParameterFrontChangePageToChargesDTO loParameterList = new PMT01900ParameterFrontChangePageToChargesDTO();

        #region for Cal Unit

        public BaseAOCFunctionUtility _AOCService = new BaseAOCFunctionUtility();

        public ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO> loListChargesInfoCalUnit = new ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO>();
        public ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO> loTempListChargesInfoCalUnit = new ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO>();

        #endregion

        #endregion

        #region DataCBillingMode

        public List<BaseAOCResponseComboBoxDTO> loRadioGroupDataCBILLING_MODE = new List<BaseAOCResponseComboBoxDTO>
        {
            new BaseAOCResponseComboBoxDTO { CCODE = "01", CDESCRIPTION = "DP" },
            new BaseAOCResponseComboBoxDTO { CCODE = "02", CDESCRIPTION = "By Period" },
        };

        #endregion

        #region For Front

        public BaseAOCFrontControlYMDDTO _oControlYMD = new BaseAOCFrontControlYMDDTO();

        public string _cCurrencyCode = "";
        public string _cTempSeqNo = "";

        public decimal _nTempFEE_AMT = 0;
        public bool LTAXABLE;


        #endregion

        #region ChargesInfo

        public async Task GetChargesInfoList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementChargesListDTO>(loParameterList);
                    var loResult = await _baseGridModel.GetAgreementChargesListAsync(poParameter: loParam);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.DSTART_DATE = (DateTime)_AOCService.ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                            item.DEND_DATE = (DateTime)_AOCService.ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                        }
                    }
                    loListChargesInfo = new ObservableCollection<BaseAOCResponseAgreementChargesListDTO>(loResult.Data);
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
                    var loParam = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementChargesItemsListDTO>(Data);
                    var loResult = await _baseGridModel.GetAgreementChargesItemsListAsync(poParameter: loParam);
                    loListChargesInfoCalUnit = new ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _model.R_ServiceGetRecordAsync(ConvertToEntityBack(poEntity));
                loEntityChargesInfo = ConvertToEntityFront(loResult);
                //CalculateYMD(poEntity.DSTART_DATE: Data.DSTART_DATE, poEntity.DEND_DATE: Data.DEND_DATE);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01900Charges_ChagesInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                int liMax_ISEQ = 0;
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                    poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                    poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                    poNewEntity.CREF_NO = loParameterList.CREF_NO;

                    // Menggunakan LINQ untuk meningkatkan ISEQ jika nilainya 0
                    loListChargesInfoCalUnit.Where(item => item.ISEQ == 0)
                                    .ToList()
                                    .ForEach(item => item.ISEQ = liMax_ISEQ += 1);
                }

                poNewEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poNewEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poNewEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poNewEntity.CUNIT_ID = loParameterList.CUNIT_ID;
                var loTempCalUnitList = R_FrontUtility.ConvertCollectionToCollection<PMT01900Charges_ChargesInfoDetail_ChargesItemDTO>(loListChargesInfoCalUnit.ToList()).ToList();
                poNewEntity.ODATA_CHARGES_ITEM = loTempCalUnitList;

                var loResult = await _model.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityChargesInfo = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poEntity.CUNIT_ID = loParameterList.CUNIT_ID;
                // Validation Before Delete
                await _model.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
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
                var loResult = await _baseListModel.GetDataCFeeMethodAsync();
                loComboBoxDataCFEE_METHOD = new List<BaseAOCResponseComboBoxDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }



        #endregion

        #region Utilities

        private PMT01900Charges_ChagesInfoDetailDTO ConvertToEntityFront(PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01900Charges_ChagesInfoDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01900Charges_ChagesInfoDetailDTO>(poEntity);
                    loReturn.DSTART_DATE = _AOCService.ConvertStringToDateTimeFormat(poEntity.CSTART_DATE!);
                    loReturn.DEND_DATE = _AOCService.ConvertStringToDateTimeFormat(poEntity.CEND_DATE!);
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

        private PMT01900Charges_ChagesInfoDetailDTO ConvertToEntityBack(PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01900Charges_ChagesInfoDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01900Charges_ChagesInfoDetailDTO>(poEntity);
                    loReturn.CSTART_DATE = _AOCService.ConvertDateTimeToStringFormat(poEntity.DSTART_DATE!);
                    loReturn.CEND_DATE = _AOCService.ConvertDateTimeToStringFormat(poEntity.DEND_DATE!);
                }

            }

            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private void CalculateYMD(ref PMT01900Charges_ChagesInfoDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01900Charges_ChagesInfoDetailDTO loData = poEntity;

            try
            {
                if (poEntity.DSTART_DATE >= poEntity.DEND_DATE)
                {
                    loData.IYEAR = loData.IMONTH = 0;
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
                    loData.IMONTH = dValueEndDateForHandleDay.Month - poEntity.DSTART_DATE!.Value.Month;
                    if (loData.IMONTH < 0)
                    {
                        loData.IMONTH = 12 + loData.IMONTH;
                        DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        loData.IYEAR = dValueEndDateForHandleMonth.Year - poEntity.DSTART_DATE!.Value.Year;
                        if (loData.IYEAR < 0)
                        {
                            loData.IYEAR = 0;
                        }
                    }

                }
                else
                {
                    loData.IMONTH = dValueEndDate.Month - poEntity.DSTART_DATE!.Value.Month;
                    if (loData.IMONTH < 0)
                    {
                        loData.IMONTH = 12 + loData.IMONTH;
                        DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        loData.IYEAR = dValueEndDateForHandleMonth.Year - poEntity.DSTART_DATE!.Value.Year;
                        if (loData.IYEAR < 0)
                        {
                            loData.IYEAR = 0;
                        }
                    }
                    else
                    {
                        loData.IYEAR = dValueEndDate.Year - poEntity.DSTART_DATE!.Value.Year;
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
