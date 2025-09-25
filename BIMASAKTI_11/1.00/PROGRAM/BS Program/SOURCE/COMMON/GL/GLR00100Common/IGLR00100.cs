using System.Collections.Generic;
using GLR00100Common.DTOs;
using GLR00100Common.Params;

namespace GLR00100Common
{
    public interface IGLR00100
    {
        GLR00100SingleDTO<GLR00100SystemParamDTO> GLR00100GetSystemParam();
        GLR00100SingleDTO<GLR00100PeriodDTO> GLR00100GetPeriod();
        GLR00100ListDTO<GLR00100PeriodDTDTO> GLR00100GetPeriodList(GLR00100PeriodDTParam poParam);
        IAsyncEnumerable<GLR00100TransCodeDTO> GLR00100GetTransCodeListStream();
    }
}