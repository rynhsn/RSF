using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_GSFRONT
{
    public partial class GSL01800 : R_Page
    {
        private LookupGSL01800ViewModel _viewModel = new LookupGSL01800ViewModel();
        private R_TreeView<GSL01800TreeDTO> _treeRef;

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
                var loParam = (GSL01800DTOParameter)eventArgs.Parameter;
                await _viewModel.GetCategoryList(loParam);

                eventArgs.ListEntityResult = _viewModel.CategoryGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        //private void Tree_R_RefreshTreeViewState(R_RefreshTreeViewStateEventArgs eventArgs)
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        //var loTreeList = (List<GSL01800DTO>)eventArgs.TreeViewList;

        //        //eventArgs.ExpandedList = loTreeList.Where(x => x.LHAS_CHILD == true).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        public async Task Button_OnClickOkAsync()
        {
            var loCurrentData = (GSL01800TreeDTO)_treeRef.CurrentSelectedData;
            var loData = _viewModel.ListResult.FirstOrDefault(x => x.CCATEGORY_ID == loCurrentData.Id);

            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
