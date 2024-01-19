using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM04500Model.ViewModel
{
    public class GSM04500JournalGroupViewModel : R_ViewModel<GSM04500JournalGroupDTO>
    {
        private GSM04500InitModel _initModel = new();
        private GSM04500JournalGroupModel _model = new();

        public ObservableCollection<GSM04500JournalGroupDTO> GridList = new();

        public GSM04500JournalGroupDTO Entity = new();

        public List<GSM04500PropertyDTO> PropertyList = new();
        public List<GSM04500FunctionDTO> TypeList = new();

        public string PropertyId = string.Empty;
        public string TypeCode = string.Empty;

        public async Task Init()
        {
            await GetPropertyList();
            await GetTypeList();
        }

        private async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _initModel.GetAllPropertyAsync();
                PropertyList = loReturn.Data;
                PropertyId = PropertyList.FirstOrDefault().CPROPERTY_ID ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _initModel.GetAllTypeAsync();
                TypeList = loReturn.Data;
                TypeCode = TypeList.FirstOrDefault().CCODE ?? string.Empty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CPROPERTY_ID, PropertyId);
                R_FrontContext.R_SetStreamingContext(GSM04500ContextConstant.CJRNGRP_TYPE, TypeCode);
                var loReturn = await _model.GetAllStreamAsync();
                
                GridList = new ObservableCollection<GSM04500JournalGroupDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(GSM04500JournalGroupDTO poParam)
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
        
        public async Task SaveEntity(GSM04500JournalGroupDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = PropertyId;
                poNewEntity.CJRNGRP_TYPE = TypeCode;
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteEntity(GSM04500JournalGroupDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM04500JournalGroupExcelDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM04500JournalGroupExcelDTO loResult = null;

            try
            {
                loResult = await _model.GSM04500DownloadTemplateFileModel();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}