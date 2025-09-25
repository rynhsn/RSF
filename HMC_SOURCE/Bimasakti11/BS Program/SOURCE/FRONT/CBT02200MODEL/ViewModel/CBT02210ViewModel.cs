using CBT02200COMMON.DTO.CBT02200;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using CBT02200COMMON.DTO.CBT02210;
using R_CommonFrontBackAPI;
using CBT02200COMMON.DTO;
using System.Xml.Linq;
using CBT02200COMMON.DTO.CBT02220;

namespace CBT02200MODEL.ViewModel
{
    public class CBT02210ViewModel : R_ViewModel<CBT02210DTO>
    {
        private CBT02200Model loChequeListModel = new CBT02200Model();

        private CBT02210Model loHeaderModel = new CBT02210Model();

        private CBT02210DetailModel loDetailModel = new CBT02210DetailModel();

        public CBT02210DTO loChequeHeader = new CBT02210DTO();

        public CBT02210DetailDTO loChequeDetail = new CBT02210DetailDTO();

        public List<GetCenterListDTO> loCenterList = null;

        public ObservableCollection<CBT02210DetailDTO> loChequeDetailList = new ObservableCollection<CBT02210DetailDTO>();

        public InitialProcessDTO loInitialProcess = null;

        public List<GetDeptLookupListDTO> loDeptLookupList = null;

        public List<GetStatusDTO> loStatusList = null;

        public RefreshCurrencyRateDTO loRefreshCurrencyRate = null;

        public GetCompanyInfoDTO loCompanyInfo = new GetCompanyInfoDTO();

        public GetCBSystemParamDTO loCBSystemParam = new GetCBSystemParamDTO();

        public GetTransCodeInfoDTO loTransCodeInfo = new GetTransCodeInfoDTO();

        public PageParameterDTO loPageParameter = null;

        public bool IsOpenAsPage = false;

        public async Task InitialProcessAsync()
        {
            R_Exception loException = new R_Exception();
            InitialProcessResultDTO loRtn = null;
            try
            {
                loRtn = await loChequeListModel.InitialProcessAsync();
                loInitialProcess = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task RefreshCurrencyRateAsync(RefreshCurrencyRateParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            RefreshCurrencyRateResultDTO loRtn = new RefreshCurrencyRateResultDTO();
            try
            {
                loRtn = await loHeaderModel.RefreshCurrencyRateAsync(poParam);
                loRefreshCurrencyRate = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChequeEntryDetailListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            CBT02210DetailResultDTO loResult = new CBT02210DetailResultDTO();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CBT02210_CHEQUE_LIST_DETAIL_REC_ID_STREAMING_CONTEXT, loChequeHeader.CREC_ID);
                loResult = await loDetailModel.GetChequeEntryDetailListStreamAsync();

                loResult.Data.ForEach(x => x.DDOCUMENT_DATE = DateTime.ParseExact(x.CDOCUMENT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));

                loChequeDetailList = new ObservableCollection<CBT02210DetailDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetCenterListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetCenterListResultDTO loResult = new GetCenterListResultDTO();
            try
            {
                loResult = await loDetailModel.GetCenterListStreamAsync();
                loResult.Data.Add(new GetCenterListDTO()
                {
                    CCENTER_CODE = "",
                    CCENTER_NAME = ""
                });
                loCenterList = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChequeHeaderAsync(CBT02210DTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                CBT02210ParameterDTO loResult = await loHeaderModel.R_ServiceGetRecordAsync(new CBT02210ParameterDTO()
                {
                    Data = poEntity,
                    CTRANS_CODE = "190020"
                });
                loResult.Data.DREF_DATE = DateTime.ParseExact(loResult.Data.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DCHEQUE_DATE = DateTime.ParseExact(loResult.Data.CCHEQUE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DDUE_DATE = DateTime.ParseExact(loResult.Data.CDUE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.Data.CCLEAR_DATE))
                {
                    loResult.Data.DCLEAR_DATE = DateTime.ParseExact(loResult.Data.CCLEAR_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                loChequeHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChequeHeaderAsync(CBT02210DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            CBT02210ParameterDTO loParam = null;
            try
            {
                poEntity.CREF_DATE = poEntity.DREF_DATE.Value.ToString("yyyyMMdd");
                poEntity.CCHEQUE_DATE = poEntity.DCHEQUE_DATE.Value.ToString("yyyyMMdd");
                poEntity.CDUE_DATE = poEntity.DDUE_DATE.Value.ToString("yyyyMMdd");

                /*if (loTransCodeInfo.LINCREMENT_FLAG == false && loCBSystemParam.LCB_NUMBERING == false)
                {
                    poEntity.CREF_NO = "";
                }*/

                if (IsOpenAsPage)
                {
                    loParam = new CBT02210ParameterDTO()
                    {
                        Data = poEntity,
                        CTRANS_CODE = "190020",
                        CCALLER_TRANS_CODE = loPageParameter.PARAM_CALLER_TRANS_CODE,
                        CCALLER_REF_NO = loPageParameter.PARAM_CALLER_REF_NO,
                        CCALLER_ID = loPageParameter.PARAM_CALLER_ID.Substring(0, Math.Min(2, loPageParameter.PARAM_CALLER_ID.Length)),
                        CGLACCOUNT_NO = loPageParameter.PARAM_GLACCOUNT_NO,
                        CCASH_FLOW_GROUP_CODE = loPageParameter.PARAM_CASH_FLOW_GROUP_CODE,
                        CCASH_FLOW_CODE = loPageParameter.PARAM_CASH_FLOW_CODE,
                        LPAGE = true
                    };
                }
                else
                {
                    loParam = new CBT02210ParameterDTO()
                    {
                        Data = poEntity,
                        CTRANS_CODE = "190020"
                    };
                }

                CBT02210ParameterDTO loResult = await loHeaderModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loResult.Data.DREF_DATE = DateTime.ParseExact(loResult.Data.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DCHEQUE_DATE = DateTime.ParseExact(loResult.Data.CCHEQUE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DDUE_DATE = DateTime.ParseExact(loResult.Data.CDUE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.Data.CCLEAR_DATE))
                {
                    loResult.Data.DCLEAR_DATE = DateTime.ParseExact(loResult.Data.CCLEAR_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }

                loChequeHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteChequeHeaderAsync(CBT02210DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loHeaderModel.R_ServiceDeleteAsync(new CBT02210ParameterDTO()
                {
                    Data = poEntity,
                    CTRANS_CODE = "190020",
                    CACTION = "DELETE"
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetChequeDetailAsync(CBT02210DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                CBT02210DetailParameterDTO loResult = await loDetailModel.R_ServiceGetRecordAsync(new CBT02210DetailParameterDTO()
                {
                    Data = poEntity,
                    CTRANS_CODE = "190020"
                });

                loResult.Data.DDOCUMENT_DATE = DateTime.ParseExact(loResult.Data.CDOCUMENT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loChequeDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChequeDetailAsync(CBT02210DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            CBT02210DetailParameterDTO loParam = null;
            try
            {
                loParam = new CBT02210DetailParameterDTO()
                {
                    Data = poEntity,
                    CTRANS_CODE = "190020",
                    CCHEQUE_ID = loChequeHeader.CREC_ID,
                    CDEPT_CODE = loChequeHeader.CDEPT_CODE,
                    CREF_NO = loChequeHeader.CREF_NO,
                    CREF_DATE = loChequeHeader.CREF_DATE,
                    CCURRENCY_CODE = loChequeHeader.CCURRENCY_CODE
                };
                CBT02210DetailParameterDTO loResult = await loDetailModel.R_ServiceSaveAsync(loParam, peCRUDMode);
                loResult.Data.DDOCUMENT_DATE = DateTime.ParseExact(loResult.Data.CDOCUMENT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loChequeDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteChequeDetailAsync(CBT02210DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loDetailModel.R_ServiceDeleteAsync(new CBT02210DetailParameterDTO()
                {
                    Data = poEntity,
                    CTRANS_CODE = "190020",
                    CACTION = "DELETE",
                    CCURRENCY_CODE = loChequeHeader.CCURRENCY_CODE,
                    CDEPT_CODE = loChequeHeader.CDEPT_CODE,
                    CCHEQUE_ID = loChequeHeader.CREC_ID,
                    CREF_DATE = loChequeHeader.CREF_DATE,
                    CREF_NO = loChequeHeader.CREF_NO
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task UpdateStatusAsync(UpdateStatusParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await loHeaderModel.UpdateStatusAsync(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
