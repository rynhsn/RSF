using FAT00300Common.Requests;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_ContextFrontEnd;
using R_CommonFrontBackAPI;
using FAT00300Common;
using FAT00300Common.DTOs;
using FAT00300FrontResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Enums;
using System.Security.Cryptography;
using System.Net.Http.Headers;

namespace FAT00300Model.VMs
{
    public class FAT00302ViewModel : R_ViewModel<FAT00300GetAssetInformationTABResultDTO>
    {
        public FAT00300Model ModelFAT00300 = new FAT00300Model();
        public FAT00300GetAllocationExpenseListParameterDTO paramListAllocExpense = new FAT00300GetAllocationExpenseListParameterDTO();
        public FAT00300GetAssetInformationTABParameterDTO paramGetAssetExpense = new FAT00300GetAssetInformationTABParameterDTO();
        public ObservableCollection<FAT00300GetAllocationExpenseListResultDTO> listAllocExpense = new ObservableCollection<FAT00300GetAllocationExpenseListResultDTO>();
        public FAT00300GetAssetInformationTABResultDTO loAllocExpense = new FAT00300GetAssetInformationTABResultDTO();

        public async Task GetListAllocExpenseAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstants.ASSET_CODE, paramListAllocExpense.CASSET_CODE);
                var loTemp = await ModelFAT00300.GetAllocationExpenseListAsync();
                if (loTemp != null)
                {
                    listAllocExpense = new ObservableCollection<FAT00300GetAllocationExpenseListResultDTO>(loTemp.Data);
                    
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetExpenseAllocationRecordAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = await ModelFAT00300.GetAssetInformationTAB(paramGetAssetExpense);
                loAllocExpense = loTemp.Data ?? new FAT00300GetAssetInformationTABResultDTO();
                loAllocExpense.DLAST_TRANS_DATE = R_FrontUtility.R_ConvertToDateTime(loAllocExpense.CLAST_TRANS_DATE, "yyyymmdd");
                loAllocExpense.DINSERVICE_DATE = R_FrontUtility.R_ConvertToDateTime(loAllocExpense.CINSERVICE_DATE, "yyyymmdd");
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
