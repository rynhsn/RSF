using System;

namespace CBB00200Common.DTOs
{
    public class CBB00200SystemParamDTO
    {
        #region for check program usage

        public bool LPRD_END_FLAG { get; set; }

        #endregion


        #region for validate

        public string CCURRENT_PERIOD { get; set; }
        public string CSOFT_PERIOD { get; set; }

        #endregion

        #region for page

        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public DateTime? DLPRD_END_DATE { get; set; }
        public string CLPRD_END_BY { get; set; }
        
        public string CSOFT_PERIOD_CONCAT { get=> CSOFT_PERIOD_YY + "-" + CSOFT_PERIOD_MM; }
        public string CCURRENT_PERIOD_CONCAT { get=> CCURRENT_PERIOD_YY + "-" + CCURRENT_PERIOD_MM; }

        #endregion
    }
}