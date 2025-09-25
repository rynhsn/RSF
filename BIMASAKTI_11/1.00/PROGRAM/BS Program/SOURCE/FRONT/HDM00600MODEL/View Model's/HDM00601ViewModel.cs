using HDM00600COMMON;
using HDM00600COMMON.DTO;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HDM00600MODEL.View_Model_s
{
    public class HDM00601ViewModel : R_IProcessProgressStatus
    {
        //var
        private HDM00600Model _model = new HDM00600Model();
        public ObservableCollection<PricelistDTO> _pricelist_List { get; set; } = new ObservableCollection<PricelistDTO>();
        public string _propertyId { get; set; } = "";
        public string _propertyName { get; set; } = "";
        public string _propertyCurr { get; set; } = "";
        public string _pricelistId { get; set; } = "";
        public string _validId { get; set; } = "";
        public string _companyId { get; set; }
        public string _userId { get; set; }
        public Action StateChangeAction { get; set; }
        public Action<R_APIException> DisplayErrorAction { get; set; }
        public Func<Task> ShowSuccessAction { get; set; }
        public string Message = "";
        public int Percentage = 0;

        //savebatch
        public async Task SaveList_PricelistAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>
                {
                    new R_KeyValue()
                    {
                        Key = PricelistMaster_ContextConstant.CPROPERTY_ID,
                        Value = _propertyId

                    }
                };

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: PricelistMaster_ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PricelistMaster_ContextConstant.DEFAULT_HTTP_NAME,
                    poProcessProgressStatus: this);

                //Set Data
                if (_pricelist_List.Count == 0)
                    return;

                //mapping data
                var loMappingData = R_FrontUtility.ConvertCollectionToCollection<PricelistBatchDTO>(_pricelist_List);
                foreach (var loItem in loMappingData)
                {
                    loItem.CPRICELIST_ID = loItem.CPRICELIST_ID?.Trim() ?? "";
                    loItem.CPRICELIST_NAME = loItem.CPRICELIST_NAME?.Trim() ?? "";
                    loItem.CDEPT_CODE = loItem.CDEPT_CODE?.Trim() ?? "";
                    loItem.CCHARGES_ID = loItem.CCHARGES_ID?.Trim() ?? "";
                    loItem.CUNIT = loItem.CUNIT?.Trim() ?? "";
                    loItem.CCURRENCY_CODE = loItem.CCURRENCY_CODE?.Trim() ?? "";
                    loItem.CDESCRIPTION = loItem.CDESCRIPTION?.Trim() ?? "";
                    loItem.CSTART_DATE = loItem.CSTART_DATE?.Trim() ?? "";
                }
                var loNumberedData = new List<PricelistBatchDTO>(
                    loMappingData.Select((item, index) =>
                {
                    item.NO = index + 1; // Assign row number starting from 1
                    return item;
                }));

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter
                {
                    COMPANY_ID = _companyId,
                    USER_ID = _userId,
                    UserParameters = loBatchParUserParameters,
                    ClassName = "HDM00600BACK.HDM00601Cls",
                    BigObject = loNumberedData
                };

                await loCls.R_BatchProcess<List<PricelistBatchDTO>>(loBatchPar, 4);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //batchprocess
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

        //helper
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
                    RESOURCE_NAME = "RSP_HD_ADD_PRICELIST_PROCESSResources"
                };

                //define
                loCls = new R_ProcessAndUploadClient(
                     pcModuleName: PricelistMaster_ContextConstant.DEFAULT_MODULE, //HD
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PricelistMaster_ContextConstant.DEFAULT_HTTP_NAME //R_DefaultServiceUrlHD
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
