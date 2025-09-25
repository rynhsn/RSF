using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02120;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON;
using PMT02100MODEL.FrontDTOs.PMT02100;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02124ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>
    {
        private PMT02120Model loModel = new PMT02120Model();

        public PMT02120EmployeeListDTO loEmployee = new PMT02120EmployeeListDTO();

        public ObservableCollection<PMT02120EmployeeListDTO> loUnassignedEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>();

        public ObservableCollection<PMT02120EmployeeListDTO> loAssignedEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>();

        public PMT02100HandoverDTO loHandover = new PMT02100HandoverDTO();

        public RescheduleProcessHeaderDTO loRescheduleHeader = new RescheduleProcessHeaderDTO();

        public async Task GetEmployeeListStreamAsync(PMT02120EmployeeListParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            PMT02120EmployeeListResultDTO loRtn = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02120_GET_EMPLOYEE_LIST_STREAM_CONTEXT, poParameter);
                loRtn = await loModel.GetEmployeeListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    x.CEMPLOYEE_DISPLAY = x.CEMPLOYEE_ID + " - " + x.CEMPLOYEE_NAME;
                });
                if (poParameter.LASSIGNED)
                {
                    loAssignedEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>(loRtn.Data);
                }
                else
                {
                    loUnassignedEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>(loRtn.Data);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task RescheduleProcessAsync(PMT02120RescheduleProcessParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.RescheduleProcessAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}