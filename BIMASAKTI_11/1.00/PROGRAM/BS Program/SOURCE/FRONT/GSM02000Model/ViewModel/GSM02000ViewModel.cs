using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM02000Model.ViewModel
{
    public class GSM02000ViewModel : R_ViewModel<GSM02000DTO>
    {
        private GSM02000Model _GSM02000Model = new GSM02000Model();
        public ObservableCollection<GSM02000GridDTO> GridList = new ObservableCollection<GSM02000GridDTO>();
        public GSM02000DTO Entity = new GSM02000DTO();
        public List<GSM02000RoundingDTO> RoundingModeList = new List<GSM02000RoundingDTO>();
        public GSM02000ActiveInactiveDTO ActiveInactiveEntity = new GSM02000ActiveInactiveDTO();
        
        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02000Model.GetAllAsync();
                GridList = new ObservableCollection<GSM02000GridDTO>(loReturn.Data);
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
        public async Task SetActiveInactive()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CTAX_ID, ActiveInactiveEntity.CTAX_ID);
                R_FrontContext.R_SetContext(ContextConstant.LACTIVE, ActiveInactiveEntity.LACTIVE);
                await _GSM02000Model.SetActiveInactiveAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public Task Validate(GSM02000DTO poParam)
        {
            var loEx = new R_Exception();
            
            try
            {
                if(poParam.CTAX_ID == null)
                    loEx.Add(new Exception("Tax ID is required"));
                if (poParam.CTAX_NAME == null)
                    loEx.Add(new Exception("Tax Name is required"));
                if (poParam.CROUNDING_MODE == null)
                    loEx.Add(new Exception("Rounding Mode is required"));
                if (poParam.IROUNDING == null)
                    loEx.Add(new Exception("Rounding Unit is required"));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            
            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }
    }
}