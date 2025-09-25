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
    public partial class GLM00430 : R_Page
    {
        private GLM00430ViewModel _SourceAllocationAccount_viewModel = new GLM00430ViewModel();
        private R_Grid<GLM00430DTO> _SourceAllocationAccount_gridRef;
        private R_Grid<GLM00430DTO> _AllocationCenterAccount_gridRef;
        private R_ConductorGrid _SourceAllocationAccount_conGrid;
        private R_ConductorGrid _AllocationCenterAccount_conGrid;

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
                AllocID = loData.CALLOC_NO;
                AllocName = loData.CALLOC_NAME;
                AllocRECID = loData.CREC_ID;

                await _SourceAllocationAccount_gridRef.R_RefreshGrid(poParameter);
                await _AllocationCenterAccount_gridRef.R_RefreshGrid(poParameter);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Account_Source_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00430DTO>(eventArgs.Parameter);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _SourceAllocationAccount_viewModel.GetSourceAllocationAccountList(loParam);

                eventArgs.ListEntityResult = _SourceAllocationAccount_viewModel.SourceAllocationAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task Allocation_Account_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00431DTO>(eventArgs.Parameter);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _SourceAllocationAccount_viewModel.GetAllocationAccountList(loParam);

                eventArgs.ListEntityResult = _SourceAllocationAccount_viewModel.AllocationAccountGrid;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private bool HasMove = false;
        private void R_Source_GridRowBeforeDrop(R_GridRowBeforeDropEventArgs eventArgs)
        {
            HasMove = true;
        }

        private void R_Source_GridRowAfterDrop(R_GridRowAfterDropEventArgs eventArgs)
        {
            HasMove = true;
        }
        private void R_Selected_GridRowBeforeDrop(R_GridRowBeforeDropEventArgs eventArgs)
        {
            HasMove = true;
        }

        private void R_Selected_GridRowAfterDrop(R_GridRowAfterDropEventArgs eventArgs)
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
            _SourceAllocationAccount_gridRef.R_MoveToTargetGrid();
        }

        private void Allocation_BtnAllMoveRight()
        {
            HasMove = true;
            _SourceAllocationAccount_gridRef.R_MoveAllToTargetGrid();
        }

        private void Allocation_BtnAllMoveLeft()
        {
            HasMove = true;
            _AllocationCenterAccount_gridRef.R_MoveAllToTargetGrid();
        }

        private void Allocation_BtnMoveLeft()
        {
            HasMove = true;
            _AllocationCenterAccount_gridRef.R_MoveToTargetGrid();
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<GLM00430DTO>)eventArgs.Data;

                List<string> idList = loData.Select(x => x.CGLACCOUNT_NO).ToList();
                string idString = string.Join(",", idList);

                var loParam = new GLM00431DTO();
                loParam.CREC_ID_ALLOCATION_ID = AllocRECID;
                loParam.CACCOUNT_LIST = idString;

                await _SourceAllocationAccount_viewModel.SaveAllocationAccountList(loParam);
                await R_MessageBox.Show("", "Allocation Accounts updated successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool ProsessMove = false;
        private async Task BtnProcess()
        {
            var loEx = new R_Exception();

            try
            {
                ProsessMove = true;
                await _AllocationCenterAccount_conGrid.R_SaveBatch();
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

            R_DisplayException(loEx);
            
        }
    }
}
