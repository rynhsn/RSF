using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.DTOs.APT00231;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APT00200COMMON.DTOs;
using System.Collections.ObjectModel;
using APT00200COMMON.DTOs.APT00211;

namespace APT00200MODEL.ViewModel
{
    public class APT00231ViewModel : R_ViewModel<APT00231DTO>
    {
        private APT00231Model loModel = new APT00231Model();

        private APT00200Model loPurchaseReturnListModel = new APT00200Model();

        private APT00211Model loPurchaseReturnItemModel = new APT00211Model();

        public ObservableCollection<APT00231DTO> loAdditionalList = new ObservableCollection<APT00231DTO>();

        public APT00231DTO loAdditional = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public APT00211HeaderDTO loHeader = new APT00211HeaderDTO();

        public AdditionalParameterDTO loTabParam = new AdditionalParameterDTO();

        public async Task GetAdditionalListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetAdditionalListParameterDTO loParam = null;
            APT00231ResultDTO loRtn = null;

            try
            {
                loParam = new GetAdditionalListParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID,
                    CADDITIONAL_TYPE = loTabParam.CADDITIONAL_TYPE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00231_GET_ADDITIONAL_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetAdditionalListStreamAsync();
                loAdditionalList = new ObservableCollection<APT00231DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHeaderInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            APT00211HeaderParameterDTO loParam = null;
            APT00211HeaderResultDTO loHeaderRtn = null;
            try
            {
                loParam = new APT00211HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loPurchaseReturnItemModel.GetHeaderInfoAsync(loParam);
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
                loResult = await loPurchaseReturnListModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdditionalAsync(APT00231DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00231ParameterDTO loParam = null;
            APT00231ParameterDTO loResult = null;
            try
            {
                loParam = new APT00231ParameterDTO()
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

        public async Task SaveAdditionalAsync(APT00231DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00231ParameterDTO loParam = null;
            APT00231ParameterDTO loResult = null;
            try
            {
                loParam = new APT00231ParameterDTO()
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

        public async Task DeleteAdditionalAsync(APT00231DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00231ParameterDTO loParam = null;
            try
            {
                loParam = new APT00231ParameterDTO()
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
