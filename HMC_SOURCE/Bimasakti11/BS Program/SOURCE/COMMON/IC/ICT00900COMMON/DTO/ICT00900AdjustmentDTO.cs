using ICT00900COMMON.Utility_DTO;
using System;
using System.Globalization;

namespace ICT00900COMMON.DTO
{
    public class ICT00900AdjustmentDTO : BaseDTO
    {
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CREF_NO { get; set; }
        public string? CDEPT_CODE{ get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CPRODUCT_NAME { get; set; }
        public string? CADJUST_METHOD { get; set; }
        public decimal NADJUST_AMOUNT { get; set; }
        public string? CALLOC_ID { get; set; }
        public string? CALLOC_NAME { get; set; }
        public bool IS_PROCESS_CHANGESTS_SUCCESS { get; set; }
        private string? _CREF_DATE;
        public string? CREF_DATE
        {
            get => _CREF_DATE;
            set
            {
                _CREF_DATE = value;
                DREF_DATE = ConvertStringToDateTimeFormat(value);
            }
        }
        public DateTime? DREF_DATE { get; set; }
        //Utility
        public DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }

            // Jika string hanya memiliki 6 karakter (YYYYMM), tambahkan "01" sebagai tanggal
            if (pcEntity.Length == 6)
            {
                pcEntity += "01";
            }

            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }
    }
}
