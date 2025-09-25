using PMT50600COMMON.DTOs;
using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50611ViewModel : R_ViewModel<PMT50611ListDTO>
    {
        private PMT50611Model loModel = new PMT50611Model();

        private PMT50600Model loCreditNoteListModel = new PMT50600Model();

        public PMT50611DetailDTO loDetail = new PMT50611DetailDTO();

        public PMT50611DetailResultDTO loDetailRtn = null;

        public PMT50611HeaderDTO loHeader = new PMT50611HeaderDTO();

        public PMT50611HeaderResultDTO loHeaderRtn = null;

        public PMT50611ListDTO loCreditNoteItem = new PMT50611ListDTO();

        public ObservableCollection<PMT50611ListDTO> loCreditNoteItemList = new ObservableCollection<PMT50611ListDTO>();

        public PMT50611ListResultDTO loCreditNoteItemListRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public string lcRecIdParameter;

        public async Task GetInvoiceItemListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT50611_REC_ID_STREAMING_CONTEXT, lcRecIdParameter);
                loCreditNoteItemListRtn = await loModel.GetInvoiceItemListStreamAsync();
                loCreditNoteItemList = new ObservableCollection<PMT50611ListDTO>(loCreditNoteItemListRtn.Data);
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
                loResult = await loCreditNoteListModel.GetCompanyInfoAsync();
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
            PMT50611HeaderParameterDTO loParam = null;
            try
            {
                loParam = new PMT50611HeaderParameterDTO()
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
            PMT50611DetailParameterDTO loParam = null;
            try
            {
                loParam = new PMT50611DetailParameterDTO()
                {
                    CREC_ID = loCreditNoteItem.CREC_ID
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
