using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100MODEL.ViewModel;
using PMT02100COMMON.DTOs.PMT02120;
using PMT02100COMMON.DTOs.PMT02100;

namespace PMT02100FRONT
{
    public partial class PMT02124 : R_Page
    {
        private PMT02124ViewModel loViewModel = new PMT02124ViewModel();
        private R_Grid<PMT02120EmployeeListDTO> _unassignedGridRef;
        private R_Grid<PMT02120EmployeeListDTO> _assignedGridRef;
        private R_ConductorGrid _unassignedConductorRef;
        private R_ConductorGrid _assignedConductorRef;

        private string label1 = "<<";
        private string label2 = "<";
        private bool IsRescheduleSuccesful = false;
        private bool VAR_INCLUDE_PRINT = true;
        private DateTime? loDefaultDate = DateTime.Today;
        //private DateTime? loRescheduledHODate = DateTime.Today;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loHandover = (PMT02100HandoverDTO)poParameter;

                if (loViewModel.loHandover != null)
                {
                    await _unassignedGridRef.R_RefreshGrid(null);
                    await _assignedGridRef.R_RefreshGrid(null);
                    loDefaultDate = loViewModel.loHandover.DSCHEDULED_HO_DATE.Value;
                    //loRescheduledHODate = loViewModel.loHandover.DSCHEDULED_HO_DATE.Value.Date;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //private void RescheduledHODate_ValueChanged(DateTime? poParam)
        //{
        //    loViewModel.loHandover.DSCHEDULED_HO_DATE = poParam;
        //    loRescheduledHODate = loViewModel.loHandover.DSCHEDULED_HO_DATE.Value.Date;
        //}

        private async Task Unassigned_Employee_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetEmployeeListStreamAsync(new PMT02120EmployeeListParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHandover.CPROPERTY_ID,
                    CDEPT_CODE = loViewModel.loHandover.CDEPT_CODE,
                    CTRANS_CODE = loViewModel.loHandover.CTRANS_CODE,
                    CREF_NO = loViewModel.loHandover.CREF_NO,
                    LASSIGNED = false
                });

                eventArgs.ListEntityResult = loViewModel.loUnassignedEmployeeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task AssigenedEmployee_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetEmployeeListStreamAsync(new PMT02120EmployeeListParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHandover.CPROPERTY_ID,
                    CDEPT_CODE = loViewModel.loHandover.CDEPT_CODE,
                    CTRANS_CODE = loViewModel.loHandover.CTRANS_CODE,
                    CREF_NO = loViewModel.loHandover.CREF_NO,
                    LASSIGNED = true
                });

                eventArgs.ListEntityResult = loViewModel.loAssignedEmployeeList;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private bool HasMove = false;
        private void R_Source_GridRowBeforeDrop(R_GridDragDropBeforeDropEventArgs<PMT02120EmployeeListDTO> eventArgs)
        {
            HasMove = true;
        }

        private void R_Source_GridRowAfterDrop(R_GridDragDropAfterDropEventArgs<PMT02120EmployeeListDTO> eventArgs)
        {
            HasMove = true;
        }
        private void R_Selected_GridRowBeforeDrop(R_GridDragDropBeforeDropEventArgs<PMT02120EmployeeListDTO> eventArgs)
        {
            HasMove = true;
        }

        private void R_Selected_GridRowAfterDrop(R_GridDragDropAfterDropEventArgs<PMT02120EmployeeListDTO> eventArgs)
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
            _unassignedGridRef.R_MoveToTargetGrid();
        }

        private void Allocation_BtnAllMoveRight()
        {
            HasMove = true;
            _unassignedGridRef.R_MoveAllToTargetGrid();
        }

        private void Allocation_BtnAllMoveLeft()
        {
            HasMove = true;
            _assignedGridRef.R_MoveAllToTargetGrid();
        }

        private void Allocation_BtnMoveLeft()
        {
            HasMove = true;
            _assignedGridRef.R_MoveToTargetGrid();
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<PMT02120EmployeeListDTO>)eventArgs.Data;

                List<string> idList = loData.Select(x => x.CEMPLOYEE_ID).ToList();
                string idString = string.Join(",", idList);

                await loViewModel.RescheduleProcessAsync(new PMT02120RescheduleProcessParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHandover.CPROPERTY_ID,
                    CDEPT_CODE = loViewModel.loHandover.CDEPT_CODE,
                    CREF_NO = loViewModel.loHandover.CREF_NO,
                    CTRANS_CODE = loViewModel.loHandover.CTRANS_CODE,
                    CEMPLOYEE_ID = idString,
                    CREASON = loViewModel.loHandover.CREASON,
                    CSCHEDULED_HO_DATE = loViewModel.loHandover.DSCHEDULED_HO_DATE.Value.ToString("yyyyMMdd"),
                    CSCHEDULED_HO_TIME = loViewModel.loHandover.DSCHEDULED_HO_DATE.Value.ToString("HH:mm"),
                    //CSCHEDULED_HO_TIME = loViewModel.loHandover.ISCHEDULED_HO_TIME_HOURS.ToString("D2") + ":" + loViewModel.loHandover.ISCHEDULED_HO_TIME_MINUTES.ToString("D2"),
                });
                if (!loEx.HasError)
                {
                    await this.Close(true, VAR_INCLUDE_PRINT);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool ProsessMove = false;
        private async Task OnReschedule()
        {
            var loEx = new R_Exception();
            bool lCancel;

            try
            {
                lCancel = !loViewModel.loHandover.DSCHEDULED_HO_DATE.HasValue;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V008"));
                }
                //lCancel = string.IsNullOrWhiteSpace(loViewModel.loHandover.CSCHEDULED_HO_TIME_HOURS) || string.IsNullOrWhiteSpace(loViewModel.loHandover.CSCHEDULED_HO_TIME_MINUTES);
                //if (lCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                //        "V009"));
                //}

                lCancel = string.IsNullOrWhiteSpace(loViewModel.loHandover.CREASON);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V010"));
                }

                lCancel = !loViewModel.loAssignedEmployeeList.Any(x => x.CEMPLOYEE_TYPE == "TR");
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V007"));
                }

                if (loEx.HasError)
                {
                    goto EndBlock;
                }

                ProsessMove = true;
                await _assignedGridRef.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            R_DisplayException(loEx);
        }

        private async Task OnClose()
        {
            var loEx = new R_Exception();

            try
            {
                if (HasMove && !ProsessMove)
                {
                    var Discard = await R_MessageBox.Show("", "Discard changes? ", R_eMessageBoxButtonType.YesNo);
                    if (Discard == R_eMessageBoxResult.Yes)
                    {
                        await this.Close(false, false);
                    }
                }
                else
                {
                    await this.Close(false, false);
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