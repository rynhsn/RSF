using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using PMM10000COMMON.SLA_Category;
using R_BlazorFrontEnd.Enums;
using PMM10000COMMON.UtilityDTO;
using PMM10000COMMON.Call_Type_Pricelist;


namespace PMM10000FRONT
{
    public partial class PMM10000Pricelist : R_Page, R_ITabPage
    {
        private PMM10000PricelistViewModel _viewModel = new();
        private R_Grid<PMM10000SLACallTypeDTO>? _gridCallType;
        private R_ConductorGrid? _conGridSLACallType;
        private int _pageSizeCallType = 7;
        private int _pageSizePricelist = 11;

        private R_Grid<PMM10000PricelistDTO>? _AssignPricelistGrid_gridRef;
        private R_Grid<PMM10000PricelistDTO>? _UnassignPricelist_gridRef;
        private R_ConductorGrid? _Pricelist_conGrid;
        private R_ConductorGrid? _UnAssignPricelist_conGrid;

        private string labelDoubleright = ">>";
        private string labelright = ">";
        private string labelleft = "<";
        private string labelDoubleleft = "<<";
        //private bool _rightArrowButton = false;
        private string? _ArrowButtonType;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.Parameter = (PMM10000DbParameterDTO)poParameter;
                if (_viewModel.Parameter.CPROPERTY_ID != null)
                {
                    await _gridCallType.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public Task RefreshTabPageAsync(object poParam)
        {
            throw new NotImplementedException();
        }
        #region CallTypeGrid
        private async Task R_CallTypeList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModel.GetSLACallTypeList();
                eventArgs.ListEntityResult = _viewModel.CallTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM10000SLACallTypeDTO)eventArgs.Data;
                _viewModel.CallTypeData = loData;
                _viewModel.Parameter.CCALL_TYPE_ID = loData.CCALL_TYPE_ID;

                await _AssignPricelistGrid_gridRef.R_RefreshGrid(null);
                await _UnassignPricelist_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region pricelist
        private bool HasMove = false;

        private async Task R_GridRowAfterDropAsync(R_GridDragDropAfterDropEventArgs<PMM10000PricelistDTO> eventArgs)
        {
            string idString = "";
            var loDataListUnassign = _UnassignPricelist_gridRef.DataSource;
            var loDataListAssign = _AssignPricelistGrid_gridRef.DataSource;

            var TargetGridId = (string)eventArgs.TargetGridId;
            var gridID = _UnassignPricelist_gridRef.Id;


            var loDataListUnassignselected = eventArgs.Items;
            var loDataListAssignselected = _AssignPricelistGrid_gridRef.TargetGridDrop;

           // var loDataListAssignselected1 = _AssignPricelistGrid_gridRef.SelectedItems.ToList();

            if (TargetGridId == gridID)
            {
                List<string> idListUnassign = loDataListUnassign.Select(x => x.CPRICELIST_ID).ToList()!;
                idString = string.Join(",", idListUnassign);
                await _viewModel.AssignPricelist(PricelistId: idString);
            }
            else
            {

                List<string> idListAssign = loDataListAssign.Select(x => x.CPRICELIST_ID).ToList()!;
                idString = string.Join(",", idListAssign);
                await _viewModel.UnassignPricelist(PricelistId: idString);

            }
            _viewModel.Enable_ArrowRight = _AssignPricelistGrid_gridRef.HasData;
            _viewModel.Enable_DoubleArrowRight = _AssignPricelistGrid_gridRef.HasData;
            _viewModel.Enable_ArrowLeft = _UnassignPricelist_gridRef.HasData;
            _viewModel.Enable_DoubleArrowLeft = _UnassignPricelist_gridRef.HasData;
        }
        private void R_CheckBoxSelectRender(R_CheckBoxSelectRenderEventArgs eventArgs)
        {

            var loData = (PMM10000PricelistDTO)eventArgs.Data;
            eventArgs.Enabled = loData.LDISABLE;

        }
        private void Allocation_BtnAllMoveRight()
        {
            _AssignPricelistGrid_gridRef.R_MoveAllToTargetGrid();
        }
        private void Allocation_BtnMoveRight()
        {
            _AssignPricelistGrid_gridRef.R_MoveToTargetGrid();
        }

        private void Allocation_BtnMoveLeft()
        {
            _UnassignPricelist_gridRef.R_MoveToTargetGrid();
        }
        private void Allocation_BtnAllMoveLeft()
        {
            _UnassignPricelist_gridRef.R_MoveAllToTargetGrid();
        }

        private async Task R_PricelistList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModel.GetPricelist();
                eventArgs.ListEntityResult = _viewModel.PricelistList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void R_DisplayAssignAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //var test = _AssignPricelistGrid_gridRef.CurrentSelectedData;
                //var test2 = _AssignPricelistGrid_gridRef.CurrentSelectedRowIndex;
                //var test3 = eventArgs.Data;
                //var test4 = _AssignPricelistGrid_gridRef.SelectedItems.ToList();
                ////_viewModel.Enable_ArrowRight = _AssignPricelistGrid_gridRef.HasData;

                //var abc = _AssignPricelistGrid_gridRef.SelectedItems; //SEt private
                //if (eventArgs.Data != null)
                //{
                //    _AssignPricelistGrid_gridRef.R_SetCurrentData(eventArgs.Data);
                //}
                //var abcd = _AssignPricelistGrid_gridRef.CurrentSelectedData;
                //var abcde = _AssignPricelistGrid_gridRef.GetCurrentData;
                // var abcdef = _AssignPricelistGrid_gridRef.;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task R_UnAssignPricelistList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModel.GetAssignPricelist();
                eventArgs.ListEntityResult = _viewModel.AssignPricelistList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private void R_DisplayUnAssignAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //    _viewModel.Enable_ArrowLeft = _UnassignPricelist_gridRef.SelectedItems.Count() > 0;
                // _viewModel.Enable_ArrowRight = _UnassignPricelist_gridRef.HasData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

    }
}
