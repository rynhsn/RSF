using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00131;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.DTOs;
using System.Collections.ObjectModel;
using APT00100COMMON.DTOs.APT00111;

namespace APT00100MODEL.ViewModel
{
    public class APT00131ViewModel : R_ViewModel<APT00131DTO>
    {
        private APT00131Model loModel = new APT00131Model();

        private APT00100Model loInvoiceListModel = new APT00100Model();

        private APT00111Model loInvoiceItemModel = new APT00111Model();

        public ObservableCollection<APT00131DTO> loAdditionalList = new ObservableCollection<APT00131DTO>();

        public APT00131DTO loAdditional = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public APT00111HeaderDTO loHeader = new APT00111HeaderDTO();

        public AdditionalParameterDTO loTabParam = new AdditionalParameterDTO();

        public async Task GetAdditionalListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetAdditionalListParameterDTO loParam = null;
            APT00131ResultDTO loRtn = null;

            try
            {
                loParam = new GetAdditionalListParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID,
                    CADDITIONAL_TYPE = loTabParam.CADDITIONAL_TYPE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00131_GET_ADDITIONAL_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetAdditionalListStreamAsync();
                loAdditionalList = new ObservableCollection<APT00131DTO>(loRtn.Data);
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
            APT00111HeaderParameterDTO loParam = null;
            APT00111HeaderResultDTO loHeaderRtn = null;
            try
            {
                loParam = new APT00111HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loInvoiceItemModel.GetHeaderInfoAsync(loParam);
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
                loResult = await loInvoiceListModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdditionalAsync(APT00131DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00131ParameterDTO loParam = null;
            APT00131ParameterDTO loResult = null;
            try
            {
                loParam = new APT00131ParameterDTO()
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

        public async Task SaveAdditionalAsync(APT00131DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00131ParameterDTO loParam = null;
            APT00131ParameterDTO loResult = null;
            try
            {
                loParam = new APT00131ParameterDTO()
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

        public async Task DeleteAdditionalAsync(APT00131DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00131ParameterDTO loParam = null;
            try
            {
                loParam = new APT00131ParameterDTO()
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
