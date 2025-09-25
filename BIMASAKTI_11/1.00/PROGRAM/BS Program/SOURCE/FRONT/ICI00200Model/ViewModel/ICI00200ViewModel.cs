using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using ICI00200Common;
using ICI00200Common.DTOs;
using ICI00200Common.Params;
using ICI00200Model.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace ICI00200Model.ViewModel
{
    public class ICI00200ViewModel : R_ViewModel<ICI00200ProductDetailDTO>
    {
        private ICI00200Model _model = new ICI00200Model();

        public ObservableCollection<ICI00200ProductDTO> GridList =
            new ObservableCollection<ICI00200ProductDTO>();

        public ICI00200ProductDetailDTO Entity = new ICI00200ProductDetailDTO();

        public string SelectedCategoryId = "";
        public string SelectedCategoryName = "";

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                var lcCategoryId = SelectedCategoryId;
                const string lcProductId = "";
                const string lcListType = "PROD";

                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CCATEGORY_ID, lcCategoryId);
                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CPRODUCT_ID, lcProductId);
                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CLIST_TYPE, lcListType);

                var loReturn = await _model.GetListStreamAsync<ICI00200ProductDTO>(
                    nameof(IICI00200.ICI00200GetProductListStream));

                GridList = new ObservableCollection<ICI00200ProductDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(string pcProductId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new ICI00200DetailParam()
                {
                    CPRODUCT_ID = pcProductId,
                    CFILTER_ID = "",
                    CTYPE = "PROD"
                };

                var loResult = await _model.GetAsync<ICI00200SingleDTO<ICI00200ProductDetailDTO>, ICI00200DetailParam>(
                    nameof(IICI00200.ICI00200GetProductDetail), loParam);

                Entity = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
    
    public class ICI00200PopupViewModel : R_ViewModel<ICI00200DeptWareDetailDTO>
    {
        private ICI00200Model _model = new ICI00200Model();

        public ObservableCollection<ICI00200DeptDTO> DeptList =
            new ObservableCollection<ICI00200DeptDTO>();
        public ObservableCollection<ICI00200WarehouseDTO> WareList =
            new ObservableCollection<ICI00200WarehouseDTO>();
        
        public ICI00200DeptWareDetailDTO DeptWareEntity = new ICI00200DeptWareDetailDTO();
        
        public ICI00200LastInfoDetailDTO LastInfoDetail = new ICI00200LastInfoDetailDTO();
        
        public ICI00200PopupDeptWareParamDTO Param = new ICI00200PopupDeptWareParamDTO();
        
        public async Task GetDeptList()
        {
            var loEx = new R_Exception();

            try
            {
                
                var lcCategoryId = Param.CCATEGORY_ID;
                var lcProductId = Param.CPRODUCT_ID;
                const string lcListType = "DEPT";

                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CCATEGORY_ID, lcCategoryId);
                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CPRODUCT_ID, lcProductId);
                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CLIST_TYPE, lcListType);
                
                var loReturn = await _model.GetListStreamAsync<ICI00200DeptDTO>(
                    nameof(IICI00200.ICI00200GetDeptListStream));

                DeptList = new ObservableCollection<ICI00200DeptDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
    }
        
        public async Task GetWareList()
        {
            var loEx = new R_Exception();

            try
            {
                var lcCategoryId = Param.CCATEGORY_ID;
                var lcProductId = Param.CPRODUCT_ID;
                const string lcListType = "WARE";

                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CCATEGORY_ID, lcCategoryId);
                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CPRODUCT_ID, lcProductId);
                R_FrontContext.R_SetStreamingContext(ICI00200ContextConstant.CLIST_TYPE, lcListType);
                
                var loReturn = await _model.GetListStreamAsync<ICI00200WarehouseDTO>(
                    nameof(IICI00200.ICI00200GetWarehouseListStream));

                WareList = new ObservableCollection<ICI00200WarehouseDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetEntity(string poFilterId, eICI00200PopupType pePopupType)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new ICI00200DetailParam()
                {
                    CPRODUCT_ID = Param.CPRODUCT_ID,
                    CFILTER_ID = poFilterId,
                    CTYPE = pePopupType == eICI00200PopupType.DEPT ? "DEPT" : "WARE"
                };

                var loResult = await _model.GetAsync<ICI00200SingleDTO<ICI00200DeptWareDetailDTO>, ICI00200DetailParam>(
                    nameof(IICI00200.ICI00200GetDeptWareDetail), loParam);

                DeptWareEntity = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetLastInfoDetail(string pcProductId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new ICI00200DetailParam()
                {
                    CPRODUCT_ID = pcProductId,
                    CFILTER_ID = "",
                    CTYPE = "LASTINFO"
                };

                var loResult =
                    await _model.GetAsync<ICI00200SingleDTO<ICI00200LastInfoDetailDTO>, ICI00200DetailParam>(
                        nameof(IICI00200.ICI00200GetLastInfoDetail), loParam);
                
                LastInfoDetail = loResult.Data;
                
                LastInfoDetail.DLAST_PO_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_PO_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastPoDate)
                    ? loLastPoDate
                    : (DateTime?)null;
                
                LastInfoDetail.DLAST_PURCHASE_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_PURCHASE_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastPurchaseDate)
                    ? loLastPurchaseDate
                    : (DateTime?)null;
                
                LastInfoDetail.DLAST_RECEIPT_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_RECEIPT_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastReceiptDate)
                    ? loLastReceiptDate
                    : (DateTime?)null;
                
                LastInfoDetail.DLAST_ISSUE_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_ISSUE_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastIssueDate)
                    ? loLastIssueDate
                    : (DateTime?)null;
                
                LastInfoDetail.DLAST_MOVE_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_MOVE_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastMoveDate)
                    ? loLastMoveDate
                    : (DateTime?)null;
                
                LastInfoDetail.DLAST_STOCK_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_STOCK_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastStockDate)
                    ? loLastStockDate
                    : (DateTime?)null;
                
                LastInfoDetail.DLAST_RECALC_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_RECALC_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastRecalcDate)
                    ? loLastRecalcDate
                    : (DateTime?)null;
                
                LastInfoDetail.DREPLACEMENT_DATE = DateTime.TryParseExact(LastInfoDetail.CLAST_REPLACE_DATE,
                    "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loLastReplaceDate)
                    ? loLastReplaceDate
                    : (DateTime?)null; 
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}