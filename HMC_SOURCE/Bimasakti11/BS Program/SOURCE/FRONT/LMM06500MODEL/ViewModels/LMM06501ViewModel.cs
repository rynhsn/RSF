using LMM06500COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
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

namespace LMM06500MODEL
{
    public class LMM06501ViewModel : R_IProcessProgressStatus
    {
        // Action StateHasChanged
        public Action StateChangeAction { get; set; }

        // Action Get Error Unhandle
        public Action<R_APIException> ShowErrorAction { get; set; }

        // Action Get DataSet
        public Func<Task> ActionDataSetExcel { get; set; }


        // DataSet Excel 
        public DataSet ExcelDataSet { get; set; }

        public string PropertyValue { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public string SourceFileName { get; set; } = "";
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public bool OverwriteData { get; set; } = false;
        public bool VisibleError { get; set; } = false;
        public bool BtnSave { get; set; } = true;
        public int SumListStaffExcel { get; set; }
        public int SumValidDataStaffExcel { get; set; }
        public int SumInvalidDataStaffExcel { get; set; }
        public string CompanyID { get; set; }
        public string UserId { get; set; }

        // Grid Display Staff Upload
        public ObservableCollection<LMM06501ErrorValidateDTO> StaffValidateUploadError { get; set; } = new ObservableCollection<LMM06501ErrorValidateDTO>();

        public async Task ConvertGrid(List<LMM06501DTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Onchange Visible Error
                VisibleError = false;
                SumValidDataStaffExcel = 0;
                SumInvalidDataStaffExcel = 0;

                // Convert Excel DTO and add SeqNo
                var loData = poEntity.Select((loTemp, i) => new LMM06501ErrorValidateDTO
                {
                    NO = i+1,
                    StaffId = loTemp.StaffId,
                    StaffName = loTemp.StaffName,
                    Active = loTemp.Active,
                    Department = loTemp.Department,
                    Position = loTemp.Position,
                    JoinDate = loTemp.JoinDate,
                    JoinDateDisplay = !string.IsNullOrWhiteSpace(loTemp.JoinDate) ? DateTime.ParseExact(loTemp.JoinDate, "yyyyMMdd", CultureInfo.InvariantCulture) : default,
                    Supervisor = loTemp.Supervisor,
                    EmailAddress = loTemp.EmailAddress,
                    MobileNo1 = loTemp.MobileNo1,
                    MobileNo2 = loTemp.MobileNo2,
                    Gender = loTemp.Gender,
                    Address = loTemp.Address,
                    InActiveDate = loTemp.InActiveDate,
                    InActiveDateDisplay = !string.IsNullOrWhiteSpace(loTemp.InActiveDate) ? DateTime.ParseExact(loTemp.InActiveDate, "yyyyMMdd", CultureInfo.InvariantCulture) : default,
                    InactiveNote = loTemp.InactiveNote,
                    Var_Exists = loTemp.Var_Exists,
                    Var_Overwrite = loTemp.Var_Overwrite
                }).ToList();

                SumListStaffExcel = loData.Count;

                StaffValidateUploadError = new ObservableCollection<LMM06501ErrorValidateDTO>(loData);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        public async Task SaveBulkFile()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<LMM06501ErrorValidateDTO> Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                // set Param
                loUserParameneters = new List<R_KeyValue>();
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.CPROPERTY_ID, Value = PropertyValue });
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.COVERWRITE, Value = OverwriteData });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //Set Data
                if (StaffValidateUploadError.Count == 0)
                    return;

                Bigobject = StaffValidateUploadError.ToList<LMM06501ErrorValidateDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = CompanyID;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loUserParameneters;
                loBatchPar.ClassName = "LMM06500BACK.LMM06501Cls";
                loBatchPar.BigObject = Bigobject;

                var lcGuid = await loCls.R_BatchProcess<List<LMM06501ErrorValidateDTO>>(loBatchPar, 20);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            

            // Call Method Action Error Unhandle
            ShowErrorAction(ex);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_LM_UPLOAD_STAFFResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }
                else
                {
                    // Display Error Handle if get seq
                    StaffValidateUploadError.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = true;
                            SumInvalidDataStaffExcel++;
                        }
                        else
                        {
                            SumValidDataStaffExcel++;
                        }
                    });

                    //Set DataSetTable and get error
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<LMM06501ExcelDTO>(StaffValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo<LMM06501ExcelDTO>(loExcelData);
                    loDataTable.TableName = "Staff";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Asign Dataset
                    ExcelDataSet = loDataSet;

                    // Dowload if get Error
                    await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }

}
