using Lookup_PMCOMMON.DTOs.LML02000;
using Lookup_PMModel;
using Lookup_PMModel.DTOs;
using Lookup_PMModel.ViewModel.LML02000;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class LML02000 : R_Page
    {
        private LookupLML02000ViewModel _viewModel = new LookupLML02000ViewModel();
        private R_TreeView<PML02000TreeDTO> _treeRef;
        private R_Conductor _conductorRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _treeRef.R_RefreshTree(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LML02000ParameterDTO)eventArgs.Parameter;
                _viewModel.poPar=loParam;
                await _viewModel.LML02000TenantCategoryList(loParam);

                eventArgs.ListEntityResult = _viewModel.TenantCategoryGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ProductCategory_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loConductorData = (LML02000DTO)eventArgs.Data;
            var loData = R_FrontUtility.ConvertObjectToObject<PML02000TreeDTO>(loConductorData);
            loData.Id = loConductorData.CCATEGORY_ID;
            loData.ParentId = loConductorData.CPARENT_ID;
            loData.Description = string.Format("[{0}] {1} - {2}", loConductorData.ILEVEL, loConductorData.CCATEGORY_ID, loConductorData.CCATEGORY_NAME);
            loData.Note = loConductorData.CCATEGORY_NAME;
            loData.Level = loConductorData.ILEVEL;

            eventArgs.GridData = loData;
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loCurrentData = (PML02000TreeDTO)_treeRef.CurrentSelectedData;

                var loData = _viewModel.TenantCategoryListResult.FirstOrDefault(x => x.CCATEGORY_ID == loCurrentData.Id);
                if (_viewModel.poPar.LCHILD_ONLY == true && loData.LHAS_CHILD == true)
                {
                        loEx.Add("Err05", _localizer["_ValidationTenantCategory"]);
                }
                else
                {
                    await this.Close(true, loData);
                }

                    
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

        #region Filter Treeview
        private string _lcSeacrhTree = "";
        private void FilterTree()
        {
            _treeRef.R_FilterTreeView(_lcSeacrhTree);
        }
        #endregion
    }
}
