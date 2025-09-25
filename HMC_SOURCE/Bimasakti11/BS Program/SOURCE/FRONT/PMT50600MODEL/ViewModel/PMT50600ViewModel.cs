using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using PMT50600FrontResources;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50600ViewModel : R_ViewModel<PMT50600DetailDTO>
    {
        private PMT50600Model loModel = new PMT50600Model();

        public PMT50600DTO loCreditNote = new PMT50600DTO();

        public PMT50600DetailDTO loSelectedInvoice = new PMT50600DetailDTO();   

        public ObservableCollection<PMT50600DetailDTO> loCreditNoteList = new ObservableCollection<PMT50600DetailDTO>();

        public PMT50600ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        //public GetPMSystemParamResultDTO loAPSystemParamRtn = null;

        //public GetPeriodYearRangeResultDTO loPeriodYearRangeRtn = null;

        //public GetCompanyInfoResultDTO loCompanyInfoRtn = null;

        //public GetGLSystemParamResultDTO loGLSystemParamRtn = null;

        //public GetTransCodeInfoResultDTO loTransCodeInfoRtn = null;

        public GetPMSystemParamDTO loAPSystemParam = null;

        public GetPeriodYearRangeDTO loPeriodYearRange = new GetPeriodYearRangeDTO();

        public GetCompanyInfoDTO loCompanyInfo = null;

        public GetGLSystemParamDTO loGLSystemParam = null;

        public GetTransCodeInfoDTO loTransCodeInfo = null;

        public async Task GetInvoiceListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT50600ParameterDTO loParam = null;
            List<PMT50600DetailDTO> loTemp = null;
            try
            {
                loParam = new PMT50600ParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CDEPT_CODE = loCreditNote.CDEPARTMENT_CODE,
                    CTENANT_ID = loCreditNote.CTENANT_ID,
                    CPERIOD_FROM = Convert.ToString(loCreditNote.IPERIOD_FROM_YEAR) + loCreditNote.CPERIOD_FROM_MONTH,
                    CPERIOD_TO = Convert.ToString(loCreditNote.IPERIOD_TO_YEAR) + loCreditNote.CPERIOD_TO_MONTH
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT50600_GET_INVOICE_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetInvoiceListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    try
                    {
                        x.DDUE_DATE = DateTime.ParseExact(x.CDUE_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        x.DREF_DATE = DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception)
                    {

                    }
                });
                loCreditNoteList = new ObservableCollection<PMT50600DetailDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetPMSystemParamAsync();
                await GetPeriodYearRangeAsync();
                await GetCompanyInfoAsync();
                await GetGLSystemParamAsync();
                await GetTransCodeInfoAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPMSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPMSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetPMSystemParamAsync(new GetPMSystemParamParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID
                });
                loAPSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGLSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetGLSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetGLSystemParamAsync();
                loGLSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodYearRangeAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPeriodYearRangeResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetPeriodYearRangeAsync();
                loPeriodYearRange = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCompanyInfoResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTransCodeInfoResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetTransCodeInfoAsync();
                loTransCodeInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void RefreshInvoiceListValidation()
        {
            bool llCancel = false;
            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(loCreditNote.CDEPARTMENT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = string.IsNullOrWhiteSpace(loCreditNote.CTENANT_ID) && loCreditNote.CTENANT_OPTIONS == "S";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V002"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}