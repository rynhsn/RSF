using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GLModel.ViewModel.GLL00110
{
    public class LookupGLL00110ViewModel
    {
        private PublicLookupGLModel _model = new PublicLookupGLModel();
        private PublicLookupGetRecordGLModel _modelGetRecord = new PublicLookupGetRecordGLModel();
        public ObservableCollection<GLL00110DTO> loList = new ObservableCollection<GLL00110DTO>();

        public async Task GLL00110ReferenceNoLookUpByPeriod(GLL00110ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLL00110ReferenceNoLookUpByPeriodAsync(poParam);

                foreach (GLL00110DTO item in loResult.Data!)
                {
                    item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                    item.DDOC_DATE = ConvertStringToDateTimeFormat(item.CDOC_DATE);
                }
                

                loList = new ObservableCollection<GLL00110DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GLL00110DTO> GLL00110ReferenceNoLookUpByPeriod(GLL00110ParameterGetRecordDTO poParam)
        {
            var loEx = new R_Exception();
            GLL00110DTO? loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.GLL00100ReferenceNoLookUpByPeriodAsync(poParam);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }

        #region Utilities

        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                DateTime result;

                // Jika string hanya memiliki 6 karakter, tambahkan "01" untuk merepresentasikan tanggalnya
                if (pcEntity.Length == 6)
                {
                    pcEntity += "01";
                }

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

        #endregion
    }
}
