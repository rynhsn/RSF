using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ICB00300Common;
using ICB00300Common.DTOs;
using ICB00300Common.Params;
using ICB00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace ICB00300Model.ViewModel
{
    public class ICB00300ViewModel : R_ViewModel<ICB00300ProductDTO>
    {
        private ICB00300Model _model = new ICB00300Model();

        public ICB00300ProcessParam Param = new ICB00300ProcessParam();
        public List<ICB00300PropertyDTO> PropertyList = new List<ICB00300PropertyDTO>();
        
        // public ObservableCollection<ICB00300ProductDTO> ProductList = new ObservableCollection<ICB00300ProductDTO>();
        // public int TotalSelected { get; set; } = 0;
        // public int TotalProduct { get; set; } = 0;
        
        public List<KeyValuePair<string, string>> RecTypeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("N", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "NewProcess")),
            new KeyValuePair<string, string>("F", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "FailProductInLastRecalculateProcess"))
        };

        public async Task Init()
        {
            Param.CRECALCULATE_TYPE = "N";
            await _getPropertyList();
        }
        
        
        private async Task _getPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICB00300ListDTO<ICB00300PropertyDTO>>(
                        nameof(IICB00300.ICB00300GetPropertyList));
                PropertyList = loReturn.Data;
                Param.CPROPERTY_ID = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        } 
        
        // public async Task GetProductList()
        // {
        //     var loEx = new R_Exception();
        //     try
        //     {
        //         R_FrontContext.R_SetStreamingContext(ICB00300ContextConstant.CPROPERTY_ID, Param.CPROPERTY_ID);
        //         R_FrontContext.R_SetStreamingContext(ICB00300ContextConstant.CRECALCULATE_TYPE,
        //             Param.CRECALCULATE_TYPE);
        //         
        //         var loReturn =
        //             await _model.GetListStreamAsync<ICB00300ProductDTO>(
        //                 nameof(IICB00300.ICB00300GetProductListStream));
        //         ProductList = new ObservableCollection<ICB00300ProductDTO>(loReturn);
        //         TotalProduct = ProductList.Count;
        //     }
        //     catch (Exception ex)
        //     {
        //         loEx.Add(ex);
        //     }
        //
        //     loEx.ThrowExceptionIfErrors();
        // }
    }
}