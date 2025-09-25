using TXB00200Common.DTOs;

namespace TXB00200Front.DTOs;

public class TXB00200PopupParamDTO
{
    public string CurrentPeriod { get; set; }
    public List<TXB00200SoftClosePeriodToDoListDTO> ErrorList { get; set; }
}