using System.Collections.Generic;
using TXB00200Common.DTOs;
using TXB00200Common.Params;

namespace TXB00200Common
{
    public interface ITXB00200 
    {
        TXB00200SingleDTO<TXB00200DTO> TXB00200GetSoftClosePeriod();
        TXB00200ListDTO<TXB00200PropertyDTO> TXB00200GetPropertyList();
        TXB00200SingleDTO<TXB00200NextPeriodDTO> TXB00200GetNextPeriod();
        TXB00200ListDTO<TXB00200PeriodDTO> TXB00200GetPeriodList(TXB00200PeriodParam poParam);
        TXB00200SingleDTO<TXB00200PeriodParam> TXB00200UpdateSoftPeriod(TXB00200PeriodParam poParams);
        TXB00200ListDTO<TXB00200SoftClosePeriodToDoListDTO> TXB00200SoftClosePeriodStream();
        // IAsyncEnumerable<TXB00200SoftClosePeriodToDoListDTO> TXB00200SoftClosePeriodStream();
        // TXB00200ListDTO<TXB00200SoftCloseParam> TXB00200ProcessSoftClose(TXB00200SoftCloseParam poParam);
    }
    
    public class TXB00200ContextConstant
    {
        public const string CPROPERTY_ID = "CPROPERTY_ID";
        public const string CPERIOD_YEAR = "CPERIOD_YEAR";
        public const string CPERIOD_MONTH = "CPERIOD_MONTH";
    }
}