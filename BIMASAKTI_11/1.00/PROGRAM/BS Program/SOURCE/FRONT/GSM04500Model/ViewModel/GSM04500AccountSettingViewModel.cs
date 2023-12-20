using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM04500Model.ViewModel
{
    public class GSM04500AccountSettingViewModel : R_ViewModel<GSM04500AccountSettingDTO>
    {
        // private GSM04500InitModel _initModel = new GSM04500InitModel();
        private GSM04500AccountSettingModel _model = new GSM04500AccountSettingModel();

        public ObservableCollection<GSM04500AccountSettingDTO> GridList =
            new ObservableCollection<GSM04500AccountSettingDTO>();

        public GSM04500JournalGroupDTO ParentEntity = new GSM04500JournalGroupDTO();
        public GSM04500AccountSettingDTO Entity = new GSM04500AccountSettingDTO();

        public async Task Init(object poParam)
        {
            ParentEntity = (GSM04500JournalGroupDTO)poParam;
        }

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CPROPERTY_ID, ParentEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CJRNGRP_TYPE, ParentEntity.CJRNGRP_TYPE);
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CJRNGRP_CODE, ParentEntity.CJRNGRP_CODE);
                var loReturn = await _model.GetAllStreamAsync();
                GridList = new ObservableCollection<GSM04500AccountSettingDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(GSM04500AccountSettingDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.R_ServiceGetRecordAsync(poParam);
                Entity = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveEntity(GSM04500AccountSettingDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = ParentEntity.CPROPERTY_ID;
                poNewEntity.CJRNGRP_TYPE = ParentEntity.CJRNGRP_TYPE;
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

    public class GSM04500AccountSettingDeptViewModel : R_ViewModel<GSM04500AccountSettingDeptDTO>
    {
        // private GSM04500InitModel _initModel = new GSM04500InitModel();
        private GSM04500AccountSettingDeptModel _model = new GSM04500AccountSettingDeptModel();

        public ObservableCollection<GSM04500AccountSettingDeptDTO> GridList =
            new ObservableCollection<GSM04500AccountSettingDeptDTO>();

        public GSM04500AccountSettingDTO ParentEntity = new GSM04500AccountSettingDTO();
        public GSM04500AccountSettingDeptDTO Entity = new GSM04500AccountSettingDeptDTO();
        
        public async Task Init(object poParam)
        {
            ParentEntity = (GSM04500AccountSettingDTO)poParam;
        }
        
        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CPROPERTY_ID, ParentEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CJRNGRP_TYPE, ParentEntity.CJRNGRP_TYPE);
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CJRNGRP_CODE, ParentEntity.CJRNGRP_CODE);
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CGOA_CODE, ParentEntity.CGOA_CODE);
                var loReturn = await _model.GetAllStreamAsync();
                GridList = new ObservableCollection<GSM04500AccountSettingDeptDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetEntity(GSM04500AccountSettingDeptDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.R_ServiceGetRecordAsync(poParam);
                Entity = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveEntity(GSM04500AccountSettingDeptDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = ParentEntity.CPROPERTY_ID;
                poNewEntity.CJRNGRP_TYPE = ParentEntity.CJRNGRP_TYPE;
                poNewEntity.CJRNGRP_CODE = ParentEntity.CJRNGRP_CODE;
                poNewEntity.CGOA_CODE = ParentEntity.CGOA_CODE;
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteEntity(GSM04500AccountSettingDeptDTO poNewEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}