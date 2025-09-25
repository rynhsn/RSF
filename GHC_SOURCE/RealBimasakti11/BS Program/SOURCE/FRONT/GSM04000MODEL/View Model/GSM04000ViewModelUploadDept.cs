using GSM04000Common;
using GSM04000Common.DTO_s;
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

namespace GSM04000Model
{
    public class GSM04000ViewModelUploadDept : R_IProcessProgressStatus
    {
        public ObservableCollection<GSM04000ExcelGridDTO> _DepartmentExcelGridData { get; set; } = new ObservableCollection<GSM04000ExcelGridDTO>();
        private List<GSM04000ExcelBatchDTO> _DeptExcelToUploadLst { get; set; } = new List<GSM04000ExcelBatchDTO>();
        public string _sourceFileName { get; set; }
        public bool _isErrorEmptyFile { get; set; } = false;
        public Action _stateChangeAction { get; set; }
        public DataSet _excelDataset { get; set; }
        public Func<Task> _actionDataSetExcel { get; set; }
        public Action<R_APIException> _showErrorAction { get; set; }
        public Action _showSuccessAction { get; set; }
        public string _progressBarMessage = "";
        public int _progressBarPercentage = 0;
        public string _ccompanyId { get; set; }
        public string _cuserId { get; set; }
        public int _sumValidDataDeptExcel { get; set; } = 0;
        public int _sumListDeptExcel { get; set; } = 0;
        public int _sumInvalidDataDeptExcel { get; set; } = 0;
        public bool _visibleError { get; set; } = false;

        public async Task ConvertGridExelToGridDTO(List<GSM04000ExcelBatchDTO> poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                // Onchange Visible Error
                _visibleError = false;
                _sumValidDataDeptExcel = 0;
                _sumInvalidDataDeptExcel = 0;

                // Convert Excel DTO and add SeqNo
                var loData = poEntity.Select((loTemp, i) => new GSM04000ExcelGridDTO
                {
                    INO = i + 1,//add sequence
                    CDEPT_CODE = loTemp.DepartmentCode,
                    CDEPT_NAME = loTemp.DepartmentName,
                    CCENTER_CODE = loTemp.CenterCode,
                    CMANAGER_CODE = loTemp.ManagerName,
                    CBRANCH_CODE = loTemp.BranchCode,
                    LACTIVE = loTemp.Active,
                    LEVERYONE = loTemp.Everyone,
                    CNON_ACTIVE_DATE = loTemp.NonActiveDate,
                    CEMAIL1 = loTemp.Email1,
                    CEMAIL2 = loTemp.Email2,
                    //CNOTES = loTemp.Notes,
                    DNON_ACTIVE_DATE_DISPLAY = !string.IsNullOrWhiteSpace(loTemp.NonActiveDate) ? DateTime.ParseExact(loTemp.NonActiveDate, "yyyyMMdd", CultureInfo.InvariantCulture) : default,//create 1 property to display date
                }).ToList();

                //count
                _sumListDeptExcel = loData.Count;

                //assign to grid object
                _DepartmentExcelGridData = new ObservableCollection<GSM04000ExcelGridDTO>(loData);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task SaveBulk_DeptExcelData()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParameneters;
            try
            {
                //set param
                loUserParameneters = new List<R_KeyValue>();

                //instance processclient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this
                    );

                //assign data
                if (_DepartmentExcelGridData.Count <= 0)
                {
                    return;
                }

                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = _ccompanyId;
                loBatchPar.USER_ID = _cuserId;
                loBatchPar.UserParameters = loUserParameneters;
                loBatchPar.ClassName = "GSM04000Back.GSM04000UploadCls";
                loBatchPar.BigObject = _DepartmentExcelGridData.Select(loTemp => new GSM04000ExcelBatchDTO
                {
                    No = loTemp.INO,
                    DepartmentCode = loTemp.CDEPT_CODE,
                    DepartmentName = loTemp.CDEPT_NAME,
                    CenterCode = loTemp.CCENTER_CODE,
                    ManagerName = loTemp.CMANAGER_CODE,
                    BranchCode = loTemp.CBRANCH_CODE,
                    Everyone = loTemp.LEVERYONE,
                    Active = loTemp.LACTIVE,
                    NonActiveDate = loTemp.CNON_ACTIVE_DATE,
                    Email1 = loTemp.CEMAIL1,
                    Email2= loTemp.CEMAIL2,
                }).ToList();

                var loKeyGuid = await loCls.R_BatchProcess<List<GSM04000ExcelBatchDTO>>(loBatchPar, 11);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_APIException();

            try
            {
                switch (poProcessResultMode)
                {
                    case eProcessResultMode.Success:
                        _visibleError = false;
                        _showSuccessAction();
                        await Task.CompletedTask;
                        break;
                    case eProcessResultMode.Fail:
                        _visibleError = true;
                        await ServiceGetError(pcKeyGuid);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.add(ex);
            }
            // Call Method Action StateHasChange
            _stateChangeAction();
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            _showErrorAction.Invoke(ex);
            // Call Method Action StateHasChange
            _stateChangeAction();
            await Task.CompletedTask;
        }
        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            _progressBarPercentage = pnProgress;
            _progressBarMessage = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            _stateChangeAction();

            await Task.CompletedTask;
        }
        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResultData;
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_DEPARTMENTResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle, if negative seq then error unhandle separate here
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    var loEx = new R_Exception();
                    loUnhandleEx.ForEach(x => loEx.Add(x));
                }

                //error, if positive seq then error handle from inputed data
                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq
                    _DepartmentExcelGridData.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.INO))
                        {
                            x.CNOTES =  loResultData.Where(y => y.SeqNo == x.INO).FirstOrDefault().ErrorMessage;
                            x.CVALID = "N";
                            _sumInvalidDataDeptExcel++;
                        }
                        else
                        {
                            x.CVALID = "Y";
                            _sumValidDataDeptExcel++;
                        }
                    });

                    //Set DataSetTable and get error

                    var loErrorData= _DepartmentExcelGridData.Select(loTemp => new ExportToExcelDTO
                    {
                        No = loTemp.INO,
                        DepartmentCode = loTemp.CDEPT_CODE,
                        DepartmentName = loTemp.CDEPT_NAME,
                        CenterCode = loTemp.CCENTER_CODE,
                        ManagerName = loTemp.CMANAGER_CODE,
                        BranchCode = loTemp.CBRANCH_CODE,
                        Everyone = loTemp.LEVERYONE,
                        Active = loTemp.LACTIVE,
                        NonActiveDate = loTemp.CNON_ACTIVE_DATE,
                        Email1 = loTemp.CEMAIL1,
                        Email2 = loTemp.CEMAIL2,
                        Notes = loTemp.CNOTES,
                    }).ToList();

                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<ExportToExcelDTO>(loErrorData);
                    var loDataTable = R_FrontUtility.R_ConvertTo<ExportToExcelDTO>(loErrorData);
                    loDataTable.TableName = "Department";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Asign Dataset
                    _excelDataset = loDataSet;

                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
