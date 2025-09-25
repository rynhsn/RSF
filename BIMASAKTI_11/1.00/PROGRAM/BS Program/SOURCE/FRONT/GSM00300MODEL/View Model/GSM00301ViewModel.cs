using GSM00300COMMON;
using GSM00300COMMON.DTO_s;
using GSM00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace GSM00300MODEL.View_Model
{
    public class GSM00301ViewModel : R_ViewModel<TaxInfoDTO>
    {
        //variables
        private const string CLASS_APPLICATION = "BIMASAKTI";
        private GSM00301Model _model = new GSM00301Model();
        public TaxInfoDTO TaxInfo { get; set; } = new TaxInfoDTO();
        public List<GSBCodeInfoDTO> IdTypeList { get; set; } = new List<GSBCodeInfoDTO>();
        public List<GSBCodeInfoDTO> TaxForTypeList { get; set; } = new List<GSBCodeInfoDTO>();

        //methods
        public async Task InitProcess(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetTaxInfoGetRecordAsync();
                await GetIdTypeList();
                await GetTaxForTypeListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        public async Task GetTaxInfoGetRecordAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(new TaxInfoDTO());
                if (loResult != null)
                {
                    loResult.DID_EXPIRED_DATE = DateTime.TryParseExact(loResult.CID_EXPIRED_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultInv)
                        ? loResultInv
                        : (DateTime?)null;
                    TaxInfo = R_FrontUtility.ConvertObjectToObject<TaxInfoDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTaxInfoAsync(TaxInfoDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, poCRUDMode);
                loResult.DID_EXPIRED_DATE = DateTime.TryParseExact(loResult.CID_EXPIRED_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultInv)
                    ? loResultInv
                    : (DateTime?)null;
                TaxInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetIdTypeList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var _classId = "_BS_ID_TYPE";
                var _recIdCharList = "";
                R_FrontContext.R_SetStreamingContext(GSM00300ContextConstant.CLASS_APPLICATION, CLASS_APPLICATION);
                R_FrontContext.R_SetStreamingContext(GSM00300ContextConstant.CLASS_ID, _classId);
                R_FrontContext.R_SetStreamingContext(GSM00300ContextConstant.REC_ID_LIST, _recIdCharList);
                var loResult = await _model.GetRftGSBCodeInfoListAsync();
                IdTypeList = new List<GSBCodeInfoDTO>(loResult);
                IdTypeList.Insert(0, new GSBCodeInfoDTO() { CCODE = "", CDESCRIPTION = "" });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxForTypeListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var _classId = "_BS_TAX_FOR_TYPE";
                var _recIdCharList = "";
                R_FrontContext.R_SetStreamingContext(GSM00300ContextConstant.CLASS_APPLICATION, CLASS_APPLICATION);
                R_FrontContext.R_SetStreamingContext(GSM00300ContextConstant.CLASS_ID, _classId);
                R_FrontContext.R_SetStreamingContext(GSM00300ContextConstant.REC_ID_LIST, _recIdCharList);
                var loResult = await _model.GetRftGSBCodeInfoListAsync();
                TaxForTypeList = new List<GSBCodeInfoDTO>(loResult);
                TaxForTypeList.Insert(0, new GSBCodeInfoDTO() { CCODE = "", CDESCRIPTION = "" });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}