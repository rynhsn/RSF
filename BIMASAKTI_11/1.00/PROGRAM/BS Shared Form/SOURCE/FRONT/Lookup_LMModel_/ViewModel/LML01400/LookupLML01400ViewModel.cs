using Lookup_PMCOMMON.DTOs.LML01400;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace Lookup_PMModel.ViewModel.LML01400
{
    public class LookupLML01400ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01400DTO> GetList = new ObservableCollection<LML01400DTO>();


        public async Task GetAgreementUnitChargesList(LML01400ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CREF_NO, poParam.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSEQ_NO, poParam.CSEQ_NO ?? "");

                var loResult = await _model.LML01400AgreementUnitChargesListAsync();

                GetList = new ObservableCollection<LML01400DTO>(loResult.Data);

                //foreach (var item in GetList)
                //{
                //    if (item != null)
                //    {
                //        item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE);
                //        item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE);
                //    }
                //};
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01400DTO> GetAgreementUnitCharges(LML01400ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01400DTO loRtn = null;
            try
            {
                LML01400DTO loResult = await _modelGetRecord.LML01400AgreementUnitChargesAsync(poParam);
                //if (loResult.CSTART_DATE != null)
                //{
                //    loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                //    loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                //}
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }

    }
}
