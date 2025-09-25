using System;
using System.Threading.Tasks;

namespace PMF00200COMMON
{
    public interface IPMF00200
    {
        PMF00200Record<PMF00200AllInitialProcessDTO> GetAllInitialProcess(PMF00200InputParameterDTO poEntity);
        PMF00200Record<PMF00200DTO> GetJournalRecord(PMF00200InputParameterDTO poEntity);
        Task<PMF00200Record<string>> SendEmail(PMF00200InputParameterDTO poEntity);
    }
}
