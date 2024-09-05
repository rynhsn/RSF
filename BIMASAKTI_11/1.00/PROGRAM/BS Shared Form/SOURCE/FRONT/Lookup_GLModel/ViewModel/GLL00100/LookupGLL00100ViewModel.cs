using Lookup_GLCOMMON.DTOs.GLL00100;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Lookup_GLModel.ViewModel.GLL00100
{
    public class LookupGLL00100ViewModel
    {
        private PublicLookupGLModel _model = new PublicLookupGLModel();
        private PublicLookupGetRecordGLModel _modelGetRecord = new PublicLookupGetRecordGLModel();
        public ObservableCollection<GLL00100DTO> loList = new ObservableCollection<GLL00100DTO>();

        public async Task GLL00100ReferenceNoLookUp(GLL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLL00100ReferenceNoLookUpAsync(poParam);

                foreach (GLL00100DTO item in loResult.Data!)
                {
                    item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE);
                    item.DDOC_DATE = ConvertStringToDateTimeFormat(item.CDOC_DATE);
                }
                loList = new ObservableCollection<GLL00100DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterGetRecordDTO poParam)
        {
            var loEx = new R_Exception();
            GLL00100DTO? loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.GLL00100ReferenceNoLookUpAsync(poParam);

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
