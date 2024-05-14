using Lookup_GSCOMMON.DTOs;
using Lookup_GSFrontResources;
using Lookup_GSModel;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;

namespace Lookup_GSFRONT
{
    public partial class GSL02000 : R_Page
    {
        private LookupGSL02000ViewModel _viewModel = new LookupGSL02000ViewModel();
        private R_TreeView<GSL02000TreeDTO> _treeRef;
        private R_Conductor _conductorRef;

        private R_ComboBox<GSL02000CountryDTO, string> Country_ComboBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetCountryGeographyList();
                if (_viewModel.CountryGeography.Count > 0)
                {
                    GSL02000CountryDTO loParam = _viewModel.CountryGeography.FirstOrDefault();
                    await Country_OnChange(loParam.CCODE);
                }

                await Country_ComboBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Country_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.CountryID = poParam;

                _viewModel.Country = _viewModel.CountryGeography.Where(x=> x.CCODE == poParam).FirstOrDefault();

                await _treeRef.R_RefreshTree(poParam);
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
                await _viewModel.GetCityGeographyList((string)eventArgs.Parameter);

                eventArgs.ListEntityResult = _viewModel.CityGeographyTree;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Tree_R_RefreshTreeViewState(R_RefreshTreeViewStateEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTreeList = (List<GSL02000CityDTO>)eventArgs.TreeViewList;

                loTreeList.ForEach(x => x.LHAS_CHILD = string.IsNullOrWhiteSpace(x.CPARENT_CODE) &&
                                    loTreeList.Where(y => y.CPARENT_CODE == x.CCODE).Count() > 0 ? true :
                                    loTreeList.Where(y => y.CPARENT_CODE == x.CCODE).Count() > 0);

                eventArgs.ExpandedList = loTreeList.Where(x => x.LHAS_CHILD);
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

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            string loId;
            GSL02000DTO loData = null;

            try
            {
                var loCurrentData = (GSL02000TreeDTO)_treeRef.CurrentSelectedData;
                if (loCurrentData.HasChildren || loCurrentData.ParentId == _viewModel.Country.CCODE)
                {
                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifGSL02000MustCity"), R_eMessageBoxButtonType.OK);
                    return;
                }

                loData = new GSL02000DTO();
                loData.CCODE_COUNTRY = _viewModel.Country.CCODE;
                loData.CNAME_COUNTRY = _viewModel.Country.CNAME;
                loData.CCODE_PROVINCE = loCurrentData.ParentId;
                loData.CNAME_PROVINCE = loCurrentData.ParentName;
                loData.CCODE = loCurrentData.Id;
                loData.CNAME = loCurrentData.Name;

                await this.Close(true, loData);
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

    }
}
