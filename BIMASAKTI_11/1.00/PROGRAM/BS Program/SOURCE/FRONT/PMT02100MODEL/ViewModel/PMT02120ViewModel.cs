using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02120;
using System.Linq;
using System.Globalization;
using PMT02100MODEL.FrontDTOs;
using PMT02100REPORTCOMMON.DTOs.PMT02100PDF;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using R_APICommonDTO;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02120ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>, R_IProcessProgressStatus
    {
        private PMT02100Model loTabOpenModel = new PMT02100Model();

        private PMT02120Model loModel = new PMT02120Model();

        public PMT02100HandoverDTO loHandover = new PMT02100HandoverDTO();

        public PMT02100HandoverBuildingDTO loHandoverBuilding = new PMT02100HandoverBuildingDTO();

        public PMT02120RescheduleListDTO loReschedule = new PMT02120RescheduleListDTO();

        public PMT02120EmployeeListDTO loEmployee = new PMT02120EmployeeListDTO();

        //public PMT02100DTO loSelectedHandover = new PMT02100DTO();

        public ObservableCollection<PMT02100HandoverDTO> loHandoverList = new ObservableCollection<PMT02100HandoverDTO>();

        public ObservableCollection<PMT02100HandoverBuildingDTO> loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>();

        public ObservableCollection<PMT02120RescheduleListDTO> loRescheduleList = new ObservableCollection<PMT02120RescheduleListDTO>();

        public ObservableCollection<PMT02120EmployeeListDTO> loEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>();

        public PMT02100TabParameterDTO loTabParameter = null;

        public GetPMSystemParamDTO loPMSystemParam = new GetPMSystemParamDTO();

        public string lcStatusCode = "";


        public string lcCompanyId = "";
        public string lcUserId = "";
        public string Message = "";
        public bool _isSuccess;
        public bool IsReinvite = false;

        public Action? StateChangeAction { get; set; }
        public Action<R_Exception>? DisplayErrorAction { get; set; }
        public Action? ShowSuccessAction { get; set; }

        public async Task<GetPMSystemParamDTO> GetPMSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPMSystemParamResultDTO loResult = null;

            try
            {
                loResult = await loTabOpenModel.GetPMSystemParamAsync(new GetPMSystemParamParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                });
                loPMSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loPMSystemParam;
        }

        public async Task GetHandoverListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02100HandoverParameterDTO loParam = null;
            PMT02100HandoverResultDTO loRtn = null;
            try
            {
                loParam = new PMT02100HandoverParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CTYPE = loTabParameter.CTYPE,
                    CHANDOVER_STATUS = lcStatusCode,
                    CBUILDING_ID = loHandoverBuilding.CBUILDING_ID,
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loTabOpenModel.GetHandoverListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    //x.DSCHEDULED_HO_DATE = !string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(x.CSCHEDULED_HO_DATE, "yyyyMMdd", null) : default;
                    //if (!string.IsNullOrEmpty(x.CSCHEDULED_HO_TIME_HOURS) && !string.IsNullOrEmpty(x.CSCHEDULED_HO_TIME_MINUTES))
                    //{
                    //    x.CSCHEDULED_HO_TIME = x.CSCHEDULED_HO_TIME_HOURS.Take(2).ToArray() + " : " + x.CSCHEDULED_HO_TIME_MINUTES.Substring(x.CSCHEDULED_HO_TIME_MINUTES.Length - 2, 2);
                    //}
                    if (!string.IsNullOrWhiteSpace(x.CSCHEDULED_HO_TIME))
                    {
                        x.CSCHEDULED_HO_TIME_HOURS = x.CSCHEDULED_HO_TIME.Substring(0, 2);
                        x.CSCHEDULED_HO_TIME_MINUTES = x.CSCHEDULED_HO_TIME.Substring(x.CSCHEDULED_HO_TIME.Length - 2, 2);
                    }
                    x.ISCHEDULED_HO_TIME_HOURS = string.IsNullOrWhiteSpace(x.CSCHEDULED_HO_TIME_HOURS) ? 0 : int.Parse(x.CSCHEDULED_HO_TIME_HOURS);
                    x.ISCHEDULED_HO_TIME_MINUTES = string.IsNullOrWhiteSpace(x.CSCHEDULED_HO_TIME_MINUTES) ? 0 : int.Parse(x.CSCHEDULED_HO_TIME_MINUTES);
                    //x.DSCHEDULED_HO_DATE = !string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(x.CSCHEDULED_HO_DATE + " " + x.CSCHEDULED_HO_TIME, "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null;

                    if (!string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE))
                    {
                        // Parse the date first
                        if (DateTime.TryParseExact(x.CSCHEDULED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        {
                            // Parse the time second
                            if (DateTime.TryParseExact(x.CSCHEDULED_HO_TIME, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
                            {
                                // Combine both the date and time
                                x.DSCHEDULED_HO_DATE = parsedDate.Date.Add(parsedTime.TimeOfDay);  // Combine date and time
                            }
                        }
                        else
                        {
                            x.DSCHEDULED_HO_DATE = null; // Handle invalid date format
                        }
                    }
                    else
                    {
                        x.DSCHEDULED_HO_DATE = null; // Handle null or empty date/time
                    }

                    x.DHO_EXPIRY_DATE = !string.IsNullOrEmpty(x.CHO_EXPIRY_DATE) ? DateTime.ParseExact(x.CHO_EXPIRY_DATE, "yyyyMMdd", null) : (DateTime?)null;
                    x.DREF_DATE = !string.IsNullOrEmpty(x.CREF_DATE) ? DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null) : (DateTime?)null;
                });
                loHandoverList = new ObservableCollection<PMT02100HandoverDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHandoverBuildingListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02100HandoverBuildingParameterDTO loParam = null;
            PMT02100HandoverBuildingResultDTO loRtn = null;
            try
            {
                loParam = new PMT02100HandoverBuildingParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CTYPE = loTabParameter.CTYPE,
                    CHANDOVER_STATUS = lcStatusCode
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_BUILDING_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loTabOpenModel.GetHandoverBuildingListStreamAsync();

                if (loRtn != null)
                {
                    loRtn.Data.ForEach(x => x.CBUILDING_DISPLAY_NAME = x.CBUILDING_ID + " - " + x.CBUILDING_NAME + " (" + x.ICOUNT.ToString() + ")");
                }

                loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetEmployeeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02120EmployeeListParameterDTO loParam = null;
            PMT02120EmployeeListResultDTO loRtn = null;
            try
            {
                loParam = new PMT02120EmployeeListParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CDEPT_CODE = loHandover.CDEPT_CODE,
                    CTRANS_CODE = loHandover.CTRANS_CODE,
                    CREF_NO = loHandover.CREF_NO,
                    LASSIGNED = true
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02120_GET_EMPLOYEE_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetEmployeeListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    x.CEMPLOYEE_DISPLAY = x.CEMPLOYEE_ID + " - " + x.CEMPLOYEE_NAME;
                });
                loEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetRescheduleListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02120RescheduleListParameterDTO loParam = null;
            PMT02120RescheduleListResultDTO loRtn = null;
            try
            {
                loParam = new PMT02120RescheduleListParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CDEPT_CODE = loHandover.CDEPT_CODE,
                    CTRANS_CODE = loHandover.CTRANS_CODE,
                    CREF_NO = loHandover.CREF_NO
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02120_GET_RESCHEDULE_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetRescheduleListStreamAsync();

                loRtn.Data.ForEach(x =>
                {
                    x.DSCHEDULED_HO_DATE = !string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE) ? DateTime.ParseExact($"{x.CSCHEDULED_HO_DATE} {x.CSCHEDULED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : default;
                    //x.DSCHEDULED_HO_DATE = !string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(x.CSCHEDULED_HO_DATE, "yyyyMMdd", null) : default;
                });

                loRescheduleList = new ObservableCollection<PMT02120RescheduleListDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        //public async Task ReinviteProcessAsync(PMT02120ReinviteProcessParameterDTO poParam)
        //{
        //    R_Exception loEx = new R_Exception();
        //    try
        //    {
        //        await loModel.ReinviteProcessAsync(poParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        public async Task ReinviteProcessAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loUserParameters = new List<R_KeyValue>();

                // set Param
                loUserParameters = new List<R_KeyValue>();
                /*
                {
                    new R_KeyValue()
                    {
                        Key = PMR01700ContextDistributeReportDTO.CPROPERTY_ID,
                        Value = poParam.ParameterSP.CPROPERTY_ID!
                    },
                    new R_KeyValue()
                    {
                        Key = PMR01700ContextDistributeReportDTO.CDEPT_CODE,
                        Value = poParam.ParameterSP.CDEPT_CODE!
                    }
                };
                */
                //Instantiate ProcessClient
                R_ProcessAndUploadClient loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                R_BatchParameter loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = lcCompanyId;
                loUploadPar.USER_ID = lcUserId;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "PMT02100BACK.PMT02110InvitationEmailCls";
                loUploadPar.BigObject = new PMT02100InvitationParameterDTO()
                {
                    CCOMPANY_ID = lcCompanyId,
                    CUSER_ID = lcUserId,
                    CDEPT_CODE = loHandover.CDEPT_CODE,
                    CPROPERTY_ID = loHandover.CPROPERTY_ID,
                    CREF_NO = loHandover.CREF_NO,
                    CTENANT_EMAIL = loHandover.CTENANT_EMAIL,
                    CTRANS_CODE = loHandover.CTRANS_CODE,
                    LCONFIRM_SCHEDULE = false,
                    CSCHEDULED_HO_DATE = "",
                    CSCHEDULED_HO_TIME = ""
                    //CSCHEDULED_HO_DATE = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("yyyyMMdd"),
                    //CSCHEDULED_HO_TIME = loScheduleHeader.DSCHEDULED_DATE.Value.ToString("HH:mm") //loScheduleHeader.CSCHEDULED_TIME_HOURS + ":" + loScheduleHeader.CSCHEDULED_TIME_MINUTES
                };
                await loCls.R_BatchProcess<PMT02100InvitationParameterDTO>(loUploadPar, 10);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region ProgressBar
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Finish Process Cancel Receipt!");
                ShowSuccessAction!();
                _isSuccess = true;
            }
            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                _isSuccess = false;

                Message = "Process Completed With Fail";
                try
                {
                    await ServiceGetError(pcKeyGuid);
                }
                catch (R_Exception ex)
                {
                    DisplayErrorAction?.Invoke(ex);
                }
            }

            StateChangeAction!();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            R_Exception loException = new R_Exception();
            var lcMessage = string.Format("Process Error with GUID {0}", pcKeyGuid);
            Message = string.Format("Process Error with ");

            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction!.Invoke(loException);
            StateChangeAction!();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            //if (TypeProcessReport == "DISTRIBUTE")
            //{
            //    ProgressBarPercentage = pnProgress;
            //    Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            //}
            StateChangeAction!();
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
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = lcCompanyId,
                    USER_ID = lcUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_HANDOVER_REINVITEResources"//INI NANTI DIGANTI
                };
                loCls = new R_ProcessAndUploadClient(
                   pcModuleName: "PM",
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: "R_DefaultServiceUrlPM",
                   poProcessProgressStatus: this);
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
                loResultData.ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                goto EndBlock;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

        }
        #endregion
    }
}