using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.UtilityDTO;
using PMM10000FrontResources;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000MODEL.ViewModel
{
    public class PMM10000PricelistViewModel
    {
        private PMM10000ListModel _GetListModel = new PMM10000ListModel();
        private PMM1000UpdatePricelistModel _Assign_UnassignModel = new PMM1000UpdatePricelistModel();

        public ObservableCollection<PMM10000SLACallTypeDTO> CallTypeList = new ObservableCollection<PMM10000SLACallTypeDTO>();
        public PMM10000SLACallTypeDTO CallTypeData = new PMM10000SLACallTypeDTO();
        public ObservableCollection<PMM10000PricelistDTO> PricelistList = new ObservableCollection<PMM10000PricelistDTO>();
        public PMM10000PricelistDTO PricelistData = new PMM10000PricelistDTO();
        public ObservableCollection<PMM10000PricelistDTO> AssignPricelistList = new ObservableCollection<PMM10000PricelistDTO>();
        public PMM10000PricelistDTO AssignPricelistData = new PMM10000PricelistDTO();

        public PMM10000DbParameterDTO Parameter = new PMM10000DbParameterDTO();
        public bool Enable_DoubleArrowRight;
        public bool Enable_ArrowRight;
        public bool Enable_DoubleArrowLeft;
        public bool Enable_ArrowLeft;

        public async Task GetSLACallTypeList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);

                var loResult = await _GetListModel.GetCallTypeListAsyncModel();
                CallTypeList = new ObservableCollection<PMM10000SLACallTypeDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetPricelist()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCALL_TYPE_ID, Parameter.CCALL_TYPE_ID);
                var loResult = await _GetListModel.GetPricelistModel();

                if (loResult.Data.Count > 0)
                {
                    PricelistList = new ObservableCollection<PMM10000PricelistDTO>(loResult.Data);
                    Enable_DoubleArrowRight = true;
                    Enable_ArrowRight = true;
                }
                else
                {
                    PricelistList = new ObservableCollection<PMM10000PricelistDTO>();
                    Enable_DoubleArrowRight = false;
                    Enable_ArrowRight = false;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetAssignPricelist()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCALL_TYPE_ID, Parameter.CCALL_TYPE_ID);
                var loResult = await _GetListModel.GetAssignPricelistModel();

                if (loResult.Data.Count > 0)
                {
                    AssignPricelistList = new ObservableCollection<PMM10000PricelistDTO>(loResult.Data);
                    Enable_DoubleArrowLeft = true;
                    Enable_ArrowLeft = true;
                }
                else
                {
                    AssignPricelistList = new ObservableCollection<PMM10000PricelistDTO>();
                    Enable_DoubleArrowLeft = false;
                    Enable_ArrowLeft = false;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task AssignPricelist(string PricelistId)
        {
            var loEx = new R_Exception();

            try
            {
                PMM10000DbParameterDTO AssignPricelist = Parameter;
                AssignPricelist.CPRICELIST_ID = PricelistId;
                await _Assign_UnassignModel.AssignPricelistAsyncModel(AssignPricelist);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task UnassignPricelist(string PricelistId)
        {
            var loEx = new R_Exception();

            try
            {
                PMM10000DbParameterDTO UnassignPricelist = Parameter;
                UnassignPricelist.CPRICELIST_ID = PricelistId;
                await _Assign_UnassignModel.UnassignPricelistAsyncModel(UnassignPricelist);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
