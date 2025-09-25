using BaseAOC_BS11Common.DTO.Front;
using BaseAOC_BS11Common.DTO.Request.Request.GridList;
using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BaseAOC_BS11Common.DTO.Response.GridList;
using BaseAOC_BS11Common.DTO.Response.List;
using BaseAOC_BS11Common.DTO.Response.Single;
using BaseAOC_BS11Common.Service;
using BaseAOC_BS11Model;
using PMT01600COMMON.DTO.Front;
using PMT01600FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01600Model.ViewModel
{
    public class PMT01600LOIListViewModel : R_ViewModel<BaseAOCResponseAgreementListDTO>
    {

        #region From Back

        //private readonly PMT01100LOI_LOIListModel _model = new PMT01100LOI_LOIListModel();
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();
        private readonly BaseAOCGetDataGridListModel _baseGridModel = new BaseAOCGetDataGridListModel();
        private readonly BaseAOCGetDataListUtilityModel _baseListModel = new BaseAOCGetDataListUtilityModel();
        public ObservableCollection<BaseAOCResponseAgreementListDTO> oLOIList = new ObservableCollection<BaseAOCResponseAgreementListDTO>();
        public ObservableCollection<BaseAOCResponseAgreementUnitInfoListDTO> oUnitList = new ObservableCollection<BaseAOCResponseAgreementUnitInfoListDTO>();
        public List<BaseAOCResponsePropertyListDTO> oPropertyList = new List<BaseAOCResponsePropertyListDTO>();
        public BaseAOCResponseAgreementListDTO loEntity = new BaseAOCResponseAgreementListDTO();

        #endregion

        #region For Front

        public bool lControlButtonSubmit = false;
        public bool lControlButtonRedraft = false;

        public BaseAOCFunctionUtility _AOCService = new BaseAOCFunctionUtility();
        public PMT01600ParameterFrontChangePageDTO oParameter = new PMT01600ParameterFrontChangePageDTO();
        public BaseAOCFrontPropertyIDDTO? oPropertyId = new BaseAOCFrontPropertyIDDTO();
        public string cFilterStatusList = "30"; ////FOR Display //
        public string cTempFilterToBack = "30"; //FOR Param to back

        #region Filtering status List

        public List<BaseAOCResponseComboBoxDTO> loRadioFilteringLOI = new List<BaseAOCResponseComboBoxDTO>
        {
            new BaseAOCResponseComboBoxDTO { CCODE = "00", CDESCRIPTION =  R_FrontUtility.R_GetMessage(typeof(Resources_PMT01600_Class), "_Draft/Open" )},
            new BaseAOCResponseComboBoxDTO { CCODE = "30", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_PMT01600_Class), "_Approved" )},
              new BaseAOCResponseComboBoxDTO { CCODE = "80", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_PMT01600_Class), "_Closed" )},
            new BaseAOCResponseComboBoxDTO { CCODE = "90", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_PMT01600_Class), "_Cancelled/Rejected" )},
        };

        #endregion

        public bool lControlButton;
        //For Control Tab Page
        public bool lControlTabLOIList = true;
        public bool lControlTabLOI = true;
        public bool lControlTabUnitandCharges = true;
        public bool lControlTabDeposit = true;

        //For Text Bulding
        public string? cBuildingSelectedUnit = "";

        #endregion

        #region Program 

        public async Task GetLOIList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementListDTO>(oParameter);
                    loParam.CTRANS_STATUS_LIST = cTempFilterToBack;

                    var loResult = await _baseGridModel.GetAgreementListAsync(poParameter: loParam);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                        {
                            item.DREF_DATE = _AOCService.ConvertStringToDateTimeFormat(item.CREF_DATE);
                            item.DFOLLOW_UP_DATE = _AOCService.ConvertStringToDateTimeFormat(item.CFOLLOW_UP_DATE);
                            item.DHO_ACTUAL_DATE = _AOCService.ConvertStringToDateTimeFormat(item.CHO_ACTUAL_DATE);
                            item.DOPEN_DATE = _AOCService.ConvertStringToDateTimeFormat(item.COPEN_DATE);
                        }
                    }
                    oLOIList = new ObservableCollection<BaseAOCResponseAgreementListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUnitList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CREF_NO))
                {
                    oParameter.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                    var loParam = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementUnitInfoListDTO>(oParameter);
                    var loResult = await _baseGridModel.GetAgreementUnitInfoListAsync(poParameter: loParam);
                    oUnitList = new ObservableCollection<BaseAOCResponseAgreementUnitInfoListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _baseListModel.GetPropertyListAsync();
                if (loResult.Data.Any())
                {
                    oPropertyList = new List<BaseAOCResponsePropertyListDTO>(loResult.Data);
                    if (string.IsNullOrEmpty(oPropertyId.CPROPERTY_ID))
                    {
                        oPropertyId.CPROPERTY_ID = oParameter.CPROPERTY_ID = loResult.Data.First().CPROPERTY_ID!;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion

        #region Proses Update Status

        public async Task<BaseAOCResponseUpdateAgreementStatusDTO> ProsesUpdateAgreementStatus(string pcStatus)
        {
            BaseAOCResponseUpdateAgreementStatusDTO loReturn = new BaseAOCResponseUpdateAgreementStatusDTO();
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CREF_NO))
                {
                    var loParam = new BaseAOCParameterRequestUpdateAgreementStatusDTO()
                    {
                        CPROPERTY_ID = oParameter.CPROPERTY_ID!,
                        CDEPT_CODE = oParameter.CDEPT_CODE!,
                        CTRANS_CODE = oParameter.CTRANS_CODE!,
                        CREF_NO = oParameter.CREF_NO,
                        CNEW_STATUS = pcStatus,

                    };

                    var loResult = await _baseSingleDataModel.ProsesUpdateAgreementStatusAsync(loParam);
                    loReturn = loResult;
                }
                else
                {
                    loReturn.LSUCCESS = false;
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

    }
}
