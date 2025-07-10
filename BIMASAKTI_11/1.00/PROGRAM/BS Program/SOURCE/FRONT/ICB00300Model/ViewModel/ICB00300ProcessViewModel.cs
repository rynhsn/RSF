using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ICB00300Common;
using ICB00300Common.DTOs;
using ICB00300Common.Params;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace ICB00300Model.ViewModel
{
    public class ICB00300ProcessViewModel : R_ViewModel<ICB00300ProductDTO>, R_IProcessProgressStatus
    {
        private ICB00300Model _model = new ICB00300Model();

        public ObservableCollection<ICB00300ProductDTO> ProductList =
            new ObservableCollection<ICB00300ProductDTO>();

        public ICB00300ProcessParam Param = new ICB00300ProcessParam();
        public int TotalSelected { get; set; } = 0;
        public int TotalProduct { get; set; } = 0;
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public bool IsError { get; set; }
        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }

        public async Task GetProductList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ICB00300ContextConstant.CPROPERTY_ID, Param.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ICB00300ContextConstant.CRECALCULATE_TYPE,
                    Param.CRECALCULATE_TYPE);

                var loReturn =
                    await _model.GetListStreamAsync<ICB00300ProductDTO>(
                        nameof(IICB00300.ICB00300GetProductListStream));
                ProductList = new ObservableCollection<ICB00300ProductDTO>(loReturn);

                // buat 5 data dummy untuk list produk
                // for (int i = 0; i < 5; i++)
                // {
                //     ProductList.Add(new ICB00300ProductDTO
                //     {
                //         CPRODUCT_ID = "Product " + i,
                //         CPRODUCT_NAME = "Product Name " + i,
                //         CFAILED = "Yes"
                //     });
                // }

                TotalProduct = ProductList.Count;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_ProcessAndUploadClient ThisCls()
        {
            return new R_ProcessAndUploadClient(
                pcModuleName: "IC",
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: "R_DefaultServiceUrlIC",
                poProcessProgressStatus: this);
        }

        public async Task SaveBulk(List<ICB00300ProductDTO> poBatchData)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = ICB00300ContextConstant.CPROPERTY_ID, Value = Param.CPROPERTY_ID });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = ICB00300ContextConstant.CRECALCULATE_TYPE, Value = Param.CRECALCULATE_TYPE });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = ICB00300ContextConstant.LUPDATE_BALANCE, Value = Param.LUPDATE_BALANCE });
                loBatchParUserParameters.Add(new R_KeyValue
                    { Key = ICB00300ContextConstant.LFAIL_FACILITY, Value = Param.LFAIL_FACILITY });


                //Instantiate ProcessClient
                // loCls = ThisCls();

                //Set Data
                if (poBatchData.Count == 0)
                    return;

                foreach (var item in poBatchData)
                {
                    item.NO = poBatchData.IndexOf(item) + 1;
                }

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = Param.CCOMPANY_ID;
                loBatchPar.USER_ID = Param.CUSER_ID;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "ICB00300Back.ICB00300ProcessBatchCls";
                loBatchPar.BigObject = poBatchData;

                await ThisCls().R_BatchProcess<List<ICB00300ProductDTO>>(loBatchPar, 3);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region ProgressStatus

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = $"Process Complete and success with GUID {pcKeyGuid}";
                    IsError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    await ServiceGetError(pcKeyGuid);
                    IsError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            //IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            var loException = new R_Exception();

            Message = $"Process Error with GUID {pcKeyGuid}";
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            // DisplayErrorAction.Invoke(loException);
            DisplayErrorAction(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = $"Process Progress {pnProgress} with status {pcStatus}";

            StateChangeAction();
            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            var loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter
                {
                    COMPANY_ID = Param.CCOMPANY_ID,
                    USER_ID = Param.CUSER_ID,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = ""
                };

                // loCls = new R_ProcessAndUploadClient(
                //     pcModuleName: "PM",
                //     plSendWithContext: true,
                //     plSendWithToken: true,
                //     pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await ThisCls().R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandledEx = loResultData.Where(y => y.SeqNo <= 0).Select(x =>
                        new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loUnhandledEx.ForEach(x => loException.Add(x));
                }
                else
                {
                    var loHandledEx = loResultData.Where(y => y.SeqNo > 0).Select(x =>
                        new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loHandledEx.ForEach(x => loException.Add(x));
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