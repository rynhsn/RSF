using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.General;
using PMM00100COMMON.DTO_s.Helper;
using PMM00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMM00100MODEL.View_Model
{
    public class PMM00100ViewModel : R_ViewModel<SystemParamDetailDTO>
    {
        private PMM00101Model _model = new PMM00101Model();
        private PMM00100Model _initModel = new PMM00100Model();
        public string CurrentPropertyId { get; set; } = "";
        public bool IsSysParamFound { get; set; } = true;

        public ObservableCollection<SystemParamDTO> SystemParams { get; set; } =
            new ObservableCollection<SystemParamDTO>();

        public SystemParamDetailDTO SystemParamDetail { get; set; } = new SystemParamDetailDTO();
        public List<PropertyDTO> Properties { get; set; } = new List<PropertyDTO>();
        public List<PeriodDtDTO> PeriodDetails { get; set; } = new List<PeriodDtDTO>();
        public PeriodYearRangeDTO PeriodYearRange { get; set; } = new PeriodYearRangeDTO();
        public List<GeneralTypeDTO> GSBCodeInfos { get; set; } = new List<GeneralTypeDTO>();
        public List<GeneralTypeDTO> WithHoldingTaxModes { get; set; } = new List<GeneralTypeDTO>();
        public int SelectedSoftPeriodYear { get; set; } = 0;
        public int SelectedCurrentPeriodYear { get; set; } = 0;
        public int SelectedInvPeriodYear { get; set; }
        public string SelectedSoftPeriodMonth { get; set; } = "";
        public string SelectedCurrentPeriodMonth { get; set; } = "";
        public string SelectedInvPeriodMonth { get; set; } = "";
        public int SelectedElectricPeriodYear { get; set; } = 0;
        public int SelectedWaterPeriodYear { get; set; } = 0;
        public int SelectedGasPeriodYear { get; set; } = 0;
        public string SelectedElectricPeriodMonth { get; set; } = "";
        public string SelectedWaterPeriodMonth { get; set; } = "";
        public string SelectedGasPeriodMonth { get; set; } = "";
        public int SelectedElectricCutOffDate { get; set; } = 1;
        public int SelectedWaterCutOffDate { get; set; } = 1;
        public int SelectedGasCutOffDate { get; set; } = 1;


        //initial
        public async Task GetInitData(R_ILocalizer<Resources_Dummy_Class> poLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                WithHoldingTaxModes = new List<GeneralTypeDTO>
                {
                    new GeneralTypeDTO { CCODE = "I", CNAME = poLocalizer["_opt_on_invoice"] },
                    new GeneralTypeDTO { CCODE = "R", CNAME = poLocalizer["_opt_on_receipt"] },
                };
                await GetPeriodYearReangeRecordAsnyc();
                await GetPeriodDetailListAsync();
                await GetGSBCodeInfoListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                Properties = await _initModel.GetPropertyListAsync();
                Properties.Add(new PropertyDTO()
                {
                    CPROPERTY_ID = "",
                    CPROPERTY_NAME = ""
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodYearReangeRecordAsnyc()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                PeriodYearRange = await _initModel.GetPeriodYearRangeRecordAsync(new PeriodYearRangeParamDTO()
                {
                    CYEAR = "",
                    CMODE = ""
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGSBCodeInfoListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lcClassIdCode = "_BS_GENERATE_INVOICE_MODE";
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CAPPLICATION,
                    PMM00100ContextConstant.CCONST_APPLICATION);
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CCLASS_ID, lcClassIdCode);
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CREC_ID_LIST,
                    PMM00100ContextConstant.CCONST_REC_ID_LIST);
                GSBCodeInfos = await _initModel.GetGSBCodeInfoListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodDetailListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CYEAR, DateTime.Now.Year.ToString());
                PeriodDetails = await _initModel.GetPeriodDtListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParamListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _initModel.GetPropertyListAsync();
                var loMappingData = R_FrontUtility.ConvertCollectionToCollection<SystemParamDTO>(loResult);

                SystemParams = new ObservableCollection<SystemParamDTO>(loMappingData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParamDetailAsync(SystemParamDetailDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = await _model.R_ServiceGetRecordAsync(poParam);

                IsSysParamFound = loData != null;
                SystemParamDetail = loData ?? new SystemParamDetailDTO();
                if (SystemParamDetail != null)
                {
                    //soft,inv,curr period
                    SelectedSoftPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CSOFT_PERIOD) &&
                                              SystemParamDetail.CSOFT_PERIOD.Length >= 6
                        ? SystemParamDetail.CSOFT_PERIOD.Substring(4, 2)
                        : "";
                    SelectedCurrentPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CCURRENT_PERIOD) &&
                                                 SystemParamDetail.CCURRENT_PERIOD.Length >= 6
                        ? SystemParamDetail.CCURRENT_PERIOD.Substring(4, 2)
                        : "";
                    SelectedInvPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CINV_PRD) &&
                                             SystemParamDetail.CINV_PRD.Length >= 6
                        ? SystemParamDetail.CINV_PRD.Substring(4, 2)
                        : "";
                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CCURRENT_PERIOD))
                    {
                        SelectedCurrentPeriodYear = int.Parse(SystemParamDetail.CCURRENT_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CSOFT_PERIOD))
                    {
                        SelectedSoftPeriodYear = int.Parse(SystemParamDetail.CSOFT_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CINV_PRD))
                    {
                        SelectedInvPeriodYear = int.Parse(SystemParamDetail.CINV_PRD.Substring(0, 4));
                    }

                    //utility
                    SelectedElectricPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CELECTRIC_PERIOD) &&
                                                  SystemParamDetail.CELECTRIC_PERIOD.Length >= 6
                        ? SystemParamDetail.CELECTRIC_PERIOD.Substring(4, 2)
                        : "";
                    SelectedWaterPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CWATER_PERIOD) &&
                                               SystemParamDetail.CWATER_PERIOD.Length >= 6
                        ? SystemParamDetail.CWATER_PERIOD.Substring(4, 2)
                        : "";
                    SelectedGasPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CGAS_PERIOD) &&
                                             SystemParamDetail.CGAS_PERIOD.Length >= 6
                        ? SystemParamDetail.CGAS_PERIOD.Substring(4, 2)
                        : "";
                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CELECTRIC_PERIOD))
                    {
                        SelectedElectricPeriodYear = int.Parse(SystemParamDetail.CELECTRIC_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CWATER_PERIOD))
                    {
                        SelectedWaterPeriodYear = int.Parse(SystemParamDetail.CWATER_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CGAS_PERIOD))
                    {
                        SelectedGasPeriodYear = int.Parse(SystemParamDetail.CGAS_PERIOD.Substring(0, 4));
                    }

                    //cutoffDate for utility
                    SelectedElectricCutOffDate =
                        !string.IsNullOrWhiteSpace(SystemParamDetail.CELECTRIC_DATE) &&
                        int.TryParse(SystemParamDetail.CELECTRIC_DATE, out var lnDate)
                            ? lnDate
                            : 0;
                    SelectedWaterCutOffDate =
                        !string.IsNullOrWhiteSpace(SystemParamDetail.CWATER_DATE) &&
                        int.TryParse(SystemParamDetail.CWATER_DATE, out lnDate)
                            ? lnDate
                            : 0;
                    SelectedGasCutOffDate =
                        !string.IsNullOrWhiteSpace(SystemParamDetail.CGAS_DATE) &&
                        int.TryParse(SystemParamDetail.CGAS_DATE, out lnDate)
                            ? lnDate
                            : 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveSytemParamDetailAsync(SystemParamDetailDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                IsSysParamFound = loResult != null;
                SystemParamDetail = loResult;
                if (SystemParamDetail != null)
                {
                    //soft,inv,curr period
                    SelectedSoftPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CSOFT_PERIOD) &&
                                              SystemParamDetail.CSOFT_PERIOD.Length >= 6
                        ? SystemParamDetail.CSOFT_PERIOD.Substring(4, 2)
                        : "";
                    SelectedCurrentPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CCURRENT_PERIOD) &&
                                                 SystemParamDetail.CCURRENT_PERIOD.Length >= 6
                        ? SystemParamDetail.CCURRENT_PERIOD.Substring(4, 2)
                        : "";
                    SelectedInvPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CINV_PRD) &&
                                             SystemParamDetail.CINV_PRD.Length >= 6
                        ? SystemParamDetail.CINV_PRD.Substring(4, 2)
                        : "";
                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CCURRENT_PERIOD))
                    {
                        SelectedCurrentPeriodYear = int.Parse(SystemParamDetail.CCURRENT_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CSOFT_PERIOD))
                    {
                        SelectedSoftPeriodYear = int.Parse(SystemParamDetail.CSOFT_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CINV_PRD))
                    {
                        SelectedInvPeriodYear = int.Parse(SystemParamDetail.CINV_PRD.Substring(0, 4));
                    }

                    //utility
                    SelectedElectricPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CELECTRIC_PERIOD) &&
                                                  SystemParamDetail.CELECTRIC_PERIOD.Length >= 6
                        ? SystemParamDetail.CELECTRIC_PERIOD.Substring(4, 2)
                        : "";
                    SelectedWaterPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CWATER_PERIOD) &&
                                               SystemParamDetail.CWATER_PERIOD.Length >= 6
                        ? SystemParamDetail.CWATER_PERIOD.Substring(4, 2)
                        : "";
                    SelectedGasPeriodMonth = !string.IsNullOrWhiteSpace(SystemParamDetail.CGAS_PERIOD) &&
                                             SystemParamDetail.CGAS_PERIOD.Length >= 6
                        ? SystemParamDetail.CGAS_PERIOD.Substring(4, 2)
                        : "";
                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CELECTRIC_PERIOD))
                    {
                        SelectedElectricPeriodYear = int.Parse(SystemParamDetail.CELECTRIC_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CWATER_PERIOD))
                    {
                        SelectedWaterPeriodYear = int.Parse(SystemParamDetail.CWATER_PERIOD.Substring(0, 4));
                    }

                    if (!string.IsNullOrWhiteSpace(SystemParamDetail.CGAS_PERIOD))
                    {
                        SelectedGasPeriodYear = int.Parse(SystemParamDetail.CGAS_PERIOD.Substring(0, 4));
                    }

                    //cutoffDate for utility
                    SelectedElectricCutOffDate =
                        !string.IsNullOrWhiteSpace(SystemParamDetail.CELECTRIC_DATE) &&
                        int.TryParse(SystemParamDetail.CELECTRIC_DATE, out var lnDate)
                            ? lnDate
                            : 0;
                    SelectedWaterCutOffDate =
                        !string.IsNullOrWhiteSpace(SystemParamDetail.CWATER_DATE) &&
                        int.TryParse(SystemParamDetail.CWATER_DATE, out lnDate)
                            ? lnDate
                            : 0;
                    SelectedGasCutOffDate =
                        !string.IsNullOrWhiteSpace(SystemParamDetail.CGAS_DATE) &&
                        int.TryParse(SystemParamDetail.CGAS_DATE, out lnDate)
                            ? lnDate
                            : 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}