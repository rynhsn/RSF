using Lookup_PMCOMMON.DTOs.GET_USER_PARAM_DETAIL;
using Lookup_PMCOMMON.DTOs.LMLTODAYDATE;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel.ViewModel.LMLTODAYDATETIME
{
    public class LmlTodayDateTimeViewModel
    {
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public LMLTODAYDATEDTO DateToday = new LMLTODAYDATEDTO();
        public async Task<LMLTODAYDATEDTO> GetTodayDateTime()
        {
            var loEx = new R_Exception();
            LMLTODAYDATEDTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.GetTodayDateTimeAsync();
                loRtn = loResult;
                loRtn.CYEAR = loRtn.DTODAY_DATE_TIME?.ToString("yyyy") ?? "";
                loRtn.CMONTH = loRtn.DTODAY_DATE_TIME?.ToString("MM") ?? "";
                loRtn.DDAY_DATE = loRtn.DTODAY_DATE_TIME?.ToString("dd") ?? "";
                loRtn.IYEAR = loRtn.DTODAY_DATE_TIME?.Year ?? 0;
                loRtn.IMONTH = loRtn.DTODAY_DATE_TIME?.Month ?? 0;
                loRtn.IDAY_DATE = loRtn.DTODAY_DATE_TIME?.Day ?? 0;
                DateToday = loRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
