using PMT01500Common.Context;
using PMT01500Common.DTO._1._AgreementList.Upload;
using PMT01500Common.Utilities.Front;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_APICommonDTO;

namespace PMT01500Model.ViewModel
{
    public class PMT01500UploadViewModel : R_IProcessProgressStatus
    {
        private readonly PMT01500AgreementListModel _PMT01500UploadModel = new PMT01500AgreementListModel();
        public PMT01500UploadParameterContext CurrentObjectParam = new PMT01500UploadParameterContext();
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
        public Action? ShowSuccessAction { get; set; }

        public int SumListExcel { get; set; }
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }

        public ObservableCollection<PMT01500UploadErrorValidateDTO> LeaseManagerValidateUploadError { get; set; } = new ObservableCollection<PMT01500UploadErrorValidateDTO>();
       //public ObservableCollection<PMT01500DTO> DataLeaseManagerList { get; set; } = new ObservableCollection<PMT01500DTO>();
        public List<PMT01500UploadErrorValidateDTO> loUploadLeaseManagerList = new List<PMT01500UploadErrorValidateDTO>();

        #region Data Radio
        public List<PMT01500PropertyListDTO> loPropertyList { get; set; } = new List<PMT01500PropertyListDTO>();
        

       public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _PMT01500UploadModel.GetPropertyListAsync();
                loPropertyList = new List<PMT01500PropertyListDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        public async Task ConvertGrid(List<PMT01500UploadFromExcelDTO> poEntity)
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
                    => new PMT01500UploadErrorValidateDTO()
                    {
                        NO = i + 1,
                        CCOMPANY_ID = CompanyId,
                        CPROPERTY_ID = CurrentObjectParam.CPROPERTY_ID,
                        CDEPT_CODE = loTemp.Department,
                        CREF_NO = loTemp.AgreementNo,
                        CREF_DATE = loTemp.AgreementDate,
                        CBUILDING_ID = loTemp.Building,
                        CDOC_NO = loTemp.DocumentNo,
                        CDOC_DATE = loTemp.DocumentDate,
                        CSTART_DATE = loTemp.StartDate,
                        CEND_DATE = loTemp.EndDate,
                        CMONTH = loTemp.Month,
                        CYEAR = loTemp.Year,
                        CDAY = loTemp.Day,
                        CSALESMAN_ID = loTemp.Salesman,
                        CTENANT_ID = loTemp.Tenant,
                        CUNIT_DESCRIPTION = loTemp.UnitDescription,
                        CCURRENCY_CODE = loTemp.Currency,
                        CLEASE_MODE = loTemp.LeaseMode,
                        CCHARGE_MODE = loTemp.ChargeMode,
                        CNOTESError = loTemp.NotesError,
                        CNOTES = loTemp.Notes,
                        //Notesnya ada 2 Coba tanyakan
                        //Ada Notes dari DTO, dan Upload membutuhkan Notes
                    }).ToList();

                LeaseManagerValidateUploadError = new ObservableCollection<PMT01500UploadErrorValidateDTO>(loData);

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
#pragma warning disable CS8601 // Possible null reference assignment.
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_UPLOAD_AGREEMENT"
                };
#pragma warning restore CS8601 // Possible null reference assignment.

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

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
                    LeaseManagerValidateUploadError.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.CNOTESError = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
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
                        R_FrontUtility.ConvertCollectionToCollection<PMT01500UploadErrorValidateDTO>(LeaseManagerValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo(loExcelData);
                    loDataTable.TableName = "LeaseManager";

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
            List<PMT01500UploadErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loUserParameneters;

            try
            {
                // set Param
#pragma warning disable CS8601 // Possible null reference assignment.
                loUserParameneters = new List<R_KeyValue>
                {
                    new R_KeyValue() { Key = PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, Value = CurrentObjectParam.CPROPERTY_ID },
                };
#pragma warning restore CS8601 // Possible null reference assignment.

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //Set Data
                if (LeaseManagerValidateUploadError.Count == 0)
                    return;

                ListFromExcel = LeaseManagerValidateUploadError.ToList();

                //preapare Batch Parameter
#pragma warning disable CS8601 // Possible null reference assignment.
                loBatchPar = new R_BatchParameter
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    UserParameters = loUserParameneters,
                    ClassName = "PMT01500Back.PMT01500UploadProcessCls",
                    BigObject = ListFromExcel
                };
#pragma warning restore CS8601 // Possible null reference assignment.
                await loCls.R_BatchProcess<List<PMT01500UploadErrorValidateDTO>>(loBatchPar, 20);
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
                    ShowSuccessAction();
                    _statusFinal = true;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    VisibleError = true;
                    _statusFinal = false;
                    await ServiceGetError(pcKeyGuid);
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
