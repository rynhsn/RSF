using GLT00200COMMON.DTOs.GLT00200;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GLT00200COMMON;
using System.Collections.ObjectModel;
using System.Diagnostics;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using R_APICommonDTO;
using R_BlazorFrontEnd.Excel;
using System.Linq;
using System.Security.Cryptography;
using System.Globalization;

namespace GLT00200MODEL.ViewModel
{
    public class GLT00200ViewModel : R_ViewModel<GLT00200DetailDTO>
    {
        private GLT00200Model loModel = new GLT00200Model();

        public Action StateChangeAction { get; set; }

        public GLT00200HeaderDTO loHeader = new GLT00200HeaderDTO();

        public GLT00200DetailDTO loDetail = new GLT00200DetailDTO();

        public ObservableCollection<GLT00200DetailDTO> loDetailList = new ObservableCollection<GLT00200DetailDTO>();

        public GLT00200DetailResultDTO loRtn = new GLT00200DetailResultDTO();

        public InitialProcessDTO loInitialProcess = new InitialProcessDTO();

        public GetCompanyDTO loCompany = new GetCompanyDTO();

        public GetSystemParamDTO loSystemParam = new GetSystemParamDTO();

        public List<GetDeptLookUpListDTO> loDeptLookUpList = new List<GetDeptLookUpListDTO>();

        public GetSoftPeriodStartDateDTO loSoftPeriodStartDate = new GetSoftPeriodStartDateDTO();

        public GetCurrentPeriodStartDateDTO loCurrentPeriodStartDate = new GetCurrentPeriodStartDateDTO();

        public GetUndoCommitJrnDTO loUndoCommitJrn = new GetUndoCommitJrnDTO();

        public GetTransactionCodeDTO loTransactionCode = new GetTransactionCodeDTO();

        public GetPeriodDTO loPeriod = new GetPeriodDTO();

        public ImportJournalSaveResultDTO loErrorRtn = new ImportJournalSaveResultDTO();

        public GetSuccessProcessResultDTO loSuccessRtn = new GetSuccessProcessResultDTO();

        public string LoginCompanyId = "";

        public string LoginUserId = "";

        public string LoginLanguageId = "";

        public string lcFileName = "";

        public string lcKeyGuid = "";

        public int lnErrorCount = 0;

        public byte[] loFileByte;


        //Upload
        public string PropertyValue = "";
        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;



        #region Upload
        public async Task SaveUploadFile()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchValidatePar;
            R_ProcessAndUploadClient loCls;
            ImportJournalParameterDTO Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GL",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlGL");

                ImportJournalSaveParameterDTO loParam = new ImportJournalSaveParameterDTO()
                {
                    HeaderData = loHeader,
                    DetailData = new List<GLT00200DetailDTO>(loDetailList)
                };

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = LoginCompanyId;
                loBatchValidatePar.USER_ID = LoginUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "GLT00200BACK.GLT00200Cls";
                loBatchValidatePar.BigObject = loParam;

                lcKeyGuid = await loCls.R_BatchProcess<ImportJournalSaveParameterDTO>(loBatchValidatePar, 1);
                await GetErrorCountAsync(lcKeyGuid);

                /*
                                VisibleError = false;*/
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            /*
                        loHeader.NTOTAL_CREDIT = loDetailList.Select(x => x.NCREDIT).Sum();

                        loHeader.NTOTAL_DEBIT = loDetailList.Select(x => x.NDEBIT).Sum();*/


        }/*
        public async Task SaveUploadFile()
        {
            var loEx = new R_Exception();
            try
            {
                ImportJournalSaveParameterDTO loParam = new ImportJournalSaveParameterDTO()
                {
                    HeaderData = loHeader,
                    DetailData = new List<GLT00200DetailDTO>(loDetailList),
                    CPROCESS_ID = lcKeyGuid
                };
                R_FrontContext.R_SetContext(ContextConstant.GLT00200_SAVE_IMPORT_JOURNAL_CONTEXT, loParam);
                loErrorRtn = await loModel.SaveImportJournalAsync();
                lnErrorCount = loErrorRtn.IERROR_COUNT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }*/

        #endregion

        #region Import Journal
        /*
                public async Task ImportJournalStreamAsync()
                {

                    var loEx = new R_Exception();
                    R_BatchParameter loUploadPar;
                    R_ProcessAndUploadClient loCls;
                    List<GLT00200DetailDTO> BigObject = new List<GLT00200DetailDTO>();
                    List<R_KeyValue> loUserParam;

                    try
                    {
                        loUserParam = new List<R_KeyValue>();
                        BigObject = new List<GLT00200DetailDTO>(loDetailList);

                        ImportJournalParameterDTO loParam = new ImportJournalParameterDTO()
                        {
                            HeaderData = loHeader,
                            DetailData = new List<GLT00200DetailDTO>(loDetailList),
                            CPROCESS_ID = lcProcessId,
                            CLANGUAGE_ID = LoginLanguageId
                        };

                        //Instantiate ProcessClient
                        loCls = new R_ProcessAndUploadClient(
                            pcModuleName: "GL",
                            plSendWithContext: true,
                            plSendWithToken: true,
                            pcHttpClientName: "R_DefaultServiceUrlGL",
                            poProcessProgressStatus: this);

                        //preapare Batch Parameter
                        loUploadPar = new R_BatchParameter();
                        loUploadPar.COMPANY_ID = LoginCompanyId;
                        loUploadPar.USER_ID = LoginUserId;
                        loUploadPar.UserParameters = loUserParam;
                        loUploadPar.ClassName = "GLT00200BACK.GLT00200Cls";
                        loUploadPar.BigObject = loParam;

                        lcKeyGuid = await loCls.R_BatchProcess<ImportJournalParameterDTO>(loUploadPar, BigObject.Count - 1);
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }
                    loEx.ThrowExceptionIfErrors();
                }
        *//*
        public async Task ImportJournalStreamAsync()
        {
            var loEx = new R_Exception();
            try
            {
                ImportJournalParameterDTO loParam = new ImportJournalParameterDTO()
                {
                    HeaderData = loHeader,
                    DetailData = new List<GLT00200DetailDTO>(loDetailList),
                    CPROCESS_ID = lcKeyGuid
                };

                R_FrontContext.R_SetStreamingContext(ContextConstant.GLT00200_IMPORT_JOURNAL_STREAMING_CONTEXT, loParam);

                GLT00200DetailResultDTO loResult = await loModel.ImportJournalStreamAsync();

                loDetailList = new ObservableCollection<GLT00200DetailDTO>(loResult.Data);

                loHeader.NTOTAL_CREDIT = loDetailList.Select(x => x.NCREDIT).Sum();

                loHeader.NTOTAL_DEBIT = loDetailList.Select(x => x.NDEBIT).Sum();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
*/
        public async Task ImportJournalStreamAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchValidatePar;
            R_ProcessAndUploadClient loCls;
            ImportJournalParameterDTO Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GL",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlGL");
                
                ImportJournalParameterDTO loParam = new ImportJournalParameterDTO()
                {
                    HeaderData = loHeader,
                    DetailData = new List<GLT00200DetailDTO>(loDetailList),
                    CLANGUAGE_ID = LoginLanguageId
                };

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = LoginCompanyId;
                loBatchValidatePar.USER_ID = LoginUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "GLT00200BACK.GLT00200ValidateUploadCls";
                loBatchValidatePar.BigObject = loParam;

                string lcKeyGuid = await loCls.R_BatchProcess<ImportJournalParameterDTO>(loBatchValidatePar, 1);

                if (loEx.HasError == false)
                {
                    await GetSuccess(lcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
/*
            loHeader.NTOTAL_CREDIT = loDetailList.Select(x => x.NCREDIT).Sum();

            loHeader.NTOTAL_DEBIT = loDetailList.Select(x => x.NDEBIT).Sum();*/
            
            
        }

        public async Task GetInitialProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loInitialProcess = await loModel.InitialProcessAsync();

                loCompany = loInitialProcess.CompanyData;
                loSystemParam = loInitialProcess.SystemParamData;
                loCurrentPeriodStartDate = loInitialProcess.CurrentPeriodStartDateData;
                loDeptLookUpList = loInitialProcess.DeptLookUpListData;
                loSoftPeriodStartDate = loInitialProcess.SoftPeriodStartDateData;
                loUndoCommitJrn = loInitialProcess.UndoCommitJrnData;
                loTransactionCode = loInitialProcess.TransactionCodeData;
                loPeriod = loInitialProcess.PeriodData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void ImportJournalValidation()
        {
            bool llCancel = false;
            string format = "yyyyMMdd";

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(loHeader.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Department is required!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeader.CREF_DATE);
                if (llCancel)
                {
                    loEx.Add("", "Reference Date is required");
                }
                llCancel = Convert.ToDateTime(loHeader.CREF_DATE) < DateTime.ParseExact(loCurrentPeriodStartDate.CSTART_DATE, format, CultureInfo.InvariantCulture);
                if (llCancel)
                {
                    loEx.Add("", "Reference Date cannot be before Current Period!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeader.CDOC_NO) == true && string.IsNullOrWhiteSpace(loHeader.CDOC_DATE) == false;
                if (llCancel)
                {
                    loEx.Add("", "Please input Document No.!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeader.CDOC_DATE) == false && Convert.ToDateTime(loHeader.CDOC_DATE) > DateTime.Now; 
                if (llCancel)
                {
                    loEx.Add("", "Document Date cannot be after today!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeader.CDOC_DATE) == false && Convert.ToDateTime(loHeader.CDOC_DATE) < DateTime.ParseExact(loCurrentPeriodStartDate.CSTART_DATE, format, CultureInfo.InvariantCulture);
                if (llCancel)
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeader.CDOC_NO) == false && string.IsNullOrWhiteSpace(loHeader.CDOC_DATE) == true;
                if (llCancel)
                {
                    loEx.Add("", "Please input Document Date!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeader.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Currency is required");
                }
                llCancel = loHeader.NTOTAL_DEBIT <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Total Debit must be greater than 0!");
                }
                llCancel = loHeader.NTOTAL_DEBIT > 0 && loHeader.NTOTAL_DEBIT != loHeader.NTOTAL_CREDIT;
                if (llCancel)
                {
                    loEx.Add("", "Total Debit must be equal to Total Credit!");
                }
                llCancel = loHeader.NTOTAL_CREDIT <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Total Credit must be greater than 0!");
                }
                llCancel = loHeader.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                }
                llCancel = loHeader.NLCURRENCY_RATE <= 0;
                if (llCancel)
                if (llCancel)
                {
                    loEx.Add("", "Local Currency Rate must be greater than 0!");
                }
                llCancel = loHeader.NBBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                }
                llCancel = loHeader.NBCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Base Currency Rate must be greater than 0!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        
        private async Task GetSuccess(string pcKeyGuid)
        {
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_GLT00200_ERROR_GUID_STREAMING_CONTEXT, pcKeyGuid);

                loSuccessRtn = await loModel.GetSuccessProcessListStreamingAsync();

                List<GLT00200DetailDTO> loTemp = new List<GLT00200DetailDTO>();
                loTemp = loSuccessRtn.Data.Select(x => new GLT00200DetailDTO()
                {
                    CGLACCOUNT_NO = x.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = x.CGLACCOUNT_NAME,
                    CDOCUMENT_NO = x.CDOCUMENT_NO,
                    CDOCUMENT_DATE = DateTime.ParseExact(x.CDOCUMENT_DATE, "yyyyMMdd", null),
                    CCENTER_CODE = x.CCENTER_CODE,
                    CDETAIL_DESC = x.CDETAIL_DESC,
                    CDBCR = x.CDBCR,
                    NTRANS_AMOUNT = x.NTRANS_AMOUNT,
                    NDEBIT = x.NDEBIT,
                    NCREDIT = x.NCREDIT,
                    NLDEBIT = x.NLDEBIT,
                    NLCREDIT = x.NLCREDIT,
                    NBDEBIT = x.NBDEBIT,
                    NBCREDIT = x.NBCREDIT,

                }).ToList();

                loDetailList = new ObservableCollection<GLT00200DetailDTO>(loTemp);

                loHeader.NTOTAL_CREDIT = loDetailList.Select(x => x.NCREDIT).Sum();

                loHeader.NTOTAL_DEBIT = loDetailList.Select(x => x.NDEBIT).Sum();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task GetErrorCountAsync(string pcKeyGuid)
        {
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.UPLOAD_GLT00200_ERROR_COUNT_GUID_CONTEXT, pcKeyGuid);
                GetErrorCountParameterDTO loParam = new GetErrorCountParameterDTO()
                {
                    CKEY_GUID = pcKeyGuid
                };

                loErrorRtn = await loModel.GetErrorCountAsync(loParam);
                lnErrorCount = loErrorRtn.IERROR_COUNT;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region Template
        public async Task<TemplateImportJournalDTO> DownloadTemplateImportJournalAsync()
        {
            var loEx = new R_Exception();
            TemplateImportJournalDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateImportJournalAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

    }
}
