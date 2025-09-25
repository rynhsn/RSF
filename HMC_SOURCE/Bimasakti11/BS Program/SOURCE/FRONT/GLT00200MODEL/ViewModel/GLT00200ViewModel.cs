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
using System.ComponentModel.Design;
using System.Data;

namespace GLT00200MODEL.ViewModel
{
    public class GLT00200ViewModel : R_ViewModel<GLT00200DetailDTO>, R_IProcessProgressStatus
    {
        private GLT00200Model loModel = new GLT00200Model();
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }
        public Action<R_Exception> ShowErrorAction { get; set; }

        public GLT00200HeaderDTO loHeader = null;

        public GLT00200HeaderDTO loHeaderDisplay = new GLT00200HeaderDTO();

        public GLT00200DetailDTO loDetail = null;

        public List<GLT00200DetailDTO> loDetailList = null;

        public ObservableCollection<GLT00200DetailDTO> loDetailDisplayList = new ObservableCollection<GLT00200DetailDTO>();

        public GLT00200DetailResultDTO loRtn = null;

        public InitialProcessDTO loInitialProcess = null;

        public GetCompanyDTO loCompany = null;

        public GetSystemParamDTO loSystemParam = null;

        public List<GetDeptLookUpListDTO> loDeptLookUpList = null;

        public GetSoftPeriodStartDateDTO loSoftPeriodStartDate = null;

        public GetCurrentPeriodStartDateDTO loCurrentPeriodStartDate = null;

        public GetUndoCommitJrnDTO loUndoCommitJrn = null;

        public GetTransactionCodeDTO loTransactionCode = null;

        public GetPeriodDTO loPeriod = null;

        public ImportJournalSaveResultDTO loErrorRtn = null;

        public GetSuccessProcessResultDTO loSuccessRtn = null;

        public string LoginCompanyId = "";

        public string lcKeyGuid = "";

        public string LoginUserId = "";

        public string LoginLanguageId = "";

        public string lcFileName = "";

        public int lnErrorCount = 0;

        public bool IsSaveSuccess = false;

        public byte[] loFileByte;

        public bool IsSeparateDCAmount = false;

        public string PROCESS_TYPE;

        public const string CALCULATE_PROCESS = "CALCULATE_PROCESS";

        public const string SAVE_PROCESS = "SAVE_PROCESS";



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
                    poProcessProgressStatus: this,
                    pcHttpClientName: "R_DefaultServiceUrlGL");

                ImportJournalSaveParameterDTO loParam = new ImportJournalSaveParameterDTO()
                {
                    HeaderData = loHeaderDisplay,
                    DetailData = new List<GLT00200DetailDTO>(loDetailDisplayList)
                };

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = LoginCompanyId;
                loBatchValidatePar.USER_ID = LoginUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "GLT00200BACK.GLT00200Cls";
                loBatchValidatePar.BigObject = loParam;

                PROCESS_TYPE = SAVE_PROCESS;
                lcKeyGuid = await loCls.R_BatchProcess<ImportJournalSaveParameterDTO>(loBatchValidatePar, 17);
                //await GetErrorCountAsync(lcKeyGuid);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        #endregion

        #region Import Journal
        public async Task ImportJournalStreamAsync()
        {
            R_Exception loEx = new R_Exception();
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
                    poProcessProgressStatus: this,
                    pcHttpClientName: "R_DefaultServiceUrlGL");

                List<GLT00200DetailDTO> loDetailTemp = new List<GLT00200DetailDTO>();

                if (IsSeparateDCAmount == true)
                {
                    foreach (var item in loDetailList)
                    {
                        if (item.NDEBIT > 0)
                        {
                            item.CDBCR = "D";
                            item.NTRANS_AMOUNT = item.NDEBIT;
                        }
                        else
                        {
                            item.CDBCR = "C";
                            item.NTRANS_AMOUNT = item.NCREDIT;
                        }
                    }
                }

                ImportJournalParameterDTO loParam = new ImportJournalParameterDTO()
                {
                    HeaderData = loHeader,
                    DetailData = loDetailList,
                    CLANGUAGE_ID = LoginLanguageId
                };

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = LoginCompanyId;
                loBatchValidatePar.USER_ID = LoginUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "GLT00200BACK.GLT00200ValidateUploadCls";
                loBatchValidatePar.BigObject = loParam;

                PROCESS_TYPE = CALCULATE_PROCESS;
                string lcKeyGuid = await loCls.R_BatchProcess<ImportJournalParameterDTO>(loBatchValidatePar, 2);

                /*if (loEx.HasError == false)
                {
                    await GetSuccess(lcKeyGuid);
                }*/
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            /*
                        loHeaderDisplay.NTOTAL_CREDIT = loDetailDisplayList.Select(x => x.NCREDIT).Sum();

                        loHeaderDisplay.NTOTAL_DEBIT = loDetailDisplayList.Select(x => x.NDEBIT).Sum();*/
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
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Department is required!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CREF_DATE);
                if (llCancel)
                {
                    loEx.Add("", "Reference Date is required");
                }
                llCancel = Convert.ToDateTime(loHeaderDisplay.CREF_DATE) < DateTime.ParseExact(loCurrentPeriodStartDate.CSTART_DATE, format, CultureInfo.InvariantCulture);
                if (llCancel)
                {
                    loEx.Add("", "Reference Date cannot be before Current Period!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CDOC_NO) == true && string.IsNullOrWhiteSpace(loHeaderDisplay.CDOC_DATE) == false;
                if (llCancel)
                {
                    loEx.Add("", "Please input Document No.!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CDOC_DATE) == false && Convert.ToDateTime(loHeaderDisplay.CDOC_DATE) > DateTime.Now; 
                if (llCancel)
                {
                    loEx.Add("", "Document Date cannot be after today!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CDOC_DATE) == false && Convert.ToDateTime(loHeaderDisplay.CDOC_DATE) < DateTime.ParseExact(loCurrentPeriodStartDate.CSTART_DATE, format, CultureInfo.InvariantCulture);
                if (llCancel)
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CDOC_NO) == false && string.IsNullOrWhiteSpace(loHeaderDisplay.CDOC_DATE) == true;
                if (llCancel)
                {
                    loEx.Add("", "Please input Document Date!");
                }
                llCancel = string.IsNullOrWhiteSpace(loHeaderDisplay.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Currency is required");
                }
                llCancel = loHeaderDisplay.NTOTAL_DEBIT <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Total Debit must be greater than 0!");
                }
                llCancel = loHeaderDisplay.NTOTAL_DEBIT > 0 && loHeaderDisplay.NTOTAL_DEBIT != loHeaderDisplay.NTOTAL_CREDIT;
                if (llCancel)
                {
                    loEx.Add("", "Total Debit must be equal to Total Credit!");
                }
                llCancel = loHeaderDisplay.NTOTAL_CREDIT <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Total Credit must be greater than 0!");
                }
                llCancel = loHeaderDisplay.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                }
                llCancel = loHeaderDisplay.NLCURRENCY_RATE <= 0;
                if (llCancel)
                if (llCancel)
                {
                    loEx.Add("", "Local Currency Rate must be greater than 0!");
                }
                llCancel = loHeaderDisplay.NBBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                }
                llCancel = loHeaderDisplay.NBCURRENCY_RATE <= 0;
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
                loTemp = loSuccessRtn.Data.Select((x, i) => new GLT00200DetailDTO()
                {
                    SEQ_NO = i + 1,
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
                    CNOTES = "",
                    CVALID = ""

                }).ToList();

                loDetailDisplayList = new ObservableCollection<GLT00200DetailDTO>(loTemp);
                loHeaderDisplay = loHeader;
                loHeaderDisplay.NTOTAL_CREDIT = loDetailDisplayList.Select(x => x.NCREDIT).Sum();
                loHeaderDisplay.NTOTAL_DEBIT = loDetailDisplayList.Select(x => x.NDEBIT).Sum();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

/*
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
*/
        #region Template
        public async Task<TemplateImportJournalDTO> DownloadTemplateImportJournalAsync()
        {
            R_Exception loEx = new R_Exception();
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

        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();
            List<R_ErrorStatusReturn> loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = LoginCompanyId,
                    USER_ID = LoginUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_GL_IMPORT_JOURNALResources"
                };
                //loCls = new R_ProcessAndUploadClient(plSendWithContext: false, plSendWithToken: false, );
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GL",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    poProcessProgressStatus: this,
                    pcHttpClientName: "R_DefaultServiceUrlGL");

                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
                //Console.WriteLine(ObjectDumper.Dump(loResultData));
                /*loResultData.ForEach(x =>
                {
                    loException.Add(x.SeqNo.ToString(), x.ErrorMessage);
                });*/
            }
            catch (Exception ex)
            {/*
                if (ex is R_APICommonDTO.R_IException)
                    //Console.WriteLine(ObjectDumper.Dump((R_APICommonDTO.R_IException)ex.ErrorList));
                else
                    Console.WriteLine(ex);*/
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResultData;
        }

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_Exception loException = new R_Exception();
            List<R_ErrorStatusReturn> loResult = null;
            switch (PROCESS_TYPE)
            {
                default:
                    break;
                case CALCULATE_PROCESS :
                    if (poProcessResultMode == eProcessResultMode.Success)
                    {
                        await GetSuccess(pcKeyGuid);
                    }
                    else
                    {
                        try
                        {
                            loResult = await ServiceGetError(pcKeyGuid);
                            loResult.ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                        }
                        catch (Exception ex)
                        {
                            loException.Add(ex);
                        }
                        if (loException.HasError)
                        {
                            ShowErrorAction(loException);
                        }
                    }
                    break;
                case SAVE_PROCESS :
                    if (poProcessResultMode == eProcessResultMode.Success)
                    {
                        IsSaveSuccess = true;
                        ShowSuccessAction();
                    }
                    else
                    {
                        IsSaveSuccess = false;
                        try
                        {
                            loResult = await ServiceGetError(pcKeyGuid);
                            loDetailDisplayList.ToList().ForEach(x =>
                            {
                                if (loResult.Any(y => y.SeqNo == x.SEQ_NO))
                                {
                                    x.CNOTES = loResult.Where(y => y.SeqNo == x.SEQ_NO).FirstOrDefault().ErrorMessage;
                                    x.CVALID = "N";
                                }
                                else
                                {
                                    x.CVALID = "Y";
                                }
                            });

                            if (loResult.Any(x => x.SeqNo < 0))
                            {
                                loResult.Where(x => x.SeqNo < 0).ToList().ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                            }
                        }
                        catch (Exception ex)
                        {
                            loException.Add(ex);
                        }
                        ShowErrorAction(loException);
                    }
                    break;
            }
            if (poProcessResultMode == eProcessResultMode.Fail)
            {
            }
            StateChangeAction();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            R_Exception loException = new R_Exception();
            ex.ErrorList.ForEach(l =>
            {
                loException.Add(l.ErrNo, l.ErrDescp);
            });
            ShowErrorAction(loException);

            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            StateChangeAction();

            await Task.CompletedTask;
        }

        #endregion

    }
}
