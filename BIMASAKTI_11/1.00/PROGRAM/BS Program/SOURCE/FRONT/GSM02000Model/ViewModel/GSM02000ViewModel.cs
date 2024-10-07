using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM02000Model.ViewModel
{
    public class GSM02000ViewModel : R_ViewModel<GSM02000DTO>
    {
        private GSM02000Model _GSM02000Model = new GSM02000Model();
        public ObservableCollection<GSM02000GridDTO> GridList = new ObservableCollection<GSM02000GridDTO>();
        public ObservableCollection<GSM02000DeductionGridDTO> DeductionGridList = new ObservableCollection<GSM02000DeductionGridDTO>();
        public GSM02000DTO Entity = new GSM02000DTO();
        public GSM02000DeductionGridDTO DeductionEntity = new GSM02000DeductionGridDTO();
        public List<GSM02000RoundingDTO> RoundingModeList = new List<GSM02000RoundingDTO>();
        public GSM02000ActiveInactiveDTO ActiveInactiveEntity = new GSM02000ActiveInactiveDTO();
        public List<GSM02000PropertyDTO> PropertyList = new List<GSM02000PropertyDTO>();
        public List<GSM02000DeductionGridDTO> DeductionList = new List<GSM02000DeductionGridDTO>();
        
        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02000Model.GetAllStreamAsync();
                GridList = new ObservableCollection<GSM02000GridDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetDeductionGridList()
        {
            var loEx = new R_Exception();

            try
            { 
                R_FrontContext.R_SetStreamingContext(GSM02000ContextConstant.CTAX_ID, Entity.CTAX_ID);
                var loReturn = await _GSM02000Model.GetAllDeductionStreamAsync();
                DeductionGridList = new ObservableCollection<GSM02000DeductionGridDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetEntity(GSM02000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _GSM02000Model.R_ServiceGetRecordAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveEntity(GSM02000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _GSM02000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteEntity(GSM02000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM02000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRoundingMode()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02000Model.GetRoundingModeAsync();
                RoundingModeList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02000Model.GetPropertyAsync();
                PropertyList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SetActiveInactive()
        {
            var loEx = new R_Exception();

            try
            {
                var loParams = new GSM02000ActiveInactiveParamsDTO();
                loParams.CTAX_ID = Entity.CTAX_ID;
                loParams.LACTIVE = !Entity.LACTIVE;
                await _GSM02000Model.SetActiveInactiveAsync(loParams);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}