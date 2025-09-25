using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;

namespace GLM00400FRONT
{
    public partial class GLM00420 : R_Page
    {
        private GLM00420ViewModel _SourceAllocationCenter_viewModel = new GLM00420ViewModel();
        private R_Grid<GLM00420DTO> _SourceAllocationCenter_gridRef;
        private R_Grid<GLM00420DTO> _AllocationCenterPeriod_gridRef;
        private R_ConductorGrid _SourceAllocationCenter_conGrid;
        private R_ConductorGrid _AllocationCenterPeriod_conGrid;


        private string label1 = "<<";
        private string label2 = "<";
        private string AllocID = "";
        private string AllocName = "";
        private string AllocRECID = "";
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GLM00410DTO)poParameter;
                AllocRECID = loData.CREC_ID_ALLOCATION_ID;
                AllocID = loData.CALLOC_NO;
                AllocName = loData.CALLOC_NAME;

                await _SourceAllocationCenter_gridRef.R_RefreshGrid(poParameter);
                await _AllocationCenterPeriod_gridRef.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Center_Source_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00420DTO>(eventArgs.Parameter);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _SourceAllocationCenter_viewModel.GetSourceAllocationCenterList(loParam);

                eventArgs.ListEntityResult = _SourceAllocationCenter_viewModel.SourceAllocationCenterGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task Allocation_Center_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00421DTO>(eventArgs.Parameter);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _SourceAllocationCenter_viewModel.GetAllocationCenterList(loParam);

                eventArgs.ListEntityResult = _SourceAllocationCenter_viewModel.AllocationCenterGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private bool HasMove = false;
        private void R_GridRowBeforeDrop(R_GridRowBeforeDropEventArgs eventArgs)
        {
            HasMove = true;
        }

        private void R_GridRowAfterDrop(R_GridRowAfterDropEventArgs eventArgs)
        {
            HasMove = true;
        }

        private void R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void Allocation_BtnMoveRight()
        {
            HasMove = true;
            _SourceAllocationCenter_gridRef.R_MoveToTargetGrid();
        }

        private void Allocation_BtnAllMoveRight()
        {
            HasMove = true;
            _SourceAllocationCenter_gridRef.R_MoveAllToTargetGrid();
        }

        private void Allocation_BtnAllMoveLeft()
        {
            HasMove = true;
            _AllocationCenterPeriod_gridRef.R_MoveAllToTargetGrid();
        }

        private void Allocation_BtnMoveLeft()
        {
            HasMove = true;
            _AllocationCenterPeriod_gridRef.R_MoveToTargetGrid();
        }
        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<GLM00420DTO>)eventArgs.Data;

                List<string> idList = loData.Select(x => x.CCENTER_CODE).ToList();
                string idString = string.Join(",", idList);

                var loParam = new GLM00421DTO();
                loParam.CREC_ID_ALLOCATION_ID = AllocRECID;
                loParam.CCENTER_LIST = idString;

                await _SourceAllocationCenter_viewModel.SaveAllocationCenterList(loParam);

                await R_MessageBox.Show("", "Allocation Centers updated successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private bool ProsessMove = false;
        private async Task BtnProcess()
        {
            var loEx = new R_Exception();

            try
            {
                ProsessMove = true;
                await _AllocationCenterPeriod_conGrid.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task BtnClose()
        {
            var loEx = new R_Exception();

            try
            {
                if (HasMove && !ProsessMove)
                {
                    var Discard = await R_MessageBox.Show("", "Discard changes? ", R_eMessageBoxButtonType.YesNo);
                    if (Discard == R_eMessageBoxResult.Yes)
                    {
                        await this.Close(true, null);
                    }
                }
                else
                {
                    await this.Close(true, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            
        }
    }
}
