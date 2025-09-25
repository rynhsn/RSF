using PMI00300COMMON;
using PMI00300COMMON.DTO;
using PMI00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMI00300MODEL.ViewModel
{
    public class PMI00300ViewModel : R_ViewModel<PMI00300DTO>
    {
        public PMI00300Model loModel = new PMI00300Model();

        public PMI00300DisplayProcessDTO loDisplayProcess = new PMI00300DisplayProcessDTO();

        public ObservableCollection<PMI00300DTO> loUnitInquiryHeaderList = new ObservableCollection<PMI00300DTO>();
        public PMI00300DTO loSelectedUnitInquiryHeader = new PMI00300DTO();
        public ObservableCollection<PMI00300DetailLeftDTO> loUnitInquiryDetailLeftList = new ObservableCollection<PMI00300DetailLeftDTO>();
        public ObservableCollection<PMI00300DetailRightDTO> loUnitInquiryDetailRightList= new ObservableCollection<PMI00300DetailRightDTO>();

        public List<PMI00300GetPropertyListDTO> loPropertyList = new List<PMI00300GetPropertyListDTO>();
        public PMI00300GetPropertyListDTO loProperty = new PMI00300GetPropertyListDTO();

        public Dictionary<string, List<PMI00300GetGSBCodeListDTO>> loLeaseStatusDictionary = new Dictionary<string, List<PMI00300GetGSBCodeListDTO>>();
        public Dictionary<string, List<PMI00300GetGSBCodeListDTO>> loStrataStatusDictionary = new Dictionary<string, List<PMI00300GetGSBCodeListDTO>>();
        public List<PMI00300GetGSBCodeListDTO> loLeaseStatusList = new List<PMI00300GetGSBCodeListDTO>();
        public List<PMI00300GetGSBCodeListDTO> loStrataStatusList = new List<PMI00300GetGSBCodeListDTO>();
        public PMI00300GetGSBCodeListDTO loStatus = new PMI00300GetGSBCodeListDTO();

        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetPropertyListAsync();
                await GetLeaseStatusListAsync();
                await GetStrataStatusListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPropertyListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (loPropertyList.Count == 0)
                {
                    var loResult = await loModel.GetPropertyListAsync();
                    loPropertyList = loResult.Data;
                }
                if (loPropertyList.Count > 0)
                {
                    loProperty = loPropertyList.FirstOrDefault();
                    loDisplayProcess.CPROPERTY_ID = loProperty.CPROPERTY_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLeaseStatusListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = new PMI00300GetLSStatusListParameterDTO() 
                { 
                    CPROPERTY_ID = loDisplayProcess.CPROPERTY_ID,
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMI00300_GET_LS_STATUS_LIST_STREAMING_CONTEXT, loParameter);

                loLeaseStatusList = new List<PMI00300GetGSBCodeListDTO>();
                if (!loLeaseStatusDictionary.TryGetValue(loParameter.CPROPERTY_ID, out loLeaseStatusList))
                {
                    var loResult = await loModel.GetLeaseStatusListAsync();
                    loLeaseStatusList = new List<PMI00300GetGSBCodeListDTO>
                    {
                        new PMI00300GetGSBCodeListDTO()
                        {
                            CCODE = "00",
                            CNAME = "All Status"
                        }
                    };
                    foreach (PMI00300GetGSBCodeListDTO data in loResult.Data)
                    {
                        loLeaseStatusList.Add(data);
                    }
                    loLeaseStatusDictionary.Add(loParameter.CPROPERTY_ID, loLeaseStatusList);
                }
                loLeaseStatusList = loLeaseStatusDictionary.GetValueOrDefault(loParameter.CPROPERTY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetStrataStatusListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = new PMI00300GetLSStatusListParameterDTO()
                {
                    CPROPERTY_ID = loDisplayProcess.CPROPERTY_ID,
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMI00300_GET_LS_STATUS_LIST_STREAMING_CONTEXT, loParameter);

                loStrataStatusList = new List<PMI00300GetGSBCodeListDTO>();
                if (!loStrataStatusDictionary.TryGetValue(loParameter.CPROPERTY_ID, out loStrataStatusList))
                {
                    var loResult = await loModel.GetStrataStatusListAsync();
                    loStrataStatusList = new List<PMI00300GetGSBCodeListDTO>
                    {
                        new PMI00300GetGSBCodeListDTO()
                        {
                            CCODE = "00",
                            CNAME = "All Status"
                        }
                    };
                    foreach (PMI00300GetGSBCodeListDTO data in loResult.Data)
                    {
                        loStrataStatusList.Add(data);
                    }
                    loStrataStatusDictionary.Add(loParameter.CPROPERTY_ID, loStrataStatusList);
                }
                loStrataStatusList = loStrataStatusDictionary.GetValueOrDefault(loParameter.CPROPERTY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUnitInquiryDetailLeftListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = new PMI00300DetailParameterDTO();
                loParameter.CPROPERTY_ID = loDisplayProcess.CPROPERTY_ID;
                loParameter.CUNIT_OPTION = loDisplayProcess.CUNIT_OPTION;
                loParameter.CBUILDING_ID = loSelectedUnitInquiryHeader.CBUILDING_ID;
                loParameter.CFLOOR_ID = loSelectedUnitInquiryHeader.CFLOOR_ID;
                loParameter.CUNIT_ID = loSelectedUnitInquiryHeader.CUNIT_ID;
                loParameter.CAGREEMENT_NO = loSelectedUnitInquiryHeader.CAGREEMENT_NO;

                R_FrontContext.R_SetStreamingContext(ContextConstant.PMI00300_GET_UNIT_INQUIRY_LEFT_DETAIL_LIST_STREAMING_CONTEXT, loParameter);

                var loResult = await loModel.GetUnitInquiryDetailLeftListAsync();
                loUnitInquiryDetailLeftList = new ObservableCollection<PMI00300DetailLeftDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUnitInquiryDetailRightListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = new PMI00300DetailParameterDTO
                {
                    CPROPERTY_ID = loDisplayProcess.CPROPERTY_ID,
                    CBUILDING_ID = loSelectedUnitInquiryHeader.CBUILDING_ID,
                    CFLOOR_ID = loSelectedUnitInquiryHeader.CFLOOR_ID,
                    CUNIT_ID = loSelectedUnitInquiryHeader.CUNIT_ID,
                    CTENANT_ID=loSelectedUnitInquiryHeader.CTENANT_ID
                };

                R_FrontContext.R_SetStreamingContext(ContextConstant.PMI00300_GET_UNIT_INQUIRY_RIGHT_DETAIL_LIST_STREAMING_CONTEXT, loParameter);

                var loResult = await loModel.GetUnitInquiryDetailRightListAsync();
                loUnitInquiryDetailRightList = new ObservableCollection<PMI00300DetailRightDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUnitInquiryHeaderListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = new PMI00300ParameterDTO();
                loParameter.CPROPERTY_ID = loDisplayProcess.CPROPERTY_ID;
                loParameter.CBUILDING_ID = loDisplayProcess.CBUILDING_ID;
                loParameter.LALL_BUILDING = loDisplayProcess.LALL_BUILDING;
                loParameter.CFROM_FLOOR_ID = loDisplayProcess.CFROM_FLOOR_ID;
                loParameter.CTO_FLOOR_ID = loDisplayProcess.CTO_FLOOR_ID;
                loParameter.CUNIT_CATEGORY = loDisplayProcess.CUNIT_CATEGORY;
                loParameter.CUNIT_OPTION = loDisplayProcess.CUNIT_OPTION;  
                loParameter.CSTATUS_ID = loDisplayProcess.CSTATUS_ID;

                R_FrontContext.R_SetStreamingContext(ContextConstant.PMI00300_GET_UNIT_INQUIRY_HEADER_LIST_STREAMING_CONTEXT, loParameter);

                var loResult = await loModel.GetUnitInquiryHeaderListAsync();

                foreach (var item in loResult.Data)
                {
                    item.CPROPERTY_ID = loProperty.CPROPERTY_ID;
                    item.CPROPERTY_NAME = loProperty.CPROPERTY_NAME;


                    if (string.IsNullOrWhiteSpace(item.CEND_DATE)) item.DEND_DATE = null;
                    else
                    {
                        DateTime result;
                        if (DateTime.TryParseExact(item.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) item.DEND_DATE = result;
                        else item.DEND_DATE = DateTime.MinValue;
                    }

                    if (item.IEXP_DAYS > 0) item.CEXP_DAYS = item.IEXP_DAYS.ToString(); 
                }

                loUnitInquiryHeaderList = new ObservableCollection<PMI00300DTO>(loResult.Data);
                loSelectedUnitInquiryHeader = loUnitInquiryHeaderList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void RefreshUnitInquiryValidation()
        {
            R_Exception loEx = new R_Exception();
            bool llCancel = false;

            try
            {
                llCancel = string.IsNullOrWhiteSpace(loDisplayProcess.CPROPERTY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = !loDisplayProcess.LALL_BUILDING && loDisplayProcess.CBUILDING_ID == "";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V002"));
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
