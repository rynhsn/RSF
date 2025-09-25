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
using PMT02100COMMON.DTOs.PMT02120;
using System.Linq;
using System.Globalization;
using PMT02100MODEL.FrontDTOs;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02121ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>
    {
        private PMT02120Model loModel = new PMT02120Model();

        public PMT02120EmployeeListDTO loEmployee = new PMT02120EmployeeListDTO();

        public ObservableCollection<PMT02120EmployeeListDTO> loUnassignedEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>();

        public ObservableCollection<PMT02120EmployeeListDTO> loAssignedEmployeeList = new ObservableCollection<PMT02120EmployeeListDTO>();

        public PMT02100HandoverDTO loHandover = new PMT02100HandoverDTO();

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

        public async Task AssignEmployeeAsync(PMT02120AssignEmployeeParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.AssignEmployeeAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}