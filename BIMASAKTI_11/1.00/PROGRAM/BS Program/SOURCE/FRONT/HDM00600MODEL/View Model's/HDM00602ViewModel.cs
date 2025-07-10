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
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HDM00600MODEL.View_Model_s
{
    public class HDM00602ViewModel : R_IProcessProgressStatus //upload
    {
        //var
        private HDM00600Model _model = new HDM00600Model();
        public ObservableCollection<PriceListExcelDisplayDTO> _pricelist_List { get; set; } = new ObservableCollection<PriceListExcelDisplayDTO>();
        public List<PricelistReadExcelDTO> _pricelist_ReadExcel { get; set; } = new List<PricelistReadExcelDTO>();
        public List<PricelistSaveToExcelDTO> _pricelist_SaveExcel { get; set; } = new List<PricelistSaveToExcelDTO>();
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
        public string _cpropertyName { get; set; }
        public int _sumValidDataPricelistExcel { get; set; } = 0;
        public int _sumListPricelistExcel { get; set; } = 0;
        public int _sumInvalidDataPricelistExcel { get; set; } = 0;
        public bool _visibleError { get; set; } = false;
        public string _progressBarMessage = "";
        public int _progressBarPercentage = 0;

        //helper
        public void ConvertGridExelToGridDTO(List<PricelistReadExcelDTO> poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = poEntity.Select((loTemp, i) => new PriceListExcelDisplayDTO
                {
                    INO = i + 1,//add sequence
                    CPRICELIST_ID = loTemp.PricelistID,
                    CPRICELIST_NAME = loTemp.PricelistName,
                    CDEPT_CODE = loTemp.Dept,
                    CCHARGES_ID = loTemp.ChargesID,
                    CUNIT = loTemp.Unit,
                    CCURRENCY_CODE = loTemp.Curr,
                    IPRICE = int.Parse(loTemp.Price),
                    CDESCRIPTION = loTemp.Description,
                    CSTART_DATE = loTemp.StartDate,
                    DSTART_DATE = !string.IsNullOrWhiteSpace(loTemp.StartDate)
                    ? DateTime.ParseExact(loTemp.StartDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                    : (DateTime?)null,
                }).ToList();

                //assign to grid object
                _pricelist_List = new ObservableCollection<PriceListExcelDisplayDTO>(loData) ?? new ObservableCollection<PriceListExcelDisplayDTO>();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public List<PricelistSaveToExcelDTO>? ConvertGridDTOToExcel(List<PriceListExcelDisplayDTO> poEntity)
        {
            R_Exception loEx = new R_Exception();
            List<PricelistSaveToExcelDTO> loRtn = null;
            try
            {
                // Convert Excel DTO and add SeqNo
                loRtn = poEntity.Select((loTemp, i) => new PricelistSaveToExcelDTO
                {
                    No = loTemp.INO,
                    PricelistID = loTemp.CPRICELIST_ID,
                    PricelistName = loTemp.CPRICELIST_NAME,
                    Dept = loTemp.CDEPT_CODE,
                    ChargesID = loTemp.CCHARGES_ID,
                    Unit = loTemp.CUNIT,
                    Curr = loTemp.CCURRENCY_CODE,
                    Price = loTemp.IPRICE.ToString(),
                    Description = loTemp.CDESCRIPTION,
                    StartDate = loTemp.CSTART_DATE,
                    Valid = loTemp.CVALID,
                    Notes = loTemp.CNOTES,
                }).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        //batch
        public async Task SaveBatch_PricelistExcelData()
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
                        Value = _cpropertyId
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
                    loItem.CPRICELIST_ID.Trim();
                    loItem.CPRICELIST_ID.Trim();
                    loItem.CPRICELIST_NAME.Trim();
                    loItem.CDEPT_CODE.Trim();
                    loItem.CCHARGES_ID.Trim();
                    loItem.CUNIT.Trim();
                    loItem.CCURRENCY_CODE.Trim();
                    loItem.CDESCRIPTION.Trim();
                    loItem.CSTART_DATE.Trim();
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
                    COMPANY_ID = _ccompanyId,
                    USER_ID = _cuserId,
                    UserParameters = loBatchParUserParameters,
                    ClassName = "HDM00600BACK.HDM00602Cls",
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
                        _pricelist_List.ToList().ForEach(x =>
                        {
                            x.CVALID = "Y";
                            _sumValidDataPricelistExcel++;
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
            List<R_ErrorStatusReturn> loResultData = null;
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
                    RESOURCE_NAME = "RSP_HD_UPLOAD_PRICELIST_PROCESSResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "HD",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlHD");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                _pricelist_List.ToList().ForEach(x =>
                {
                    if (loResultData.Any(y => y.SeqNo == x.INO))
                    {
                        x.CNOTES = loResultData.Where(y => y.SeqNo == x.INO).FirstOrDefault().ErrorMessage;
                        x.CVALID = "N";
                        _sumInvalidDataPricelistExcel++;
                    }
                    else
                    {
                        x.CVALID = "Y";
                        _sumInvalidDataPricelistExcel++;
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
