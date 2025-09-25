using GLB00700COMMON.DTO_s;
using GLB00700COMMON.DTO_s.Helper;
using System;

namespace GLB00700COMMON.Interfaces
{
    public interface IGLB00700
    {
        GeneralAPIResultDTO<TodayDTO> GetTodayRecord();
        GeneralAPIResultDTO<GLSysParamDTO> GetGLSysParamRecord();
        GeneralAPIResultDTO<LastRateRevaluationDTO> GetLastRateRevaluationRecord(LastRateRevaluationParamDTO poParam);
    }
}
