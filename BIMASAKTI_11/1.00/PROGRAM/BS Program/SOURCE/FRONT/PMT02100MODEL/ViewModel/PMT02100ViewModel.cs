using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02100ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>
    {
        private PMT02100Model loModel = new PMT02100Model();

        public PMT02100HandoverBuildingDTO loHandoverBuilding = new PMT02100HandoverBuildingDTO();

        public PMT02100HandoverDTO loHandover = new PMT02100HandoverDTO();

        //public PMT02100DTO loSelectedHandover = new PMT02100DTO();

        public ObservableCollection<PMT02100HandoverDTO> loHandoverList = new ObservableCollection<PMT02100HandoverDTO>();

        public ObservableCollection<PMT02100HandoverBuildingDTO> loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetPMSystemParamDTO loPMSystemParam = null;

        public PMT02100TabParameterDTO loTabParameter = new PMT02100TabParameterDTO();

        public List<TypeFilterDTO> loTypeList = new List<TypeFilterDTO>()
        {
            //new TypeFilterDTO() { CCODE = "S", CNAME = "Strata" },
            new TypeFilterDTO() { CCODE = "O", CNAME = "Owner" },
            new TypeFilterDTO() { CCODE = "L", CNAME = "Lease" },
        };

        public TypeFilterDTO loSelectedType = new TypeFilterDTO();

        public string lcStatusCode = "";


        public async Task GetHandoverListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02100HandoverParameterDTO loParam = null;
            PMT02100HandoverResultDTO loRtn = null;
            try
            {
                loParam = new PMT02100HandoverParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CTYPE = loSelectedType.CCODE,
                    CHANDOVER_STATUS = lcStatusCode,
                    CBUILDING_ID = loHandoverBuilding.CBUILDING_ID,
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetHandoverListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE))
                    {
                        // Parse the date first
                        if (DateTime.TryParseExact(x.CSCHEDULED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        {
                            // Parse the time second
                            if (DateTime.TryParseExact(x.CSCHEDULED_HO_TIME, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
                            {
                                // Combine both the date and time
                                x.DSCHEDULED_HO_DATE = parsedDate.Date.Add(parsedTime.TimeOfDay);  // Combine date and time
                            }
                        }
                        else
                        {
                            x.DSCHEDULED_HO_DATE = null; // Handle invalid date format
                        }
                    }
                    else
                    {
                        x.DSCHEDULED_HO_DATE = null; // Handle null or empty date/time
                    }

                    //x.DSCHEDULED_HO_DATE = !string.IsNullOrEmpty(x.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(x.CSCHEDULED_HO_DATE, "yyyyMMdd", null) : (DateTime?)null;
                    x.DHO_ACTUAL_DATE = !string.IsNullOrEmpty(x.CHO_ACTUAL_DATE) ? DateTime.ParseExact(x.CHO_ACTUAL_DATE, "yyyyMMdd", null) : (DateTime?)null;
                    x.DHO_PLAN_DATE = !string.IsNullOrEmpty(x.CHO_PLAN_DATE) ? DateTime.ParseExact(x.CHO_PLAN_DATE, "yyyyMMdd", null) : (DateTime?)null;
                    //if (!string.IsNullOrEmpty(x.CSCHEDULED_HO_TIME_HOURS) && !string.IsNullOrEmpty(x.CSCHEDULED_HO_TIME_MINUTES))
                    //{
                    //    x.CSCHEDULED_HO_TIME = x.CSCHEDULED_HO_TIME_HOURS.Take(2).ToArray() + ":" + x.CSCHEDULED_HO_TIME_MINUTES.Substring(x.CSCHEDULED_HO_TIME_MINUTES.Length - 2, 2);
                    //    TimeSpan time = TimeSpan.Parse(x.CSCHEDULED_HO_TIME);
                    //    x.DSCHEDULED_HO_DATE = x.DSCHEDULED_HO_DATE.Value.Date + time;
                    //}
                    x.CTENANT_DISPLAY = x.CTENANT_NAME + " (" + x.CTENANT_ID + ")";
                    x.DREF_DATE = !string.IsNullOrEmpty(x.CREF_DATE) ? DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null) : (DateTime?)null;
                });
                loHandoverList = new ObservableCollection<PMT02100HandoverDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHandoverBuildingListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02100HandoverBuildingParameterDTO loParam = null;
            PMT02100HandoverBuildingResultDTO loRtn = null;
            try
            {
                loParam = new PMT02100HandoverBuildingParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CTYPE = loSelectedType.CCODE,
                    CHANDOVER_STATUS = lcStatusCode
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02100_GET_HANDOVER_BUILDING_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetHandoverBuildingListStreamAsync();

                if (loRtn != null)
                {
                    loRtn.Data.ForEach(x => x.CBUILDING_DISPLAY_NAME = x.CBUILDING_ID + " - " + x.CBUILDING_NAME + " (" + x.ICOUNT.ToString() + ")");
                }

                loHandoverBuildingList = new ObservableCollection<PMT02100HandoverBuildingDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetPropertyListResultDTO loRtn = null;
            try
            {
                loRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task<GetPMSystemParamDTO> GetPMSystemParamAsync(GetPMSystemParamParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPMSystemParamResultDTO loResult = null;

            try
            {
                loResult = await loModel.GetPMSystemParamAsync(poParam);
                loPMSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loPMSystemParam;
        }
    }
}