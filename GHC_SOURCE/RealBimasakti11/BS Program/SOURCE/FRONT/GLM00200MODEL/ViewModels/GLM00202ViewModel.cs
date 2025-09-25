using GLM00200COMMON;
using GLM00200COMMON.DTO_s;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GLM00200MODEL.ViewModels
{
    public class GLM00202ViewModel : R_IProcessProgressStatus //upload VM
    {
        //var
        public ObservableCollection<RecurringUploadErrorDTO> RecurringUploadErrors { get; set; } = new ObservableCollection<RecurringUploadErrorDTO>();
        private ObservableCollection<RecurringUploadDTO> RecurringUploads { get; set; } = new ObservableCollection<RecurringUploadDTO>();
        public Action _stateChangeAction { get; set; }
        public string _sourceFileName { get; set; }
        public DataSet _excelDataset { get; set; }
        public Func<Task> _actionDataSetExcel { get; set; }
        public Action<R_APIException> _showErrorAction { get; set; }
        public Action<string, int> _setPercentageAndMessageAction { get; set; }
        public Action _showSuccessAction { get; set; }
        public string _ccompanyId { get; set; }
        public string _cuserId { get; set; }
        public string _cpropertyId { get; set; }
        public int _sumValidData_RecurringExcel { get; set; } = 0;
        public int _sumList_RecurringExcel { get; set; } = 0;
        public int _sumInvalidData_RecurringExcel { get; set; } = 0;
        public bool _visibleError { get; set; } = false;
        public string _progressBarMessage = "";
        public int _progressBarPercentage = 0;

        //method    
        public async Task Savebatch_RecurringAsync(string pcCompanyId, string pcUserId)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            try
            {
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: RecurringJournalContext.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: RecurringJournalContext.DEFAULT_HTTP_NAME,
                    poProcessProgressStatus: this);

                if (RecurringUploadErrors.Count == 0)
                    return;
                loBatchPar = new R_BatchParameter
                {
                    COMPANY_ID = pcCompanyId,
                    USER_ID = pcUserId,
                    UserParameters = new List<R_KeyValue>(),
                    ClassName = "GLM00200BACK.GLM00200UploadCls",
                    BigObject = R_FrontUtility.ConvertCollectionToCollection<RecurringUploadDTO>(RecurringUploadErrors)
                };

                await loCls.R_BatchProcess<List<RecurringUploadDTO>>(loBatchPar, 30);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        //process
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_APIException loEx = new R_APIException();
            List<R_ErrorStatusReturn> loResult = null;
            try
            {
                switch (poProcessResultMode)
                {
                    case eProcessResultMode.Success:
                        _visibleError = false;
                        RecurringUploadErrors.ToList().ForEach(x =>
                        {
                            x.Valid = "Y";
                            _sumValidData_RecurringExcel++;
                        });
                        _showSuccessAction();
                        break;
                    case eProcessResultMode.Fail:
                        _visibleError = true;
                        loResult = await ServiceGetError(pcKeyGuid);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.add(ex);
            }
            _stateChangeAction();
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            _showErrorAction.Invoke(ex);
            _stateChangeAction();
            await Task.CompletedTask;
        }
        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            _progressBarMessage = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            _progressBarPercentage = pnProgress;
            _progressBarMessage = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            _setPercentageAndMessageAction(_progressBarMessage, _progressBarPercentage);
            _stateChangeAction();
            await Task.CompletedTask;
        }

        //error handling
        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn>? loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = _ccompanyId,
                    USER_ID = _cuserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_GL_RECURRING_UPLOAD"
                };

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: RecurringJournalContext.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: RecurringJournalContext.DEFAULT_HTTP_NAME);

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                RecurringUploadErrors.ToList().ForEach(x =>
                {
                    if (loResultData.Any(y => y.SeqNo == x.SEQ_NO))
                    {
                        x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_NO).FirstOrDefault().ErrorMessage;
                        x.Valid = "N";
                        _sumInvalidData_RecurringExcel++;
                    }
                    else
                    {
                        x.Valid = "Y";
                        _sumInvalidData_RecurringExcel++;
                    }
                });

                // unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    var loEx = new R_Exception();
                    loUnhandleEx.ForEach(x => loEx.Add(x));
                    loException = R_FrontUtility.R_ConvertToAPIException(loEx);
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResultData;
        }
    }
}
