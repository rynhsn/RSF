using PMB01800COMMON.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB01800MODEL.ViewModel
{
    public class PMB01801ViewModel : R_IProcessProgressStatus
    {
        public string _companyId { get; set; }
        public string _userId { get; set; }
        public int TotalRows { get; set; }
        public bool IsError { get; set; } = false;
        public int ValidRows { get; set; }
        public int InvalidRows { get; set; }

        public string Message = "";
        public int Percentage = 0;
        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_APIException> DisplayErrorAction { get; set; }
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_APIException loEx = new R_APIException();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    await SaveListGetError_PricelistServiceAsync(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.add(ex);
            }
            StateChangeAction();
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            DisplayErrorAction.Invoke(ex);
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

        public async Task SaveBulkFile(PMB01800GetDepositListParamDTO Param,List<PMB01800BatchDTO> poDataList)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<PMB01800BatchDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                _companyId = Param.CCOMPANY_ID;
                _userId = Param.CUSER_ID;
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                { Key = Batch_ContextConstant.CPROPERTY_ID, Value = Param.CPROPERTY_ID });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: Batch_ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: Batch_ContextConstant.DEFAULT_HTTP_NAME,
                    poProcessProgressStatus: this);

                //Set Data
                if (poDataList.Count == 0)
                    return;

                ListFromExcel = poDataList.ToList();
                ListFromExcel.Select(x => x.INO = ListFromExcel.IndexOf(x) + 1).ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = Param.CCOMPANY_ID;
                loBatchPar.USER_ID = Param.CUSER_ID;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "PMB01800BACK.PMB01801Cls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<PMB01800BatchDTO>>(loBatchPar, 4);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SaveListGetError_PricelistServiceAsync(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = _companyId,
                    USER_ID = _userId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_GENERATE_DEPOSIT_ADJResources"
                };

                //define
                loCls = new R_ProcessAndUploadClient(
                     pcModuleName: Batch_ContextConstant.DEFAULT_MODULE, 
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: Batch_ContextConstant.DEFAULT_HTTP_NAME 
                    );

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // add error to exception for throwing
                var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();

                var loEx = new R_Exception();
                loUnhandleEx.ForEach(x => loEx.Add(x));

                loException = R_FrontUtility.R_ConvertToAPIException(loEx);
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        
    }
}
