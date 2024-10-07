using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMR02600Common;
using PMR02600Common.DTOs;
using PMR02600Common.DTOs.Print;
using PMR02600Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMR02600Model.ViewModel
{
    public class PMR02600ViewModel : R_ViewModel<PMR02600DataResultDTO>
    {
        private PMR02600Model _model = new PMR02600Model();
        public List<PMR02600PropertyDTO> PropertyList = new List<PMR02600PropertyDTO>();
        public PMR02600ReportParam ReportParam = new PMR02600ReportParam();

        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public async Task Init()
        {
            await GetPropertyList();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<PMR02600ListDTO<PMR02600PropertyDTO>>(
                        nameof(IPMR02600.PMR02600GetPropertyList));
                PropertyList = loReturn.Data;
                ReportParam.CPROPERTY = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : ReportParam.CPROPERTY ;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}