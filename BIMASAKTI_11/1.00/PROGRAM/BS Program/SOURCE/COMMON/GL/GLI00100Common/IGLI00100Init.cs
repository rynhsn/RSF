using System.Collections.Generic;
using GLI00100Common.DTOs;

namespace GLI00100Common
{
    public interface IGLI00100Init
    {
        GLI00100GSMCompanyDTO GLI00100GetGSMCompany();
        GLI00100GLSystemParamDTO GLI00100GetGLSystemParam();
        GLI00100GSMPeriodDTO GLI00100GetGSMPeriod();
        GLI00100PeriodCountDTO GLI00100GetPeriodCount(GLI00100YearParamsDTO poParams);
        IAsyncEnumerable<GLI00100AccountGridDTO> GLI00100GetGLAccountListStream();
    }
}