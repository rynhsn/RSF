using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FAT01100Common;
using FAT01100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace FAT01100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT01100 List operations - Get Transaction List
    /// Handles UI data binding and business logic for list operations
    /// </summary>
    public class FAT01100ViewModel : R_ViewModel<FAT01100GeTransListResultDTO>
    {
        private readonly FAT01100Model _listModel = new FAT01100Model();

        /// <summary>
        /// Transaction list collection for grid binding
        /// </summary>
        public ObservableCollection<FAT01100GeTransListResultDTO> TransList { get; set; } = new ObservableCollection<FAT01100GeTransListResultDTO>();

        /// <summary>
        /// Parameter DTO for Get Transaction List
        /// </summary>
        public FAT01100GeTransListParameterDTO ParameterDTO { get; set; } = new FAT01100GeTransListParameterDTO();

        /// <summary>
        /// Department lookup list (from FAT01100GetDeptLookupList)
        /// </summary>
        public ObservableCollection<FAT01100GetDeptLookupListResultDTO> DeptLookupList { get; set; } = new ObservableCollection<FAT01100GetDeptLookupListResultDTO>();

        /// <summary>
        /// System parameter result (from FAT01100GetGetSystemParam)
        /// </summary>
        public FAT01100GetGetSystemParamResultDTO SystemParamData { get; set; } = new FAT01100GetGetSystemParamResultDTO();

        /// <summary>
        /// Year range result (from FAT01100GetYearRange)
        /// </summary>
        public FAT01100GetYearRangeResultDTO YearRangeData { get; set; } = new FAT01100GetYearRangeResultDTO();

        /// <summary>
        /// Month list for Period From/To ComboBox
        /// </summary>
        public List<PeriodMonthDTO> MonthList { get; set; } = new List<PeriodMonthDTO>
        {
            new PeriodMonthDTO { CPERIOD_NO = "01" },
            new PeriodMonthDTO { CPERIOD_NO = "02" },
            new PeriodMonthDTO { CPERIOD_NO = "03" },
            new PeriodMonthDTO { CPERIOD_NO = "04" },
            new PeriodMonthDTO { CPERIOD_NO = "05" },
            new PeriodMonthDTO { CPERIOD_NO = "06" },
            new PeriodMonthDTO { CPERIOD_NO = "07" },
            new PeriodMonthDTO { CPERIOD_NO = "08" },
            new PeriodMonthDTO { CPERIOD_NO = "09" },
            new PeriodMonthDTO { CPERIOD_NO = "10" },
            new PeriodMonthDTO { CPERIOD_NO = "11" },
            new PeriodMonthDTO { CPERIOD_NO = "12" }
        };

        public int YearFrom { get; set; } = DateTime.Now.Year;
        public int YearTo { get; set; } = DateTime.Now.Year;
        public string MonthFrom { get; set; } = DateTime.Now.Month.ToString("00");
        public string MonthTo { get; set; } = DateTime.Now.Month.ToString("00");
        public string DeptName { get; set; } = string.Empty;
        public string AssetName { get; set; } = string.Empty;

        public FAT01100ViewModel()
        {
            R_SetCurrentData(new FAT01100GeTransListResultDTO());
        }

        #region Init / Year Range

        /// <summary>
        /// Get year range for period filter - calls FAT01100Model.FAT01100GetYearRange
        /// </summary>
        public async Task GetYearRangeAsync(string pcCompanyId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT01100GetYearRangeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CCYEAR = "",
                    CMODE = ""
                };
                var loResult = await _listModel.FAT01100GetYearRange(loParam);
                YearRangeData = loResult.Data ?? new FAT01100GetYearRangeResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get system param - calls FAT01100Model.FAT01100GetGetSystemParam
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLanguageId">Language ID</param>
        public async Task GetGetSystemParamAsync(string pcCompanyId, string pcLanguageId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT01100GetGetSystemParamParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId ?? string.Empty,
                    CLANGUAGE_ID = pcLanguageId ?? string.Empty
                };
                var loResult = await _listModel.FAT01100GetGetSystemParam(loParam);
                SystemParamData = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get department lookup list - calls FAT01100Model.FAT01100GetDeptLookupList
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcProgramId">Program ID for department filter</param>
        public async Task GetDeptLookupListAsync(string pcCompanyId, string pcUserId, string pcProgramId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT01100GetDeptLookupListParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId ?? string.Empty,
                    CUSER_ID = pcUserId ?? string.Empty,
                    CPROGRAM_ID = pcProgramId ?? string.Empty
                };
                var loResult = await _listModel.FAT01100GetDeptLookupList(loParam);
                DeptLookupList = new ObservableCollection<FAT01100GetDeptLookupListResultDTO>(loResult.Data ?? new List<FAT01100GetDeptLookupListResultDTO>());
                var foundDept = DeptLookupList?.ToList().Find(x => x.CDEPT_CODE == SystemParamData.CTRANS_DEPT_CODE);
                if (foundDept != null)
                {
                    ParameterDTO.CDEPT_CODE = foundDept.CDEPT_CODE;
                    DeptName = foundDept.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region List Methods

        /// <summary>
        /// Get transaction list - calls FAT01100Model.FAT01100GeTransList and assigns result to TransList
        /// </summary>
        public async Task FAT01100GeTransListAsync()
        {
            var loEx = new R_Exception();

            try
            {
                ParameterDTO.CFROM_PERIOD = YearFrom + MonthFrom;
                ParameterDTO.CTO_PERIOD = YearTo + MonthTo;

                var loResult = await _listModel.FAT01100GeTransList(ParameterDTO);
                TransList = new ObservableCollection<FAT01100GeTransListResultDTO>(loResult.Data ?? new List<FAT01100GeTransListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }

    /// <summary>
    /// DTO for Period Month ComboBox
    /// </summary>
    public class PeriodMonthDTO
    {
        public string CPERIOD_NO { get; set; } = string.Empty;
    }
}
