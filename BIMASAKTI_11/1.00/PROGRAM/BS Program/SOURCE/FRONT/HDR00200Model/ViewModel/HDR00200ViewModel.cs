using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HDR00200Common;
using HDR00200Common.DTOs;
using HDR00200Common.DTOs.Print;
using HDR00200FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace HDR00200Model.ViewModel
{
    public class HDR00200ViewModel : R_ViewModel<HDR00200DataResultDTO>
    {
        private HDR00200Model _model = new HDR00200Model();
        public List<HDR00200PropertyDTO> PropertyList = new List<HDR00200PropertyDTO>();
        public List<HDR00200CodeDTO> CodeList01 = new List<HDR00200CodeDTO>();
        public List<HDR00200CodeDTO> CodeList02 = new List<HDR00200CodeDTO>();
        public HDR00200DefaultParamDTO DefaultParam = new HDR00200DefaultParamDTO();

        public HDR00200ReportParam ReportParam = new HDR00200ReportParam();

        public KeyValuePair<string, string>[] ReportTypeCare = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("C", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "CARE"))
        };

        public KeyValuePair<string, string>[] ReportTypeMaintenance = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("M",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Maintenance"))
        };
        
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public async Task Init()
        {
            await GetCodeList();
            await GetPropertyList();
            ReportParam.CPROPERTY_ID = PropertyList[0].CPROPERTY_ID;
            await ResetProcess();
        }

        public async Task ResetProcess()
        {
            await GetDefaultParam();

            ReportParam.CREPORT_TYPE = "C";
            ReportParam.CAREA = "01";

            ReportParam.LCOMPLAINT = true;
            ReportParam.LREQUEST = true;
            ReportParam.LINQUIRY = true;
            ReportParam.LHANDOVER = false;

            ReportParam.DFROM_PERIOD = DateTime.Now;
            ReportParam.DTO_PERIOD = DateTime.Now;

            ReportParam.LSUBMITTED = true;
            ReportParam.LOPEN = true;
            ReportParam.LASSIGNED = true;
            ReportParam.LON_PROGRESS = true;
            ReportParam.LSOLVED = true;
            ReportParam.LCOMPLETED = true;
            ReportParam.LCONFIRMED = true;
            ReportParam.LCLOSED = true;
            ReportParam.LCANCELLED = true;
            ReportParam.LTERMINATED = true;

            ReportParam.CFROM_BUILDING_ID = DefaultParam.CFIRST_BUILDING_ID;
            ReportParam.CFROM_BUILDING_NAME = DefaultParam.CFIRST_BUILDING_NAME;
            ReportParam.CTO_BUILDING_ID = DefaultParam.CLAST_BUILDING_ID;
            ReportParam.CTO_BUILDING_NAME = DefaultParam.CLAST_BUILDING_NAME;
            ReportParam.CFROM_DEPT_CODE = DefaultParam.CFIRST_DEPT_CODE;
            ReportParam.CFROM_DEPT_NAME = DefaultParam.CFIRST_DEPT_NAME;
            ReportParam.CTO_DEPT_CODE = DefaultParam.CLAST_DEPT_CODE;
            ReportParam.CTO_DEPT_NAME = DefaultParam.CLAST_DEPT_NAME;
        }

        private async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<HDR00200ListDTO<HDR00200PropertyDTO>>(
                        nameof(IHDR00200.HDR00200GetPropertyList));
                PropertyList = loReturn.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetCodeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<HDR00200ListDTO<HDR00200CodeDTO>>(
                        nameof(IHDR00200.HDR00200GetCodeList));
                CodeList01 = loReturn.Data.FindAll(x => x.CCODE == "01");
                CodeList02 = loReturn.Data.FindAll(x => x.CCODE == "02");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetDefaultParam()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new HDR00200PropertyDTO()
                {
                    CPROPERTY_ID = ReportParam.CPROPERTY_ID,
                    CPROPERTY_NAME = PropertyList.Find(x => x.CPROPERTY_ID == ReportParam.CPROPERTY_ID)?.CPROPERTY_NAME
                };

                var loReturn =
                    await _model.GetAsync<HDR00200SingleDTO<HDR00200DefaultParamDTO>, HDR00200PropertyDTO>(
                        nameof(IHDR00200.HDR00200GetDefaultParam), loParam);
                DefaultParam = loReturn.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }


        public void ValidateDataBeforePrint()
        {
            var loEx = new R_Exception();
            try
            {
                ReportParam.CFROM_PERIOD = ReportParam.DFROM_PERIOD?.ToString("yyyyMMdd");
                ReportParam.CTO_PERIOD = ReportParam.DTO_PERIOD?.ToString("yyyyMMdd");

                if (ReportParam.CREPORT_TYPE == "C" &&
                    !ReportParam.LCOMPLAINT &&
                    !ReportParam.LREQUEST &&
                    !ReportParam.LINQUIRY &&
                    !ReportParam.LHANDOVER)
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectCategory"));
                }

                if (ReportParam.CREPORT_TYPE == "C" &&
                    ReportParam.CAREA == "01" &&
                    string.IsNullOrEmpty(ReportParam.CFROM_BUILDING_ID))
                {
                    loEx.Add("",
                        R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectBuildingFrom"));
                }

                if (ReportParam.CREPORT_TYPE == "C" &&
                    ReportParam.CAREA == "01" &&
                    string.IsNullOrEmpty(ReportParam.CTO_BUILDING_ID))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectBuildingTo"));
                }

                if (ReportParam.CREPORT_TYPE == "M" &&
                    string.IsNullOrEmpty(ReportParam.CFROM_DEPT_CODE))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectDeptFrom"));
                }

                if (ReportParam.CREPORT_TYPE == "M" &&
                    string.IsNullOrEmpty(ReportParam.CTO_DEPT_CODE))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectDeptTo"));
                }

                if (string.IsNullOrEmpty(ReportParam.CFROM_PERIOD))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseFillPeriodFrom"));
                }

                if (string.IsNullOrEmpty(ReportParam.CTO_PERIOD))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseFillPeriodTo"));
                }

                if (ReportParam.DFROM_PERIOD > ReportParam.DTO_PERIOD)
                {
                    loEx.Add("",
                        R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PeriodFromCannotBeAfterPeriodTo"));
                }

                if (!ReportParam.LOPEN &&
                    !ReportParam.LSUBMITTED &&
                    !ReportParam.LASSIGNED &&
                    !ReportParam.LON_PROGRESS &&
                    !ReportParam.LSOLVED &&
                    !ReportParam.LCOMPLETED &&
                    !ReportParam.LCONFIRMED &&
                    !ReportParam.LCLOSED &&
                    !ReportParam.LCANCELLED &&
                    !ReportParam.LTERMINATED)
                {
                    loEx.Add("",
                        R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseCheckAtLeastOneStatus"));
                }

                

                if (!loEx.HasError)
                {
                    ReportParam.CPROPERTY_NAME = PropertyList.Find(x => x.CPROPERTY_ID == ReportParam.CPROPERTY_ID)
                        ?.CPROPERTY_NAME;
                    ReportParam.CREPORT_TYPE_NAME = ReportParam.CREPORT_TYPE == "C"
                        ? R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "CARE")
                        : R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Maintenance");
                    ReportParam.CAREA_NAME = ReportParam.CAREA == "01"
                        ? CodeList01.Find(x => x.CCODE == "01")?.CNAME
                        : CodeList02.Find(x => x.CCODE == "02")?.CNAME;
                    
                    #region set CCATEGORY and CSTATUS

                // -	IF LHANDOVER 	= TRUE, 	CCATEGORY add ‘02’
                // -	IF LCOMPLAINT 	= TRUE, 	CCATEGORY add ‘03’
                // -	IF LREQUEST 	= TRUE, 	CCATEGORY add ‘04’
                // -	IF LINQUIRY 	= TRUE, 	CCATEGORY add ‘05’
                
                // gunakan separator koma
                
                var loCategory = "";
                if (ReportParam.LHANDOVER)
                {
                    loCategory += "02,";
                }
                
                if (ReportParam.LCOMPLAINT)
                {
                    loCategory += "03,";
                }
                
                if (ReportParam.LREQUEST)
                {
                    loCategory += "04,";
                }
                
                if (ReportParam.LINQUIRY)
                {
                    loCategory += "05,";
                }
                
                if (loCategory.Length > 0)
                {
                    ReportParam.CCATEGORY = loCategory.Substring(0, loCategory.Length - 1);
                }
                
                // -	IF LSUBMITTED 	= TRUE, 	CSTATUS add ‘01’
                // -	IF LRECEIVED 	= TRUE, 	CSTATUS add ‘02’
                // -	IF LASSIGNED 	= TRUE, 	CSTATUS add ‘03’
                // -	IF LON_PROGRESS 	= TRUE, 	CSTATUS add ‘04’
                // -	IF LSOLVED 	= TRUE, 	CSTATUS add ‘05’
                // -	IF LCOMPLETED 	= TRUE, 	CSTATUS add ‘06’
                // -	IF LCONFIRMED 	= TRUE, 	CSTATUS add ‘07’
                // -	IF LCLOSED 	= TRUE, 	CSTATUS add ‘08’
                // -	IF LCANCELLED 	= TRUE, 	CSTATUS add ‘09’
                // -	IF LTERMINATED 	= TRUE, 	CSTATUS add ‘10’
                
                // gunakan separator koma
                
                var loStatus = "";
                if (ReportParam.LOPEN)
                {
                    loStatus += "01,";
                }
                
                
                if (ReportParam.LSUBMITTED)
                {
                    loStatus += "02,";
                }
                
                if (ReportParam.LASSIGNED)
                {
                    loStatus += "03,";
                }
                
                if (ReportParam.LON_PROGRESS)
                {
                    loStatus += "04,";
                }
                
                if (ReportParam.LSOLVED)
                {
                    loStatus += "05,";
                }
                
                if (ReportParam.LCOMPLETED)
                {
                    loStatus += "06,";
                }
                
                if (ReportParam.LCONFIRMED)
                {
                    loStatus += "07,";
                }
                
                if (ReportParam.LCLOSED)
                {
                    loStatus += "08,";
                }
                
                if (ReportParam.LCANCELLED)
                {
                    loStatus += "09,";
                }
                
                if (ReportParam.LTERMINATED)
                {
                    loStatus += "10,";
                }
                
                if (loStatus.Length > 0)
                {
                    ReportParam.CSTATUS = loStatus.Substring(0, loStatus.Length - 1);
                }

                #endregion
                    
                    // var categoryMap = new Dictionary<bool, string>
                    // {
                    //     { ReportParam.LHANDOVER, "02" },
                    //     { ReportParam.LCOMPLAINT, "03" },
                    //     { ReportParam.LREQUEST, "04" },
                    //     { ReportParam.LINQUIRY, "05" }
                    // };
                    //
                    // var statusMap = new Dictionary<bool, string>
                    // {
                    //     { ReportParam.LSUBMITTED, "01" },
                    //     { ReportParam.LRECEIVED, "02" },
                    //     { ReportParam.LASSIGNED, "03" },
                    //     { ReportParam.LON_PROGRESS, "04" },
                    //     { ReportParam.LSOLVED, "05" },
                    //     { ReportParam.LCOMPLETED, "06" },
                    //     { ReportParam.LCONFIRMED, "07" },
                    //     { ReportParam.LCLOSED, "08" },
                    //     { ReportParam.LCANCELLED, "09" },
                    //     { ReportParam.LTERMINATED, "10" }
                    // };
                    //
                    // var loCategory = new StringBuilder();
                    // foreach (var entry in categoryMap)
                    // {
                    //     if (entry.Key)
                    //     {
                    //         loCategory.Append(entry.Value).Append(",");
                    //     }
                    // }
                    //
                    // if (loCategory.Length > 0)
                    // {
                    //     ReportParam.CCATEGORY = loCategory.ToString(0, loCategory.Length - 1);
                    // }
                    //
                    // var loStatus = new StringBuilder();
                    // foreach (var entry in statusMap)
                    // {
                    //     if (entry.Key)
                    //     {
                    //         loStatus.Append(entry.Value).Append(",");
                    //     }
                    // }
                    //
                    // if (loStatus.Length > 0)
                    // {
                    //     ReportParam.CSTATUS = loStatus.ToString(0, loStatus.Length - 1);
                    // }
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