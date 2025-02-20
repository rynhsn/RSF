using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HDM00400Common;
using HDM00400Common.DTOs;
using HDM00400Common.Param;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace HDM00400Model.ViewModel
{
    public class HDM00400ViewModel : R_ViewModel<HDM00400PublicLocationDTO>
    {
        private HDM00400Model _model = new HDM00400Model();

        public ObservableCollection<HDM00400PublicLocationDTO> GridList =
            new ObservableCollection<HDM00400PublicLocationDTO>();

        public HDM00400PublicLocationDTO Entity = new HDM00400PublicLocationDTO();
        public List<HDM00400PropertyDTO> PropertyList = new List<HDM00400PropertyDTO>();

        public string SelectedProperty { get; set; } = string.Empty;

        public async Task Init()
        {
            var loEx = new R_Exception();

            try
            {
                await GetProperty();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(HDM00400PublicLocationDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceGetRecordAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveEntity(HDM00400PublicLocationDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteEntity(HDM00400PublicLocationDTO poEntity)
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

        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn =
                    await _model.GetAsync<HDM00400ListDTO<HDM00400PropertyDTO>>(
                        nameof(IHDM00400.HDM00400GetPropertyList));
                PropertyList = loReturn.Data;
                SelectedProperty = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<HDM00400ActiveInactiveDTO> SetActiveInactive()
        {
            var loEx = new R_Exception();
            var loReturn = new HDM00400SingleDTO<HDM00400ActiveInactiveDTO>();

            try
            {
                var loParams = new HDM00400ActiveInactiveParamsDTO
                {
                    CPROPERTY_ID = Entity.CPROPERTY_ID,
                    CPUBLIC_LOC_ID = Entity.CPUBLIC_LOC_ID,
                    LACTIVE = !Entity.LACTIVE
                };
                loReturn = await _model.GetAsync<HDM00400SingleDTO<HDM00400ActiveInactiveDTO>, HDM00400ActiveInactiveParamsDTO>(
                    nameof(IHDM00400.HDM00400ActivateInactivate), loParams);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn.Data;
        }

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(HDM00400ContextConstant.CPROPERTY_ID, SelectedProperty);
                var loReturn = await _model.GetListStreamAsync<HDM00400PublicLocationDTO>(
                    nameof(IHDM00400.HDM00400GetPublicLocationListStream));
                GridList = new ObservableCollection<HDM00400PublicLocationDTO>(loReturn);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<HDM00400PublicLocationExcelDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            var loResult = new HDM00400PublicLocationExcelDTO();

            try
            {
                loResult = await _model.GetAsync<HDM00400PublicLocationExcelDTO>(
                        nameof(IHDM00400.HDM00400DownloadTemplateFileModel));
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