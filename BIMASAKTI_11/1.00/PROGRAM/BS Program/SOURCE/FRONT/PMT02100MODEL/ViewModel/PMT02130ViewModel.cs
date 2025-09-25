using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON;
using PMT02100MODEL.FrontDTOs;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02130ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>
    {
        private PMT02100Model loOpenTabModel = new PMT02100Model();

        public PMT02100HandoverDTO loHandover = new PMT02100HandoverDTO();

        public PMT02100HandoverBuildingDTO loHandoverBuilding = new PMT02100HandoverBuildingDTO();

        //public PMT02100DTO loSelectedHandover = new PMT02100DTO();

        public ObservableCollection<PMT02100HandoverDTO> loHandoverList = new ObservableCollection<PMT02100HandoverDTO>();

        public ObservableCollection<PMT02100HandoverBuildingDTO> loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>();

        public PMT02100TabParameterDTO loTabParameter = new PMT02100TabParameterDTO();

        public DateTime? VAR_FROM_DATE;

        public DateTime? VAR_TO_DATE;

        public string lcStatusCode = "";

        public async Task GetHandoverListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02100HandoverParameterDTO loParam = null;
            PMT02100HandoverResultDTO loRtn = null;
            try
            {
                loParam = new PMT02100HandoverParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CTYPE = loTabParameter.CTYPE,
                    CHANDOVER_STATUS = lcStatusCode,
                    CBUILDING_ID = loHandoverBuilding.CBUILDING_ID,
                    CFROM_DATE = VAR_FROM_DATE.HasValue ? VAR_FROM_DATE.Value.ToString("yyyyMMdd") : "",
                    CTO_DATE = VAR_TO_DATE.HasValue ? VAR_TO_DATE.Value.ToString("yyyyMMdd") : "",
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loOpenTabModel.GetHandoverListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    x.DHO_ACTUAL_DATE = !string.IsNullOrEmpty(x.CHO_ACTUAL_DATE) ? DateTime.ParseExact(x.CHO_ACTUAL_DATE, "yyyyMMdd", null) : (DateTime?)null;
                    x.DREF_DATE = !string.IsNullOrEmpty(x.CREF_DATE) ? DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null) : (DateTime?)null;
                    x.CTENANT_DISPLAY = x.CTENANT_NAME + "(" + x.CTENANT_ID + ")";
                });
                loHandoverList = new ObservableCollection<PMT02100HandoverDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHandoverBuildingListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02100HandoverBuildingParameterDTO loParam = null;
            PMT02100HandoverBuildingResultDTO loRtn = null;
            try
            {
                loParam = new PMT02100HandoverBuildingParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CTYPE = loTabParameter.CTYPE,
                    CHANDOVER_STATUS = lcStatusCode,
                    CFROM_DATE = VAR_FROM_DATE.HasValue ? VAR_FROM_DATE.Value.ToString("yyyyMMdd") : "",
                    CTO_DATE = VAR_TO_DATE.HasValue ? VAR_TO_DATE.Value.ToString("yyyyMMdd") : "",
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_BUILDING_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loOpenTabModel.GetHandoverBuildingListStreamAsync();

                if (loRtn != null)
                {
                    loRtn.Data.ForEach(x => x.CBUILDING_DISPLAY_NAME = x.CBUILDING_ID + " - " + x.CBUILDING_NAME + " (" + x.ICOUNT.ToString() + ")");
                }

                loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}