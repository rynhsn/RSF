using FAF00100COMMON;
using FAF00100COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FAF00100Model.VMs
{
    public class FAF00100ViewModel : R_ViewModel<FAF00100GetAssetResultDTO>
    {
        private FAF00100Model modelFAF00100 = new FAF00100Model();
        public ObservableCollection<FAF00100GetAssetAllocResultDTO> loListAssetAlloc = new ObservableCollection<FAF00100GetAssetAllocResultDTO>();
        public FAF00100GetAssetAllocParameterDTO loParamListAllocExpense = new FAF00100GetAssetAllocParameterDTO();
        public FAF00100GetAssetResultDTO loAssetInformation = new FAF00100GetAssetResultDTO();
        public FAF00100GetAssetAllocParameterDTO paramListAllocExpense = new FAF00100GetAssetAllocParameterDTO();
        public FAF00100GetAssetResultDTO paramGetAssetExpense = new FAF00100GetAssetResultDTO();

        public async Task GetListAssetAlloc()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.ASSET_CODE, loParamListAllocExpense.CASSET_CODE);
                var loTemp = await modelFAF00100.GetListAssetAllocAsync();
                if (loTemp != null)
                {
                    loListAssetAlloc = new ObservableCollection<FAF00100GetAssetAllocResultDTO>(loTemp.Data);

                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAssetAllocationInfo()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = await modelFAF00100.R_ServiceGetRecordAsync(paramGetAssetExpense);
                if (loTemp != null)
                {
                    loAssetInformation = loTemp;
                    loAssetInformation.DLAST_TRANS_DATE = R_FrontUtility.R_ConvertToDateTime(loAssetInformation.CLAST_TRANS_DATE, "yyyymmdd");
                    loAssetInformation.DINSERVICE_DATE = R_FrontUtility.R_ConvertToDateTime(loAssetInformation.CINSERVICE_DATE, "yyyymmdd");
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
