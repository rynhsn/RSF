using PMT02100COMMON.DTOs.PMT02100;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON;
using PMT02100MODEL.FrontDTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02120;
using PMT02100COMMON.DTOs.PMT02110;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02110ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>
    {
        private PMT02100Model loOpenTabModel = new PMT02100Model();

        private PMT02110Model loModel = new PMT02110Model();

        public PMT02100HandoverDTO loHandover = new PMT02100HandoverDTO();

        //public PMT02100DTO loSelectedHandover = new PMT02100DTO();

        public ObservableCollection<PMT02100HandoverDTO> loHandoverList = new ObservableCollection<PMT02100HandoverDTO>();

        public ObservableCollection<PMT02100HandoverBuildingDTO> loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>();

        public PMT02100TabParameterDTO loTabParameter = null;

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
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loOpenTabModel.GetHandoverListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    //x.DSCHEDULED_HO_DATE = !string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(x.CSCHEDULED_HO_DATE, "yyyyMMdd", null) : default;
                    x.DHO_ACTUAL_DATE = !string.IsNullOrEmpty(x.CHO_ACTUAL_DATE) ? DateTime.ParseExact(x.CHO_ACTUAL_DATE, "yyyyMMdd", null) : default;
                    x.DHO_PLAN_DATE = !string.IsNullOrEmpty(x.CHO_PLAN_DATE) ? DateTime.ParseExact(x.CHO_PLAN_DATE, "yyyyMMdd", null) : default;
                    //x.DREF_DATE = !string.IsNullOrEmpty(x.CREF_DATE) ? DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null) : default;
                    //x.DHO_EXPIRY_DATE = !string.IsNullOrEmpty(x.CHO_EXPIRY_DATE) ? DateTime.ParseExact(x.CHO_EXPIRY_DATE, "yyyyMMdd", null) : default;
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
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_BUILDING_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loOpenTabModel.GetHandoverBuildingListStreamAsync();
                loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task ConfirmScheduleProcessAsync(PMT02110ConfirmParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.ConfirmScheduleProcessAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}