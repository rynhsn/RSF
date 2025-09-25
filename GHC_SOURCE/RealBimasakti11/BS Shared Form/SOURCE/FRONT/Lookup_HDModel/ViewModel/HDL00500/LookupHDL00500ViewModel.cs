using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_HDCOMMON.DTOs.HDL00500;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_HDModel.ViewModel
{
    public class LookupHDL00500ViewModel
    {
        private PublicHDLookupModel _model = new PublicHDLookupModel();
        private PublicHDLookupRecordModel _modelRecord = new PublicHDLookupRecordModel();
        public ObservableCollection<HDL00500DTO> TaskLookup = new ObservableCollection<HDL00500DTO>();
        public HDL00500DTO TaskLookupRecord { get; set; }
        public HDL00500ParameterDTO ParameterLookup { get; set; }


        public async Task GetTaskList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.HDL00500TaskLookupAsync(ParameterLookup);
                TaskLookup = new ObservableCollection<HDL00500DTO>(loResult);
                
                foreach (var list in TaskLookup)
                {
                    if (string.IsNullOrWhiteSpace(list.CSTART_DATE))
                    {
                        list.CSTART_DISPLAY = null;
                    }
                    else
                    {
                        list.CSTART_DISPLAY = DateTime.ParseExact(list.CSTART_DATE.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    
                    if (string.IsNullOrWhiteSpace(list.CEND_DATE))
                    {
                        list.CEND_DATE_DISPLAY = null;
                    }
                    else
                    {
                        list.CEND_DATE_DISPLAY = DateTime.ParseExact(list.CEND_DATE.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<HDL00500DTO> GetTaskRecord(HDL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00500DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.HDL00500GetRecordAsync(poEntity);
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