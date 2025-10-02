using PMB01800Common.DTOs;
using PMB01800COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB01800MODEL.ViewModel
{
    public class PMB01800ViewModel : R_ViewModel<PMB01800GetDepositListDTO>
    {
        private PMB01800Model _model = new PMB01800Model();
        public ObservableCollection<PMB01800GetDepositListDTO> DepositList { get; set; } = new ObservableCollection<PMB01800GetDepositListDTO>();
        public PMB01800GetDepositListDTO Entity = new PMB01800GetDepositListDTO();
        public List<PMB01800PropertyDTO> PropertyList = new List<PMB01800PropertyDTO>();
        public static string CPAR_TRANS_CODE_UNIT { get; set; } = "802061,802030";
        public  string CPROGRAM_ID { get; set; } = "PMB01800";
        public PMB01800GetDepositListParamDTO Param = new PMB01800GetDepositListParamDTO() {
            CTRANS_TYPE ="A",
            CPAR_TRANS_CODE = CPAR_TRANS_CODE_UNIT,
            CPAR_DEPT_CODE = ""
        };
        public List<PMB01800TransTypeDTO> TransTypeList = new List<PMB01800TransTypeDTO>
        {
            new PMB01800TransTypeDTO { CCODE = "A", CNAME = "Agreement" },
            new PMB01800TransTypeDTO { CCODE = "L", CNAME = "LOI" }
        };
        public List<PMB01800TransTypeDTO> TransTypeCodeList = new List<PMB01800TransTypeDTO>
        {
            new PMB01800TransTypeDTO { CCODE = CPAR_TRANS_CODE_UNIT, CNAME = "Unit" },
            new PMB01800TransTypeDTO { CCODE = "802062,802032", CNAME = "Casual" },
            new PMB01800TransTypeDTO { CCODE = "802063,802033", CNAME = "Event" }
        };
        public List<PMB01800TransTypeDTO> DepartmentTypeList = new List<PMB01800TransTypeDTO>
        {
            new PMB01800TransTypeDTO { CCODE = "1", CNAME = "All Department" },
            new PMB01800TransTypeDTO { CCODE = "2", CNAME = "Selected Department" }
        };
        public string selectedRadioDeptType = "1";

        public async Task Init()
        {
            await GetProperty();
        }
        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.PMB01800GetPropertyList();
                PropertyList = loResult.Data;
                if (PropertyList.Count > 0)
                {
                    Param.CPROPERTY_ID = PropertyList.FirstOrDefault().CPROPERTY_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task PMB01800GetDepositList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB01800ContextHeaderDTO.CPROPERTY_ID, Param.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB01800ContextHeaderDTO.CTRANS_TYPE, Param.CTRANS_TYPE);
                R_FrontContext.R_SetStreamingContext(PMB01800ContextHeaderDTO.CPAR_TRANS_CODE, Param.CPAR_TRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMB01800ContextHeaderDTO.CPAR_DEPT_CODE, Param.CPAR_DEPT_CODE);
                var loResult = await _model.PMB01800GetDepositListStreamAsync<PMB01800GetDepositListDTO>();
                loResult.Select(x =>
                        x.DSTART_DATE = DateTime.ParseExact(x.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    .ToList();
                loResult.Select(x =>
                    x.DEND_DATE = DateTime.ParseExact(x.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)).ToList();
                DepositList = new ObservableCollection<PMB01800GetDepositListDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        

    }
}
