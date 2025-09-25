using Lookup_APCOMMON.DTOs.APL00500;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_APModel.ViewModel.APL00500
{
    public class LookupAPL00500ViewModel : R_ViewModel<APL00500DTO>
    {
        private PublicAPLookupModel _model = new PublicAPLookupModel();
        private PublicAPLookupRecordModel _modelRecord = new PublicAPLookupRecordModel();
        public ObservableCollection<APL00500DTO> TransactionLookupGrid = new ObservableCollection<APL00500DTO>();
        public APL00500DTO TransactionLookupEntity = new APL00500DTO();
        public APL00500PeriodDTO PeriodLookup = new APL00500PeriodDTO();
        public APL00500ParameterDTO ParameterLookup = new APL00500ParameterDTO();
        public int VAR_GSM_PERIOD = DateTime.Now.Year;

        public List<APL00500DTO> RadioButton = new List<APL00500DTO>()
        {
            new APL00500DTO() { Code = "A", Desc = "All" },
            new APL00500DTO() { Code = "P", Desc = "For Period" }
        };

        public List<APL00500DTO> Month = new List<APL00500DTO>()
        {
            new APL00500DTO() { Code = "01", Desc = "01" },
            new APL00500DTO() { Code = "02", Desc = "02" },
            new APL00500DTO() { Code = "03", Desc = "03" },
            new APL00500DTO() { Code = "04", Desc = "04" },
            new APL00500DTO() { Code = "05", Desc = "05" },
            new APL00500DTO() { Code = "06", Desc = "06" },
            new APL00500DTO() { Code = "07", Desc = "07" },
            new APL00500DTO() { Code = "08", Desc = "08" },
            new APL00500DTO() { Code = "09", Desc = "09" },
            new APL00500DTO() { Code = "10", Desc = "10" },
            new APL00500DTO() { Code = "11", Desc = "11" },
            new APL00500DTO() { Code = "12", Desc = "12" }
        };

        public async Task GetTransactionLookup()
        {
            var loEx = new R_Exception();
            try
            {
                // ParameterLookup.CSUPPLIER_ID = TransactionLookupEntity.CSUPPLIER_ID;
                ParameterLookup.CPERIOD = TransactionLookupEntity.CPERIOD;
                var loResult = await _model.APL00500TransactionLookupAsync(ParameterLookup);
                TransactionLookupGrid = new ObservableCollection<APL00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInitialTransactionLookupList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APLInitiateTransactionLookupAsync();
                PeriodLookup = (loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void GetPeriod()
        {
            var loEx = new R_Exception();
            try
            {
               TransactionLookupEntity.CPERIOD = TransactionLookupEntity.Month + "/" + TransactionLookupEntity.VAR_GSM_PERIOD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<APL00500DTO> GetTransactionLookup(APL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00500DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.APL00500GetRecordAsync(poEntity);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}