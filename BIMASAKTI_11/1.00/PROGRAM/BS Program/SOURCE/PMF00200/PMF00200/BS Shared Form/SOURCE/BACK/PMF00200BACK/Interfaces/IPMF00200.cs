using PMF00200COMMON;
using System;
using System.Threading.Tasks;

namespace PMF00200BACK
{
    public interface IPMF00200
    {
        Task<PMF00200Record<PMF00200AllInitialProcessDTO>> GetAllInitialProcess(PMF00200InputParameterDTO poEntity);
        Task<PMF00200Record<PMF00200DTO>> GetJournalRecord(PMF00200InputParameterDTO poEntity);
        Task<PMF00200Record<string>> SendEmail(PMF00200InputParameterDTO poEntity);
    }
}
