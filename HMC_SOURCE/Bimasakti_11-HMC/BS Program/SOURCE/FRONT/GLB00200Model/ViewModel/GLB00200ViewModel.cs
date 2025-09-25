using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLB00200Common;
using GLB00200FrontResources;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace GLB00200Model.ViewModel
{
    public class GLB00200ViewModel : R_ViewModel<GLB00200DTO>, R_IProcessProgressStatus
    {
        private GLB00200Model _modelGLB00200Model = new GLB00200Model();

        //List for Front
        public ObservableCollection<GLB00200DTO> ReversingJournalProcessList =
            new ObservableCollection<GLB00200DTO>();
        public ObservableCollection<GLB00200JournalDetailDTO> DetailReversingJournalList =
            new ObservableCollection<GLB00200JournalDetailDTO>();
        //List for Process
        public List<GLB00200DTO> loProcessReversingList = new List<GLB00200DTO>();
        //List for Convert
        public List<int> NO_Convert = new List<int>();

        public GLB00200InitalProcessDTO GetInitialProcess = new GLB00200InitalProcessDTO();
        public int PeriodYear = DateTime.Now.Year;
        public string PeriodMonth = DateTime.Now.Month.ToString("D2");
        public string lcSearchText = "";
        public List<GetMonthDTO> GetMonthList { get; set; }
        public GLB00200DTO CurrentReversingJournal = new GLB00200DTO();

        public bool ButtonEnable = false;

        public string? COMPANYID;
        public string? USERID;

        List<GLB00200DTO> InboxApprovaltBatchListSelected = new List<GLB00200DTO>();
        private int Var_Data_Count = 0;

        public string Message = "";
        public int Percentage = 0;
        public Action? StateChangeAction { get; set; }
        public Action<R_APIException>? DisplayErrorAction { get; set; }

        public async Task GetInitialprocess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLB00200Model.GetInitialProcessAsyncModel();
                GetInitialProcess = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetAllReversingJournalProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcPeriod = PeriodYear.ToString() + PeriodMonth;

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, lcPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, lcSearchText);

                var loResult = await _modelGLB00200Model.GetReversingJournalProcessAsyncModel();
                if (loResult.Data.Any())
                {
                    foreach (var item in loResult.Data)
                    {
                        item.DREVERSE_DATE = ConvertStringToDateTimeFormat(item.CREVERSE_DATE);
                        item.DDOC_DATE = ConvertStringToDateTimeFormat(item.CDOC_DATE);
                    }
                }
                ReversingJournalProcessList = new ObservableCollection<GLB00200DTO>(loResult.Data);

                if (ReversingJournalProcessList.Count > 0)
                {
                    ButtonEnable = true;
                }
                else
                {
                    ButtonEnable = false;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task GetDetail_ReversingJournal()
        {
            R_Exception loException = new R_Exception();
            try
            {
                GLB00200DTO loParam = new GLB00200DTO()
                {
                    CREC_ID = CurrentReversingJournal.CREC_ID
                };
                var Result =
                    await _modelGLB00200Model.GetDetail_ReversingJournalAsyncModel(loParam);
                DetailReversingJournalList = new ObservableCollection<GLB00200JournalDetailDTO>(Result.Data);
                ConvertBigIntToInt();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public void ConvertBigIntToInt()
        {
            R_Exception loException = new R_Exception();
            try
            {
                foreach (var item in DetailReversingJournalList)
                {
                    int temp = Convert.ToInt32(item.INO);
                    item.NO_Convert = temp;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        }
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                #region ValidationEmpty

                if (string.IsNullOrEmpty(lcSearchText))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_00202");
                    loEx.Add(loErr);
                }
                if (!string.IsNullOrEmpty(lcSearchText)
                    && lcSearchText.Length < 3)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_00203");
                    loEx.Add(loErr);
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public async Task GetSelectedDataToReversingJournal()
        {
            R_Exception loException = new R_Exception();
            List<GLB00200DTO> tempDataSelected = new List<GLB00200DTO>();
            try
            {
                tempDataSelected = loProcessReversingList.Where(x => x.LSELECTED == true).ToList();
                if (tempDataSelected.Count == 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_00204");
                    loException.Add(loErr);
                    goto EndBlock;
                }
                Var_Data_Count = tempDataSelected.Count;

                InboxApprovaltBatchListSelected = tempDataSelected;

                await ProcessDataSelected(COMPANYID, USERID);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        #region "Process"
        public async Task ProcessDataSelected(string COMPANYID, string USERID)
        {
            var loEx = new R_Exception();
            try
            {
                var loUserParameters = new List<R_KeyValue>();
                //Instantiate ProcessClient
                R_ProcessAndUploadClient loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GL",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlGL",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                R_BatchParameter loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = COMPANYID;
                loUploadPar.USER_ID = USERID;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "GLB00200Back.GLB00200ProcessingCls";
                loUploadPar.BigObject = InboxApprovaltBatchListSelected;

                await loCls.R_BatchProcess<List<GLB00200DTO>>(loUploadPar, InboxApprovaltBatchListSelected.Count);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        #endregion

        #region ProgressBar

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Finish Processing Reversing Journal! ");
            }

            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                Message = "Process Completed With Fail";
                try
                {
                    await ServiceGetError(pcKeyGuid);
                }
                catch (R_Exception ex)
                {
                    var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                    DisplayErrorAction(loException);
                }
            }
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            R_Exception loException = new R_Exception();
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);


            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction(ex);
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

        private async Task ServiceGetError(string pcKeyGuid)
        {
            //  R_Exception loException = new R_Exception();
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = COMPANYID,
                    USER_ID = USERID,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_GL_PROCESS_REVERSING_JRNResources"
                };
                loCls = new R_ProcessAndUploadClient(
                   pcModuleName: "GL",
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: "R_DefaultServiceUrlGL",
                   poProcessProgressStatus: this);

                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                R_Exception loEx = new R_Exception();
                loUnhandleEx.ForEach(x => loEx.Add(x));

                loException = R_FrontUtility.R_ConvertToAPIException(loEx); goto EndBlock;
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

        EndBlock:;
            loException.ThrowExceptionIfErrors();

        }
        #endregion

        #region Utilities

        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }

        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }

        #endregion


    }
}
