using PMB02200COMMON;
using PMB02200COMMON.DTO_s;
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
using System.Linq;
using System.Threading.Tasks;

namespace PMB02200MODEL.View_Models
{
    public class PMB02200BatchViewModel : R_IProcessProgressStatus
    {
        //init

        private PMB02200HelperModel _modelHelper = new PMB02200HelperModel();

        public UtilityChargesParamDTO UtilityChargesParam { get; set; } = new UtilityChargesParamDTO();

        public List<PropertyDTO> Properties { get; set; } = new List<PropertyDTO>();

        public List<GeneralTypeDTO> UtilityTypes { get; set; } = new List<GeneralTypeDTO>();

        public List<R_APICommonDTO.R_Error> ErrorList { get; set; } = new List<R_APICommonDTO.R_Error>();

        public async Task InitProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                Properties = new List<PropertyDTO>(await _modelHelper.GetPropertyListAsync());
                if (Properties.Count > 0)
                {
                    UtilityChargesParam.CPROPERTY_ID = Properties[0].CPROPERTY_ID;
                }
                else
                {
                    UtilityChargesParam.CPROPERTY_ID = "";
                }
                UtilityTypes = new List<GeneralTypeDTO>(await _modelHelper.GetUtilityTypeListAsync());
                UtilityChargesParam.LALL_BUILDING = true;
                UtilityChargesParam.CUTILITY_TYPE = "";
                UtilityChargesParam.CBUILDING_ID = "";
                UtilityChargesParam.CBUILDING_NAME = "";
                UtilityChargesParam.CTENANT_ID = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //refresh process

        private PMB02200Model _model = new PMB02200Model();

        public ObservableCollection<UtilityChargesDTO> _utilityChargesList { get; set; } = new ObservableCollection<UtilityChargesDTO>();

        public async Task GetUtilityChargesListAsync()
        {

            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB02200ContextConstant.CPROPERTY_ID, UtilityChargesParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB02200ContextConstant.CDEPT_CODE, UtilityChargesParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMB02200ContextConstant.CBUILDING_ID, UtilityChargesParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMB02200ContextConstant.LALL_BUILDING, UtilityChargesParam.LALL_BUILDING);
                R_FrontContext.R_SetStreamingContext(PMB02200ContextConstant.CTENANT_ID, UtilityChargesParam.CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMB02200ContextConstant.CUTILITY_TYPE, UtilityChargesParam.CUTILITY_TYPE);
                var loResult = await _model.GetUtilityChargesAsync();
                _utilityChargesList = new ObservableCollection<UtilityChargesDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //apply

        public string OldChargeId { get; set; } = "";

        public string NewChargeId { get; set; } = "";

        public string OldTaxId { get; set; } = "";

        public string NewTaxId { get; set; } = "";

        public void ApplyNewIdOnList(eListUtilChrgActionType type)
        {
            var loEx = new R_Exception();
            try
            {

                foreach (var item in _utilityChargesList)
                {
                    switch (type)
                    {
                        case eListUtilChrgActionType.ApplyNewCharge:
                            if (item.CCHARGES_ID == OldChargeId || item.CCHARGES_ID.ToLower() == OldChargeId || item.CCHARGES_ID.ToUpper() == OldChargeId)
                            {
                                item.CNEW_CHARGES_ID = NewChargeId;
                            }
                            break;

                        case eListUtilChrgActionType.ApplyNewTax:
                            if (item.CTAX_ID == OldTaxId || item.CTAX_ID.ToLower() == OldTaxId || item.CTAX_ID.ToUpper() == OldTaxId)
                            {
                                item.CNEW_TAX_ID = NewTaxId;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //save

        private List<UtilityChargesDbDTO> _utilityChargesToProcessList { get; set; } = new List<UtilityChargesDbDTO>();

        public string CompanyId { get; set; }

        public string UserId { get; set; }

        public async Task ProcessSelectedData(string pcCompanyId, string pcUserId)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                CompanyId = pcCompanyId;
                UserId = pcUserId;
                var loSelectedData = _utilityChargesList.Where(x => x.LSELECTED).ToList();
                _utilityChargesToProcessList = R_FrontUtility.ConvertCollectionToCollection<UtilityChargesDbDTO>(loSelectedData).ToList();
                await SaveBulkAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SaveBulkAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = PMB02200ContextConstant.CPROPERTY_ID, Value = UtilityChargesParam.CPROPERTY_ID });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: PMB02200ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PMB02200ContextConstant.DEFAULT_HTTP_NAME,
                    poProcessProgressStatus: this);

                //Set Data
                if (_utilityChargesToProcessList.Count == 0)
                    return;
                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyId;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "PMB02200BACK.PMB02201Cls";
                loBatchPar.BigObject = _utilityChargesToProcessList;

                await loCls.R_BatchProcess<List<UtilityChargesDbDTO>>(loBatchPar, _utilityChargesToProcessList.Count);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //batch

        public Action StateChangeAction { get; set; }

        public Action<R_APIException> DisplayErrorAction { get; set; }

        public Func<Task> ShowSuccessAction { get; set; }

        public string Message = "";

        public int Percentage = 0;

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
                    await ServiceGetError(pcKeyGuid);
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
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_PROCESS_UPDATE_UTILITY_CHARGESResources"
                };

                loCls = new R_ProcessAndUploadClient(
                     pcModuleName: PMB02200ContextConstant.DEFAULT_MODULE, //pm
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PMB02200ContextConstant.DEFAULT_HTTP_NAME //R_DefaultServiceUrlPM
                    );

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

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
