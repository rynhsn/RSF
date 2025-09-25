using APT00100COMMON.DTOs;
using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL.ViewModel
{
    public class APT00111ViewModel : R_ViewModel<APT00111ListDTO>
    {
        private APT00111Model loModel = new APT00111Model();

        private APT00100Model loInvoiceListModel = new APT00100Model();

        public APT00111DetailDTO loDetail = new APT00111DetailDTO();

        public APT00111DetailResultDTO loDetailRtn = null;

        public APT00111HeaderDTO loHeader = new APT00111HeaderDTO();

        public APT00111HeaderResultDTO loHeaderRtn = null;

        public APT00111ListDTO loInvoiceItem = new APT00111ListDTO();

        public ObservableCollection<APT00111ListDTO> loInvoiceItemList = new ObservableCollection<APT00111ListDTO>();

        public APT00111ListResultDTO loInvoiceItemListRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public string lcRecIdParameter;

        public async Task GetInvoiceItemListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00111_REC_ID_STREAMING_CONTEXT, lcRecIdParameter);
                loInvoiceItemListRtn = await loModel.GetInvoiceItemListStreamAsync();
                loInvoiceItemList = new ObservableCollection<APT00111ListDTO>(loInvoiceItemListRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
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

        public async Task GetHeaderInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            APT00111HeaderParameterDTO loParam = null;
            try
            {
                loParam = new APT00111HeaderParameterDTO()
                {
                    CREC_ID = lcRecIdParameter
                };
                loHeaderRtn = await loModel.GetHeaderInfoAsync(loParam);
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

        public async Task GetDetailInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            APT00111DetailParameterDTO loParam = null;
            try
            {
                loParam = new APT00111DetailParameterDTO()
                {
                    CREC_ID = loInvoiceItem.CREC_ID
                };
                loDetailRtn = await loModel.GetDetailInfoAsync(loParam);
                loDetail = loDetailRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
