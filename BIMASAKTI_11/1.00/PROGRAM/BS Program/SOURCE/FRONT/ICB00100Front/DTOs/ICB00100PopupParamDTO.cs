using ICB00100Common.DTOs;

namespace ICB00100Front.DTOs;

public class ICB00100PopupParamDTO
{
    public string CurrentPeriod { get; set; }
    public List<ICB00100SoftClosePeriodToDoListDTO> ErrorList { get; set; }
}