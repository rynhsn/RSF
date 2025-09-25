using System.Threading.Tasks;
using PMR03000Common.DTOs;
using PMR03000Common.DTOs.Print;
using PMR03000Common.Params;

namespace PMR03000Common
{
    public interface IPMR03000
    {
        Task<PMR03000ListDTO<PMR03000PropertyDTO>> PMR03000GetPropertyList();
        Task<PMR03000ListDTO<PMR03000PeriodDTO>> PMR03000GetPeriodList(PMR03000PeriodParam poParam);
        Task<PMR03000ListDTO<PMR03000ReportTemplateDTO>> PMR03000GetReportTemplateList(PMR03000ReportTemplateParam poParam);
        Task<PMR03000ListDTO<PMR03000MessageInfoDTO>> PMR03000GetMessageInfoList(PMR03000MessageInfoParam poParam);
    }
}