using APT00200COMMON.DTOs;
using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APT00200MODEL.ViewModel
{
    public class APT00211ViewModel : R_ViewModel<APT00211ListDTO>
    {
        private APT00211Model loModel = new APT00211Model();

        private APT00200Model loPurchaseReturnListModel = new APT00200Model();

        public APT00211DetailDTO loDetail = new APT00211DetailDTO();

        public APT00211DetailResultDTO loDetailRtn = null;

        public APT00211HeaderDTO loHeader = new APT00211HeaderDTO();

        public APT00211HeaderResultDTO loHeaderRtn = null;

        public APT00211ListDTO loPurchaseReturnItem = new APT00211ListDTO();

        public ObservableCollection<APT00211ListDTO> loPurchaseReturnItemList = new ObservableCollection<APT00211ListDTO>();

        public APT00211ListResultDTO loPurchaseReturnItemListRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public string lcRecIdParameter;

        public async Task GetPurchaseReturnItemListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00211_REC_ID_STREAMING_CONTEXT, lcRecIdParameter);
                loPurchaseReturnItemListRtn = await loModel.GetPurchaseReturnItemListStreamAsync();
                loPurchaseReturnItemList = new ObservableCollection<APT00211ListDTO>(loPurchaseReturnItemListRtn.Data);
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
                loResult = await loPurchaseReturnListModel.GetCompanyInfoAsync();
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
            APT00211HeaderParameterDTO loParam = null;
            try
            {
                loParam = new APT00211HeaderParameterDTO()
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
            APT00211DetailParameterDTO loParam = null;
            try
            {
                loParam = new APT00211DetailParameterDTO()
                {
                    CREC_ID = loPurchaseReturnItem.CREC_ID
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
