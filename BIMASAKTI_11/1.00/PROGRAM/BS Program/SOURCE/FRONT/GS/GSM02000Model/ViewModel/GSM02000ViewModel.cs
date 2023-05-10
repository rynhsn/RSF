﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
                // var loParam = new GSM02000DTO { CTAX_ID = piEntityId };
                // var loResult = await _GSM02000Model.R_ServiceGetRecordAsync(poEntity);
                // Entity = loResult;
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
                // var loResult = await _GSM02000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
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
                // var loParam = new GSM02000DTO { CTAX_ID = poNewEntity.CTAX_ID };
                // await _GSM02000Model.R_ServiceDeleteAsync(loParam);
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
    }
}