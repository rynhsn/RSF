using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02000ViewModel : R_ViewModel<GSL02000TreeDTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public List<GSL02000CountryDTO> CountryGeography { get; set; } = new List<GSL02000CountryDTO>();
        public List<GSL02000TreeDTO> CityGeographyTree { get; set; } = new List<GSL02000TreeDTO>();
        public GSL02000CountryDTO Country { get ; set; } = new GSL02000CountryDTO();
        public string CountryID { get; set; } = "";
        public async Task GetCountryGeographyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02000GetCountryGeographyListAsync();

                CountryGeography = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetCityGeographyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02000GetCityGeographyListListAsync(CountryID);

                if (Country != null)
                {
                    var loParentData = R_FrontUtility.ConvertObjectToObject<GSL02000CityDTO>(Country);

                    loResult.Add(loParentData);
                }

                var loGridData = loResult.Select(x =>
                new GSL02000TreeDTO
                {
                    ParentId = string.IsNullOrWhiteSpace(x.CPARENT_CODE) ? null : x.CPARENT_CODE,
                    ParentName = x.CPARENT_NAME,
                    Id = x.CCODE,
                    Name = x.CNAME,
                    Description = string.IsNullOrWhiteSpace(x.CCODE_CNAME_DISPLAY) ? string.Format("{0} - {1}", x.CCODE, x.CNAME) : x.CCODE_CNAME_DISPLAY,
                }).ToList();

                CityGeographyTree = loGridData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02000DTO> GetCityGeography(GSL02000ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02000DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL02000GetCityGeographyAsync(poEntity);

                var loData = R_FrontUtility.ConvertObjectToObject<GSL02000DTO>(loResult);

                loData.CCODE_PROVINCE = loResult.CPARENT_CODE;
                loData.CNAME_PROVINCE = loResult.CPARENT_NAME;
                loData.CCODE = loResult.CCODE;
                loData.CNAME = loResult.CNAME;

                loRtn = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
