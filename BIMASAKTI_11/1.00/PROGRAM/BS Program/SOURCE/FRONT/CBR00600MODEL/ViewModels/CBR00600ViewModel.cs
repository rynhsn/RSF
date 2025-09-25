using CBR00600COMMON;
using CBR00600FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CBR00600MODEL
{
    public class CBR00600ViewModel 
    {
        private CBR00600Model _CBR00600Model = new CBR00600Model();

        public List<CBR00600PropetyDTO> VAR_PROPERTY_LIST {  get; set; }
        public List<CBR00600TrxTypeDTO> VAR_TRX_TYPE_LIST {  get; set; }
        public BindingList<CBR00600PeriodDTO> VAR_PERIOD_MONTH_LIST {  get; set; }
        public CBR00600DTO ReportParameter {  get; set; }  = new CBR00600DTO();

        public bool LbForPeriod { get; set; } 
        public int LiPeriodYear { get; set; }
        public string LcPeriodMonth { get; set; }
        public string CFROM_REF_NO { get; set; } = "";
        public string CTO_REF_NO { get; set; } = "";
        public DateTime? DFROM_DATE { get; set; }
        public DateTime? DTO_DATE { get; set; }

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> FilterByList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("REF_NO", R_FrontUtility.R_GetMessage(typeof(CBR00600FrontResources.Resources_Dummy_Class), "_ReferenceNo")),
            new KeyValuePair<string, string>("REF_DATE", R_FrontUtility.R_GetMessage(typeof(CBR00600FrontResources.Resources_Dummy_Class), "_Date")),
        };
        public List<KeyValuePair<string, string>> MessageTypeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("04", R_FrontUtility.R_GetMessage(typeof(CBR00600FrontResources.Resources_Dummy_Class), "_Invoice")),
            new KeyValuePair<string, string>("02", R_FrontUtility.R_GetMessage(typeof(CBR00600FrontResources.Resources_Dummy_Class), "_Receipt")),
        };
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _CBR00600Model.GetInitialAsyncDTO();
                VAR_PROPERTY_LIST = loResult.Data.VAR_PROPERTY_LIST;
                if (loResult.Data.VAR_PROPERTY_LIST.Count > 0)
                {
                    ReportParameter.CPROPERTY_ID = loResult.Data.VAR_PROPERTY_LIST[0].CPROPERTY_ID;
                }
                
                VAR_TRX_TYPE_LIST = loResult.Data.VAR_TRX_TYPE_LIST;
                LiPeriodYear = DateTime.Now.Year;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPeriodBindingList()
        {
            var loEx = new R_Exception();
            List<CBR00600PeriodDTO> PeriodDisplay = null;

            try
            {
                var loResult = await _CBR00600Model.GetAllPeriodDTStreamAsync(LiPeriodYear);
                LcPeriodMonth = loResult.FirstOrDefault(x => x.CPERIOD_NO == DateTime.Now.Month.ToString("D2")).CPERIOD_NO;
                VAR_PERIOD_MONTH_LIST = new BindingList<CBR00600PeriodDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        
            loEx.ThrowExceptionIfErrors();
        }
    }
}
