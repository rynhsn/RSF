using TXB00200Common.DTOs;
using TXB00200Common.Params;

namespace TXB00200Common
{
    public interface ITXB00200
    {
        TXB00200ListDTO<TXB00200PropertyDTO> TXB00200GetPropertyList();
        TXB00200ListDTO<TXB00200PeriodDTO> TXB00200GetPeriodList(TXB00200YearParam poParam);
        TXB00200ListDTO<TXB00200SoftCloseParam> TXB00200ProcessSoftClose(TXB00200SoftCloseParam poParam);
    }
}