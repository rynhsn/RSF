using PMB04000COMMONPrintBatch;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMB04000COMMON.Print.Model
{
    public class GenarateDataModel
    {
        public static PMB04000ResultDataDTO DefaultDataWithHeader()
        {
            PMB04000ResultDataDTO loReturn = new PMB04000ResultDataDTO()
            {
                Column = new PMB04000ColumnDTO(),
                Label = new PMB04000LabelDTO(),
                Header = new PMB04000BaseHeaderDTO(),
                Data = GenerateData()
            };

            return loReturn;
        }
        public static List<PMB04000DataReportDTO> GenerateData()
        {
            List<PMB04000DataReportDTO> DataDummy = new List<PMB04000DataReportDTO>();

            for (int j = 1; j < 7; j++)
            {
                DataDummy.Add(new PMB04000DataReportDTO()
                {
                    CREF_NO = "CREF NO.",
                    CTENANT_NAME = "TENANT NAME",
                    CNOMINAL = "NOMINAL",
                    CUNIT_DESCRIPTION = "UNIT DESC",
                    CINVOICE_NO = $"Invoice No-{j}",
                    CINVOICE_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    DINVOICE_DATE = DateTime.Now,
                    CTRANS_DESC = $"Transaction Desc No-{j}",
                    NINV_AMOUNT = 4000000m * j,
                    CCURRENCY_SYMBOL = "RP",
                    NTRANS_AMOUNT = 55000000m * j,
                    CCITY = "JAKARTA",
                    CTODAY_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    DTODAY_DATE = DateTime.Now,
                    CPERSON = "NAME OF PERSON",
                    CPOSITION = "MANAGER",
                    CNAME01 = "Name 01",
                    CNAME02 = "Name 02",
                    CNAME03 = "Name 03",
                    CNAME04 = "Name 04",
                    CNAME05 = "Name 05",
                    CNAME06 = "Name 06",
                    CPOSITION01 = "Position 01",
                    CPOSITION02 = "Position 02",
                    CPOSITION03 = "Position 03",
                    CPOSITION04 = "Position 04",
                    CPOSITION05 = "Position 05",
                    CPOSITION06 = "Position 06",
                });
            }
            return DataDummy;
        }
        #region Utilities
        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }
            else
            {
                DateTime result;
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
                    return null;
                }
            }
        }
        #endregion
    }
}
