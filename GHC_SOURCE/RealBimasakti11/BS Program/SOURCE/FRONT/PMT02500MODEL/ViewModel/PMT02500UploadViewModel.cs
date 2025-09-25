using R_ProcessAndUploadFront;
using PMT02500Common.DTO._1._AgreementList.Upload.Agreement;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using PMT02500Common.DTO._1._AgreementList.Upload.Unit;
using PMT02500Common.DTO._1._AgreementList.Upload.Utility;
using PMT02500Common.DTO._1._AgreementList.Upload.Charges;
using PMT02500Common.DTO._1._AgreementList.Upload.Deposit;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using R_APICommonDTO;
using PMT02500Common.DTO._1._AgreementList.Upload;
using System.Linq;
using PMT02500Common.Utilities;

namespace PMT02500Model.ViewModel
{
    public class PMT02500UploadViewModel : R_ViewModel<UploadAgreementDTO>, R_IProcessProgressStatus
    {
        public Action<R_Exception>? ShowErrorAction { get; set; }
        public Action? StateChangeAction { get; set; }
        public Action? ShowSuccessAction { get; set; }
        public Action? SetValidInvalidAction { get; set; }
        //public Action StartUploadAgreementAction { get; set; }
        public Action<string, int>? SetPercentageAndMessageAction { get; set; }

        public ObservableCollection<UploadAgreementDTO>? loUploadAgreementDisplayList = new ObservableCollection<UploadAgreementDTO>();

        public List<UploadAgreementDTO> loUploadAgreementList = new List<UploadAgreementDTO>();

        public ObservableCollection<UploadUnitDTO> loUploadUnitDisplayList = new ObservableCollection<UploadUnitDTO>();

        public List<UploadUnitDTO> loUploadUnitList = new List<UploadUnitDTO>();

        public ObservableCollection<UploadUtilityDTO> loUploadUtilityDisplayList = new ObservableCollection<UploadUtilityDTO>();

        public List<UploadUtilityDTO> loUploadUtilityList = new List<UploadUtilityDTO>();

        public ObservableCollection<UploadChargesDTO> loUploadChargesDisplayList = new ObservableCollection<UploadChargesDTO>();

        public List<UploadChargesDTO> loUploadChargesList = new List<UploadChargesDTO>();

        public ObservableCollection<UploadDepositDTO> loUploadDepositDisplayList = new ObservableCollection<UploadDepositDTO>();

        public List<UploadDepositDTO> loUploadDepositList = new List<UploadDepositDTO>();

        public PMT02500PropertyListDTO loParameter = new PMT02500PropertyListDTO();

        public bool isUploadAgreementSuccessful = false;
        public int SumValid { get; set; }
        public int SumInvalid { get; set; }
        public int SumList { get; set; }

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public bool IsOverWrite = false;

        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[]? fileByte = null;

        public bool VisibleError = false;
        public bool IsErrorEmptyFile = false;
        public bool IsUploadSuccesful = true;

        public string lcFilterResult = "";

        public async Task SaveUploadAgreementAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            //List<UploadAgreementDTO> Bigobject;
            List<R_KeyValue> loUserParam;
            UploadBigObjectParameterDTO loBigObject = new UploadBigObjectParameterDTO();

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02500_UPLOAD_CONTEXT, Value = loParameter.CPROPERTY_ID! });
                //loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_Agreement_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadAgreementDisplayList.Count == 0)
                {
                    loBigObject.AgreementList = new List<UploadAgreementSaveDTO>();
                }
                else
                {
                    //var loTemp = loUploadAgreementDisplayList.ToList<UploadAgreementDTO>();
                    //loBigObject.AgreementList = (List<UploadAgreementSaveDTO>)R_FrontUtility.ConvertCollectionToCollection<UploadAgreementSaveDTO>(loTemp);

                    loBigObject.AgreementList = loUploadAgreementDisplayList
                        .Select(x => new UploadAgreementSaveDTO
                        {
                            NO = x.NO,
                            CCOMPANY_ID = SelectedCompanyId,
                            CPROPERTY_ID = loParameter.CPROPERTY_ID!,
                            CBUILDING_ID = x.Building,
                            CDEPT_CODE = x.Department,
                            CREF_NO = x.AgreementNo,
                            CREF_DATE = x.AgreementDate,
                            CDOC_NO = x.DocumentNo,
                            CDOC_DATE = x.DocumentDate,
                            CSTART_DATE = x.StartDate,
                            CEND_DATE = x.EndDate,
                            CYEAR = x.Year,
                            CMONTH = x.Month,
                            CDAY = x.Day,
                            CCURRENCY_CODE = x.Currency,
                            CLEASE_MODE = x.LeaseMode,
                            CCHARGE_MODE = x.ChargeMode,
                            CSALESMAN_ID = x.Salesman,
                            CTENANT_ID = x.Tenant,
                            CUNIT_DESCRIPTION = x.UnitDescription,
                            CNOTES = x.Notes
                        })
                        .ToList();
                }

                if (loUploadUnitDisplayList.Count == 0)
                {
                    loBigObject.UnitList = new List<UploadUnitSaveDTO>();
                }
                else
                {
                    //var loTemp = loUploadUnitDisplayList.ToList<UploadUnitDTO>();
                    //loBigObject.UnitList = (List<UploadUnitSaveDTO>)R_FrontUtility.ConvertCollectionToCollection<UploadUnitSaveDTO>(loTemp);

                    loBigObject.UnitList = loUploadUnitDisplayList
                        .Select(x => new UploadUnitSaveDTO
                        {
                            NO = x.NO,
                            //CREF_NO = x.AgreementNo,
                            CDOC_NO = x.DocumentNo,
                            CUNIT_ID = x.UnitId,
                            CBUILDING_ID = x.BuildingId,
                            CFLOOR_ID = x.FloorId,
                        })
                        .ToList();
                }

                if (loUploadUtilityDisplayList.Count == 0)
                {
                    loBigObject.UtilityList = new List<UploadUtilitySaveDTO>();
                }
                else
                {
                    //var loTemp = loUploadUtilityDisplayList.ToList<UploadUtilityDTO>();
                    //loBigObject.UtilityList = (List<UploadUtilitySaveDTO>)R_FrontUtility.ConvertCollectionToCollection<UploadUtilitySaveDTO>(loTemp);

                    loBigObject.UtilityList = loUploadUtilityDisplayList
                        .Select(x => new UploadUtilitySaveDTO
                        {
                            NO = x.NO,
                            //CREF_NO = x.AgreementNo,
                            CDOC_NO = x.DocumentNo,
                            CUTILITY_TYPE = x.UtilityType,
                            CUNIT_ID = x.UnitId,
                            CMETER_NO = x.MeterNo,
                            CCHARGES_ID = x.ChargesId,
                            CTAX_ID = x.TaxId
                        })
                        .ToList();
                }

                if (loUploadChargesDisplayList.Count == 0)
                {
                    loBigObject.ChargesList = new List<UploadChargesSaveDTO>();
                }
                else
                {
                    //var loTemp = loUploadChargesDisplayList.ToList<UploadChargesDTO>();
                    //loBigObject.ChargesList = (List<UploadChargesSaveDTO>)R_FrontUtility.ConvertCollectionToCollection<UploadChargesSaveDTO>(loTemp);

                    loBigObject.ChargesList = loUploadChargesDisplayList
                        .Select(x =>
                        {
                            int tenureYear = 0;
                            int tenureMonth = 0;
                            int tenureDays = 0;

                            int.TryParse(x.TenureYear, out tenureYear);
                            int.TryParse(x.TenureMonth, out tenureMonth);
                            int.TryParse(x.TenureDays, out tenureDays);
                            return new UploadChargesSaveDTO
                            {
                                NO = x.NO,
                                //CREF_NO = x.AgreementNo,
                                CDOC_NO = x.DocumentNo,
                                CCHARGES_ID = x.ChargesId,
                                CTAX_ID = x.TaxId,
                                IYEARS = tenureYear,
                                IMONTHS = tenureMonth,
                                IDAYS = tenureDays,
                                LBASED_OPEN_DATE = x.BaseonOpeningDate,
                                CSTART_DATE = x.StartDate,
                                CEND_DATE = x.EndDate,
                                CBILLING_MODE = x.BillingMode,
                                CCURENCY_CODE = x.Currency,
                                CFEE_METHOD = x.FeeMethod,
                                NFEE_AMT = x.FeeAmount,
                                CPERIOD_MODE = x.PeriodMode,
                                LPRORATE = x.Prorate,
                                CDESCRIPTION = x.Description
                            };
                        })
                        .ToList();
                }

                if (loUploadDepositDisplayList.Count == 0)
                {
                    loBigObject.DepositList = new List<UploadDepositSaveDTO>();
                }
                else
                {
                    //var loTemp = loUploadDepositDisplayList.ToList<UploadDepositDTO>();
                    //loBigObject.DepositList = (List<UploadDepositSaveDTO>)R_FrontUtility.ConvertCollectionToCollection<UploadDepositSaveDTO>(loTemp);

                    loBigObject.DepositList = loUploadDepositDisplayList
                        .Select(x => new UploadDepositSaveDTO
                        {
                            NO = x.NO,
                            CDOC_NO = x.DocumentNo,
                            LCONTRACTOR = x.FlagContractor,
                            CCONTRACTOR_ID = x.ContractorId,
                            CDEPOSIT_ID = x.DepositId,
                            CDEPOSIT_DATE = x.DepositDate,
                            CCURRENCY_CODE = x.Currency,
                            NDEPOSIT_AMT = x.DepositAmount,
                            LPAID = x.FlagPaid,
                            CDESCRIPTION = x.Description
                        })
                        .ToList();
                }


                //Bigobject = loUploadAgreementDisplayList.ToList<UploadAgreementDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "PMT02500Back.PMT02500UploadProcessCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = loBigObject;

                lcGuid = await loCls.R_BatchProcess<UploadBigObjectParameterDTO>(loBatchPar, 34);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void CheckingDataWithEmptyDocNo()
        {
            R_Exception loEx = new R_Exception();
            List<UploadAgreementDTO>? loAgreementFilterResult = null;
            List<UploadUnitDTO>? loUnitFilterResult = null;
            List<UploadUtilityDTO>? loUtilityFilterResult = null;
            List<UploadChargesDTO>? loChargesFilterResult = null;
            List<UploadDepositDTO>? loDepositFilterResult = null;
            try
            {
                if (loUploadAgreementDisplayList.Count() > 0)
                {
                    loAgreementFilterResult = loUploadAgreementDisplayList.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loAgreementFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Agreement : ";
                        foreach (UploadAgreementDTO item in loAgreementFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (loUploadUnitDisplayList.Count() > 0)
                {
                    loUnitFilterResult = loUploadUnitDisplayList.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loUnitFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Unit : ";
                        foreach (UploadUnitDTO item in loUnitFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (loUploadUtilityDisplayList.Count() > 0)
                {
                    loUtilityFilterResult = loUploadUtilityDisplayList.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loUtilityFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Utility : ";
                        foreach (UploadUtilityDTO item in loUtilityFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (loUploadChargesDisplayList.Count() > 0)
                {
                    loChargesFilterResult = loUploadChargesDisplayList.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loChargesFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Charges : ";
                        foreach (UploadChargesDTO item in loChargesFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (loUploadDepositDisplayList.Count() > 0)
                {
                    loDepositFilterResult = loUploadDepositDisplayList.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loDepositFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Deposit : ";
                        foreach (UploadDepositDTO item in loDepositFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
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
            R_Exception loException = new R_Exception();
            List<R_ErrorStatusReturn>? loResult = null;

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    SetPercentageAndMessageAction(Message, Percentage);
                    VisibleError = false;
                    isUploadAgreementSuccessful = true;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    SetPercentageAndMessageAction(Message, Percentage);

                    try
                    {
                        loResult = await ServiceGetError(pcKeyGuid);
                        loUploadAgreementDisplayList.ToList().ForEach(x =>
                        {
                            if (loResult.Any(y => y.SeqNo == x.NO))
                            {
                                x.Notes = loResult.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                                x.Valid = "N";
                                SumInvalid++;
                            }
                            else
                            {
                                x.Valid = "Y";
                                SumValid++;
                            }
                        });

                        if (loResult.Any(x => x.SeqNo < 0))
                        {
                            loResult.Where(x => x.SeqNo < 0).ToList().ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                        }
                        SetValidInvalidAction();
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                    }
                    if (loException.HasError)
                    {
                        ShowErrorAction(loException);
                    }
                    VisibleError = true;
                }
                //StartUploadAgreementAction();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loException.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            SetPercentageAndMessageAction(Message, Percentage);

            R_Exception loException = new R_Exception();
            ex.ErrorList.ForEach(l =>
            {
                loException.Add(l.ErrNo, l.ErrDescp);
            });

            ShowErrorAction(loException);
            StateChangeAction();
            //StartUploadAgreementAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            SetPercentageAndMessageAction(Message, Percentage);
            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn>? loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = SelectedCompanyId,
                    USER_ID = SelectedUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_UPLOAD_AgreementResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loResultData!;
        }

        #endregion
    }
}
