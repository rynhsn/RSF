using BlazorClientHelper;
using LMM01500COMMON;
using LMM01500MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Xml.Linq;

namespace LMM01500FRONT
{
    public partial class LMM01530 : R_Page, R_ITabPage
    {
        private LMM01530ViewModel _OtherCharges_viewModel = new LMM01530ViewModel();
        private R_Grid<LMM01530DTO> _OtherCharges_gridRef;
        private R_ConductorGrid _OtherCharges_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<LMM01530DTO>(poParameter);

                _OtherCharges_viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                _OtherCharges_viewModel.InvGrpCode = loData.CINVGRP_CODE;
                _OtherCharges_viewModel.InvGrpName = loData.CINVGRP_NAME;

                await _OtherCharges_gridRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        private bool enableAdd = true;
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<LMM01530DTO>(poParam);
            enableAdd = loParam.LTabEnalbleDept;
            if (loParam.LTabEnalbleDept)
            {
                _OtherCharges_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                _OtherCharges_viewModel.InvGrpCode = loParam.CINVGRP_CODE;
                _OtherCharges_viewModel.InvGrpName = loParam.CINVGRP_NAME;

                await _OtherCharges_gridRef.R_RefreshGrid(loParam);
            }
            else
            {
                _OtherCharges_gridRef.DataSource.Clear();
                _OtherCharges_viewModel.InvGrpCode = "";
                _OtherCharges_viewModel.InvGrpName = "";
            }
        }

        private async Task OtherCharges_Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _OtherCharges_viewModel.GetOtherChargesList((LMM01530DTO)eventArgs.Parameter);

                eventArgs.ListEntityResult = _OtherCharges_viewModel.OtherChargesGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                LMM01530DTO loParam = (LMM01530DTO)eventArgs.Data;
                await _OtherCharges_viewModel.GetOtherCharges(loParam);

                eventArgs.Result = _OtherCharges_viewModel.OtherCharges;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01530DTO loData = (LMM01530DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext;
                    loData.CINVGRP_CODE = _OtherCharges_viewModel.InvGrpCode;
                }

                await _OtherCharges_viewModel.SaveOtherCharges(
                    loData,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _OtherCharges_viewModel.OtherCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01530DTO loData = (LMM01530DTO)eventArgs.Data;
                await _OtherCharges_viewModel.DeleteOtherCharges(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OtherCharges_Before_Open_Grid_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {

            var param = new LMM01531DTO()
            {
                CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext,
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LMM01531);
        }

        private void OtherCharges_After_Open_Grid_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (LMM01531DTO)eventArgs.Result;
            var loData = (LMM01530DTO)eventArgs.ColumnData;
            if (loTempResult is null)
            {
                return;
            }

            loData.CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext;
            loData.CINVGRP_CODE = _OtherCharges_viewModel.Data.CINVGRP_CODE;
            loData.CCHARGES_ID = loTempResult.CCHARGES_ID;
            loData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            loData.UNIT_UTILITY_CHARGE = loTempResult.UNIT_UTILITY_CHARGE;
            loData.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
            loData.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
        }

    }
}
