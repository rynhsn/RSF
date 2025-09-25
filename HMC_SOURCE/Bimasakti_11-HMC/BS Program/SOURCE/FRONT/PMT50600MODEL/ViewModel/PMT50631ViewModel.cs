using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50631;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT50600COMMON.DTOs;
using System.Collections.ObjectModel;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.DTOs.PMT50621;
using R_BlazorFrontEnd.Helpers;
using PMT50600FrontResources;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50631ViewModel : R_ViewModel<PMT50631DTO>
    {
        private PMT50631Model loModel = new PMT50631Model();

        private PMT50600Model loCreditNoteListModel = new PMT50600Model();

        private PMT50611Model loCreditNoteItemModel = new PMT50611Model();

        public ObservableCollection<PMT50631DTO> loAdditionalList = new ObservableCollection<PMT50631DTO>();

        public PMT50631DTO loAdditional = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public PMT50611HeaderDTO loHeader = new PMT50611HeaderDTO();

        public AdditionalParameterDTO loTabParam = new AdditionalParameterDTO();

        public async Task GetAdditionalListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetAdditionalListParameterDTO loParam = null;
            PMT50631ResultDTO loRtn = null;

            try
            {
                loParam = new GetAdditionalListParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID,
                    CADDITIONAL_TYPE = loTabParam.CADDITIONAL_TYPE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT50631_GET_ADDITIONAL_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetAdditionalListStreamAsync();
                loAdditionalList = new ObservableCollection<PMT50631DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        public void AdditionalValidation(PMT50631DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CADD_DEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V036"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCHARGES_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V037"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCHARGES_DESC);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V038"));
                }

                llCancel = poParam.NADDITION_AMOUNT <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V039"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetHeaderInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            PMT50611HeaderParameterDTO loParam = null;
            PMT50611HeaderResultDTO loHeaderRtn = null;
            try
            {
                loParam = new PMT50611HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loCreditNoteItemModel.GetHeaderInfoAsync(loParam);
                loHeader = loHeaderRtn.Data;
                loHeader.CLOCAL_CURRENCY_CODE = loCompanyInfo.CLOCAL_CURRENCY_CODE;
                try
                {
                    loHeader.DREF_DATE = DateTime.ParseExact(loHeader.CREF_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCompanyInfoResultDTO loResult = null;
            try
            {
                loResult = await loCreditNoteListModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdditionalAsync(PMT50631DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PMT50631ParameterDTO loParam = null;
            PMT50631ParameterDTO loResult = null;
            try
            {
                loParam = new PMT50631ParameterDTO()
                {
                    Data = poEntity
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);
                loAdditional = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAdditionalAsync(PMT50631DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            PMT50631ParameterDTO loParam = null;
            PMT50631ParameterDTO loResult = null;
            try
            {
                loParam = new PMT50631ParameterDTO()
                {
                    Data = poEntity,
                    CREC_ID = loTabParam.CREC_ID,
                    CADDITIONAL_TYPE = loTabParam.CADDITIONAL_TYPE,
                    CREF_NO = loHeader.CREF_NO,
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    CDEPT_CODE = loHeader.CDEPT_CODE
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loAdditional = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteAdditionalAsync(PMT50631DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PMT50631ParameterDTO loParam = null;
            try
            {
                loParam = new PMT50631ParameterDTO()
                {
                    Data = poEntity
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
