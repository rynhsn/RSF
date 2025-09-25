using CBT02300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using CBT02300FrontResources;
using System.ComponentModel.Design;
using System.Linq;
using R_APICommonDTO;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System.Globalization;
using CBT02300COMMON.Master_DTO;

namespace CBT02300MODEL.ViewModel
{
    public class CBT02300BankInChequeViewModel : R_ViewModel<CBT02300ChequeInfoFrontDTO>, R_IProcessProgressStatus
    {
        private CBT02300BankInChequeModel _model = new CBT02300BankInChequeModel();
        public ObservableCollection<CBT02300BankInChequeFrontDTO> BankInChequeList =
    new ObservableCollection<CBT02300BankInChequeFrontDTO>();
        public CBT02300ChequeInfoFrontDTO BankInChequeInfo = new CBT02300ChequeInfoFrontDTO();
        public CBT02300DBFilterListParamDTO? loParamaterFilter = new CBT02300DBFilterListParamDTO();
        public CBT02300DBFilterListParamDTO? loTempParamaterFilter = new CBT02300DBFilterListParamDTO();
        public bool _enableBtn;
        public CBT02300BankInChequeDTO? _currentBankInCheque;
        CBT02300ProcessDataDTO _loProcessData = new CBT02300ProcessDataDTO();

        public string? COMPANYID;
        public string? USERID;

        public string Message = "";
        public int Percentage = 0;
        public bool _isSuccess;
        public Action? StateChangeAction { get; set; }
        public Action<R_APIException>? DisplayErrorAction { get; set; }
        public Action? ShowSuccessAction { get; set; }
        public string? _typeBankIn { get; set; }
        public enum TYPE_BANK_IM_CHEQUE { Bank_in, Undo_Bank_In, Hold, Bounce, Clear, Undo_Clear }
        public TYPE_BANK_IM_CHEQUE leTypeBank;

        public async Task GetBankInChequeList(string pcTransType)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_TYPE, pcTransType);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, loParamaterFilter!.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCB_CODE, loParamaterFilter.CCB_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCB_ACCOUNT_NO, loParamaterFilter.CCB_ACCOUNT_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDATE, ConvertDateTimeToStringFormat(loParamaterFilter.CDATE_FRONT));
                // var dept = ConvertDateTimeToStringFormat(loParamaterFilter.CDATE_FRONT);

                var loResult = await _model.GetbankInChequeStreamAsyncModel();

                if (loResult.Data.Count() > 0)
                {

                    List<CBT02300BankInChequeFrontDTO> tempBankInChequeList = new List<CBT02300BankInChequeFrontDTO>();

                    foreach (var item in loResult.Data!)
                    {
                        tempBankInChequeList.Add(ConvertToEntityFrontList(item));
                    }

                    BankInChequeList = new ObservableCollection<CBT02300BankInChequeFrontDTO>(tempBankInChequeList);

                    var firstData = BankInChequeList[0];
                    CBT02300ChequeInfoFrontDTO loTempFirstData = new CBT02300ChequeInfoFrontDTO()
                    {
                        CREC_ID = firstData.CREC_ID
                    };

                    await GetChequeInfo(loTempFirstData);

                    _enableBtn = true;
                }
                else
                {
                    _enableBtn = false;
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02301");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<CBT02300ChequeInfoDTO> GetChequeInfo(CBT02300ChequeInfoFrontDTO poProperty)
        {
            var loEx = new R_Exception();
            CBT02300ChequeInfoDTO loResult = new CBT02300ChequeInfoDTO();
            try
            {
                var loParam = new CBT02300DBParamDetailDTO()
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CUSER_ID = poProperty.CUSER_ID,
                    CREC_ID = poProperty.CREC_ID,
                    CLANGUAGE_ID = poProperty.CLANGUAGE_ID
                };

                loResult = await _model.GetBankInChequeInfoModel(loParam);
                BankInChequeInfo = ConvertToEntityFrontDetail(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #region Seprated data
        public void GetSelectedData(CBT02300ProcessDataDTO poParam, List<CBT02300BankInChequeFrontDTO> poListParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                List<string> tempDataSelected = poListParameter.Where(x => x.LSELECTED == true).Select(x => x.CREC_ID).ToList()!;

                if (tempDataSelected.Count == 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02305");
                    loException.Add(loErr);
                    goto EndBlock;
                }
                if (poParam.CACTION == "HOLD" || poParam.CACTION == "BOUNCE" || poParam.CACTION == "UNDO_CLEAR")
                {
                    if (string.IsNullOrWhiteSpace(poParam.CREASON))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02314");
                        loException.Add(loErr);
                        goto EndBlock;

                    }
                }
                COMPANYID = poParam.CCOMPANY_ID;
                USERID = poParam.CUSER_ID;
                poParam.CREC_ID_LIST = tempDataSelected;

                _loProcessData = poParam;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        public async Task ProcessData()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await ProcessDataSelected(_loProcessData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion
        #region validation
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                var temp = loParamaterFilter;

                if (string.IsNullOrEmpty(loParamaterFilter!.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02302");
                    loEx.Add(loErr);
                    //  goto EndBlock;
                }

                if (string.IsNullOrEmpty(loParamaterFilter.CCB_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02303");
                    loEx.Add(loErr);
                    //  goto EndBlock;
                }
                if (string.IsNullOrEmpty(loParamaterFilter.CCB_ACCOUNT_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02304");
                    loEx.Add(loErr);

                }
                if (loParamaterFilter.CDATE_FRONT == null)
                {
                    switch (_typeBankIn)
                    {
                        case "BANK_IN":
                            var loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02306");
                            loEx.Add(loErr);
                            break;
                        case "HOLD":
                            var loErr3 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02307");
                            loEx.Add(loErr3);
                            break;
                        case "CLEAR":
                            var loErr2 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02308");
                            loEx.Add(loErr2);
                            break;
                        case "UNDO_CLEAR":
                            var loErr4 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02308");
                            loEx.Add(loErr4);
                            break;
                        case "BOUNCE":
                            var loErr1 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02309");
                            loEx.Add(loErr1);
                            break;
                    }
                }
                if (loParamaterFilter.CDATE_FRONT > DateTime.Now)
                {
                    switch (_typeBankIn)
                    {
                        case "BANK_IN":
                            R_BlazorFrontEnd.Exceptions.R_Error loErr = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02310");
                            loEx.Add(loErr);
                            break;
                        case "HOLD":
                            var loErr3 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02311");
                            loEx.Add(loErr3);
                            break;
                        case "CLEAR":
                            var loErr2 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02312");
                            loEx.Add(loErr2);
                            break;
                        case "UNDO_CLEAR":
                            var loErr4 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02312");
                            loEx.Add(loErr4);
                            break;
                        case "BOUNCE":
                            var loErr1 = R_FrontUtility.R_GetError(typeof(Resources_CBT02300_Class), "Error_02313");
                            loEx.Add(loErr1);
                            break;
                    }
                }
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
        #endregion

        #region ConvertDTO 

        public CBT02300BankInChequeFrontDTO ConvertToEntityFrontList(CBT02300BankInChequeDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            CBT02300BankInChequeFrontDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<CBT02300BankInChequeFrontDTO>(poEntity);
                loReturn.DCHEQUE_DATE = ConvertStringToDateTimeFormat(poEntity.CCHEQUE_DATE!);
                loReturn.DDUE_DATE = ConvertStringToDateTimeFormat(poEntity.CDUE_DATE!);
                loReturn.DREF_DATE = ConvertStringToDateTimeFormat(poEntity.CREF_DATE!);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn!;
        }
        public CBT02300ChequeInfoFrontDTO ConvertToEntityFrontDetail(CBT02300ChequeInfoDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            CBT02300ChequeInfoFrontDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<CBT02300ChequeInfoFrontDTO>(poEntity);

                loReturn.DCHEQUE_DATE = ConvertStringToDateTimeFormat(poEntity.CCHEQUE_DATE!);
                loReturn.DDUE_DATE = ConvertStringToDateTimeFormat(poEntity.CDUE_DATE!);
                loReturn.DBANK_IN_DATE = ConvertStringToDateTimeFormat(poEntity.CBANK_IN_DATE!);
                loReturn.DHOLD_DATE = ConvertStringToDateTimeFormat(poEntity.CHOLD_DATE!);
                loReturn.DUNDO_CLEAR_DATE = ConvertStringToDateTimeFormat(poEntity.CUNDO_CLEAR_DATE!);
                loReturn.DCLEAR_DATE = ConvertStringToDateTimeFormat(poEntity.CCLEAR_DATE!);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn!;
        }
        private DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
            }
            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
            return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
        }
        /*
       public CBT02300BankInChequeDTO ConvertToEntityBackList(CBT02300BankInChequeFrontDTO poEntity)
       {
           R_Exception loException = new R_Exception();
           CBT02300BankInChequeDTO? loReturn = null;
           try
           {
               loReturn = R_FrontUtility.ConvertObjectToObject<CBT02300BankInChequeDTO>(poEntity);
               loReturn.CCHEQUE_DATE = ConvertDateTimeToStringFormat(poEntity.DCHEQUE_DATE);
               loReturn.CDUE_DATE = ConvertDateTimeToStringFormat(poEntity.DDUE_DATE);
               loReturn.CREF_DATE = ConvertDateTimeToStringFormat(poEntity.DREF_DATE);

           }
           catch (Exception ex)
           {
               loException.Add(ex);
           }
           loException.ThrowExceptionIfErrors();
           return loReturn!;
       }
       */

        /*
        public CBT02300ChequeInfoDTO ConvertToEntityBackDetail(CBT02300ChequeInfoFrontDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            CBT02300ChequeInfoDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<CBT02300ChequeInfoDTO>(poEntity);
                loReturn.CCHEQUE_DATE = ConvertDateTimeToStringFormat(poEntity.DCHEQUE_DATE);
                loReturn.CDUE_DATE = ConvertDateTimeToStringFormat(poEntity.DCHEQUE_DATE);
                loReturn.CBANK_IN_DATE = ConvertDateTimeToStringFormat(poEntity.DBANK_IN_DATE);
                loReturn.CUNDO_CLEAR_DATE = ConvertDateTimeToStringFormat(poEntity.DUNDO_CLEAR_DATE);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn!;
        }
        */

        private string ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (ptEntity == DateTime.MinValue)
            {
                // Jika DateTime adalah DateTime.MinValue, kembalikan string kosong
                return ""; // atau null, tergantung pada kebutuhan Anda
            }

            // Format DateTime ke string "yyyyMMdd"
            return ptEntity?.ToString("yyyyMMdd")!;
        }

        #endregion
        #region "Process"
        public async Task ProcessDataSelected(CBT02300ProcessDataDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loUserParameters = new List<R_KeyValue>();
                // set Param
                loUserParameters = new List<R_KeyValue>
                {
                    new R_KeyValue()
                    { Key = ContextConstant.CPROCESS_DATE, Value = poParam.CPROCESS_DATE! },

                    new R_KeyValue()
                    { Key = ContextConstant.CACTION, Value = poParam.CACTION! },

                    new R_KeyValue()
                    { Key = ContextConstant.CREASON, Value = poParam.CREASON! }
                };

                //Instantiate ProcessClient
                R_ProcessAndUploadClient loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "CB",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlCB",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                R_BatchParameter loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = poParam.CCOMPANY_ID!;
                loUploadPar.USER_ID = poParam.CUSER_ID!; ;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "CBT02300BACK.CBT02300ProcessCls";
                loUploadPar.BigObject = poParam.CREC_ID_LIST!;

                await loCls.R_BatchProcess<List<string>>(loUploadPar, 5);
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
            if (leTypeBank == TYPE_BANK_IM_CHEQUE.Bank_in)
            {

                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Finish Bank In Cheque!");
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
                        var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                        DisplayErrorAction(loException);
                    }
                }
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Undo_Bank_In)
            {

                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Finish Undo Bank In Cheque!");
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
                        var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                        DisplayErrorAction(loException);
                    }
                }
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Hold)
            {

                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Finish Hold Cheque!");
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
                        var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                        DisplayErrorAction(loException);
                    }
                }
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Clear)
            {

                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Finish Clear Cheque!");
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
                        var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                        DisplayErrorAction(loException);
                    }
                }
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Bounce)
            {

                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Finish Bounce Cheque!");
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
                        var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                        DisplayErrorAction(loException);
                    }
                }
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Undo_Clear)
            {

                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Finish Undo Clear Cheque!");
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
                        var loException = R_FrontUtility.R_ConvertToAPIException(ex);
                        DisplayErrorAction(loException);
                    }
                }
            }
            StateChangeAction!();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            //R_Exception loException = new R_Exception();

            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            if (leTypeBank == TYPE_BANK_IM_CHEQUE.Bank_in)
            {
                DisplayErrorAction(ex);
                // ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Undo_Bank_In)
            {
                DisplayErrorAction(ex);
                //ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Hold)
            {
                DisplayErrorAction(ex);
                //  ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Clear)
            {
                DisplayErrorAction(ex);
                //ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Bounce)
            {
                DisplayErrorAction(ex);
                //  ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Undo_Clear)
            {
                DisplayErrorAction(ex);
                //   ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            }


            DisplayErrorAction(ex);
            StateChangeAction!();
            await Task.CompletedTask;
        }
        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            if (leTypeBank == TYPE_BANK_IM_CHEQUE.Bank_in)
            {
                Percentage = pnProgress;
                Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Undo_Bank_In)
            {
                Percentage = pnProgress;
                Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Hold)
            {
                Percentage = pnProgress;
                Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Clear)
            {
                Percentage = pnProgress;
                Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Bounce)
            {
                Percentage = pnProgress;
                Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            }
            else if (leTypeBank == TYPE_BANK_IM_CHEQUE.Undo_Clear)
            {
                Percentage = pnProgress;
                Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            }

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            StateChangeAction!();
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
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = COMPANYID!,
                    USER_ID = USERID!,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_CB_PROCESS_BANK_IN_CHEQUE"
                };
                loCls = new R_ProcessAndUploadClient(
                   pcModuleName: "CB",
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: "R_DefaultServiceUrlCB",
                   poProcessProgressStatus: this);

                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                R_Exception loEx = new R_Exception();
                loUnhandleEx.ForEach(x => loEx.Add(x));
                loException = R_FrontUtility.R_ConvertToAPIException(loEx);
                goto EndBlock;
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

        }
        #endregion
    }
}
