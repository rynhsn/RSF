using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM06000Common;
using System.Data;
using R_BlazorFrontEnd.Helpers;

namespace GSM06000Model.ViewModel
{
    public class UploadCenterViewModel : R_IProcessProgressStatus
    {
        private readonly UploadCenterModel _GSM06000UploadModel = new UploadCenterModel();
        private readonly GSM06000Model _modelGSM06000 = new GSM06000Model();
        public GSM06000ParamDTO CurrentObjectParam = new GSM06000ParamDTO();
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
        public Action? StateChangeAction { get; set; } 
        
        
        public string PropertyValue = "";
        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage;
        public bool IsOverWrite = false;
        public bool ChecklistOverwrite = false;
        public bool IsErrorEmptyFile = false;
        public byte[]? fileByte = null;
        public bool VisibleError = false;
        public bool ButtonSave;
        //New

        public string? CompanyId { get; set; }
        public string? UserId { get; set; }

        public DataSet? ExcelDataSet { get; set; }
        public Func<Task>? ActionDataSetExcel { get; set; }
        public Action<R_Exception>? DisplayErrorAction { get; set; }

        public int SumListExcel { get; set; }
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }

        public ObservableCollection<GSM06000UploadErrorValidateDTO> JournalGroupValidateUploadError { get; set; } = new ObservableCollection<GSM06000UploadErrorValidateDTO>();
        public ObservableCollection<GSM06000DTO> DataJournalGroupList { get; set; } = new ObservableCollection<GSM06000DTO>();
        public List<GSM06000UploadErrorValidateDTO> loUploadLJournalGroupList = new List<GSM06000UploadErrorValidateDTO>();

        #region Data Radio
        public List<GSM06000CodeDTO> loTypeModeList { get; set; } = new List<GSM06000CodeDTO>();
        public List<GSM06000CodeDTO> loTypeBankList { get; set; } = new List<GSM06000CodeDTO>();
        
        
        public async Task GetTypeHeaderParameter()
        {
            var loEx = new R_Exception();

            try
            {
                var loResultTypeList = await _modelGSM06000.GetCashTypeAsync();
                loTypeModeList = loResultTypeList.Type;

                var loResultBankList = await _modelGSM06000.GetBankTypeAsync();
                loTypeBankList = loResultBankList.Type;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        
        #endregion
        
        public async Task ConvertGrid(List<GSM06000UploadFromExcelDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Onchange Visible Error
                VisibleError = false;
                SumValidDataExcel = 0;
                SumInvalidDataExcel = 0;
                //get data from excel
                var loData = poEntity.Select((loTemp, i) 
                    => new GSM06000UploadErrorValidateDTO()
                {
                    NO = i + 1,
                    CCB_CODE = loTemp.CBCode,
                    CCB_NAME = loTemp.CBName,
                    CADDRESS = loTemp.Address,
                    CEMAIL = loTemp.Email,
                    CPHONE1 = loTemp.Phone1,
                    CPHONE2 = loTemp.Phone2,
                    CATTENTION_NAME = loTemp.AttentionName,
                    CATTENTION_POSITION = loTemp.AttentionPosition,
                    CNOTES = loTemp.Notes,
                }).ToList();

                JournalGroupValidateUploadError = new ObservableCollection<GSM06000UploadErrorValidateDTO>(loData);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn>? loResultData;
            R_GetErrorWithMultiLanguageParameter? loParameterData;
            R_ProcessAndUploadClient? loCls;


            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_GS_UPLOAD_CASH_BANK"
                };

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle, jika nilai dari seq negatif maka error unhandle maka dipisahkan disini
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x =>
                        new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }
                // ERROR, jika nilai dari seq positif maka error handle dari data yang diinput
                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq
                    JournalGroupValidateUploadError.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.CNOTES = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = "Y";
                            SumInvalidDataExcel++;
                        }
                        else
                        {
                            SumValidDataExcel++;
                        }
                    });

                    //Set DataSetTable and get error
                    var loExcelData =
                        R_FrontUtility.ConvertCollectionToCollection<GSM06000UploadErrorValidateDTO>(JournalGroupValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo(loExcelData);
                    loDataTable.TableName = "CashBank";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Assign Dataset
                    ExcelDataSet = loDataSet;

                    // Download if get Error
                    //await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveFileBulkFile()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<GSM06000UploadErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loUserParameneters;

            try
            {
                // set Param
#pragma warning disable CS8601 // Possible null reference assignment.
                loUserParameneters = new List<R_KeyValue>
                {
                    new R_KeyValue() { Key = ContextConstant.CBANK_TYPE, Value = CurrentObjectParam.CBANK_TYPE },
                    new R_KeyValue() { Key = ContextConstant.CCB_TYPE, Value = CurrentObjectParam.CCB_TYPE },
                };
#pragma warning restore CS8601 // Possible null reference assignment.

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Set Data
                if (JournalGroupValidateUploadError.Count == 0)
                    return;

                ListFromExcel = JournalGroupValidateUploadError.ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    UserParameters = loUserParameneters,
                    ClassName = "GSM06000Back.GSM06000UploadProcessCls",
                    BigObject = ListFromExcel
                };
                await loCls.R_BatchProcess<List<GSM06000UploadErrorValidateDTO>>(loBatchPar, 11);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Progress Bar
        public bool _statusFinal;

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                    _statusFinal = true;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                    _statusFinal = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            //IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            R_Exception loException = new R_Exception();

            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction.Invoke(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            StateChangeAction();
            await Task.CompletedTask;
        }
        #endregion
    
    }
}
